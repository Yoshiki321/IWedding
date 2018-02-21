using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class MainPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(MainPanelEvent.CreatePlan_Click, CreatePlanHandle);
        AddViewListener(MainPanelEvent.StorePlan_Click, StorePlanHandle);

    }

    public override void OnRemove()
    {
        RemoveViewListener(MainPanelEvent.CreatePlan_Click, CreatePlanHandle);
        RemoveViewListener(MainPanelEvent.StorePlan_Click, StorePlanHandle);
    }

    private void CreatePlanHandle(EventObject e)
    {
        UIManager.OpenUI(UI.ProjectPanel);
    }

    private void StorePlanHandle(EventObject e)
    {
        UIManager.OpenUI(UI.StorePanel);
    }
}
