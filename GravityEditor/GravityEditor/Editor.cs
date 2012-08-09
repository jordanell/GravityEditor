using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Forms = System.Windows.Forms;
using GravityEditor.TileMap;
using GravityEditor.Drawing;

namespace GravityEditor
{
    enum EditorState
    {
        idle,
        brush,          //"stamp mode": user double clicked on an item to add multiple instances of it
        cameramoving,   //user is moving the camera
        moving,         //user is moving an item
        rotating,       //user is rotating an item
        scaling,        //user is scaling an item
        selecting,      //user has opened a select box by dragging the mouse (windows style)
    }

    class Editor
    {
        public static Editor Instance;

        // Tile Map
        public Camera camera;
        public TileMap.TileMap map;
        private TileLayer selectedlayer;
        public List<Tile> SelectedTiles;

        // Random variables
        EditorState state;
        public Texture2D dummytexture;
        Forms.Cursor cursorRot, cursorScale, cursorDup;

        // Editor Variables
        private bool commandInProgress = false;
        Stack<Command> undoBuffer = new Stack<Command>();
        Stack<Command> redoBuffer = new Stack<Command>();
        List<Vector2> initialpos;                   //position before user interaction
        List<float> initialrot;                     //rotation before user interaction
        List<Vector2> initialscale;
        EditorBrush currentbrush;
        Vector2 mouseworldpos, grabbedpoint, initialcampos, newPosition;
        bool drawSnappedPoint = false;
        Vector2 posSnappedPoint = Vector2.Zero;
        Rectangle selectionrectangle = new Rectangle();

        // Input variables
        KeyboardState kstate, oldkstate;
        MouseState mstate, oldmstate;
        Tile lastTile;

        public TileLayer SelectedLayer
        {
            get 
            { 
                return selectedlayer;
            }
            set
            {
                selectedlayer = value;
            }
        }

        public Editor()
        {
            Instance = this;

            camera = new Camera(MainWindow.Instance.drawingBox.Width, MainWindow.Instance.drawingBox.Height);

            SelectedTiles = new List<Tile>();
            initialpos = new List<Vector2>();
            initialrot = new List<float>();
            initialscale = new List<Vector2>();

            Logger.Instance.log("Loading preferences.");
            Preferences.Instance.Import("preferences.xml");
            Logger.Instance.log("Preferences loaded.");

            Logger.Instance.log("Creating new level.");
            MainWindow.Instance.newMap();
            Logger.Instance.log("New level created.");
        }

        public void loadMap(TileMap.TileMap map)
        {
            if (map.ContentRootFolder == null)
            {
                map.ContentRootFolder = Preferences.Instance.DefaultContentRootFolder;
                if (!Directory.Exists(map.ContentRootFolder))
                {
                    Forms.DialogResult dr = Forms.MessageBox.Show(
                        "The DefaultContentRootFolder \"" + map.ContentRootFolder + "\" (as set in the Settings Dialog) doesn't exist!\n"
                        + "The ContentRootFolder of the new level will be set to the Editor's work directory (" + Forms.Application.StartupPath + ").\n"
                        + "Please adjust the DefaultContentRootFolder in the Settings Dialog.\n"
                        + "Do you want to open the Settings Dialog now?", "Error",
                        Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Exclamation);
                    if (dr == Forms.DialogResult.Yes)
                    {
                        // Create a new settings form here
                    }
                }
            }
            else
            {
                if (!Directory.Exists(map.ContentRootFolder))
                {
                    Forms.MessageBox.Show("The directory \"" + map.ContentRootFolder + "\" doesn't exist! "
                        + "Please adjust the XML file before trying again.");
                    return;
                }
            }

            TextureLoader.Instance.Clear();

            foreach (TileLayer layer in map.Layers)
            {
                layer.map = map;
                foreach (Tile mapObject in layer.Tiles)
                {
                    mapObject.layer = layer;
                    if (!mapObject.loadIntoEditor())
                        return;
                }
            }

            this.map = map;
            MainWindow.Instance.loadFolder(map.ContentRootFolder);
            if (map.Name == null)
                map.Name = "Map_01";

            SelectedLayer = null;
            if (map.Layers.Count > 0) SelectedLayer = map.Layers[0];
            SelectedTiles.Clear();

            camera = new Camera(MainWindow.Instance.drawingBox.Width, MainWindow.Instance.drawingBox.Height);
            camera.Position = map.EditorRelated.CameraPosition;
            MainWindow.Instance.zoomCombo.Text = "100%";
            undoBuffer.Clear();
            redoBuffer.Clear();
            MainWindow.Instance.undoButton.Enabled = MainWindow.Instance.undoMenuItem.Enabled = undoBuffer.Count > 0;
            MainWindow.Instance.redoButton.Enabled = MainWindow.Instance.redoMenuItem.Enabled = redoBuffer.Count > 0;
            commandInProgress = false;

            updateTreeView();
        }

