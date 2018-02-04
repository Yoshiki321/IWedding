using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPanelEvent : EventObject
{
    public static string TRANSFORM_CHANGE = "ComponentPanelEvent_Transform_Change";

    public static string Open = "ComponentPanelEvent_Open";
    public static string UPDATE = "ComponentPanelEvent_Update";

    public static string COLLAGE_CHANGE = "ComponentPanelEvent_COLLAGE_CHANGE";

    public List<AssetVO> newAssets;
    public List<AssetVO> oldAssets;
    public List<ObjectSprite> items;

    public ComponentPanelEvent(string types, List<ObjectSprite> items, List<AssetVO> oldAssets = null, List<AssetVO> newAssets = null)
        : base(types)
    {
        this.oldAssets = oldAssets;
        this.newAssets = newAssets;
        this.items = items;
    }
}
