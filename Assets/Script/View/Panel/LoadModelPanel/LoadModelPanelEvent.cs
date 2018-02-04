using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadModelPanelEvent : EventObject
{
    public static string LOAD_MODEL = "LoadModelPanelEvent_LOAD_MODEL";
    public static string LOAD_ALBEDO = "LoadModelPanelEvent_LOAD_ALBEDO";
    public static string CREATE = "LoadModelPanelEvent_CREATE";

    public LoadModelPanelEvent(string types)
        : base(types)
    {
    }
}