        public void updateTreeViewSelection()
        {
            MainWindow.Instance.propertyGrid1.SelectedObject = null;
            if (SelectedTiles.Count > 0)
            {
                Forms.TreeNode[] nodes = MainWindow.Instance.mapTree.Nodes.Find(SelectedTiles[0].Name, true);
                if (nodes.Length > 0)
                {
                    List<Tile> selecteditemscopy = new List<Tile>(SelectedTiles);
                    MainWindow.Instance.propertyGrid1.SelectedObject = SelectedTiles[0];
                    MainWindow.Instance.mapTree.SelectedNode = nodes[0];
                    MainWindow.Instance.mapTree.SelectedNode.EnsureVisible();
                    SelectedTiles = selecteditemscopy;
                }
            }
            else if (SelectedLayer != null)
            {
                Forms.TreeNode[] nodes = MainWindow.Instance.mapTree.Nodes[0].Nodes.Find(SelectedLayer.Name, false);
                if (nodes.Length > 0)
                {
                    MainWindow.Instance.mapTree.SelectedNode = nodes[0];
                    MainWindow.Instance.mapTree.SelectedNode.EnsureVisible();
                }
            }
        }

        public void beginCommand(string description)
        {
            if (commandInProgress)
            {
                undoBuffer.Pop();
            }
            undoBuffer.Push(new Command(description));
            commandInProgress = true;
        }

        public void endCommand()
        {
            if (!commandInProgress) return;
            undoBuffer.Peek().saveAfterState();
            redoBuffer.Clear();
            MainWindow.Instance.dirtyFlag = true;
            MainWindow.Instance.undoButton.Enabled = MainWindow.Instance.undoMenuItem.Enabled = undoBuffer.Count > 0;
            MainWindow.Instance.redoButton.Enabled = MainWindow.Instance.redoMenuItem.Enabled = redoBuffer.Count > 0;

            Forms.ToolStripMenuItem item = new Forms.ToolStripMenuItem(undoBuffer.Peek().Description);
            item.Tag = undoBuffer.Peek();
            commandInProgress = false;
        }

        public void addLayer(TileLayer l)
        {
            l.map = map;
            if (!l.map.Layers.Contains(l)) l.map.Layers.Add(l);
        }

        public void updateTreeView()
        {
            MainWindow.Instance.mapTree.Nodes.Clear();
            map.treeNode = MainWindow.Instance.mapTree.Nodes.Add(map.Name);
            map.treeNode.Tag = map;
            map.treeNode.Checked = map.Visible;

            foreach (TileLayer layer in map.Layers)
            {
                Forms.TreeNode layernode = map.treeNode.Nodes.Add(layer.Name, layer.Name);
                layernode.Tag = layer;
                layernode.Checked = layer.Visible;
                layernode.ImageIndex = layernode.SelectedImageIndex = 0;

                foreach (Tile item in layer.Tiles)
                {
                    Forms.TreeNode itemnode = layernode.Nodes.Add(item.Name, item.Name);
                    itemnode.Tag = item;
                    itemnode.Checked = true;
                    int imageindex = 0;
                    if (item is Tile) 
                        imageindex = 1;
                    itemnode.ImageIndex = itemnode.SelectedImageIndex = imageindex;
                }
                layernode.Expand();
            }
            map.treeNode.Expand();

            updateTreeViewSelection();
        }

        public void moveLayerUp(TileLayer l)
        {
            int index = map.Layers.IndexOf(l);
            map.Layers[index] = map.Layers[index - 1];
            map.Layers[index - 1] = l;
            selectLayer(l);
        }

