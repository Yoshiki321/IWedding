using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;
using System.Windows.Forms;

public class ProjectPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(ProjectPanelEvent.CREATE, CreateHandle);
        AddViewListener(ProjectPanelEvent.OPEN, OpenHandle);

        AddContextListener(FileEvent.NEW_PROJECT_COMPLETE, ProjectCompleteHandle);
        AddContextListener(FileEvent.OPEN_PROJECT_FAIL, ProjectOpenFailHandle);
        AddContextListener(FileEvent.OPEN_PROJECT_SUCCESS, ProjectOpenSuccessHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(ProjectPanelEvent.CREATE, CreateHandle);
        RemoveViewListener(ProjectPanelEvent.OPEN, OpenHandle);

        RemoveContextListener(FileEvent.NEW_PROJECT_COMPLETE, ProjectCompleteHandle);
        RemoveContextListener(FileEvent.OPEN_PROJECT_FAIL, ProjectOpenFailHandle);
        RemoveContextListener(FileEvent.OPEN_PROJECT_SUCCESS, ProjectOpenSuccessHandle);
    }

    private void CreateHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.NEW_PROJECT, (panel as ProjectPanel).location, (panel as ProjectPanel).projectName));
        SwitchToolPanel.Instance.WirtePorjectName((panel as ProjectPanel).projectName);
    }

    private void OpenHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.OPEN_PROJECT));
    }

    private void IntoProjectHandle()
    {
        UIManager.OpenUI(UI.ComponentPanel);
        UIManager.OpenUI(UI.ItemSelectPanel);
        UIManager.OpenUI(UI.CorePanel);
        UIManager.OpenUI(UI.SwitchToolPanel);
        UIManager.OpenUI(UI.ComponentPanel);
        UIManager.OpenUI(UI.ItemToolPanel);
        UIManager.OpenUI(UI.TopToolPanel);
        UIManager.CloseUI(panel.name);
        UIManager.CloseUI(UI.MainPanel);
        CameraManager.ChangeCamera(CameraFlags.Two);
    }

    private void ProjectCompleteHandle(EventObject e)
    {
        IntoProjectHandle();
        CodeManager.LoadCode("Config/DefaultLayout", true);
        CodeManager.SaveCode();
    }

    private void ProjectOpenFailHandle(EventObject e)
    {

    }

    private void ProjectOpenSuccessHandle(EventObject e)
    {
        IntoProjectHandle();
    }
}
