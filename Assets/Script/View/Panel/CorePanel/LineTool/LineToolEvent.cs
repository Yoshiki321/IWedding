using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;

public class LineToolEvent : EventObject
{
    public static string LineTool_From = "Event_LineTool_From";
    public static string LineTool_To = "Event_LineTool_To";
    public static string LineTool_Line = "Event_LineTool_Line";
    public static string LineTool_lengthText = "Event_LineTool_lengthText";
    public static string LineTool_EndEdit_lengthText = "Event_LineTool_EndEdit_lengthText";

    public float data;

    public LineToolEvent(string types, float data = 0)
        : base(types)
    {
        this.data = data;
    }
}