        public void selectLayer(TileLayer l)
        {
            if (SelectedTiles.Count > 0)
                selectTile(null);
            SelectedLayer = l;
            updateTreeViewSelection();
            MainWindow.Instance.propertyGrid1.SelectedObject = l;
        }

        public void selectTile(Tile i)
        {
            SelectedTiles.Clear();
            if (i != null)
            {
                SelectedTiles.Add(i);
                SelectedLayer = i.layer;
                updateTreeViewSelection();
                MainWindow.Instance.propertyGrid1.SelectedObject = i;
            }
            else
            {
                selectLayer(SelectedLayer);
            }
        }

        public void moveTileUp(Tile i)
        {
            int index = i.layer.Tiles.IndexOf(i);
            i.layer.Tiles[index] = i.layer.Tiles[index - 1];
            i.layer.Tiles[index - 1] = i;
        }

        public void moveLayerDown(TileLayer l)
        {
            int index = map.Layers.IndexOf(l);
            map.Layers[index] = map.Layers[index + 1];
            map.Layers[index + 1] = l;
            selectLayer(l);
        }

        public void moveTileDown(Tile i)
        {
            int index = i.layer.Tiles.IndexOf(i);
            i.layer.Tiles[index] = i.layer.Tiles[index + 1];
            i.layer.Tiles[index + 1] = i;
            selectTile(i);
        }

        public void deleteLayer(TileLayer l)
        {
            if (map.Layers.Count > 0)
            {
                Editor.Instance.beginCommand("Delete Layer \"" + l.Name + "\"");
                map.Layers.Remove(l);
                Editor.Instance.endCommand();
            }
            if (map.Layers.Count > 0) SelectedLayer = map.Layers.Last();
            else SelectedLayer = null;
            selectTile(null);
            updateTreeView();
        }

        public void deleteSelectedMapObjects()
        {
            beginCommand("Delete Tile(s)");
            List<Tile> selecteditemscopy = new List<Tile>(SelectedTiles);

            List<Tile> itemsaffected = new List<Tile>();

            foreach (Tile selitem in selecteditemscopy)
            {
                selitem.layer.Tiles.Remove(selitem);
            }
            endCommand();
            selectTile(null);
            updateTreeView();

            if (itemsaffected.Count > 0)
            {
                string message = "";
                foreach (Tile item in itemsaffected) message += item.Name + " (Layer: " + item.layer.Name + ")\n";
                Forms.MessageBox.Show("The following Items have Custom Properties of Type \"Item\" that refered to items that have just been deleted:\n\n"
                    + message + "\nThe corresponding Custom Properties have been set to NULL, since the Item referred to doesn't exist anymore.");
            }

        }

        public void moveTileToLayer(Tile i1, TileLayer l2, Tile i2)
        {
            int index2 = i2 == null ? 0 : l2.Tiles.IndexOf(i2);
            i1.layer.Tiles.Remove(i1);
            l2.Tiles.Insert(index2, i1);
            i1.layer = l2;
        }

        public void selectMap()
        {
            MainWindow.Instance.propertyGrid1.SelectedObject = map;
        }

        public void createTextureBrush(string fullpath)
        {
            state = EditorState.brush;
            currentbrush = new EditorBrush(fullpath);
        }

        public void setmousepos(int screenx, int screeny)
        {
            Vector2 maincameraposition = camera.Position;
            if (SelectedLayer != null) camera.Position *= SelectedLayer.ScrollSpeed;
            mouseworldpos = Vector2.Transform(new Vector2(screenx, screeny), Matrix.Invert(camera.matrix));
            if (Preferences.Instance.SnapToGrid || kstate.IsKeyDown(Keys.G))
            {
                mouseworldpos = snapToGrid(mouseworldpos);
            }
            camera.Position = maincameraposition;
        }

        public Vector2 snapToGrid(Vector2 input)
        {
            Vector2 result = input;
            result.X = Preferences.Instance.GridSpacing.X * (int)Math.Round(result.X / Preferences.Instance.GridSpacing.X);
            result.Y = Preferences.Instance.GridSpacing.Y * (int)Math.Round(result.Y / Preferences.Instance.GridSpacing.Y);
            posSnappedPoint = result;
            drawSnappedPoint = true;
            return result;
        }

