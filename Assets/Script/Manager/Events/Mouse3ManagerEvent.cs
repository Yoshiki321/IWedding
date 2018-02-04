using Build3D;
using System.Collections;
using System.Collections.Generic;

public class Mouse3ManagerEvent : EventObject
{
    public static string SELECT_SURFACE = "Mouse3ManagerEvent_SELECT_SURFACE";
    public static string SELECT_LINE = "Mouse3ManagerEvent_SELECT_LINE";
    public static string SELECT_ITEM = "Mouse3ManagerEvent_SELECT_ITEM";

    public static string RELEASE_SURFACE = "Mouse3ManagerEvent_RELEASE_SURFACE";
    public static string RELEASE_LINE = "Mouse3ManagerEvent_RELEASE_LINE";
    public static string RELEASE_ITEM = "Mouse3ManagerEvent_RELEASE_ITEM";

    public ArrayList objects;

    public Mouse3ManagerEvent(string types, ArrayList objects = null)
        : base(types)
    {
        this.objects = objects;
    }
}

