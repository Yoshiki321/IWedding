using UnityEngine;
using System.Collections;

public class CameraChangeEvent : EventObject
{
	public static string CHANGE_CAMERA = "CameraCommandEvent_VISUAL";

	public CameraFlags data;

	public CameraChangeEvent(string types, CameraFlags data, bool bubbles = false, bool cancelable = false)
		: base(types, bubbles, cancelable)
	{
		this.data = data;
	}
}
  