        public void paintTextureBrush(bool continueAfterPaint)
        {
            if (SelectedLayer == null)
            {
                System.Windows.Forms.MessageBox.Show("No Layer is selected");
                destroyTextureBrush();
                return;
            }
            Tile i = new Tile(currentbrush.fullPath, new Vector2((int)mouseworldpos.X, (int)mouseworldpos.Y));
            i.Name = i.getNamePrefix() + map.getNextItemNumber();
            i.layer = SelectedLayer;
            beginCommand("Add Item \"" + i.Name + "\"");
            addItem(i);
            endCommand();
            updateTreeView();
            if (!continueAfterPaint) destroyTextureBrush();
        }

        public void addItem(Tile i)
        {
            if (!i.layer.Tiles.Contains(i))
                i.layer.Tiles.Add(i);
        }

        public void destroyTextureBrush()
        {
            state = EditorState.idle;
            currentbrush = null;
        }

        public void undo()
        {
            if (commandInProgress)
            {
                undoBuffer.Pop();
                commandInProgress = false;
            }
            if (undoBuffer.Count == 0) return;
            undoBuffer.Peek().Undo();
            redoBuffer.Push(undoBuffer.Pop());
            MainWindow.Instance.propertyGrid1.Refresh();
            MainWindow.Instance.dirtyFlag = true;
            MainWindow.Instance.undoButton.Enabled = MainWindow.Instance.undoMenuItem.Enabled = undoBuffer.Count > 0;
            MainWindow.Instance.redoButton.Enabled = MainWindow.Instance.redoMenuItem.Enabled = redoBuffer.Count > 0;
        }

        public void redo()
        {
            if (commandInProgress)
            {
                undoBuffer.Pop();
                commandInProgress = false;
            }
            if (redoBuffer.Count == 0) return;
            redoBuffer.Peek().Redo();
            undoBuffer.Push(redoBuffer.Pop());
            MainWindow.Instance.propertyGrid1.Refresh();
            MainWindow.Instance.dirtyFlag = true;
            MainWindow.Instance.undoButton.Enabled = MainWindow.Instance.undoMenuItem.Enabled = undoBuffer.Count > 0;
            MainWindow.Instance.redoButton.Enabled = MainWindow.Instance.redoMenuItem.Enabled = redoBuffer.Count > 0;
        }

        public void getSelectionFromMap()
        {
            SelectedTiles.Clear();
            SelectedLayer = null;
            string[] itemnames = map.selectedObjects.Split(';');
            foreach (string itemname in itemnames)
            {
                if (itemname.Length > 0) SelectedTiles.Add(map.getTileByName(itemname));
            }
            SelectedLayer = map.getLayerByName(map.selectedLayers);
        }

        public void selectAll()
        {
            if (SelectedLayer == null) return;
            SelectedTiles.Clear();
            foreach (Tile i in SelectedLayer.Tiles)
            {
                SelectedTiles.Add(i);
            }
            updateTreeViewSelection();
        }

        public Tile getItemAtPos(Vector2 mouseworldpos)
        {
            if (SelectedLayer == null)
                return null;
            return SelectedLayer.getItemAtPos(mouseworldpos);
        }

        public void startMoving()
        {
            grabbedpoint = mouseworldpos;

            //save the distance to mouse for each item
            initialpos.Clear();
            foreach (Tile selitem in SelectedTiles)
            {
                initialpos.Add(selitem.pPosition);
            }

            state = EditorState.moving;
            //MainForm.Instance.pictureBox1.Cursor = Forms.Cursors.SizeAll;
        }

        public void abortCommand()
        {
            if (!commandInProgress)
                return;
            undoBuffer.Pop();
            commandInProgress = false;
        }

        public void saveMap(string filename)
        {
            map.EditorRelated.CameraPosition = camera.Position;
            map.export(filename);
        }

