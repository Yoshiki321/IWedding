using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class HotelPanel : BasePanel
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
            dispatchEvent(new HotelPanelEvent(HotelPanelEvent.CLOSE));
        }
    }

    void Update()
    {
    }
}
