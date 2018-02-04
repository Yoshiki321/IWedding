using UnityEngine;
using System.Collections;

public class AddNestedCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        if (se.isUndoAction)
        {
            NestedVO assets = se.oldAssets[0] as NestedVO;

            AssetsModel.Instance.Remove(assets.id);
        }
        else
        {
            SaveOldValue(se, CloneAssets(se.oldAssets));

            NestedVO oldAssets = se.oldAssets[0] as NestedVO;
            NestedVO newAssets = se.newAssets[0] as NestedVO;

            AssetsModel.Instance.CreateNested(newAssets);

            CommitHistoryEvent((SceneEvent)e);
        }
    }
}