        public void Update(GameTime gameTime)
        {
            if (map == null) 
                return;

            oldkstate = kstate;
            oldmstate = mstate;
            kstate = Keyboard.GetState();
            mstate = Mouse.GetState();
            int mwheeldelta = mstate.ScrollWheelValue - oldmstate.ScrollWheelValue;

            if (mwheeldelta > 0)
            {
                float zoom = (float)Math.Round(camera.Scale * 10) * 10.0f + 10.0f;
                MainWindow.Instance.zoomCombo.Text = zoom.ToString() + "%";
                camera.Scale = zoom / 100.0f;
            }
            if (mwheeldelta < 0 )
            {
                float zoom = (float)Math.Round(camera.Scale * 10) * 10.0f - 10.0f;
                if (zoom <= 0.0f) return;
                MainWindow.Instance.zoomCombo.Text = zoom.ToString() + "%";
                camera.Scale = zoom / 100.0f;
            }

            //Camera movement
            float delta;
            if (kstate.IsKeyDown(Keys.LeftShift)) 
                delta = Preferences.Instance.CameraFastSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else 
                delta = Preferences.Instance.CameraSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.W) && kstate.IsKeyUp(Keys.LeftControl)) 
                camera.Position += (new Vector2(0, -delta));
            if (kstate.IsKeyDown(Keys.S) && kstate.IsKeyUp(Keys.LeftControl)) 
                camera.Position += (new Vector2(0, +delta));
            if (kstate.IsKeyDown(Keys.A) && kstate.IsKeyUp(Keys.LeftControl)) 
                camera.Position += (new Vector2(-delta, 0));
            if (kstate.IsKeyDown(Keys.D) && kstate.IsKeyUp(Keys.LeftControl)) 
                camera.Position += (new Vector2(+delta, 0));


            if (kstate.IsKeyDown(Keys.Subtract))
            {
                float zoom = (float)(camera.Scale * 0.995);
                MainWindow.Instance.zoomCombo.Text = (zoom * 100).ToString("###0.0") + "%";
                camera.Scale = zoom;
            }
            if (kstate.IsKeyDown(Keys.Add))
            {
                float zoom = (float)(camera.Scale * 1.005);
                MainWindow.Instance.zoomCombo.Text = (zoom * 100).ToString("###0.0") + "%";
                camera.Scale = zoom;
            }

            //get mouse world position considering the ScrollSpeed of the current layer
            Vector2 maincameraposition = camera.Position;
            if (SelectedLayer != null) 
                camera.Position *= SelectedLayer.ScrollSpeed;
            mouseworldpos = Vector2.Transform(new Vector2(mstate.X, mstate.Y), Matrix.Invert(camera.matrix));
            mouseworldpos = mouseworldpos.Round();
            camera.Position = maincameraposition;


