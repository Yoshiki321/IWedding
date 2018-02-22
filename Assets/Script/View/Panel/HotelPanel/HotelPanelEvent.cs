using UnityEngine;
using System.Collections;

public class HotelPanelEvent : EventObject
{
    public static string CLOSE = "HotelPanelEvent_CLOSE";

    public HotelPanelEvent(string types)
        : base(types)
    {
    }
}
