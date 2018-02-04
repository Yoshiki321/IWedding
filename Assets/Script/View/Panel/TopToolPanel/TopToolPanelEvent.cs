using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopToolPanelEvent : EventObject
{
	public static string SAVE = "TopToolPanelEvent_Save";
	public static string LOAD = "TopToolPanelEvent_Load";
    public static string UNDO = "TopToolPanelEvent_Undo";
    public static string REDO = "TopToolPanelEvent_Redo";
    public static string COPY = "TopToolPanelEvent_Copy";
    public static string PASTE = "TopToolPanelEvent_Paste";
    public static string DELETE = "TopToolPanelEvent_Delete";
    public static string FILTER = "TopToolPanelEvent_Filter";
    public static string ADDITEM = "TopToolPanelEvent_AddItem";
    public static string ADDHOME = "TopToolPanelEvent_AddHome";
    public static string HELP = "TopToolPanelEvent_Help";
    public static string GROUP = "TopToolPanelEvent_Group";
    public static string REGROUP = "TopToolPanelEvent_ReGroup";
    public static string DRAWSTAGE = "TopToolPanelEvent_DRAWSTAGE";

    public string name;

    public TopToolPanelEvent(string types, bool bubbles = false, bool cancelable = false)
		: base(types, bubbles, cancelable)
	{
    }
}