            if (state == EditorState.idle)
            {
                //get item under mouse cursor
                Tile item = getItemAtPos(mouseworldpos);
                if (item != null)
                {
                    item.onMouseOver(mouseworldpos);
                    if (kstate.IsKeyDown(Keys.LeftControl)) 
                        MainWindow.Instance.drawingBox.Cursor = cursorDup;
                }
                if (item != lastTile && lastTile != null)
                    lastTile.onMouseOut();
                lastTile = item;

                //LEFT MOUSE BUTTON CLICK
                if ((mstate.LeftButton == ButtonState.Pressed && oldmstate.LeftButton == ButtonState.Released) ||
                    (kstate.IsKeyDown(Keys.D1) && oldkstate.IsKeyUp(Keys.D1)))
                {
                    if (item != null) item.onMouseButtonDown(mouseworldpos);
                    if (kstate.IsKeyDown(Keys.LeftControl) && item != null)
                    {
                        if (!SelectedTiles.Contains(item)) 
                            selectTile(item);

                        beginCommand("Add Item(s)");

                        List<Tile> selecteditemscopy = new List<Tile>();
                        foreach (Tile selitem in SelectedTiles)
                        {
                            Tile i2 = (Tile)selitem.clone();
                            selecteditemscopy.Add(i2);
                        }
                        foreach (Tile selitem in selecteditemscopy)
                        {
                            selitem.Name = selitem.getNamePrefix() + map.getNextItemNumber();
                            addItem(selitem);
                        }
                        selectTile(selecteditemscopy[0]);
                        updateTreeView();
                        for (int i = 1; i < selecteditemscopy.Count; i++) SelectedTiles.Add(selecteditemscopy[i]);
                        startMoving();
                    }
                    else if (kstate.IsKeyDown(Keys.LeftShift) && item != null)
                    {
                        if (SelectedTiles.Contains(item)) SelectedTiles.Remove(item);
                        else SelectedTiles.Add(item);
                    }
                    else if (SelectedTiles.Contains(item))
                    {
                        beginCommand("Change Item(s)");
                        startMoving();
                    }
                    else if (!SelectedTiles.Contains(item))
                    {
                        selectTile(item);
                        if (item != null)
                        {
                            beginCommand("Change Item(s)");
                            startMoving();
                        }
                        else
                        {
                            grabbedpoint = mouseworldpos;
                            selectionrectangle = Rectangle.Empty;
                            state = EditorState.selecting;
                        }

                    }
                }

                //MIDDLE MOUSE BUTTON CLICK
                if ((mstate.MiddleButton == ButtonState.Pressed && oldmstate.MiddleButton == ButtonState.Released) ||
                    (kstate.IsKeyDown(Keys.D2) && oldkstate.IsKeyUp(Keys.D2)))
                {
                    if (item != null) item.onMouseOut();
                    if (kstate.IsKeyDown(Keys.LeftControl))
                    {
                        grabbedpoint = new Vector2(mstate.X, mstate.Y);
                        initialcampos = camera.Position;
                        state = EditorState.cameramoving;
                        MainWindow.Instance.drawingBox.Cursor = Forms.Cursors.SizeAll;
                    }
                    else
                    {
                        if (SelectedTiles.Count > 0)
                        {
                            grabbedpoint = mouseworldpos - SelectedTiles[0].pPosition;

                            //save the initial rotation for each item
                            initialrot.Clear();
                            foreach (Tile selitem in SelectedTiles)
                            {
                                initialrot.Add(selitem.Rotation);
                            }

                            state = EditorState.rotating;
                            MainWindow.Instance.drawingBox.Cursor = cursorRot;

                            beginCommand("Rotate Item(s)");
                        }
                    }
                }

                //RIGHT MOUSE BUTTON CLICK
                if ((mstate.RightButton == ButtonState.Pressed && oldmstate.RightButton == ButtonState.Released) ||
                    (kstate.IsKeyDown(Keys.D3) && oldkstate.IsKeyUp(Keys.D3)))
                {
                    if (item != null) item.onMouseOut();
                    if (SelectedTiles.Count > 0)
                    {
                        grabbedpoint = mouseworldpos - SelectedTiles[0].pPosition;

                        //save the initial scale for each item
                        initialscale.Clear();
                        foreach (Tile selitem in SelectedTiles)
                        {
                            initialscale.Add(selitem.Scale);
                        }

                        state = EditorState.scaling;
                        MainWindow.Instance.drawingBox.Cursor = cursorScale;

                        beginCommand("Scale Item(s)");
                    }
                }
            }

            if (state == EditorState.moving)
            {
                int i = 0;
                foreach (Tile selitem in SelectedTiles)
                {
                    newPosition = initialpos[i] + mouseworldpos - grabbedpoint;
                    if (Preferences.Instance.SnapToGrid || kstate.IsKeyDown(Keys.G)) 
                        newPosition = snapToGrid(newPosition);
                    drawSnappedPoint = false;
                    selitem.pPosition = newPosition;
                    i++;
                }
                MainWindow.Instance.propertyGrid1.Refresh();
                if ((mstate.LeftButton == ButtonState.Released && oldmstate.LeftButton == ButtonState.Pressed) ||
                    (kstate.IsKeyUp(Keys.D1) && oldkstate.IsKeyDown(Keys.D1)))
                {

                    foreach (Tile selitem in SelectedTiles) 
                        selitem.onMouseButtonUp(mouseworldpos);

                    state = EditorState.idle;
                    MainWindow.Instance.drawingBox.Cursor = Forms.Cursors.Default;
                    if (mouseworldpos != grabbedpoint) endCommand(); else abortCommand();
                }
            }

