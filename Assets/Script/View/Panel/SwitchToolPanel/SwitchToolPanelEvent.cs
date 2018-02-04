using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToolPanelEvent : EventObject
{
	public static string FILE = "SwitchToolPanelEvent_File";
	public static string EDIT = "SwitchToolPanelEvent_Edit";
    public static string FILTER = "SwitchToolPanelEvent_Filter";
    public static string WINDOW = "SwitchToolPanelEvent_Window";
    public static string HELP = "SwitchToolPanelEvent_Help";
    public static string CLOSE = "SwitchToolPanelEvent_Close";

    public string name;

	public SwitchToolPanelEvent(string types,bool bubbles = false, bool cancelable = false)
		: base(types, bubbles, cancelable)
	{
	}
}