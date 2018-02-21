using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : BasePanel
{
    private GameObject returnBtn;

    void Start()
    {
        returnBtn = GetUI("ReturnBtn");
        AddEventClick(returnBtn);
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj == returnBtn)
        {
            dispatchEvent(new StorePanelEvent(StorePanelEvent.CLOSE));
        }
    }

    void Update()
    {
    }
}
