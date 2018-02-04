using UnityEngine;
using System.Collections;

public class UndoCommand : Command
{
    public override void Execute(EventObject value)
    {
        UndoRedoModel.Instance.Undo();
    }
}
