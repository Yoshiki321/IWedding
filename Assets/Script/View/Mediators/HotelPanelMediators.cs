using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class HotelPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(HotelPanelEvent.CLOSE, ClosePanelHandle);
        AddViewListener(HotelPanelEvent.ENTERHOTEL, Open2DPanelHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(HotelPanelEvent.CLOSE, ClosePanelHandle);
        RemoveViewListener(HotelPanelEvent.ENTERHOTEL, Open2DPanelHandle);
    }
    private void Open2DPanelHandle(EventObject e)
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

    private void ClosePanelHandle(EventObject e)
    {
        UIManager.CloseUI(UI.HotelPanel);
    }
}
