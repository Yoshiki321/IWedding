using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToolPanelEvent : EventObject
{
	public static string Space = "ChooseSurfacePanelEvent_Space";
	public static string Nested = "ChooseSurfacePanelEvent_Nested";
	public static string Asset = "ChooseSurfacePanelEvent_Asset";
	public static string Item = "ChooseSurfacePanelEvent_Item";

	public SceneToolPanelEvent(string types)
		: base(types)
	{
	}
}

