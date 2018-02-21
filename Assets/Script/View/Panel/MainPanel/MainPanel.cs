using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
	private GameObject createBtn;
	private GameObject realBtn;
    private GameObject storeBtn;

    void Start()
	{
		createBtn = GetUI("createBtn");
		realBtn = GetUI("realBtn");
        storeBtn = GetUI("storeBtn");

        AddEventClick(createBtn);
		AddEventClick(realBtn);
        AddEventClick(storeBtn);
    }

    protected override void OnClick(GameObject obj)
	{
		if (obj.name == "createBtn")
		{
            dispatchEvent(new MainPanelEvent(MainPanelEvent.CreatePlan_Click));
		}
		if (obj.name == "realBtn")
		{
			dispatchEvent(new MainPanelEvent(MainPanelEvent.RealPlan_Click));
		}
        if (obj.name == "storeBtn")
        {
            dispatchEvent(new MainPanelEvent(MainPanelEvent.StorePlan_Click));
        }
    }

	void Update()
	{
	}
}
