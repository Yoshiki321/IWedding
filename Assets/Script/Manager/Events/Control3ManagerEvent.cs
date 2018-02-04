using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control3ManagerEvent : EventObject
{
    public static string TRANSFORM = "Control3ManagerEvent_TRANSFORM";

    public static string TRANSFORM_UPDATE = "Control3ManagerEvent_TRANSFORM_UPDATE";

    public static string RESOLVE = "Control3ManagerEvent_RESOLVE";
    public static string COMBINATION = "Control3ManagerEvent_COMBINATION";

    public List<AssetVO> oldAssetVOs;
    public List<AssetVO> newAssetVOs;

    public Control3ManagerEvent(string types, List<AssetVO> oldAssetVOs, List<AssetVO> newAssetVOs)
        : base(types)
    {
        this.oldAssetVOs = oldAssetVOs;
        this.newAssetVOs = newAssetVOs;
    }
}
