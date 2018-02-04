using UnityEngine;
using System.Collections;

public class RedoCommand : Command
{
    public override void Execute(EventObject value)
    {
        UndoRedoModel.Instance.Redo();
    }
}
