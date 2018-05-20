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
    public static string OPENBRUSH = "TopToolPanelEvent_BRUSH";
    public static string CLOSEBRUSH = "TopToolPanelEvent_BRUSH";
    public static string ALIGN = "TopToolPanelEvent_AGLIN";
    public static string PHOTO = "TopToolPanelEvent_PHOTO";
    public static string VIEWONE = "TopToolPanelEvent_VIEWONE";
    public static string VIEWTWO = "TopToolPanelEvent_VIEWTWO";
    public static string VIEWTHREE = "TopToolPanelEvent_VIEWTHREE";
    public static string VIEWFOUR = "TopToolPanelEvent_VIEWFOUR";
    public static string VIEWFIVE = "TopToolPanelEvent_VIEWFIVE";
    public static string OPENLIGHT = "TopToolPanelEvent_OPENLIGHT";
    public static string CLOSELIGHT = "TopToolPanelEvent_CLOSELIGHT";
    public static string OPENDRAWPANEL = "TopToolPanelEvent_OPENDRAWPANEL";
    public static string OPENMODELPANEL = "TopToolPanelEvent_OPENMODELPANEL";
    public static string HIDE = "TopToolPanelEvent_HIDE";
    public string name;

    public TopToolPanelEvent(string types, bool bubbles = false, bool cancelable = false)
		: base(types, bubbles, cancelable)
	{
    }
}