            if (state == EditorState.rotating)
            {
                Vector2 newpos = mouseworldpos - SelectedTiles[0].pPosition;
                float deltatheta = (float)Math.Atan2(grabbedpoint.Y, grabbedpoint.X) - (float)Math.Atan2(newpos.Y, newpos.X);
                int i = 0;
                foreach (Tile selitem in SelectedTiles)
                {
                        selitem.Rotation = initialrot[i] - deltatheta;

                        if (kstate.IsKeyDown(Keys.LeftControl))
                        {
                            selitem.pRotation = ((float)Math.Round(selitem.pRotation / MathHelper.PiOver4) * MathHelper.PiOver4);
                        }
                        i++;
                }
                MainWindow.Instance.propertyGrid1.Refresh();
                if ((mstate.MiddleButton == ButtonState.Released && oldmstate.MiddleButton == ButtonState.Pressed) ||
                    (kstate.IsKeyUp(Keys.D2) && oldkstate.IsKeyDown(Keys.D2)))
                {
                    state = EditorState.idle;
                    MainWindow.Instance.drawingBox.Cursor = Forms.Cursors.Default;
                    endCommand();
                }
            }

            if (state == EditorState.scaling)
            {
                Vector2 newdistance = mouseworldpos - SelectedTiles[0].pPosition;
                float factor = newdistance.Length() / grabbedpoint.Length();
                int i = 0;
                foreach (Tile selitem in SelectedTiles)
                {
                        Vector2 newscale = initialscale[i];
                        if (!kstate.IsKeyDown(Keys.Y)) newscale.X = initialscale[i].X * (((factor - 1.0f) * 0.5f) + 1.0f);
                        if (!kstate.IsKeyDown(Keys.X)) newscale.Y = initialscale[i].Y * (((factor - 1.0f) * 0.5f) + 1.0f);
                        selitem.pScale = (newscale);

                        if (kstate.IsKeyDown(Keys.LeftControl))
                        {
                            Vector2 scale;
                            scale.X = (float)Math.Round(selitem.pScale.X * 10) / 10;
                            scale.Y = (float)Math.Round(selitem.pScale.Y * 10) / 10;
                            selitem.pScale = (scale);
                        }
                        i++;
                }
                MainWindow.Instance.propertyGrid1.Refresh();
                if ((mstate.RightButton == ButtonState.Released && oldmstate.RightButton == ButtonState.Pressed) ||
                    (kstate.IsKeyUp(Keys.D3) && oldkstate.IsKeyDown(Keys.D3)))
                {
                    state = EditorState.idle;
                    MainWindow.Instance.drawingBox.Cursor = Forms.Cursors.Default;
                    endCommand();
                }
            }

            if (state == EditorState.cameramoving)
            {
                Vector2 newpos = new Vector2(mstate.X, mstate.Y);
                Vector2 distance = (newpos - grabbedpoint) / camera.Scale;
                if (distance.Length() > 0)
                {
                    camera.Position = initialcampos - distance;
                }
                if (mstate.MiddleButton == ButtonState.Released)
                {
                    state = EditorState.idle;
                    MainWindow.Instance.drawingBox.Cursor = Forms.Cursors.Default;
                }
            }

            if (state == EditorState.selecting)
            {
                if (SelectedLayer == null) return;
                Vector2 distance = mouseworldpos - grabbedpoint;
                if (distance.Length() > 0)
                {
                    SelectedTiles.Clear();
                    selectionrectangle = Extensions.RectangleFromVectors(grabbedpoint, mouseworldpos);
                    foreach (Tile i in SelectedLayer.Tiles)
                    {
                        if (i.Visible && selectionrectangle.Contains((int)i.pPosition.X, (int)i.pPosition.Y)) SelectedTiles.Add(i);
                    }
                    updateTreeViewSelection();
                }
                if (mstate.LeftButton == ButtonState.Released)
                {
                    state = EditorState.idle;
                    MainWindow.Instance.drawingBox.Cursor = Forms.Cursors.Default;
                }
            }

