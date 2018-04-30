using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSurfacePanel : BasePanel
{
    private List<GameObject> _btnList = new List<GameObject>();
    private List<GameObject> _homeImgList = new List<GameObject>();
    private List<GameObject> _hardImgList = new List<GameObject>();
    //定义xml配置列表
    private List<ItemData> _itemList;
    private List<SurfaceDrawData> _surfaceList;

    private GameObject _hardContent;
    private GameObject _surfaceContent;
    private GameObject _houseBtn;
    private GameObject _hardBtn;

    private int mainNum = 0;
    private int secNum = 0;

    private void Start()
    {
        _houseBtn = GetUI("HouseBtn");
        _hardBtn = GetUI("HardBtn");
        AddEventClick(_houseBtn);
        AddEventOver(_houseBtn);
        AddEventExit(_houseBtn);
        AddEventClick(_hardBtn);
        AddEventOver(_hardBtn);
        AddEventExit(_hardBtn);
        //侧边按钮列表
        _btnList.Add(GetUI("MainTingBtn"));
        _btnList.Add(GetUI("SecTingBtn"));
        _btnList.Add(GetUI("DoorBtn"));
        for (int i = 0; i < _btnList.Count; i++)
        {
            AddEventClick(_btnList[i].gameObject);
            AddEventOver(_btnList[i].gameObject);
            AddEventExit(_btnList[i].gameObject);
        }
        //户型类别的实例图片加入组

        for (int i = 0; i < _homeImgList.Count; i++)
        {
            AddEventClick(_homeImgList[i].gameObject);
        }
        //户型配置获取
        _surfaceContent = GetUI("SurfaceImgList").transform.Find("Viewport").transform.Find("Content").transform.gameObject;
        //获取Item配置列表
        _surfaceList = SurfaceManager.SurfaceDrawDataList;
        for (int _surfaceListNum = 0; _surfaceListNum < _surfaceList.Count; _surfaceListNum++)
        {
            GameObject _itemImg = Instantiate(Resources.Load("UI/ItemTool/ItemImg")) as GameObject;
            _homeImgList.Add(_itemImg);
            _itemImg.transform.parent = _surfaceContent.transform;
            _itemImg.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
            _itemImg.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            _itemImg.GetComponent<Image>().overrideSprite = Resources.Load(_surfaceList[_surfaceListNum].thumbnail, typeof(Sprite)) as Sprite;
            _itemImg.name = _surfaceList[_surfaceListNum].id;
            AddEventClick(_itemImg);
        }

        //硬装配置获取
        _hardContent = GetUI("HardImgList").transform.Find("Viewport").transform.Find("Content").transform.gameObject;
        //获取Item配置列表
        _itemList = ItemManager.ItemDataList;
        for (int _itemListNum = 0; _itemListNum < _itemList.Count; _itemListNum++)
        {
            if (_itemList[_itemListNum].id.Substring(0, 1) == "3")
            {
                GameObject _itemImg = Instantiate(Resources.Load("UI/ItemTool/ItemImg")) as GameObject;
                _hardImgList.Add(_itemImg);
                _itemImg.transform.parent = _hardContent.transform;
                _itemImg.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                _itemImg.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                _itemImg.GetComponent<Image>().overrideSprite = Resources.Load(_itemList[_itemListNum].thumbnail, typeof(Sprite)) as Sprite;
                _itemImg.name = _itemList[_itemListNum].id;
                AddEventClick(_itemImg);
            }
        }


        ShowHomePanelHandle();
        Open();
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj.name == "MainTingBtn")
        {
            secNum = 0;
            ShowHomePanelHandle();
            changeBtnStyle(0);
            hName = "主厅";
            _btnList[0].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
            _btnList[1].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
            _btnList[2].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
        }
        if (obj.name == "SecTingBtn")
        {
            secNum = 1;
            ShowHomePanelHandle();
            changeBtnStyle (1);
            hName = "次厅";
            _btnList[0].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
            _btnList[1].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
            _btnList[2].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
        }
        if (obj.name == "DoorBtn")
        {
            secNum = 2;
            ShowHardPanelHandle();
            changeBtnStyle(2);
            hName = "";
            _btnList[0].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
            _btnList[1].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
            _btnList[2].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
        }
        if (obj == _houseBtn)
        {
            secNum = 0;
            ShowHomePanelHandle();
            changeBtnStyle(0);
            hName = "主厅";
            mainNum = 0;
            _btnList[0].SetActive(true);
            _btnList[1].SetActive(true);
            _btnList[2].SetActive(false);
            _btnList[0].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
            _btnList[1].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
            _houseBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
            _hardBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);

        }
        else if (obj == _hardBtn)
        {
            secNum = 2;
            ShowHardPanelHandle();
            changeBtnStyle(2);
            hName = "";
            mainNum = 1;
            _btnList[0].SetActive(false);
            _btnList[1].SetActive(false);
            _btnList[2].SetActive(true);
            _hardBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
            _btnList[2].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
            _houseBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
        }
        for (int _hardImgNum = 0; _hardImgNum < _hardImgList.Count; _hardImgNum++)
        {
            if (obj == _hardImgList[_hardImgNum])
            {
                dispatchEvent(new ChooseSurfacePanelEvent(ChooseSurfacePanelEvent.HARD, _hardImgList[_hardImgNum].gameObject.name.ToString()));
            }
        }
        for (int _homeBtnListNum = 0; _homeBtnListNum < _homeImgList.Count; _homeBtnListNum++)
        {
            if (obj == _homeImgList[_homeBtnListNum])
            {
                dispatchEvent(new ChooseSurfacePanelEvent(ChooseSurfacePanelEvent.CREATE_SURFACE, hName, hType, _surfaceList[_homeBtnListNum].points));
            }
        }
    }

    public override void Open()
    {
        base.Open();
    }

    protected override void OnEnter(GameObject obj)
    {
        if (obj == _houseBtn)
        {
            obj.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
        }
        if (obj == _hardBtn)
        {
            obj.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
        }
        if(obj != _houseBtn && obj != _hardBtn)
        {
            obj.transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
        }
    }

    protected override void OnExit(GameObject obj)
    {
        if (obj == _houseBtn)
        {
            obj.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
        }
        if (obj == _hardBtn)
        {
            obj.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
        }
        if (obj != _houseBtn && obj != _hardBtn)
        {
            obj.transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 255F / 255F, 255F / 255F, 1);
        }
        if (mainNum == 0)
        {
            _houseBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
        }
        if (mainNum == 1)
        {
            _hardBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
        }
        _btnList[secNum].transform.Find("Text").GetComponent<Text>().color = new Color(255F / 255F, 206F / 255F, 0 / 255F, 1);
    }


    private string hName = "主厅";
    private LayoutConstant hType = LayoutConstant.BULGE;

    void ShowHomePanelHandle()
    {
        _surfaceContent.transform.parent.transform.parent.gameObject.SetActive(true);
        _hardContent.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    void ShowHardPanelHandle()
    {
        _surfaceContent.transform.parent.transform.parent.gameObject.SetActive(false);
        _hardContent.transform.parent.transform.parent.gameObject.SetActive(true);
    }

    void changeBtnStyle(int btnNum){
		for (int i = 0; i < _btnList.Count; i++)
		{
			if (i == btnNum)
			{
                _btnList[i].GetComponent<Image>().overrideSprite = Resources.Load("UI/ItemTool/iwedding_11", typeof(Sprite)) as Sprite;
			}
			else
			{
                _btnList[i].GetComponent<Image>().overrideSprite = Resources.Load("UI/ItemTool/iwedding_10", typeof(Sprite)) as Sprite;
			}
		}
	}

    void Update()
    {
    }
}
