using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class StorePanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(StorePanelEvent.CLOSE, ClosePanelHandle);

    }

    public override void OnRemove()
    {
        AddViewListener(StorePanelEvent.CLOSE, ClosePanelHandle);
    }

    private void ClosePanelHandle(EventObject e)
    {
        UIManager.CloseUI(UI.StorePanel);
    }
}
