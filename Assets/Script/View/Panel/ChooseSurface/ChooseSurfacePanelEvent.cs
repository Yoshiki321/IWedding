using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSurfacePanelEvent : EventObject
{
	public static string CREATE_SURFACE = "ChooseSurfacePanelEvent_Create_Surface";
    public static string HARD = "ChooseSurfacePanelEvent_HARD";
    public static string EXIT = "ChooseSurfacePanelEvent_Exit";

	public string name;
    public LayoutConstant type;

    public ChooseSurfacePanelEvent(string types, string name, LayoutConstant type = LayoutConstant.BULGE, bool bubbles = false, bool cancelable = false)
        : base(types, bubbles, cancelable)
    {
        this.name = name;
        this.type = type;
    }
}
