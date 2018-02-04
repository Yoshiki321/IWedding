using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEvent : HistoryEvent
{
    public static string CHANGE_COMPONENT = "SceneEvent_CHANGE_COMPONENT";
    public static string CHANGE_GROUP_ITEM = "SceneEvent_CHANGE_GROUP_ITEM";
    public static string CHANGE_ITEM = "SceneEvent_CHANGE_ITEM";
    public static string CHANGE_ITEM_3 = "SceneEvent_CHANGE_ITEM_3";

    public static string ADD_ITEM = "SceneEvent_ADD_ITEM";
    public static string ADD_NESTED = "SceneEvent_ADD_NESTED";
    public static string ADD_SURFACE = "SceneEvent_ADD_SURFACE";

    public static string CHANGE_NESTED = "SceneEvent_CHANGE_NESTED";
    public static string CHANGE_BUILD = "SceneEvent_CHANGE_BUILD";
    public static string CHANGE_SURFACE = "SceneEvent_CHANGE_SURFACE"; 
    public static string CHANGE_COLLAGE = "SceneEvent_CHANGE_COLLAGE";

    public static string TRANSFORM = "SceneEvent_TRANSFORM";
    
    public static string DELETE = "SceneEvent_DELETE";

    public SceneEvent(string types, List<AssetVO> oldAssets, List<AssetVO> newAssets)
        : base(types, oldAssets, newAssets)
    {
        this.oldAssets = CloneAssets(oldAssets);
        this.newAssets = CloneAssets(newAssets);
    }

    public EventObject Clone()
    {
        SceneEvent e = new SceneEvent(type, oldAssets, newAssets);
        e.newAssets = CloneAssets(newAssets);
        e.oldAssets = CloneAssets(oldAssets);
        return e;
    }

    protected List<AssetVO> CloneAssets(List<AssetVO> list)
    {
        if (list == null) return null;

        List<AssetVO> reList = new List<AssetVO>();
        foreach (AssetVO vo in list)
        {
            if (vo == null)
            {
                reList.Add(null);
            }
            else
            {
                reList.Add(vo.Clone());
            }
        }
        return reList;
    }
}
