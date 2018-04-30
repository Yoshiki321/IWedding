using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class MainPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(MainPanelEvent.BtnOne_Click, CreatePlanHandle);
        AddViewListener(MainPanelEvent.BtnTwo_Click, HotelPlanHandle);
        AddViewListener(MainPanelEvent.StorePlan_Click, StorePlanHandle);

    }

    public override void OnRemove()
    {
        RemoveViewListener(MainPanelEvent.BtnOne_Click, CreatePlanHandle);
        RemoveViewListener(MainPanelEvent.BtnTwo_Click, HotelPlanHandle);
        RemoveViewListener(MainPanelEvent.StorePlan_Click, StorePlanHandle);
    }

    private void HotelPlanHandle(EventObject e)
    {
        //UIManager.OpenUI(UI.HotelPanel);
    }

    private void CreatePlanHandle(EventObject e)
    {
        UIManager.OpenUI(UI.ProjectPanel);
    }

    private void StorePlanHandle(EventObject e)
    {
        //UIManager.OpenUI(UI.StorePanel);
    }
}
