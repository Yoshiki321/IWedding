using UnityEngine;
using System.Collections;
using BuildManager;
using System.Collections.Generic;

public class ItemSelectPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(ItemSelectPanelEvent.SELECT_ITEM, SelectItemHandle);
        AddViewListener(ItemSelectPanelEvent.FOCUSON_SELECTION, FocusOnSelectionHandle);

        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_ITEM, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_ITEM, SelectObject3Handle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.ESC, ESCHandle);

        AddContextListener(SceneEvent.CHANGE_GROUP_ITEM, ChangeGroupItemHandle);

        AddContextListener(FileEvent.LOAD, EventDispatcher_LoadCodeHandler);

        AddContextListener(SceneEvent.ADD_ITEM, EventDispatcher_AddObjectHandler);
        AddContextListener(SceneEvent.DELETE, EventDispatcher_DeleteHandler);

        AddContextListener(ProgressEvent.LOAD_COMPLETE, LoadCompleteHandle);
    }

    public override void OnRemove()
    {
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_ITEM, SelectObject3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_ITEM, SelectObject3Handle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.ESC, ESCHandle);

        RemoveViewListener(ItemSelectPanelEvent.SELECT_ITEM, SelectItemHandle);
        RemoveViewListener(ItemSelectPanelEvent.FOCUSON_SELECTION, FocusOnSelectionHandle);

        RemoveContextListener(FileEvent.LOAD, EventDispatcher_LoadCodeHandler);

        RemoveContextListener(SceneEvent.ADD_ITEM, EventDispatcher_AddObjectHandler);
        RemoveContextListener(SceneEvent.DELETE, EventDispatcher_DeleteHandler);

        RemoveContextListener(ProgressEvent.LOAD_COMPLETE, LoadCompleteHandle);
    }

    private void LoadCompleteHandle(EventObject e)
    {
        (panel as ItemSelectPanel).UpdateItem(AssetsModel.Instance.itemDatas);
    }

    private void SelectObject3Handle(EventObject e)
    {
        (panel as ItemSelectPanel).UpdateTextName();
    }

    private void ESCHandle(EventObject e)
    {
        (panel as ItemSelectPanel).ClearSelect();
    }

    private void FocusOnSelectionHandle(EventObject e)
    {
        SceneManager.Instance.control3Manager.FocusOnSelection();
    }

    private void SelectItemHandle(EventObject e)
    {
        ItemSelectPanelEvent se = e as ItemSelectPanelEvent;

        if (CameraManager.visual != CameraFlags.Two)
        {
            SceneManager.Instance.mouse3Manager.ClearSelect();
            SceneManager.Instance.editorObjectSelection.SetSelectedObjects(new List<GameObject>()
            {
                AssetsModel.Instance.GetItemData(se.name).object3.gameObject
            }, false);
        }
    }

    private void ChangeGroupItemHandle(EventObject e)
    {
        (panel as ItemSelectPanel).UpdateItem(AssetsModel.Instance.itemDatas);
    }

    private void EventDispatcher_LoadCodeHandler(EventObject e)
    {
        (panel as ItemSelectPanel).UpdateItem(AssetsModel.Instance.itemDatas);
    }

    private void EventDispatcher_AddObjectHandler(EventObject e)
    {
        (panel as ItemSelectPanel).UpdateItem(AssetsModel.Instance.itemDatas);
    }

    private void EventDispatcher_DeleteHandler(EventObject e)
    {
        (panel as ItemSelectPanel).UpdateItem(AssetsModel.Instance.itemDatas);
    }
}
