using UnityEngine;
using System.Collections;

public class UndoRedoEvent : EventObject
{
    public static string UNDO = "UndoRedoEvent_undo";
    public static string REDO = "UndoRedoEvent_redo";

    public static string UNDO_LIST_CHANGE = "UndoRedoEvent_undo_list_change";

    public UndoRedoEvent(string types)
            : base(types)
    {
    }
}
