using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToolbarEvent : EventObject
{

    public static string UNDO = "SceneToolbarEvent_Undo";
    public static string REDO = "SceneToolbarEvent_Redo";
    public static string COPY = "SceneToolbarEvent_Copy";
    public static string PASTE = "SceneToolbarEvent_Paste";
    public static string DELETE = "SceneToolbarEvent_Delete";
    public static string BRUSH = "SceneToolbarEvent_Brush";
    public static string VR = "SceneToolbarEvent_VR";

    public static string GROUP = "SceneToolbarEvent_Group";
    public static string REGROUP = "SceneToolbarEvent_ReGroup";
    public static string PHOTO = "SceneToolbarEvent_PHOTO";
    public static string CLEAR = "SceneToolbarEvent_CLEAR";
    public static string TO2D = "SceneToolbarEvent_To2D";
    public static string TO3D = "SceneToolbarEvent_To3D";
    public static string POSTION = "SceneToolbarEvent_Postion";
    public static string ROTATION = "SceneToolbarEvent_Rotation";
    public static string SCALE = "SceneToolbarEvent_Scale";
    public static string ALIGN = "SceneToolbarEvent_Align";
    public static string FILTER = "SceneToolbarEvent_Filter";
    public static string RENDER = "SceneToolbarEvent_RENDER";
    public static string LIGHT = "SceneToolbarEvent_LIGHT";
    public static string ADDITEM = "SceneToolbarEvent_ADDITEM";
    public static string ADDHOME = "SceneToolbarEvent_ADDHOME";

    public static string SAVE = "SceneToolbarEvent_Save";
	public static string LOAD = "SceneToolbarEvent_Load";
	public static string CAMERA = "SceneToolbarEvent_CAMERA";

    public int ViewId;

    public SceneToolbarEvent(string types, int ViewId = 0)
        : base(types)
    {
        this.ViewId = ViewId;
    }
}