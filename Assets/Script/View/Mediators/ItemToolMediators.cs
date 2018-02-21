using Build3D;
using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolMediators : Mediators
{
    ItemToolPanel ItemToolPanel = new ItemToolPanel();

    public override void OnRegister()
    {
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.ESC, Keyboard_EscHandle);

        AddViewListener(ItemToolPanelEvent.Create_Item, CreateItemHandle);
        AddViewListener(ItemToolPanelEvent.Create_Combination, CreateCombinationHandle);
        AddViewListener(ItemToolPanelEvent.CreatPlane, CreatPlaneHandle);
        AddViewListener(ItemToolPanelEvent.Exit, ExitHandle);
    }

    public override void OnRemove()
    {
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.ESC, Keyboard_EscHandle);

        RemoveViewListener(ItemToolPanelEvent.Create_Item, CreateItemHandle);
        RemoveViewListener(ItemToolPanelEvent.Create_Combination, CreateCombinationHandle);
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

        itemId = l.id;
        itemvo = AssetsModel.Instance.CreateItemVO(l.id);

        if (SceneManager.Instance.brushManager.brushMode == BrushManager.BrushMode.Direct)
        {
            SceneManager.Instance.brushManager.SetItem(l.id, CreateItem);
        }
        else
        {
            Vector3 v = CameraManager.GetCameraForward();
            CreateItem(v, new Vector3());
        }
    }

    private void Keyboard_EscHandle(EventObject e)
    {
        if (SceneManager.Instance.brushManager.brushMode == BrushManager.BrushMode.Direct)
        {
            SceneManager.Instance.brushManager.SetItem("", null);
        }
    }

    string itemId;
    ItemVO itemvo;

    private void CreateItem(Vector3 v, Vector3 r)
    {
        ItemVO itemvo = AssetsModel.Instance.CreateItemVO(itemId);

        TransformVO tvo = itemvo.GetComponentVO<TransformVO>();
        tvo.x = v.x;
        tvo.y = v.y;
        tvo.z = v.z;
        tvo.rotateX = r.x;
        tvo.rotateY = r.y;
        tvo.rotateZ = r.z;

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
            new List<AssetVO>() { null },
            new List<AssetVO>() { itemvo }
            ));
    }

    private void ExitHandle(EventObject e)
    {
        UIManager.CloseUI(panel.name);
    }
}
