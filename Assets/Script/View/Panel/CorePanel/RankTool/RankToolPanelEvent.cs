using UnityEngine;
using System.Collections;

public class RankToolPanelEvent : EventObject
{
    public static string POSITION_X = "RankToolPanelEvent_POSITION_X";
    public static string POSITION_Y = "RankToolPanelEvent_POSITION_Y";
    public static string POSITION_Z = "RankToolPanelEvent_POSITION_Z";

    public static string SCALE_X = "RankToolPanelEvent_SCALE_X";
    public static string SCALE_Y = "RankToolPanelEvent_SCALE_Y";
    public static string SCALE_Z = "RankToolPanelEvent_SCALE_Z";

    public static string ROTATION_X = "RankToolPanelEvent_ROTATION_X";
    public static string ROTATION_Y = "RankToolPanelEvent_ROTATION_Y";
    public static string ROTATION_Z = "RankToolPanelEvent_ROTATION_Z";

    public static string RAYDOWN = "RankToolPanelEvent_RAYDOWN";
    public static string RANDOMROTATE = "RankToolPanelEvent_RANDOMROTATE";

    public static string CLOSE = "RankToolPanelEvent_CLOSE";

    public RankToolPanelEvent(string types)
		: base(types)
	{
    }
}
