using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BuildManager;
using Build3D;

public class TransformCommand : HistoryCommand
{
    public override void Execute(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        List<AssetVO> assets = (e as SceneEvent).newAssets;

        if (se.isRedoAction || se.isUndoAction)
        {
            List<GameObject> objects = new List<GameObject>();

            for (int i = 0; i < assets.Count; i++)
            {
                ObjectSprite item = AssetsModel.Instance.GetItemData(assets[i].id).item3;
                objects.Add(item.gameObject);
                item.VO.GetComponentVO<TransformVO>().FillFromObject(assets[i] as ComponentVO);
            }
            SceneManager.Instance.editorObjectSelection.SetSelectedObjects(objects, false);

            return;
        }

        SaveOldValue(se, CloneAssets(se.oldAssets));

        for (int i = 0; i < assets.Count; i++)
        {
            TransformVO vo = se.newAssets[i] as TransformVO;
            ItemStruct data = AssetsModel.Instance.GetItemData(vo.id);
            if (data != null)
            {
                ObjectSprite item = data.item3;
                TransformVO itemvo = item.VO.GetComponentVO<TransformVO>();

                itemvo.x = vo.x;
                itemvo.y = vo.y;
                itemvo.z = vo.z;
                itemvo.scaleX = vo.scaleX;
                itemvo.scaleY = vo.scaleY;
                itemvo.scaleZ = vo.scaleZ;
                itemvo.rotateX = vo.rotateX;
                itemvo.rotateY = vo.rotateY;
                itemvo.rotateZ = vo.rotateZ;
            }
        }

        CommitHistoryEvent((SceneEvent)e);
    }
}
