using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        if (se.isUndoAction)
        {
            Undo(se.newAssets);
            return;
        }

        SaveOldValue(se, CloneAssets(se.oldAssets));

        foreach (AssetVO vo in se.oldAssets)
        {
            if (vo is ItemVO)
            {
                AssetsModel.Instance.Remove(vo.id);
            }

            if (vo is SurfaceVO)
            {
                BuilderModel.Instance.Remove(vo.id);
            }

            if (vo is NestedVO)
            {
                AssetsModel.Instance.Remove(vo.id);
            }
        }

        CommitHistoryEvent((SceneEvent)e);
    }

    private void Undo(List<AssetVO> list)
    {
        foreach (AssetVO vo in list)
        {
            if (vo is ItemVO)
            {
                AssetsModel.Instance.CreateItem(vo as ItemVO);
            }

            if (vo is SurfaceVO)
            {
                SurfaceVO svo = vo as SurfaceVO;
                foreach (LineVO linevo in svo.linesVO)
                {
                    BuilderModel.Instance.CreateLine(linevo);
                }
                BuilderModel.Instance.CreateSurface(vo as SurfaceVO);
            }

            if (vo is NestedVO)
            {
                AssetsModel.Instance.CreateNested(vo as NestedVO);
            }
        }
    }
}
