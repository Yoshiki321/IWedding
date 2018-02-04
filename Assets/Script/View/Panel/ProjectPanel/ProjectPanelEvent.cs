using UnityEngine;
using System.Collections;

public class ProjectPanelEvent : EventObject
{
    public static string OPEN = "ProjectPanelEvent_OPEN";
    public static string CREATE = "ProjectPanelEvent_CREATE";

    public ProjectPanelEvent(string types)
        : base(types)
    {
    }
}
