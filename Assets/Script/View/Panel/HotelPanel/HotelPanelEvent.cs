using UnityEngine;
using System.Collections;

public class HotelPanelEvent : EventObject
{
    public static string CLOSE = "HotelPanelEvent_CLOSE";
    public static string ENTERHOTEL = "HotelPanelEvent_ENTERHOTEL";

    public HotelPanelEvent(string types)
        : base(types)
    {
    }
}
