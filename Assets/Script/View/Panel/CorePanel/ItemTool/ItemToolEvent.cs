using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;

public class ItemToolEvent : EventObject
{
    public static string ITEMTOOL_LEFT = "Event_ItemTool_left";
    public static string ITEMTOOL_RIGHT = "Event_ItemTool_right";
    public static string ITEMTOOL_RESET = "Event_ItemTool_reset";
    public static string ITEMTOOL_REMOVE = "Event_ItemTool_remove";
    public static string ITEMTOOL_COMBINATION = "Event_ItemTool_combination"; 
    public static string ITEMTOOL_RESOLVE = "Event_ItemTool_resolve";

    public float data;

    public ItemToolEvent(string types, float data = 0)
        : base(types)
    {
        this.data = data;
    }
}
