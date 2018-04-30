using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
	private GameObject createBtn;
	private GameObject realBtn;
    private GameObject storeBtn;
    private GameObject btnOne;
    private GameObject btnTwo;
    private GameObject closeBtn;
    private GameObject selectPanel;

    void Start()
	{
		createBtn = GetUI("createBtn");
		realBtn = GetUI("realBtn");
        storeBtn = GetUI("storeBtn");
        selectPanel = GetUI("SelectPanel");
        btnOne = GetUI("SelectPanel").transform.Find("Btn1").gameObject;
        btnTwo = GetUI("SelectPanel").transform.Find("Btn2").gameObject;
        closeBtn = GetUI("SelectPanel").transform.Find("closeBtn").gameObject;

        AddEventClick(btnOne);
        AddEventClick(btnTwo);
        AddEventClick(closeBtn);

        AddEventClick(createBtn);
		AddEventClick(realBtn);
        AddEventClick(storeBtn);
    }

    protected override void OnClick(GameObject obj)
	{
        if (obj.name == "Btn1")
        {
            dispatchEvent(new MainPanelEvent(MainPanelEvent.BtnOne_Click));
        }
        if (obj.name == "Btn2")
        {
            dispatchEvent(new MainPanelEvent(MainPanelEvent.BtnTwo_Click));
        }
        if (obj.name == "closeBtn")
        {
            selectPanel.SetActive(false);
        }
        if (obj.name == "createBtn")
		{
            dispatchEvent(new MainPanelEvent(MainPanelEvent.BtnOne_Click));
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
