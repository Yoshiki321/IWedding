using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelEvent : EventObject
{
	public static string CreatePlan_Click = "MainPanelEvent_CreatePlan_Click";
	public static string RealPlan_Click = "MainPanelEvent_RealPlan_Click";
    public static string StorePlan_Click = "MainPanelEvent_StorePlan_Click";
    public static string BtnOne_Click = "MainPanelEvent_BtnOne_Click";
    public static string BtnTwo_Click = "MainPanelEvent_BtnTwo_Click";
    public static string CloseBtn_Click = "MainPanelEvent_CloseBtn_Click";

    public MainPanelEvent(string types, bool bubbles = false, bool cancelable = false)
		: base(types, bubbles, cancelable)
	{
	}
}
