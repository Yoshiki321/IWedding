using System.Collections.Generic;

public class ControlManagerEvent : EventObject
{
    public static string CHANGE_ITEM = "ControlManagerEvent_CHANGE_ITEM";
    public static string CHANGE_LINE = "ControlManagerEvent_CHANGE_LINE";
    public static string CHANGE_SURFACE = "ControlManagerEvent_CHANGE_SURFACE";
    public static string CHANGE_NESTED = "ControlManagerEvent_CHANGE_NESTED";

    public static string TRANSFORM_RELEASE = "ControlManagerEvent_TRANSFORM_RELEASE";

    public static string TRANSFORM_MOVE = "ControlManagerEvent_TRANSFORM_MOVE";

    public List<AssetVO> oldAssetVOs;
    public List<AssetVO> newAssetVOs;
    public List<ObjectSprite> items;

    public ControlManagerEvent(string types, List<ObjectSprite> items, List<AssetVO> vo, List<AssetVO> newvo = null)
        : base(types)
    {
        this.oldAssetVOs = vo;
        this.newAssetVOs = newvo;
        this.items = items;
    }
}
