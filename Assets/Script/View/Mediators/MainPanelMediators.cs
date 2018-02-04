using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class MainPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(MainPanelEvent.CreatePlan_Click, CreatePlanHandle);
        AddViewListener(MainPanelEvent.RealPlan_Click, RealPlanHandle);


    }

    public override void OnRemove()
    {
        RemoveViewListener(MainPanelEvent.CreatePlan_Click, CreatePlanHandle);
        RemoveViewListener(MainPanelEvent.RealPlan_Click, RealPlanHandle); 
    }

    private void CreatePlanHandle(EventObject e)
    {
        UIManager.OpenUI(UI.ProjectPanel);
    }

    private void RealPlanHandle(EventObject e)
    {

    }
}
