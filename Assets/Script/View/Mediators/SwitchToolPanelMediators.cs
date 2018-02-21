using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class SwitchToolPanelMediators : Mediators
{
	public override void OnRegister()
	{
		AddViewListener(SwitchToolPanelEvent.FILE, OpenFileHandle);
        AddViewListener(SwitchToolPanelEvent.EDIT, OpenEditHandle);
        AddViewListener(SwitchToolPanelEvent.FILTER, OpenFilterHandle);
        AddViewListener(SwitchToolPanelEvent.WINDOW, OpenWindowHandle);
        AddViewListener(SwitchToolPanelEvent.HELP, OpenHelpHandle);
        AddViewListener(SwitchToolPanelEvent.CLOSE, OpenCloseHandle);
        AddViewListener(SwitchToolPanelEvent.RETURN, ReturnHandle);

    }

    public override void OnRemove()
	{
        RemoveViewListener(SwitchToolPanelEvent.FILE, OpenFileHandle);
        RemoveViewListener(SwitchToolPanelEvent.EDIT, OpenEditHandle);
        RemoveViewListener(SwitchToolPanelEvent.FILTER, OpenFilterHandle);
        RemoveViewListener(SwitchToolPanelEvent.WINDOW, OpenWindowHandle);
        RemoveViewListener(SwitchToolPanelEvent.HELP, OpenHelpHandle);
        RemoveViewListener(SwitchToolPanelEvent.CLOSE, OpenCloseHandle);
        RemoveViewListener(SwitchToolPanelEvent.RETURN, ReturnHandle);
    }

    private void OpenCloseHandle(EventObject e)
    {
        TopToolPanel.Instance.CloseAllPanelHandle();
    }

    private void ReturnHandle(EventObject e)
    {
        Application.LoadLevel("XBuild");
    }

    private void OpenFileHandle(EventObject e)
	{
            TopToolPanel.Instance.OpenFilePanel();
    }

    private void OpenEditHandle(EventObject e)
    {
            TopToolPanel.Instance.OpenEditPanel();
    }

    private void OpenFilterHandle(EventObject e)
    {
            TopToolPanel.Instance.OpenFilterPanel();
    }

    private void OpenWindowHandle(EventObject e)
    {
            TopToolPanel.Instance.OpenWindowPanel();
    }

    private void OpenHelpHandle(EventObject e)
    {
            TopToolPanel.Instance.OpenHelpPanel();
    }
}
