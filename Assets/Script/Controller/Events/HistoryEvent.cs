using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryEvent : EventObject
{
    public List<AssetVO> newAssets;
    public List<AssetVO> oldAssets;

    public bool isUndoAction = false;
    public bool isRedoAction = false;

    public HistoryEvent(string types, List<AssetVO> oldAssets, List<AssetVO> newAssets)
            : base(types)
    {
        this.oldAssets = oldAssets;
        this.newAssets = newAssets;
    }
}

  