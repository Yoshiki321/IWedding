using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
	private GameObject createBtn;
	private GameObject realBtn;

	void Start()
	{
		createBtn = GetUI("createBtn");
		realBtn = GetUI("realBtn");

        AddEventClick(createBtn);
		AddEventClick(realBtn);
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
	}

	void Update()
	{
	}
}
