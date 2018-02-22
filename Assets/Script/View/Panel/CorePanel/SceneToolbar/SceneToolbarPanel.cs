using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneToolbarPanel : BasePanel
{
	public GameObject tooLineAddItemBtn;
    public GameObject tooLineAddHomeBtn;

	//private GameObject toolLineUndoBtn;
	//private GameObject toolLineRedoBtn;
    //private GameObject toolLineCopyBtn;
    //private GameObject toolLinePasteBtn;
    //private GameObject toolLineDeleteBtn;
    private GameObject toolLineBrushBtn;

    private GameObject toolLineChangeViewBtn;

    private GameObject toolLineAlignBtn;

    private GameObject toolLinePhotoBtn;
    private GameObject toolLineFilterBtn;
    private GameObject toolLineRenderBtn;
    private GameObject toolLineSaveBtn;
    private GameObject toolLineLoadBtn;
    private GameObject toolLineLightBtn;
    private GameObject toolLineVRBtn;
    private GameObject toolLineModelBtn;
    private GameObject viewChangeContent;

    private bool lightFlag = true;
    private bool viewFlag = false;

    private int LeftSelectedNum = 1;
    private List<GameObject> toolBtnList = new List<GameObject>();
    private List<GameObject> viewChangeList = new List<GameObject>();

    private void Awake()
    {
		tooLineAddItemBtn = transform.Find("ToolLineBg").Find("ToolLineAddItem").gameObject;
		tooLineAddHomeBtn = transform.Find("ToolLineBg").Find("ToolLineAddHome").gameObject;
        viewChangeContent = transform.Find("ToolLineBg").Find("ToolLineVRBtn").Find("SelectedContent").gameObject;
        //toolLineUndoBtn = transform.Find("ToolLineBg").Find("ToolLineUndoBtn").gameObject;
        //toolLineRedoBtn = transform.Find("ToolLineBg").Find("ToolLineRedoBtn").gameObject;
        //toolLineCopyBtn = transform.Find("ToolLineBg").Find("ToolLineCopyBtn").gameObject;
        //toolLinePasteBtn = transform.Find("ToolLineBg").Find("ToolLinePasteBtn").gameObject;
        //toolLineDeleteBtn = transform.Find("ToolLineBg").Find("ToolLineDeleteBtn").gameObject;
        toolLineBrushBtn = transform.Find("ToolLineBg").Find("ToolLineBrushBtn").gameObject;
        toolLineVRBtn = transform.Find("ToolLineBg").Find("ToolLineVRBtn").gameObject;

        toolLinePhotoBtn = transform.Find("ToolLineBg").Find("ToolLinePhotoBtn").gameObject;
        toolLineChangeViewBtn = transform.Find("ToolLineBg").Find("ToolLineChangeView").gameObject;

        toolLineAlignBtn = transform.Find("ToolLineBg").Find("ToolLineAlignBtn").gameObject;

        toolLineFilterBtn = transform.Find("ToolLineBg").Find("ToolLineFilterBtn").gameObject;
        toolLineRenderBtn = transform.Find("ToolLineBg").Find("ToolLineRenderBtn").gameObject;
        toolLineSaveBtn = transform.Find("ToolLineBg").Find("ToolLineSaveBtn").gameObject;
        toolLineLoadBtn = transform.Find("ToolLineBg").Find("ToolLineLoadBtn").gameObject;
        toolLineLightBtn = transform.Find("ToolLineBg").Find("ToolLineLightBtn").gameObject;
        toolLineModelBtn = transform.Find("ToolLineBg").Find("ToolLineModelBtn").gameObject;
        for (int viewNum = 0; viewNum < viewChangeContent.transform.childCount; viewNum++)
        {
            viewChangeList.Add(viewChangeContent.transform.GetChild(viewNum).gameObject);
            AddEventClick(viewChangeContent.transform.GetChild(viewNum).gameObject);
            AddEventExit(viewChangeContent.transform.GetChild(viewNum).gameObject);
            AddEventOver(viewChangeContent.transform.GetChild(viewNum).gameObject);
        }
        toolBtnList.Add(tooLineAddItemBtn);
		toolBtnList.Add(tooLineAddHomeBtn);

        //toolBtnList.Add(toolLineUndoBtn);
        //toolBtnList.Add(toolLineRedoBtn);
        //toolBtnList.Add(toolLineCopyBtn);
        //toolBtnList.Add(toolLinePasteBtn);
        //toolBtnList.Add(toolLineDeleteBtn);
        toolBtnList.Add(toolLineBrushBtn);

        toolBtnList.Add(toolLinePhotoBtn);
        toolBtnList.Add(toolLineChangeViewBtn);
        toolBtnList.Add(toolLineFilterBtn);
        toolBtnList.Add(toolLineRenderBtn);
        toolBtnList.Add(toolLineSaveBtn);
        toolBtnList.Add(toolLineLoadBtn);
        toolBtnList.Add(toolLineLightBtn);
        toolBtnList.Add(toolLineModelBtn);
        
        toolBtnList.Add(toolLineAlignBtn);

        toolBtnList.Add(toolLineVRBtn);

		for (int i = 0; i < toolBtnList.Count; i++) {
			AddEventClick (toolBtnList [i]);
            AddEventDown(toolBtnList[i]);
            AddEventUp(toolBtnList[i]);
            AddEventExit(toolBtnList[i]);
            AddEventOver(toolBtnList[i]);
        }
        viewFlag = true;

        Open();
    }

    public override void Open()
    {
        base.Open();
        init();
        SelectItemBtn();
    }

    protected override void OnEnter(GameObject obj)
    {
        if (obj != tooLineAddItemBtn && obj != tooLineAddHomeBtn && obj != viewChangeList[0] && obj != viewChangeList[1] && obj != viewChangeList[2])
        {
            obj.transform.Find("bgImg").GetComponent<Image>().color = new Color(32f / 255F, 32f / 255F, 32f / 255F, 1);
        }
        else if (obj == viewChangeList[0])
        {
            viewOpenFlag = false;

            obj.transform.GetComponent<Image>().color = new Color(135F / 255F, 135F / 255F, 135F / 255F, 1);
        }
        else if (obj == viewChangeList[1])
        {
            viewOpenFlag = false;

            obj.transform.GetComponent<Image>().color = new Color(135F / 255F, 135F / 255F, 135F / 255F, 1);
        }
        else if (obj == viewChangeList[2])
        {
            viewOpenFlag = false;

            obj.transform.GetComponent<Image>().color = new Color(135F / 255F, 135F / 255F, 135F / 255F, 1);
        }
        else
        {
            obj.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 1);
        }
    }

    protected override void OnExit(GameObject obj)
    {
        if (obj != tooLineAddItemBtn && obj != tooLineAddHomeBtn && obj != viewChangeList[0] && obj != viewChangeList[1] && obj != viewChangeList[2])
        {
            obj.transform.Find("bgImg").GetComponent<Image>().color = new Color(32f / 255F, 32f / 255F, 32f / 255F, 0);
        }
        else if (obj == viewChangeList[0])
        {
            viewOpenFlag = true;

            obj.transform.GetComponent<Image>().color = new Color(135F / 255F, 135F / 255F, 135F / 255F, 0);
        }
        else if (obj == viewChangeList[1])
        {
            viewOpenFlag = true;

            obj.transform.GetComponent<Image>().color = new Color(135F / 255F, 135F / 255F, 135F / 255F, 0);
        }
        else if (obj == viewChangeList[2])
        {
            viewOpenFlag = true;

            obj.transform.GetComponent<Image>().color = new Color(135F / 255F, 135F / 255F, 135F / 255F, 0);
        }
        else {
            if (LeftSelectedNum == 1) {
                tooLineAddItemBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 1);
                tooLineAddHomeBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 0);
            }
            else if (LeftSelectedNum == 2) {
                tooLineAddHomeBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 1);
                tooLineAddItemBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 0);
            }
        }
    }
   
    protected override void OnClick(GameObject obj)
    {
        viewChangeContent.SetActive(false);
        if (obj == tooLineAddItemBtn) {
            SelectItemBtn();
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.ADDITEM));
        }
        if (obj == tooLineAddHomeBtn) {
            SelectHouseBtn();
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.ADDHOME));
        }
        //if (obj == toolLineUndoBtn)
        //{
        //    transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.UNDO));
        //}
        //if (obj == toolLineRedoBtn)
        //{
        //    transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.REDO));
        //}
        //if (obj == toolLineCopyBtn)
        //{
        //    transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.COPY));
        //}
        //if(obj == toolLinePasteBtn)
        //{
        //    transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.PASTE));
        //}
        //if (obj == toolLineDeleteBtn)
        //{
        //    transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.DELETE));
        //}
        if (obj == toolLinePhotoBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.PHOTO));
        }
        if (obj == toolLineBrushBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.BRUSH));
            if (SceneManager.Instance.brushManager.brushMode == BrushManager.BrushMode.Place)
            {
                toolLineBrushBtn.transform.Find("Text").GetComponent<Text>().text = "开 启 笔 刷";
            }
            else
            {
                toolLineBrushBtn.transform.Find("Text").GetComponent<Text>().text = "关 闭 笔 刷";
            }
        }
        if (obj == toolLineVRBtn)
        {
            if (viewChangeContent.activeSelf)
            {
                viewChangeContent.SetActive(false);
            }
            else
            {
                viewChangeContent.SetActive(true);
            }
            //transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.VR));
            //if (CameraManager.visual == CameraFlags.Roam)
            //{
            //    toolLineBrushBtn.transform.Find("Text").GetComponent<Text>().text = "3 D 视 角";
            //    toolLineVRBtn.GetComponent<Image>().overrideSprite = Resources.Load("UI/CorePanel/ToolLine/Fly", typeof(Sprite)) as Sprite;
            //}
            //else if (CameraManager.visual == CameraFlags.Fly)
            //{
            //    toolLineBrushBtn.transform.Find("Text").GetComponent<Text>().text = "V R 视 角";
            //    toolLineVRBtn.GetComponent<Image>().overrideSprite = Resources.Load("UI/CorePanel/ToolLine/VR", typeof(Sprite)) as Sprite;
            //}
            //else if (CameraManager.visual == CameraFlags.VR)
            //{
            //    toolLineBrushBtn.transform.Find("Text").GetComponent<Text>().text = "普 通 视 角";
            //    toolLineVRBtn.GetComponent<Image>().overrideSprite = Resources.Load("UI/CorePanel/ToolLine/Roam", typeof(Sprite)) as Sprite;
            //}
        }
        if (obj == viewChangeList[0])
        {
            viewChangeContent.SetActive(false);
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.VR, 0));
        }

        if (obj == viewChangeList[1])
        {
            viewChangeContent.SetActive(false);
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.VR, 1));
        }

        if (obj == viewChangeList[2])
        {
            viewChangeContent.SetActive(false);
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.VR, 2));
        }

        if (obj == toolLineChangeViewBtn)
        {
            if (viewFlag)
            {
                toolLineChangeViewBtn.transform.Find("Text").GetComponent<Text>().text = "切 换 2 D";
                toolLineChangeViewBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = Resources.Load("UI/CorePanel/ToolLine/2d", typeof(Sprite)) as Sprite;
                transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.TO3D));
                viewFlag = false;
            }
            else {
                toolLineChangeViewBtn.transform.Find("Text").GetComponent<Text>().text = "切 换 3 D";
                toolLineChangeViewBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = Resources.Load("UI/CorePanel/ToolLine/3d", typeof(Sprite)) as Sprite;
                transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.TO2D));
                viewFlag = true;
            }
        }

        if (obj == toolLineFilterBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.FILTER));
        }

        if (obj == toolLineModelBtn)
        {
            UIManager.OpenUI(UI.LoadModelPanel);
        }

        if (obj == toolLineRenderBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.RENDER));
        }

        if (obj == toolLineSaveBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.SAVE));
        }

        if (obj == toolLineLoadBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.LOAD));
        }

        if (obj == toolLineAlignBtn)
        {
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.ALIGN));
        }

        if (obj == toolLineLightBtn)
		{
            ToggleLight();
            transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.LIGHT));
		}
		if (obj == toolLineVRBtn)
		{
			transform.parent.GetComponent<BasePanel>().dispatchEvent(new SceneToolbarEvent(SceneToolbarEvent.CAMERA));
		}
    }

    private void ToggleLight()
    {
        if (lightFlag)
        {
            lightFlag = false;
            toolLineLightBtn.transform.Find("Text").GetComponent<Text>().text = "开 启 灯 光";
        }
        else
        {
            lightFlag = true;
            toolLineLightBtn.transform.Find("Text").GetComponent<Text>().text = "关 闭 灯 光";
        }
    }

    public void SelectItemBtn() {
        LeftSelectedNum = 1;
        tooLineAddItemBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 1);
        tooLineAddHomeBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 0);
    }

    public void SelectHouseBtn()
    {
        LeftSelectedNum = 2;
        tooLineAddItemBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 0);
        tooLineAddHomeBtn.transform.Find("Image").GetComponent<Image>().color = new Color(40F / 255F, 41F / 255F, 43F / 255F, 1);
    }

    private void HideToolBtn()
    {
        for (int i = 0; i < toolBtnList.Count; i++)
        {
            toolBtnList[i].SetActive(false);
        }
    }

    private void ShowToolBtn()
    {
        for (int i = 0; i < toolBtnList.Count; i++)
        {
            toolBtnList[i].SetActive(true);
        }
    }

    private void init()
    {
    }
    private bool viewOpenFlag = false;

    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (viewOpenFlag)
            {
                if (viewChangeContent.activeSelf)
                {
                    viewChangeContent.SetActive(false);
                    viewOpenFlag = false;
                }
            }
        }

    }
}
