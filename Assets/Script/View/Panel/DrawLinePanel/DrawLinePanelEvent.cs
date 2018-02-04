using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DrawLinePanelEvent : EventObject
{
    public static string REVISE_ITEM = "DrawLinePanelEvent_REVISE_ITEM";
    public static string ADD_ITEM = "DrawLinePanelEvent_ADD_ITEM";
    public static string SAVE = "DrawLinePanelEvent_SAVE";
    public static string LOAD = "DrawLinePanelEvent_LOAD";
    public static string TILINGX = "DrawLinePanelEvent_TilingX";
    public static string TILINGY = "DrawLinePanelEvent_TilingY";
    public static string OFFESTX = "DrawLinePanelEvent_OffestX";
    public static string OFFESTY = "DrawLinePanelEvent_OffestY";

    public ItemVO itemVO;
    public XmlNode code;

    public DrawLinePanelEvent(string types, ItemVO vo = null,XmlNode xml = null)
        : base(types)
    {
        itemVO = vo;
        code = xml;
    }
}