using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;
using System;

public class TopToolPanelMediators : Mediators
{
	public override void OnRegister()
	{
        AddViewListener(TopToolPanelEvent.SAVE, TopToolSaveHandle);
        AddViewListener(TopToolPanelEvent.LOAD, TopToolLoadHandle);

        AddViewListener(TopToolPanelEvent.UNDO, TopToolUndoHandle);
        AddViewListener(TopToolPanelEvent.REDO, TopToolRedoHandle);
        AddViewListener(TopToolPanelEvent.COPY, TopToolCopyHandle);
        AddViewListener(TopToolPanelEvent.PASTE, TopToolPasteHandle);
        AddViewListener(TopToolPanelEvent.DELETE, TopToolDeleteHandle);
        AddViewListener(TopToolPanelEvent.GROUP, TopToolGroupHandle);
        AddViewListener(TopToolPanelEvent.REGROUP, TopToolReGroupHandle);
        AddViewListener(TopToolPanelEvent.DRAWSTAGE, TopToolDrawStageHandle);

        AddViewListener(TopToolPanelEvent.FILTER, TopToolFilterHandle);

        AddViewListener(TopToolPanelEvent.ADDITEM, TopToolAddItemHandle);
        AddViewListener(TopToolPanelEvent.ADDHOME, TopToolAddHomeHandle);

        AddViewListener(TopToolPanelEvent.HELP, TopToolHelpHandle);
        AddViewListener(TopToolPanelEvent.HIDE, HideHandler);
        AddViewListener(TopToolPanelEvent.OPENBRUSH, TopToolOpenBrushHandle);
        AddViewListener(TopToolPanelEvent.CLOSEBRUSH, TopToolCloseBrushHandle);
        AddViewListener(TopToolPanelEvent.ALIGN, TopToolAlignStageHandle);
        AddViewListener(TopToolPanelEvent.PHOTO, TopToolPhotoStageHandle);
        AddViewListener(TopToolPanelEvent.VIEWONE, TopToolViewOneStageHandle);
        AddViewListener(TopToolPanelEvent.VIEWTWO, TopToolViewTwoStageHandle);
        AddViewListener(TopToolPanelEvent.VIEWTHREE, TopToolViewThreeStageHandle);
        AddViewListener(TopToolPanelEvent.VIEWFOUR, TopToolViewFourStageHandle);
        AddViewListener(TopToolPanelEvent.VIEWFIVE, TopToolViewFiveStageHandle);
        AddViewListener(TopToolPanelEvent.OPENLIGHT, TopToolOpenLightStageHandle);
        AddViewListener(TopToolPanelEvent.CLOSELIGHT, TopToolCloseLightStageHandle);
        AddViewListener(TopToolPanelEvent.OPENDRAWPANEL, TopToolOpenDrawStageHandle);
        AddViewListener(TopToolPanelEvent.OPENMODELPANEL, TopToolOpenModelStageHandle);

        AddContextListener(FileEvent.OPEN_PROJECT_SUCCESS, ProjectOpenSuccessHandle);
    }

    public override void OnRemove()
	{
        RemoveViewListener(TopToolPanelEvent.SAVE, TopToolSaveHandle);
        RemoveViewListener(TopToolPanelEvent.LOAD, TopToolLoadHandle);

        RemoveViewListener(TopToolPanelEvent.UNDO, TopToolUndoHandle);
        RemoveViewListener(TopToolPanelEvent.REDO, TopToolRedoHandle);
        RemoveViewListener(TopToolPanelEvent.COPY, TopToolCopyHandle);
        RemoveViewListener(TopToolPanelEvent.PASTE, TopToolPasteHandle);
        RemoveViewListener(TopToolPanelEvent.DELETE, TopToolDeleteHandle);
        RemoveViewListener(TopToolPanelEvent.GROUP, TopToolGroupHandle);
        RemoveViewListener(TopToolPanelEvent.REGROUP, TopToolReGroupHandle);
        RemoveViewListener(TopToolPanelEvent.FILTER, TopToolFilterHandle);
        RemoveViewListener(TopToolPanelEvent.DRAWSTAGE, TopToolDrawStageHandle);

        RemoveViewListener(TopToolPanelEvent.ADDITEM, TopToolAddItemHandle);
        RemoveViewListener(TopToolPanelEvent.ADDHOME, TopToolAddHomeHandle);
        RemoveViewListener(TopToolPanelEvent.HIDE, HideHandler);
        RemoveViewListener(TopToolPanelEvent.HELP, TopToolHelpHandle);
        RemoveViewListener(TopToolPanelEvent.OPENBRUSH, TopToolOpenBrushHandle);
        RemoveViewListener(TopToolPanelEvent.CLOSEBRUSH, TopToolCloseBrushHandle);
        RemoveViewListener(TopToolPanelEvent.ALIGN, TopToolAlignStageHandle);
        RemoveViewListener(TopToolPanelEvent.PHOTO, TopToolPhotoStageHandle);
        RemoveViewListener(TopToolPanelEvent.VIEWONE, TopToolViewOneStageHandle);
        RemoveViewListener(TopToolPanelEvent.VIEWTWO, TopToolViewTwoStageHandle);
        RemoveViewListener(TopToolPanelEvent.VIEWTHREE, TopToolViewThreeStageHandle);
        RemoveViewListener(TopToolPanelEvent.VIEWFOUR, TopToolViewFourStageHandle);
        RemoveViewListener(TopToolPanelEvent.VIEWFIVE, TopToolViewFiveStageHandle);
        RemoveViewListener(TopToolPanelEvent.OPENLIGHT, TopToolOpenLightStageHandle);
        RemoveViewListener(TopToolPanelEvent.CLOSELIGHT, TopToolCloseLightStageHandle);
        RemoveViewListener(TopToolPanelEvent.OPENDRAWPANEL, TopToolOpenDrawStageHandle);
        RemoveViewListener(TopToolPanelEvent.OPENMODELPANEL, TopToolOpenModelStageHandle);
        RemoveContextListener(FileEvent.OPEN_PROJECT_SUCCESS, ProjectOpenSuccessHandle);
    }

