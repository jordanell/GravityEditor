using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GravityEditor.TileMap;

namespace GravityEditor.Drawing
{
    enum CommandType
    {
        Transform, Add, Delete, NameChange, OrderChange, WholeLevel
    }

    class Command
    {
        public String Description;
        public CommandType ComType;
        public List<IUndoable> ObjectsBefore = new List<IUndoable>();
        public List<IUndoable> ObjectsAfter = new List<IUndoable>();

        public Command(string description)
        {
            ComType = CommandType.WholeLevel;
            Description = description;
            ObjectsBefore.Add(Editor.Instance.map.cloneForUndo());
        }

        public List<IUndoable> Undo()
        {
            switch (ComType)
            {
                case CommandType.WholeLevel:
                    Editor.Instance.map = (TileMap.TileMap)ObjectsBefore[0];
                    Editor.Instance.getSelectionFromMap();
                    Editor.Instance.updateTreeView();
                    break;
            }
            return null;
        }

        public List<IUndoable> Redo()
        {
            switch (ComType)
            {
                case CommandType.WholeLevel:
                    Editor.Instance.map = (TileMap.TileMap)ObjectsAfter[0];
                    Editor.Instance.getSelectionFromMap();
                    Editor.Instance.updateTreeView();
                    break;
            }
            return null;
        }

        // Need to add here after editor is created
        public void saveAfterState()
        {
            ObjectsAfter.Add(Editor.Instance.map.cloneForUndo());
        }
    }
}