            if (state == EditorState.brush)
            {
                if (Preferences.Instance.SnapToGrid || kstate.IsKeyDown(Keys.G))
                {
                    mouseworldpos = snapToGrid(mouseworldpos);
                }
                if (mstate.RightButton == ButtonState.Pressed && oldmstate.RightButton == ButtonState.Released) state = EditorState.idle;
                if (mstate.LeftButton == ButtonState.Pressed && oldmstate.LeftButton == ButtonState.Released) paintTextureBrush(true);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GravityEditor.Instance.GraphicsDevice.Clear(Preferences.Instance.ColorBackground);
            if (map == null || !map.Visible) 
                return;

            foreach (TileLayer l in map.Layers)
            {
                Vector2 maincameraposition = camera.Position;
                camera.Position *= l.ScrollSpeed;
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.matrix);

                l.drawInEditor(spriteBatch);
                if (l == SelectedLayer && state == EditorState.selecting)
                {
                    Primitives.Instance.drawBoxFilled(spriteBatch, selectionrectangle, Preferences.Instance.ColorSelectionBox);
                }
                if (l == SelectedLayer && state == EditorState.brush)
                {
                    spriteBatch.Draw(currentbrush.texture, new Vector2(mouseworldpos.X, mouseworldpos.Y), null, new Color(1f, 1f, 1f, 0.7f),
                        0, new Vector2(currentbrush.texture.Width / 2, currentbrush.texture.Height / 2), 1, SpriteEffects.None, 0);
                }
                spriteBatch.End();
                //restore main camera position
                camera.Position = maincameraposition;
            }

            if (SelectedTiles.Count > 0)
            {
                Vector2 maincameraposition = camera.Position;
                camera.Position *= SelectedTiles[0].layer.ScrollSpeed;
                spriteBatch.Begin();
                int i = 0;
                foreach (Tile item in SelectedTiles)
                {
                    if (item.Visible && item.layer.Visible && kstate.IsKeyUp(Keys.Space))
                    {
                        Color color = i == 0 ? Preferences.Instance.ColorSelectionFirst : Preferences.Instance.ColorSelectionRest;
                        item.drawSelectionFrame(spriteBatch, camera.matrix, color);
                        if (i == 0 && (state == EditorState.rotating || state == EditorState.scaling))
                        {
                            Vector2 center = Vector2.Transform(item.Position, camera.matrix);
                            Vector2 mouse = Vector2.Transform(mouseworldpos, camera.matrix);
                            Primitives.Instance.drawLine(spriteBatch, center, mouse, Preferences.Instance.ColorSelectionFirst, 1);
                        }
                    }
                    i++;
                }
                spriteBatch.End();
                //restore main camera position
                camera.Position = maincameraposition;
            }


            if (Preferences.Instance.ShowGrid)
            {
                spriteBatch.Begin();
                int max = Preferences.Instance.GridNumberOfGridLines / 2;
                for (int x = 0; x <= max; x++)
                {
                    Vector2 start = Vector2.Transform(new Vector2(x, -max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    Vector2 end = Vector2.Transform(new Vector2(x, max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                    start = Vector2.Transform(new Vector2(-x, -max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    end = Vector2.Transform(new Vector2(-x, max) * Preferences.Instance.GridSpacing.X, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                }
                for (int y = 0; y <= max; y++)
                {
                    Vector2 start = Vector2.Transform(new Vector2(-max, y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    Vector2 end = Vector2.Transform(new Vector2(max, y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                    start = Vector2.Transform(new Vector2(-max, -y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    end = Vector2.Transform(new Vector2(max, -y) * Preferences.Instance.GridSpacing.Y, camera.matrix);
                    Primitives.Instance.drawLine(spriteBatch, start, end, Preferences.Instance.GridColor, Preferences.Instance.GridLineThickness);
                }
                spriteBatch.End();
            }

            if (Preferences.Instance.ShowWorldOrigin)
            {
                spriteBatch.Begin();
                Vector2 worldOrigin = Vector2.Transform(Vector2.Zero, camera.matrix);
                Primitives.Instance.drawLine(spriteBatch, worldOrigin + new Vector2(-20, 0), worldOrigin + new Vector2(+20, 0), Preferences.Instance.WorldOriginColor, Preferences.Instance.WorldOriginLineThickness);
                Primitives.Instance.drawLine(spriteBatch, worldOrigin + new Vector2(0, -20), worldOrigin + new Vector2(0, 20), Preferences.Instance.WorldOriginColor, Preferences.Instance.WorldOriginLineThickness);
                spriteBatch.End();
            }

            if (drawSnappedPoint)
            {
                spriteBatch.Begin();
                posSnappedPoint = Vector2.Transform(posSnappedPoint, camera.matrix);
                Primitives.Instance.drawBoxFilled(spriteBatch, posSnappedPoint.X - 5, posSnappedPoint.Y - 5, 10, 10, Preferences.Instance.ColorSelectionFirst);
                spriteBatch.End();

            }
            drawSnappedPoint = false;
        }
    }
}
