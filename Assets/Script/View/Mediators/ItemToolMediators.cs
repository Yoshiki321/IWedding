using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolMediators : Mediators
{
    ItemToolPanel ItemToolPanel = new ItemToolPanel();

    public override void OnRegister()
    {
        AddViewListener(ItemToolPanelEvent.Create_Item, CreateItemHandle);
        AddViewListener(ItemToolPanelEvent.Create_Combination, CreateCombinationHandle);
        AddViewListener(ItemToolPanelEvent.CreatPlane, CreatPlaneHandle);
        AddViewListener(ItemToolPanelEvent.Exit, ExitHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(ItemToolPanelEvent.Create_Item, CreateItemHandle);
        RemoveViewListener(ItemToolPanelEvent.CreatPlane, CreatPlaneHandle);
        RemoveViewListener(ItemToolPanelEvent.Exit, ExitHandle);
    }

    private void CreatPlaneHandle(EventObject e)
    {
        ItemToolPanelEvent l = (ItemToolPanelEvent)e;
        switch (l.id)
        {
            case "tdBtn":
                break;
            case "bedBtn":
                break;
            case "gzBtn":
                break;
            case "cjBtn":
                break;
            case "zzBtn":
                break;
        }
    }

    private void CreateCombinationHandle(EventObject e)
    {
        ItemToolPanelEvent l = (ItemToolPanelEvent)e;

        List<AssetVO> list = new List<AssetVO>();
        switch (l.id)
        {
            case "10100001":
                //list = CodeManager.LoadCombination(l.modelUrl);
                break;
            case "10100002":
                //list = CodeManager.LoadCombination(l.modelUrl);
                break;
        }

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
            new List<AssetVO>() { null },
            list
            ));
    }

    private void CreateItemHandle(EventObject e)
    {
        ItemToolPanelEvent l = (ItemToolPanelEvent)e;
        ItemVO itemvo = AssetsModel.Instance.CreateItemVO(l.id);

        TransformVO tvo = itemvo.GetComponentVO<TransformVO>();
        Vector3 v = CameraManager.GetCameraForward();
        tvo.x = v.x;
        tvo.y = v.y;
        tvo.z = v.z;

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
            new List<AssetVO>() { null },
            new List<AssetVO>() { itemvo }
            ));

        ItemVO itemvo1 = AssetsModel.Instance.CreateItemVO("40010001");

        TransformVO tvo1 = itemvo.GetComponentVO<TransformVO>();
        Vector3 v1 = CameraManager.GetCameraForward();
        tvo1.x = v1.x;
        tvo1.y = v1.y;
        tvo1.z = v1.z;

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
            new List<AssetVO>() { null },
            new List<AssetVO>() { itemvo1 }
            ));
    }

    private void ExitHandle(EventObject e)
    {
        Debug.Log(panel.name);

        UIManager.CloseUI(panel.name);
    }
}
