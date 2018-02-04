using UnityEngine;
using System.Collections;
using Build3D;

public class AddItemCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        if (se.isUndoAction)
        {
            foreach (ItemVO vo in se.oldAssets)
            {
                AssetsModel.Instance.Remove(vo.id);
            }
        }
        else
        {
            SaveOldValue(se, CloneAssets(se.oldAssets));

            foreach (ItemVO vo in se.newAssets)
            {
                Item3D item = AssetsModel.Instance.CreateItem(vo).object3 as Item3D;

                item.mousePoint = item.transform.position;

                item.UpdateComponent();
            }

            CommitHistoryEvent((SceneEvent)e);
        }
    }
}
