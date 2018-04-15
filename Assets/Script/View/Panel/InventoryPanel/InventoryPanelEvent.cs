using UnityEngine;
using System.Collections;

public class InventoryPanelEvent : EventObject
{
    public static string CLOSE = "HotelPanelEvent_CLOSE";

    public InventoryPanelEvent(string types)
        : base(types)
    {
    }
}
