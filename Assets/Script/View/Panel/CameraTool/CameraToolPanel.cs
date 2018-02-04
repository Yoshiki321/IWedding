using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToolPanel : BasePanel
{
    private GameObject into2;
    private GameObject into3;

    void Start()
    {
        into2 = GetUI("into2");
        into3 = GetUI("into3");

		AddEventClick(into2);
		AddEventClick(into3);

		into2.SetActive(false);
		into3.SetActive(false);
    }

    protected override void OnClick(GameObject obj)
    {
		into2.SetActive(true);
		into3.SetActive(true);

		if (obj == into2)
		{
			dispatchEvent(new CameraToolPanelEvent(CameraToolPanelEvent.Into2D));
		}
		if (obj == into3)
		{
			dispatchEvent(new CameraToolPanelEvent(CameraToolPanelEvent.Into3D));
		}
    }

    void Update()
    {

    }
}