    private bool isUIOpen = true;
    private void HideHandler(EventObject e)
    {
        if (isUIOpen)
        {
            isUIOpen = false;
            GameObject.Find("UI/ItemSelectPanel").SetActive(false);
            GameObject.Find("UI/SwitchToolPanel").SetActive(false);
            GameObject.Find("UI/ComponentPanel").SetActive(false);
            GameObject.Find("UI/CorePanel").SetActive(false);
            GameObject.Find("UI/ItemToolPanel").SetActive(false);
            TopToolPanel.Instance.HideAllMenu();
        }
        else
        {
            isUIOpen = true;
            GameObject.Find("UI").transform.Find("ItemSelectPanel").gameObject.SetActive(true);
            GameObject.Find("UI").transform.Find("SwitchToolPanel").gameObject.SetActive(true);
            GameObject.Find("UI").transform.Find("ComponentPanel").gameObject.SetActive(true);
            GameObject.Find("UI").transform.Find("CorePanel").gameObject.SetActive(true);
            GameObject.Find("UI").transform.Find("ItemToolPanel").gameObject.SetActive(true);
            TopToolPanel.Instance.ShowAllMenu();
        }

    }

    private void ProjectOpenSuccessHandle(EventObject e)
    {

    }

    private void TopToolOpenDrawStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.OPENDRAWPANEL));
    }

    private void TopToolOpenModelStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.OPENMODELPANEL));
    }

    private void TopToolViewOneStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.VIEWONE));
    }

    private void TopToolViewTwoStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.VIEWTWO));
    }

    private void TopToolViewThreeStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.VIEWTHREE));
    }

    private void TopToolViewFourStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.VIEWFOUR));
    }

    private void TopToolViewFiveStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.VIEWFIVE));
    }

    private void TopToolOpenLightStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.OPENLIGHT));
    }
    
    private void TopToolCloseLightStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.CLOSELIGHT));
    }

    private void TopToolOpenBrushHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.OPENBRUSH));
    }

    private void TopToolCloseBrushHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.CLOSEBRUSH));
    }

    private void TopToolAlignStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.ALIGN));
    }

    private void TopToolPhotoStageHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.PHOTO));
    }

    private void TopToolSaveHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.SAVE));
    }

    private void TopToolLoadHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.LOAD));
    }

    private void TopToolUndoHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.UNDO));
    }

    private void TopToolRedoHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.REDO));
    }

    private void TopToolCopyHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.COPY));
    }

    private void TopToolPasteHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.PASTE));
    }

    private void TopToolDeleteHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.DELETE));
    }

    private void TopToolGroupHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.GROUP));
    }

    private void TopToolReGroupHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.REGROUP));
    }

    private void TopToolFilterHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.FILTER));
    }

    private void TopToolAddItemHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.ADDITEM));
    }

    private void TopToolAddHomeHandle(EventObject e)
    {
        DispatcherEvent(new TopToolPanelEvent(TopToolPanelEvent.ADDHOME));
    }

    private void TopToolDrawStageHandle(EventObject e)
    {
        UIManager.OpenUI(UI.DrawLinePanel);
    }
    private void TopToolHelpHandle(EventObject e)
	{
    }
}
