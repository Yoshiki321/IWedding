using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePanelEvent : EventObject
{
    public static string COLLAGE_BUILD = "CorePanelEvent_COLLAGE_BUILD";

    public BuildVO vo;
    public BuildVO newvo;

    public CorePanelEvent(string types, BuildVO vo = null, BuildVO newvo = null)
        : base(types)
    {
        this.vo = vo;
        this.newvo = newvo;
    }
}
