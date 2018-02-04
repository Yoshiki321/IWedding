using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopToolPanel : BasePanel
{
    private List<GameObject> TopToolBtnList = new List<GameObject>();

    private List<GameObject> TopToolFileBtnList = new List<GameObject>();
    private List<GameObject> TopToolEditBtnList = new List<GameObject>();
    private List<GameObject> TopToolFilterBtnList = new List<GameObject>();
    private List<GameObject> TopToolWindowBtnList = new List<GameObject>();
    private List<GameObject> TopToolHelpBtnList = new List<GameObject>();
    public bool TopToolFlag = false;
    private void Awake()
    {

    }

    private static TopToolPanel _Instance;

    public static TopToolPanel Instance
    {
        get { return _Instance; }
    }

    private void Start()
    {
        _Instance = this;

        TopToolBtnList.Add(GetUI("FilePanel"));
        TopToolBtnList.Add(GetUI("EditPanel"));
        TopToolBtnList.Add(GetUI("FilterPanel"));
        TopToolBtnList.Add(GetUI("WindowPanel"));
        TopToolBtnList.Add(GetUI("HelpPanel"));
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            TopToolBtnList[i].SetActive(false);
        }

        TopToolFileBtnList.Add(TopToolBtnList[0].transform.Find("Btn1").gameObject);
        TopToolFileBtnList.Add(TopToolBtnList[0].transform.Find("Btn2").gameObject);
        TopToolFileBtnList.Add(TopToolBtnList[0].transform.Find("Btn3").gameObject);

        for (int i = 0; i < TopToolFileBtnList.Count; i++)
        {
            AddEventClick(TopToolFileBtnList[i]);
            AddEventDown(TopToolFileBtnList[i]);
            AddEventUp(TopToolFileBtnList[i]);
            AddEventOver(TopToolFileBtnList[i]);
            AddEventExit(TopToolFileBtnList[i]);
        }

        TopToolEditBtnList.Add(TopToolBtnList[1].transform.Find("Btn1").gameObject);
        TopToolEditBtnList.Add(TopToolBtnList[1].transform.Find("Btn2").gameObject);
        TopToolEditBtnList.Add(TopToolBtnList[1].transform.Find("Btn3").gameObject);
        TopToolEditBtnList.Add(TopToolBtnList[1].transform.Find("Btn4").gameObject);
        TopToolEditBtnList.Add(TopToolBtnList[1].transform.Find("Btn5").gameObject);

        for (int i = 0; i < TopToolEditBtnList.Count; i++)
        {
            AddEventClick(TopToolEditBtnList[i]);
            AddEventDown(TopToolEditBtnList[i]);
            AddEventUp(TopToolEditBtnList[i]);
            AddEventOver(TopToolEditBtnList[i]);
            AddEventExit(TopToolEditBtnList[i]);
        }

        TopToolFilterBtnList.Add(TopToolBtnList[2].transform.Find("Btn1").gameObject);
        for (int i = 0; i < TopToolFilterBtnList.Count; i++)
        {
            AddEventClick(TopToolFilterBtnList[i]);
            AddEventDown(TopToolFilterBtnList[i]);
            AddEventUp(TopToolFilterBtnList[i]);
            AddEventOver(TopToolFilterBtnList[i]);
            AddEventExit(TopToolFilterBtnList[i]);
        }

        TopToolWindowBtnList.Add(TopToolBtnList[3].transform.Find("Btn1").gameObject);
        TopToolWindowBtnList.Add(TopToolBtnList[3].transform.Find("Btn2").gameObject);
        for (int i = 0; i < TopToolWindowBtnList.Count; i++)
        {
            AddEventClick(TopToolWindowBtnList[i]);
            AddEventDown(TopToolWindowBtnList[i]);
            AddEventUp(TopToolWindowBtnList[i]);
            AddEventOver(TopToolWindowBtnList[i]);
            AddEventExit(TopToolWindowBtnList[i]);
        }

        TopToolHelpBtnList.Add(TopToolBtnList[4].transform.Find("Btn1").gameObject);
        for (int i = 0; i < TopToolHelpBtnList.Count; i++)
        {
            AddEventClick(TopToolHelpBtnList[i]);
            AddEventDown(TopToolHelpBtnList[i]);
            AddEventUp(TopToolHelpBtnList[i]);
            AddEventOver(TopToolHelpBtnList[i]);
            AddEventExit(TopToolHelpBtnList[i]);
        }

        TopToolBtnList[4].GetComponentInChildren<Text>().text = "版本号： "+SceneManager.Version;
    }

    protected override void OnClick(GameObject obj)
    {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            TopToolBtnList[i].SetActive(false);
        }
        //文件菜单  
        //保存 
        if (obj == TopToolFileBtnList[0])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.SAVE));
        }
        //读取
        if (obj == TopToolFileBtnList[1])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.LOAD));
        }

        if (obj == TopToolFileBtnList[2])
        {
            UIManager.OpenUI(UI.ProjectPanel);
        }
        //编辑
        //撤销 
        if (obj == TopToolEditBtnList[0])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.UNDO));
        }
        //恢复
        if (obj == TopToolEditBtnList[1])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.REDO));
        }
        //复制 
        if (obj == TopToolEditBtnList[2])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.COPY));
        }
        //粘贴
        if (obj == TopToolEditBtnList[3])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.PASTE));
        }
        //删除
        if (obj == TopToolEditBtnList[4])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.DELETE));
        }
        //滤镜
        //相机滤镜 
        if (obj == TopToolFilterBtnList[0])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.FILTER));
        }
        //窗口
        //物品添加 
        if (obj == TopToolWindowBtnList[0])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.ADDITEM));
        }
        //空间添置 
        if (obj == TopToolWindowBtnList[1])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.ADDHOME));
        }
        //帮助
        //帮助信息 
        if (obj == TopToolHelpBtnList[0])
        {
            dispatchEvent(new TopToolPanelEvent(TopToolPanelEvent.HELP));
        }
    }
    protected override void OnDown(GameObject obj)
    {
        TopToolFlag = true;
        SetButtonColor(obj);
    }
    protected override void OnUp(GameObject obj)
    {
        TopToolFlag = false;
        IntButtonColor(obj);
    }

    public void CloseAllPanelHandle()
    {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            TopToolBtnList[i].SetActive(false);
        }
    }
    protected override void OnEnter(GameObject obj)
    {
        SetButtonBgColor(obj);
        print(obj);
    }
    protected override void OnExit(GameObject obj)
    {
        IntButtonBgColor(obj);
        print(obj);
    }

    public  void OpenFilePanel() {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            if (i == 0)
            {
                TopToolBtnList[i].SetActive(true);
            }
            else
            {
                TopToolBtnList[i].SetActive(false);
            }
        }
    }

    public void OpenEditPanel()
    {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            if (i == 1)
            {
                TopToolBtnList[i].SetActive(true);
            }
            else
            {
                TopToolBtnList[i].SetActive(false);
            }
        }
    }

    public void OpenFilterPanel()
    {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            if (i == 2)
            {
                TopToolBtnList[i].SetActive(true);
            }
            else
            {
                TopToolBtnList[i].SetActive(false);
            }
        }
    }

    public void OpenWindowPanel()
    {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            if (i == 3)
            {
                TopToolBtnList[i].SetActive(true);
            }
            else
            {
                TopToolBtnList[i].SetActive(false);
            }
        }
    }

    public void OpenHelpPanel()
    {
        for (int i = 0; i < TopToolBtnList.Count; i++)
        {
            if (i == 4)
            {
                TopToolBtnList[i].SetActive(true);
            }
            else
            {
                TopToolBtnList[i].SetActive(false);
            }
        }
    }

    public void SetButtonBgColor(GameObject e)
    {
        e.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    public void IntButtonBgColor(GameObject e)
    {
        e.GetComponent<Image>().color = new Color(0.235f, 0.247f, 0.286f, 1);
    }

    public void SetButtonColor(GameObject e)
    {
        e.transform.Find("Text").GetComponent<Text>().color = new Color(0.952F, 0.706F, 0.2902F, 1);
    }

    public void IntButtonColor(GameObject e)
    {
        e.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }

    void Update()
    {
    }
}
