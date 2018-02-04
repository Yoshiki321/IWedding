using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class ProjectPanel : BasePanel
{
    private InputField projectNameText;
    private InputField locationText;
    private GameObject locationBtn;
    private GameObject createBtn;
    private GameObject readBtn;
    private GameObject closeProjectBtn;

    public string location
    {
        get { return locationText.text; }
    }

    public string projectName
    {
        get { return projectNameText.text; }
    }

    void Start()
    {
        projectNameText = GetUI("ProjectName").GetComponent<InputField>();
        locationText = GetUI("Location").GetComponent<InputField>();
        locationBtn = GetUI("LocationBtn");
        createBtn = GetUI("CreateBtn");
        readBtn = GetUI("ReadBtn");
        locationBtn = GetUI("LocationBtn");
        closeProjectBtn = GetUI("CloseProjectBtn");

        projectNameText.text = "新建工程";
        projectNameText.transform.Find("Text").GetComponent<Text>().fontSize = 40;
        locationText.text = UnityEngine.Application.absoluteURL;
        locationText.transform.Find("Text").GetComponent<Text>().fontSize = 40;
        if (PlayerPrefs.GetString("LoactionUrl") != "") {
            locationText.text = PlayerPrefs.GetString("LoactionUrl");
        }
        AddEventClick(locationBtn);
        AddEventClick(createBtn);
        AddEventClick(readBtn);
        AddEventClick(closeProjectBtn);
    }

    protected override void OnClick(GameObject obj)
    {
        base.OnClick(obj);

        if (obj == locationBtn)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空");
                    return;
                }
                locationText.text = dialog.SelectedPath;
                PlayerPrefs.SetString("LoactionUrl", locationText.text);
            }
        }
        if (obj == createBtn)
        {
            dispatchEvent(new ProjectPanelEvent(ProjectPanelEvent.CREATE));
        }
        if (obj == readBtn)
        {
            dispatchEvent(new ProjectPanelEvent(ProjectPanelEvent.OPEN));
        }
        if (obj == closeProjectBtn)
        {
            UIManager.CloseUI(UI.ProjectPanel);
        }
    }

    void Update()
    {
    }
}
