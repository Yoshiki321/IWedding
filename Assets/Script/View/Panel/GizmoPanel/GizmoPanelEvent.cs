using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoPanelEvent : EventObject
{
    public static string Transform = "GizmoPanelEvent_Transform";
    public static string Scale = "GizmoPanelEvent_Scale";
    public static string Rotation = "GizmoPanelEvent_Rotation";

    public GizmoPanelEvent(string types, bool bubbles = false, bool cancelable = false)
        : base(types, bubbles, cancelable)
    {
    }
}