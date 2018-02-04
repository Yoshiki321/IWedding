using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadModelPanel : BasePanel
{
	private GameObject _modelUrlBtn;
	private InputField _modelUrlText;
    private GameObject _albedoUrlBtn;
    private InputField _albedoUrlText;
    private GameObject _createBtn;
    private GameObject _exitBtn;

    void Start()
	{
        _modelUrlBtn = GetUI("ModelUrlBtn");
        _albedoUrlBtn = GetUI("AlbedoUrlBtn");
        _createBtn = GetUI("CreateBtn");
        _exitBtn = GetUI("ExitBtn");
        _modelUrlText = GetUI("ModelUrlText").GetComponent<InputField>();
        _albedoUrlText = GetUI("AlbedoUrlText").GetComponent<InputField>();

        AddEventClick(_modelUrlBtn);
		AddEventClick(_albedoUrlBtn);
		AddEventClick(_createBtn);
		AddEventClick(_exitBtn);
	}

	protected override void OnClick(GameObject obj)
	{
        if (obj == _modelUrlBtn)
        {
            dispatchEvent(new LoadModelPanelEvent(LoadModelPanelEvent.LOAD_MODEL));
        }
        if (obj == _albedoUrlBtn)
        {
            dispatchEvent(new LoadModelPanelEvent(LoadModelPanelEvent.LOAD_ALBEDO));
        }
        if (obj == _createBtn)
        {
            dispatchEvent(new LoadModelPanelEvent(LoadModelPanelEvent.CREATE));
        }
        if (obj == _exitBtn)
        {
            CloseSelf();
        }
    }

	void Update()
	{
	}
}
