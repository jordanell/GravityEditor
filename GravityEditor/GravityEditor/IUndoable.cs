using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GravityEditor
{
    public interface IUndoable
    {
        IUndoable cloneForUndo();
        void makeLike(IUndoable other);
    }
}
