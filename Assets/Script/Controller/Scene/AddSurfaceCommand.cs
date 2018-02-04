using UnityEngine;
using System.Collections;

public class AddSurfaceCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        if (se.isUndoAction)
        {
            SurfaceVO assets = se.oldAssets[0] as SurfaceVO;

            BuilderModel.Instance.Remove(assets.id);
        }
        else
        {
            SaveOldValue(se, CloneAssets(se.oldAssets));

            SurfaceVO oldAssets = se.oldAssets[0] as SurfaceVO;
            SurfaceVO newAssets = se.newAssets[0] as SurfaceVO;

            BuilderModel.Instance.CreateSurface(newAssets);

            CommitHistoryEvent((SceneEvent)e);
        }
    }
}
