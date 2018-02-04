using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneToolPanel : BasePanel
{
    public GameObject space;
    public GameObject nested;
    public GameObject item;

    void Awake()
    {
		space = GetUI("space");
        nested = GetUI("nested");
		item = GetUI("item");

		AddEventClick(space);
		AddEventClick(nested);
		AddEventClick(item);
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj == space)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolPanelEvent(SceneToolPanelEvent.Space));
        }
        if (obj == nested)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolPanelEvent(SceneToolPanelEvent.Nested));
        }
        if (obj == item)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolPanelEvent(SceneToolPanelEvent.Item));
        }
    }

    void Update()
    {

    }
}
