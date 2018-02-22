using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class HotelPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(HotelPanelEvent.CLOSE, ClosePanelHandle);

    }

    public override void OnRemove()
    {
        AddViewListener(HotelPanelEvent.CLOSE, ClosePanelHandle);
    }

    private void ClosePanelHandle(EventObject e)
    {
        UIManager.CloseUI(UI.HotelPanel);
    }
}
