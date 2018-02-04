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

    private GameObject _hardContent;

    private void Awake()
    {
        //侧边按钮列表
        _btnList.Add(GetUI("MainTingBtn"));
        _btnList.Add(GetUI("SecTingBtn"));
        _btnList.Add(GetUI("HardBtn"));
        for (int i = 0; i < _btnList.Count; i++)
        {
            AddEventClick(_btnList[i].gameObject);
        }
        //户型类别的实例图片加入组
        _homeImgList.Add(GetUI("btnp0"));
        _homeImgList.Add(GetUI("btnp1"));
        _homeImgList.Add(GetUI("btnp3"));
        _homeImgList.Add(GetUI("btnp4"));


        for (int i = 0; i < _homeImgList.Count; i++)
        {
            AddEventClick(_homeImgList[i].gameObject);
        }

        _hardContent = GetUI("HardImgList").transform.Find("Viewport").transform.Find("Content").transform.gameObject;
        //获取Item配置列表
        _itemList = ItemManager.ItemDataList;
        for (int _itemListNum = 0; _itemListNum < _itemList.Count; _itemListNum++)
        {
            if (_itemList[_itemListNum].id.Substring(0, 4) == "3001")
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
        Open();
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj.name == "MainTingBtn")
        {
            ShowHomePanelHandle();
            changeBtnStyle(0);
            hName = "主厅";
        }
        if (obj.name == "SecTingBtn")
        {
            ShowHomePanelHandle();
            changeBtnStyle (1);
            hName = "次厅";
        }
        if (obj.name == "HardBtn")
        {
            ShowHardPanelHandle();
            changeBtnStyle(2);
            hName = "";
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
                switch(obj.name)
                {
                    case "btnp0":
                        hType = LayoutConstant.BULGE;
                        break;
                    case "btnp1":
                        hType = LayoutConstant.CONCAVE;
                        break;
                    case "btnp2":
                        hType = LayoutConstant.L;
                        break;
                    case "btnp3":
                        hType = LayoutConstant.RECT;
                        break;
                }
                dispatchEvent(new ChooseSurfacePanelEvent(ChooseSurfacePanelEvent.CREATE_SURFACE, hName, hType));
            }
        }
    }

    public override void Open()
    {
        base.Open();
        ShowHomePanelHandle();
    }

    private string hName = "主厅";
    private LayoutConstant hType;

    void ShowHomePanelHandle()
    {
        for (int i = 0; i < _homeImgList.Count; i++)
        {
            _homeImgList[i].SetActive(true);
        }
        for (int i = 0; i < _hardImgList.Count; i++)
        {
            _hardImgList[i].SetActive(false);
        }
    }

    void ShowHardPanelHandle()
    {
        for (int i = 0; i < _homeImgList.Count; i++)
        {
            _homeImgList[i].SetActive(false);
        }
        for (int i = 0; i < _hardImgList.Count; i++)
        {
            _hardImgList[i].SetActive(true);
        }
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
