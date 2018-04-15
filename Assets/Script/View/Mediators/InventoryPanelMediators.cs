using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class InventoryPanelMediators : Mediators
{
    public override void OnRegister()
    {
        OpenPanel();
    }

    public override void OnRemove()
    {
    }

    private void OpenPanel()
    {
        (panel as InventoryPanel).UpdateItem(AssetsModel.Instance.itemDatas);
    }
}
