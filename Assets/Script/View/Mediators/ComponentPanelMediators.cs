using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;
using System;

public class ComponentPanelMediators : Mediators
{
    public override void OnRegister()
    {
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_ITEM, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_SURFACE, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_LINE, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_ITEM, ReleaseObject3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_SURFACE, ReleaseObject3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_LINE, ReleaseObject3Handle);

        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.ESC, ESCHandle);

        AddViewListener(ComponentPanelEvent.TRANSFORM_CHANGE, TransformChangeComponentHandle);
        AddViewListener(ComponentPanelEvent.UPDATE, UpdateComponent);

        SceneManager.Instance.control3Manager.addEventListener(Control3ManagerEvent.TRANSFORM_UPDATE, TransformItem3UpdateHandle);

        AddContextListener(SceneEvent.CHANGE_ITEM, ChangeItemComponent);
        AddContextListener(SceneEvent.CHANGE_COMPONENT, ChangeComponentComponent);
        AddContextListener(SceneEvent.TRANSFORM, ChangeItemComponent);

        AddContextListener(SceneEvent.DELETE, EventDispatcher_DeleteHandler);

        InitComponentUI();
    }

    public override void OnRemove()
    {
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_ITEM, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_SURFACE, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_LINE, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_ITEM, ReleaseObject3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_SURFACE, ReleaseObject3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_LINE, ReleaseObject3Handle);

        RemoveViewListener(ComponentPanelEvent.UPDATE, UpdateComponent);

        RemoveContextListener(SceneEvent.CHANGE_ITEM, ChangeItemComponent);
        RemoveContextListener(SceneEvent.TRANSFORM, ChangeItemComponent);
    }

    private void EventDispatcher_DeleteHandler(EventObject e)
    {
        (panel as ComponentPanel).ClearComponent();
    }

    private void TransformItem3UpdateHandle(EventObject e)
    {
        UIManager.UpdateComponent(UI.TransformComponentUI, AssetsModel.Instance.GetItemData((e as Control3ManagerEvent).newAssetVOs));
    }

    private void TransformChangeComponentHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.EstablishActiveGizmoPosition();
    }

    private void UpdateComponent(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.CHANGE_COMPONENT,
          (e as ComponentPanelEvent).oldAssets,
          (e as ComponentPanelEvent).newAssets));
    }

    private void ChangeItemComponent(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        ArrayList list = new ArrayList();
        foreach (AssetVO vo in se.newAssets)
        {
            list.Add(SceneManager.GetObject3(vo.id));
        }

        foreach (Type type in hashUI.Keys)
        {
            List<ObjectSprite> assets = CanOpenComponent(list, type);
            if (assets != null && assets.Count != 0) UIManager.UpdateComponent(hashUI[type] as string, assets);
        }
    }

    private void ChangeComponentComponent(EventObject e)
    {
        ChangeItemComponent(e);

        SceneEvent se = e as SceneEvent;

        foreach (AssetVO vo in se.newAssets)
        {
            ObjectSprite obj = SceneManager.GetObject3(vo.id);
            obj.VO = obj.VO;
        }
    }

    private void NestedChangeComponent(EventObject e)
    {
    }

    private void SurfaceChangeComponent(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.CHANGE_SURFACE,
        (e as ComponentPanelEvent).oldAssets,
        (e as ComponentPanelEvent).newAssets));
    }

    private Hashtable hashUI;

    private void InitComponentUI()
    {
        hashUI = new Hashtable();
        hashUI.Add(typeof(TransformVO), UI.TransformComponentUI);
        hashUI.Add(typeof(SpotlightVO), UI.SpotlightComponentUI);
        hashUI.Add(typeof(MultipleSpotlightVO), UI.MultipleSpotlighComponentUI);
        hashUI.Add(typeof(BallLampVO), UI.BallLampComponentUI);
        hashUI.Add(typeof(RadiationLampVO), UI.RadiationLampComponentUI);
        hashUI.Add(typeof(FrameVO), UI.FrameComponentUI);
        hashUI.Add(typeof(CollageVO), UI.CollageComponentUI);
        hashUI.Add(typeof(TubeLightVO), UI.TubeLightComponentUI);
        hashUI.Add(typeof(BubbleVO), UI.BubbleComponentUI); 
        hashUI.Add(typeof(SmokeVO), UI.SmokeComponentUI);
        hashUI.Add(typeof(EditorCameraVO), UI.EditorCameraComponentUI);
        hashUI.Add(typeof(PointLightVO), UI.PointLightComponentUI); 
        hashUI.Add(typeof(RelationVO), UI.RelationComponentUI);
        hashUI.Add(typeof(ThickIrregularVO), UI.ThickIrregularComponentUI);
        hashUI.Add(typeof(SprinkleVO), UI.SprinkleComponentUI);
        hashUI.Add(typeof(FlowerWallVO), UI.FlowerWallComponentUI);
        hashUI.Add(typeof(CurvyColumnVO), UI.CurvyColumnComponentUI);
    }

    private void ReleaseObject3Handle(EventObject e)
    {
        if (Mouse3Manager.selectionItem.Count == 0)
        {
            (panel as ComponentPanel).ClearComponent();
        }
    }

    private void ESCHandle(EventObject e)
    {
        (panel as ComponentPanel).ClearComponent();
    }

    private void SelectObject3Handle(EventObject e)
    {
        UIManager.ClosePanel(Panel.SelectTexturePanel);

        Mouse3ManagerEvent me = e as Mouse3ManagerEvent;

        (panel as ComponentPanel).ClearComponent();

        foreach (Type type in hashUI.Keys)
        {
            List<ObjectSprite> assets = CanOpenComponent(me.objects, type);
            if (assets != null && assets.Count != 0)
            {
                if (hashUI[type] as string == UI.CollageComponentUI)
                {
                    if (SameCollage(assets))
                    {
                        UIManager.OpenComponent(hashUI[type] as string, assets);
                    }
                }
                else
                {
                    UIManager.OpenComponent(hashUI[type] as string, assets);
                }
            }
        }
    }

    private bool SameCollage(List<ObjectSprite> list)
    {
        ObjectSprite o = null;

        foreach (ObjectSprite item in list)
        {
            if (o == null)
            {
                o = item;
            }
            else
            {
                if (item.GetType() != o.GetType())
                {
                    return false;
                }

                if ((item.VO as ItemVO).assetId != (o.VO as ItemVO).assetId)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private List<ObjectSprite> CanOpenComponent(ArrayList value, Type type)
    {
        List<ObjectSprite> assets = new List<ObjectSprite>();

        object lastObject = null;

        foreach (object obj in value)
        {
            if (lastObject == null) lastObject = obj;
            if (lastObject.GetType() != obj.GetType()) return null;

            if (obj is ObjectSprite3D)
            {
                if ((obj as ObjectSprite3D).VO.GetComponentVO(type) == null)
                {
                    return null;
                }
                else
                {
                    assets.Add(obj as ObjectSprite3D);
                }
            }

            if (obj is Line3D)
            {
                if ((obj as Line3D).VO.GetComponentVO(type) == null)
                {
                    return null;
                }
                else
                {
                    assets.Add(obj as Line3D);
                }
            }

            if (obj is Surface3D)
            {
                if ((obj as Surface3D).VO.GetComponentVO(type) == null)
                {
                    return null;
                }
                else
                {
                    assets.Add(obj as Surface3D);
                }
            }
        }

        return assets;
    }
}
