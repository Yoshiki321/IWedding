using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPlaneEvent : EventObject
{
    public static string CHANGE = "DrawPlaneEvent_CHANGE";

    public DrawPlaneEvent(string types)
        : base(types)
    {
    }
}
