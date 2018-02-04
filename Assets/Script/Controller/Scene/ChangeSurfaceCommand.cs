using UnityEngine;
using System.Collections;
using Build2D;

public class ChangeSurfaceCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;
        if (se.isRedoAction || se.isUndoAction) return;

        SaveOldValue(se, CloneAssets(se.oldAssets));

        SurfaceVO assets = se.oldAssets[0] as SurfaceVO;
        SurfaceVO newAssets = se.newAssets[0] as SurfaceVO;

        CommitHistoryEvent((SceneEvent)e);
    }
}
