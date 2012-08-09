namespace GravityEditor
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.drawingBox = new System.Windows.Forms.PictureBox();
            this.contextControl = new System.Windows.Forms.TabControl();
            this.texturePage = new System.Windows.Forms.TabPage();
            this.listViewTexture = new System.Windows.Forms.ListView();
            this.chooseFolder = new System.Windows.Forms.Button();
            this.buttonFolderUp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.mapTree = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.newLayerButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.moveUpButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownButton = new System.Windows.Forms.ToolStripButton();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.quickToolStrip = new System.Windows.Forms.ToolStrip();
            this.newMapButton = new System.Windows.Forms.ToolStripButton();
            this.loadMapButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.zoomCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showGridButton = new System.Windows.Forms.ToolStripButton();
            this.alignToGridButton = new System.Windows.Forms.ToolStripButton();
            this.originButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesButton = new System.Windows.Forms.ToolStripButton();
            this.imageList48 = new System.Windows.Forms.ImageList(this.components);
            this.imageList64 = new System.Windows.Forms.ImageList(this.components);
            this.imageList96 = new System.Windows.Forms.ImageList(this.components);
            this.imageList128 = new System.Windows.Forms.ImageList(this.components);
            this.imageList256 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).BeginInit();
            this.contextControl.SuspendLayout();
            this.texturePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.quickToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoMenuItem.Text = "Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
            // 
            // redoMenuItem
            // 
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoMenuItem.Text = "Redo";
            this.redoMenuItem.Click += new System.EventHandler(this.redoMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickGuideToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // quickGuideToolStripMenuItem
            // 
            this.quickGuideToolStripMenuItem.Name = "quickGuideToolStripMenuItem";
            this.quickGuideToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.quickGuideToolStripMenuItem.Text = "Quick Guide";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(350, 52);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.drawingBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.contextControl);
            this.splitContainer1.Size = new System.Drawing.Size(914, 628);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.TabIndex = 2;
            // 
            // drawingBox
            // 
            this.drawingBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingBox.Location = new System.Drawing.Point(0, 0);
            this.drawingBox.Name = "drawingBox";
            this.drawingBox.Size = new System.Drawing.Size(911, 381);
            this.drawingBox.TabIndex = 0;
            this.drawingBox.TabStop = false;
            this.drawingBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.drawingBox_DragDrop);
            this.drawingBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.drawingBox_DragEnter);
            this.drawingBox.DragOver += new System.Windows.Forms.DragEventHandler(this.drawingBox_DragOver);
            this.drawingBox.DragLeave += new System.EventHandler(this.drawingBox_DragLeave);
            this.drawingBox.MouseEnter += new System.EventHandler(this.drawingBox_MouseEnter);
            this.drawingBox.MouseLeave += new System.EventHandler(this.drawingBox_MouseLeave);
            this.drawingBox.Resize += new System.EventHandler(this.drawingBox_Resize);
            // 
            // contextControl
            // 
            this.contextControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contextControl.Controls.Add(this.texturePage);
            this.contextControl.Location = new System.Drawing.Point(3, 3);
            this.contextControl.Name = "contextControl";
            this.contextControl.SelectedIndex = 0;
            this.contextControl.Size = new System.Drawing.Size(908, 237);
            this.contextControl.TabIndex = 0;
            // 
            // texturePage
            // 
            this.texturePage.Controls.Add(this.listViewTexture);
            this.texturePage.Controls.Add(this.chooseFolder);
            this.texturePage.Controls.Add(this.buttonFolderUp);
            this.texturePage.Controls.Add(this.label2);
            this.texturePage.Controls.Add(this.comboBox1);
            this.texturePage.Controls.Add(this.textBoxFolder);
            this.texturePage.Controls.Add(this.label1);
            this.texturePage.Location = new System.Drawing.Point(4, 22);
            this.texturePage.Name = "texturePage";
            this.texturePage.Padding = new System.Windows.Forms.Padding(3);
            this.texturePage.Size = new System.Drawing.Size(900, 211);
            this.texturePage.TabIndex = 0;
            this.texturePage.Text = "Tiles";
            this.texturePage.UseVisualStyleBackColor = true;
            // 
            // listViewTexture
            // 
            this.listViewTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTexture.LargeImageList = this.imageList64;
            this.listViewTexture.Location = new System.Drawing.Point(0, 32);
            this.listViewTexture.Name = "listViewTexture";
            this.listViewTexture.Size = new System.Drawing.Size(900, 179);
            this.listViewTexture.TabIndex = 19;
            this.listViewTexture.UseCompatibleStateImageBehavior = false;
            this.listViewTexture.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewTexture_ItemDrag);
            this.listViewTexture.Click += new System.EventHandler(this.listViewTexture_Click);
            this.listViewTexture.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewTexture_DragDrop);
            this.listViewTexture.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewTexture_DragOver);
            this.listViewTexture.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.listViewTexture_GiveFeedback);
            this.listViewTexture.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewTexture_MouseDoubleClick);
            // 
            // chooseFolder
            // 
            this.chooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseFolder.Location = new System.Drawing.Point(533, 4);
            this.chooseFolder.Name = "chooseFolder";
            this.chooseFolder.Size = new System.Drawing.Size(60, 23);
            this.chooseFolder.TabIndex = 18;
            this.chooseFolder.Text = "Choose...";
            this.chooseFolder.UseVisualStyleBackColor = true;
            this.chooseFolder.Click += new System.EventHandler(this.chooseFolder_Click);
            // 
            // buttonFolderUp
            // 
            this.buttonFolderUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolderUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonFolderUp.Image")));
            this.buttonFolderUp.Location = new System.Drawing.Point(495, 4);
            this.buttonFolderUp.Name = "buttonFolderUp";
            this.buttonFolderUp.Size = new System.Drawing.Size(32, 23);
            this.buttonFolderUp.TabIndex = 17;
            this.buttonFolderUp.UseVisualStyleBackColor = true;
            this.buttonFolderUp.Click += new System.EventHandler(this.buttonFolderUp_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(641, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Size:";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "48 x 48",
            "64 x 64",
            "96 x 96",
            "128 x 128",
            "256 x 256"});
            this.comboBox1.Location = new System.Drawing.Point(671, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(74, 21);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(51, 6);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.ReadOnly = true;
            this.textBoxFolder.Size = new System.Drawing.Size(438, 20);
            this.textBoxFolder.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Folder:";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 52);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.toolStripContainer1);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(344, 628);
            this.splitContainer2.SplitterDistance = 314;
            this.splitContainer2.TabIndex = 3;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.mapTree);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(344, 257);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 28);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(344, 282);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // mapTree
            // 
            this.mapTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mapTree.Location = new System.Drawing.Point(4, 0);
            this.mapTree.Name = "mapTree";
            this.mapTree.Size = new System.Drawing.Size(340, 263);
            this.mapTree.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLayerButton,
            this.deleteButton,
            this.moveUpButton,
            this.moveDownButton});
            this.toolStrip2.Location = new System.Drawing.Point(9, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(104, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // newLayerButton
            // 
            this.newLayerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newLayerButton.Image = ((System.Drawing.Image)(resources.GetObject("newLayerButton.Image")));
            this.newLayerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newLayerButton.Name = "newLayerButton";
            this.newLayerButton.Size = new System.Drawing.Size(23, 22);
            this.newLayerButton.Text = "toolStripButton1";
            this.newLayerButton.ToolTipText = "Create a new layer";
            this.newLayerButton.Click += new System.EventHandler(this.newLayerButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(23, 22);
            this.deleteButton.Text = "toolStripButton2";
            this.deleteButton.ToolTipText = "Delete selected object";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpButton.Image")));
            this.moveUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(23, 22);
            this.moveUpButton.Text = "toolStripButton3";
            this.moveUpButton.ToolTipText = "Move object up";
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownButton.Image")));
            this.moveDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(23, 22);
            this.moveDownButton.Text = "toolStripButton4";
            this.moveDownButton.ToolTipText = "Move object down";
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(4, 4);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(340, 306);
            this.propertyGrid1.TabIndex = 0;
            // 
            // quickToolStrip
            // 
            this.quickToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapButton,
            this.loadMapButton,
            this.saveButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.zoomCombo,
            this.toolStripSeparator3,
            this.showGridButton,
            this.alignToGridButton,
            this.originButton,
            this.toolStripSeparator4,
            this.preferencesButton});
            this.quickToolStrip.Location = new System.Drawing.Point(0, 24);
            this.quickToolStrip.Name = "quickToolStrip";
            this.quickToolStrip.Size = new System.Drawing.Size(1264, 25);
            this.quickToolStrip.TabIndex = 8;
            this.quickToolStrip.Text = "toolStrip1";
            // 
            // newMapButton
            // 
            this.newMapButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newMapButton.Image = ((System.Drawing.Image)(resources.GetObject("newMapButton.Image")));
            this.newMapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newMapButton.Name = "newMapButton";
            this.newMapButton.Size = new System.Drawing.Size(23, 22);
            this.newMapButton.Text = "toolStripButton2";
            this.newMapButton.ToolTipText = "Create a new map";
            this.newMapButton.Click += new System.EventHandler(this.newMapButton_Click_1);
            // 
            // loadMapButton
            // 
            this.loadMapButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadMapButton.Image = ((System.Drawing.Image)(resources.GetObject("loadMapButton.Image")));
            this.loadMapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadMapButton.Name = "loadMapButton";
            this.loadMapButton.Size = new System.Drawing.Size(23, 22);
            this.loadMapButton.Text = "toolStripButton3";
            this.loadMapButton.ToolTipText = "Load a map";
            this.loadMapButton.Click += new System.EventHandler(this.loadMapButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "toolStripButton1";
            this.saveButton.ToolTipText = "Save map";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 22);
            this.undoButton.Text = "toolStripButton4";
            this.undoButton.ToolTipText = "Undo your action";
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click_1);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(23, 22);
            this.redoButton.Text = "toolStripButton5";
            this.redoButton.ToolTipText = "Redo your action";
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Zoom";
            // 
            // zoomCombo
            // 
            this.zoomCombo.Name = "zoomCombo";
            this.zoomCombo.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // showGridButton
            // 
            this.showGridButton.CheckOnClick = true;
            this.showGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showGridButton.Image = ((System.Drawing.Image)(resources.GetObject("showGridButton.Image")));
            this.showGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showGridButton.Name = "showGridButton";
            this.showGridButton.Size = new System.Drawing.Size(23, 22);
            this.showGridButton.Text = "toolStripButton6";
            this.showGridButton.ToolTipText = "Show grid";
            this.showGridButton.Click += new System.EventHandler(this.showGridButton_Click);
            // 
            // alignToGridButton
            // 
            this.alignToGridButton.CheckOnClick = true;
            this.alignToGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignToGridButton.Image = ((System.Drawing.Image)(resources.GetObject("alignToGridButton.Image")));
            this.alignToGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignToGridButton.Name = "alignToGridButton";
            this.alignToGridButton.Size = new System.Drawing.Size(23, 22);
            this.alignToGridButton.Text = "toolStripButton7";
            this.alignToGridButton.ToolTipText = "Align your placement to grid";
            this.alignToGridButton.Click += new System.EventHandler(this.alignToGridButton_Click);
            // 
            // originButton
            // 
            this.originButton.CheckOnClick = true;
            this.originButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.originButton.Image = ((System.Drawing.Image)(resources.GetObject("originButton.Image")));
            this.originButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.originButton.Name = "originButton";
            this.originButton.Size = new System.Drawing.Size(23, 22);
            this.originButton.Text = "toolStripButton8";
            this.originButton.ToolTipText = "Show origin";
            this.originButton.Click += new System.EventHandler(this.originButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // preferencesButton
            // 
            this.preferencesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.preferencesButton.Image = ((System.Drawing.Image)(resources.GetObject("preferencesButton.Image")));
            this.preferencesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.Size = new System.Drawing.Size(23, 22);
            this.preferencesButton.Text = "toolStripButton9";
            this.preferencesButton.ToolTipText = "Edit your preferences";
            this.preferencesButton.Click += new System.EventHandler(this.preferencesButton_Click_1);
            // 
            // imageList48
            // 
            this.imageList48.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList48.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList48.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList64
            // 
            this.imageList64.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList64.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList64.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList96
            // 
            this.imageList96.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList96.ImageSize = new System.Drawing.Size(96, 96);
            this.imageList96.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList128
            // 
            this.imageList128.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList128.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList128.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList256
            // 
            this.imageList256.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList256.ImageSize = new System.Drawing.Size(256, 256);
            this.imageList256.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.quickToolStrip);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Gravity Level Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).EndInit();
            this.contextControl.ResumeLayout(false);
            this.texturePage.ResumeLayout(false);
            this.texturePage.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.quickToolStrip.ResumeLayout(false);
            this.quickToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem undoMenuItem;
        public System.Windows.Forms.ToolStripMenuItem redoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickGuideToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        public System.Windows.Forms.PictureBox drawingBox;
        private System.Windows.Forms.TabControl contextControl;
        private System.Windows.Forms.TabPage texturePage;
        private System.Windows.Forms.ListView listViewTexture;
        private System.Windows.Forms.Button chooseFolder;
        private System.Windows.Forms.Button buttonFolderUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton newLayerButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton moveUpButton;
        private System.Windows.Forms.ToolStripButton moveDownButton;
        public System.Windows.Forms.TreeView mapTree;
        private System.Windows.Forms.ToolStrip quickToolStrip;
        private System.Windows.Forms.ToolStripButton newMapButton;
        private System.Windows.Forms.ToolStripButton loadMapButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton undoButton;
        public System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripComboBox zoomCombo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton showGridButton;
        private System.Windows.Forms.ToolStripButton alignToGridButton;
        private System.Windows.Forms.ToolStripButton originButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton preferencesButton;
        private System.Windows.Forms.ImageList imageList48;
        private System.Windows.Forms.ImageList imageList64;
        private System.Windows.Forms.ImageList imageList96;
        private System.Windows.Forms.ImageList imageList128;
        private System.Windows.Forms.ImageList imageList256;
    }
}

