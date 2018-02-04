using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

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

        RemoveViewListener(TopToolPanelEvent.HELP, TopToolHelpHandle);

        RemoveContextListener(FileEvent.OPEN_PROJECT_SUCCESS, ProjectOpenSuccessHandle);
    }

    private void ProjectOpenSuccessHandle(EventObject e)
    {

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
