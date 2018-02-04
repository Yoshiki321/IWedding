using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToolPanelEvent : EventObject
{
	public static string Into2D = "CameraToolPanelEvent_Into2D";
	public static string Into3D = "CameraToolPanelEvent_Into3D";

	public CameraToolPanelEvent(string types, bool bubbles = false, bool cancelable = false)
		: base(types, bubbles, cancelable)
	{
        
	}
}
