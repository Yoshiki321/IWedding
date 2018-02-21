using UnityEngine;
using System.Collections;

public class StorePanelEvent : EventObject
{
    public static string CLOSE = "StorePanelEvent_CLOSE";

    public StorePanelEvent(string types)
        : base(types)
    {
    }
}
