using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryCommand : Command
{
    protected void CommitHistoryEvent(SceneEvent e)
    {
        AddToHistory(e);
    }

    protected void SaveOldValue(SceneEvent e, List<AssetVO> prevAssets)
    {
        e.oldAssets = prevAssets;
    }

    protected void AddToHistory(SceneEvent e)
    {
        if (!e.isUndoAction && !e.isRedoAction)
        {
            UndoRedoModel.Instance.RegisterAction(e.Clone() as SceneEvent);
        }
    }

    protected List<AssetVO> CloneAssets(List<AssetVO> list)
    {
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
