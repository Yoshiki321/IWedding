using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolPanelEvent : EventObject
{
	public static string Create_Item = "ItemToolPanelEvent_Create_Item";
    public static string Create_Combination = "ItemToolPanelEvent_Create_Combination";
    public static string CreatPlane = "ItemToolPanelEvent_CreatPlane";
    public static string Exit = "ItemToolPanelEvent_Exit";

    public string id;

    public ItemToolPanelEvent(string types, string id)
		: base(types)
	{
        this.id = id;
    }
}