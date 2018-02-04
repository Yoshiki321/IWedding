using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToolPanel : BasePanel
{
    private GameObject thisTimeText;
    public GameObject thisNameText;
    private GameObject FileBtn;
    private GameObject EditBtn;
    private GameObject FilterBtn;
    private GameObject WindowBtn;
    private GameObject HelpBtn;
    private List<GameObject> TopBtnList = new List<GameObject>();

    public static SwitchToolPanel Instance
    {
        private set;
        get;
    }

    void OnDestroy()
    {
        if (Instance != null)
        {
            Instance = null;
        }
    }

    private void Awake()
	{
        Instance = this;
        thisTimeText = GetUI("ThisTime");
        thisNameText = GetUI("ThisName");
        thisTimeText.GetComponent<Text>().text = System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day.ToString();
    }

    private void Start()
    {
        FileBtn = GameObject.Find("TopImg").transform.Find("FileBtn").gameObject;
        EditBtn = GameObject.Find("TopImg").transform.Find("EditBtn").gameObject;
        FilterBtn = GameObject.Find("TopImg").transform.Find("FilterBtn").gameObject;
        WindowBtn = GameObject.Find("TopImg").transform.Find("WindowBtn").gameObject;
        HelpBtn = GameObject.Find("TopImg").transform.Find("HelpBtn").gameObject;

        TopBtnList.Add(FileBtn);
        TopBtnList.Add(EditBtn);
        TopBtnList.Add(FilterBtn);
        TopBtnList.Add(WindowBtn);
        TopBtnList.Add(HelpBtn);

        for (int i = 0; i < TopBtnList.Count; i++)
        {
            AddEventClick(TopBtnList[i]);
            AddEventDown(TopBtnList[i]);
            AddEventUp(TopBtnList[i]);
            AddEventExit(TopBtnList[i]);
            AddEventOver(TopBtnList[i]);
        }
    }
    void OnMouseDown()
    {
       
    }

    protected override void OnEnter(GameObject obj)
    {
        SetButtonBgColor(obj);
    }
    protected override void OnExit(GameObject obj)
    {
        IntButtonBgColor(obj);
    }

    protected override void OnClick(GameObject obj)
    {

        if (obj.name == "FileBtn")
        {
            dispatchEvent(new SwitchToolPanelEvent(SwitchToolPanelEvent.FILE));
        }
        if (obj.name == "EditBtn")
        {
            dispatchEvent(new SwitchToolPanelEvent(SwitchToolPanelEvent.EDIT));
        }
        if (obj.name == "FilterBtn")
        {
            dispatchEvent(new SwitchToolPanelEvent(SwitchToolPanelEvent.FILTER));
        }
        if (obj.name == "WindowBtn")
        {
            dispatchEvent(new SwitchToolPanelEvent(SwitchToolPanelEvent.WINDOW));
        }
        if (obj.name == "HelpBtn")
        {
            dispatchEvent(new SwitchToolPanelEvent(SwitchToolPanelEvent.HELP));
        }
    }

    protected override void OnDown(GameObject obj)
    {
        SetButtonColor(obj);
    }

    protected override void OnUp(GameObject obj)
    {
        IntButtonColor(obj);
    }

    public void SetButtonColor(GameObject e)
    {
        e.GetComponent<Text>().color = new Color(0.952F, 0.706F, 0.2902F, 1);
    }

    public void IntButtonColor(GameObject e)
    {
        e.GetComponent<Text>().color = Color.white;
    }

    void Update()
	{
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!TopToolPanel.Instance.TopToolFlag) {
                dispatchEvent(new SwitchToolPanelEvent(SwitchToolPanelEvent.CLOSE));
                TopToolPanel.Instance.TopToolFlag = false;
            }
        }
    }

    public void WirtePorjectName(string a)
    {
        thisNameText.GetComponent<Text>().text = a;
    }
    public void SetButtonBgColor(GameObject e)
    {
        e.GetComponent<Text>().color = new Color(0.3059f, 0.647f, 1f, 1);
    }

    public void IntButtonBgColor(GameObject e)
    {
        e.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1);
    }
}
