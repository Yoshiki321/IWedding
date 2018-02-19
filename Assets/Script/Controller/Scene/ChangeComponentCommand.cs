using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BuildManager;
using Build3D;

public class ChangeComponentCommand : HistoryCommand
{
    private SceneEvent se;

    public override void Execute(EventObject e)
    {
        se = e as SceneEvent;
        if (se.isRedoAction || se.isUndoAction)
        {
            List<AssetVO> assets = (e as SceneEvent).newAssets;

            UpdateVO();

            return;
        }

        SaveOldValue(se, CloneAssets(se.oldAssets));

        UpdateVO();

        CommitHistoryEvent((SceneEvent)e);
    }

    private void UpdateVO()
    {
        for (int i = 0; i < se.newAssets.Count; i++)
        {
            ObjectSprite item = SceneManager.GetObject3(se.newAssets[i].id);

            if (se.newAssets[i] is SpotlightVO)
            {
                item.VO.GetComponentVO<SpotlightVO>().FillFromObject(se.newAssets[i] as SpotlightVO);
            }
            if (se.newAssets[i] is TubeLightVO)
            {
                item.VO.GetComponentVO<TubeLightVO>().FillFromObject(se.newAssets[i] as TubeLightVO);
            }
            if (se.newAssets[i] is BallLampVO)
            {
                item.VO.GetComponentVO<BallLampVO>().FillFromObject(se.newAssets[i] as BallLampVO);
            }
            if (se.newAssets[i] is CollageVO)
            {
                item.VO.GetComponentVO<CollageVO>().FillFromObject(se.newAssets[i] as CollageVO);
            }
            if (se.newAssets[i] is FrameVO)
            {
                item.VO.GetComponentVO<FrameVO>().FillFromObject(se.newAssets[i] as FrameVO);
            }
            if (se.newAssets[i] is MultipleSpotlightVO)
            {
                item.VO.GetComponentVO<MultipleSpotlightVO>().FillFromObject(se.newAssets[i] as MultipleSpotlightVO);
            }
            if (se.newAssets[i] is RadiationLampVO)
            {
                item.VO.GetComponentVO<RadiationLampVO>().FillFromObject(se.newAssets[i] as RadiationLampVO);
            }
            if (se.newAssets[i] is TransformVO)
            {
                item.VO.GetComponentVO<TransformVO>().FillFromObject(se.newAssets[i] as TransformVO);
            }
            if (se.newAssets[i] is BubbleVO)
            {
                item.VO.GetComponentVO<BubbleVO>().FillFromObject(se.newAssets[i] as BubbleVO);
            }
            if (se.newAssets[i] is SmokeVO)
            {
                item.VO.GetComponentVO<SmokeVO>().FillFromObject(se.newAssets[i] as SmokeVO);
            }
            if (se.newAssets[i] is EditorCameraVO)
            {
                item.VO.GetComponentVO<EditorCameraVO>().FillFromObject(se.newAssets[i] as EditorCameraVO);
            }
            if (se.newAssets[i] is PointLightVO)
            {
                item.VO.GetComponentVO<PointLightVO>().FillFromObject(se.newAssets[i] as PointLightVO);
            }
            if (se.newAssets[i] is RelationVO)
            {
                item.VO.GetComponentVO<RelationVO>().FillFromObject(se.newAssets[i] as RelationVO);
            }
            if (se.newAssets[i] is SprinkleVO)
            {
                item.VO.GetComponentVO<SprinkleVO>().FillFromObject(se.newAssets[i] as SprinkleVO);
            }
            if (se.newAssets[i] is CurvyColumnVO)
            {
                item.VO.GetComponentVO<CurvyColumnVO>().FillFromObject(se.newAssets[i] as CurvyColumnVO);
            }
        }
    }
}
