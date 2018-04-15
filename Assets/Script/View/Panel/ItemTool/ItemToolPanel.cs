using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;

public class ItemToolPanel : BasePanel
{
    //定义物品和特效的主按钮
    private GameObject _itemObjectBtn;
    private GameObject _itemEffectBtn;
    //定义content内容用来置放所有可见的img
    private GameObject _itemContent;
    //定义 BtnList内容来置放所有可见的Btn
    private GameObject _itemBtnList;
    //定义xml配置列表
    private List<ItemData> _itemList;
    //定义ID分类以及类别个数总数
    private List<string> _itemNumListAll = new List<string>();
    private List<string> _itemBtnNameList = new List<string>();
    private int _itemNumListOneCout;
    private int _itemNumListTwoCout;
    //定义所有的类别Panel容器;
    private List<List<GameObject>> _itemPanelList = new List<List<GameObject>>();
    //定义所有的类别Btn容器;
    private List<List<GameObject>> _itemBtnsList = new List<List<GameObject>>();
    //定义所有的类别Btn容器;
    private int mainNum = 1;
    private int secNum = 0;
    private int styleNum = 0;
    //欧式中式等风格按钮
    private GameObject _sytleOneBtn;
    private GameObject _sytleTwoBtn;
    private GameObject _sytleThreeBtn;
    private GameObject _sytleFourBtn;
    private List<GameObject> showImgList = new List<GameObject>();
    private List<GameObject> styleBtnList = new List<GameObject>();

    private void Start()
    {
        //初始化两个大分类按钮
        _itemObjectBtn = GetUI("ItemObjectBtn");
        _itemEffectBtn = GetUI("ItemEffectBtn");
        _sytleOneBtn = GetUI("styleOne");
        _sytleTwoBtn = GetUI("styleTwo");
        _sytleThreeBtn = GetUI("styleThree");
        _sytleFourBtn = GetUI("styleFour");
        styleBtnList.Add(_sytleOneBtn);
        styleBtnList.Add(_sytleTwoBtn);
        styleBtnList.Add(_sytleThreeBtn);
        styleBtnList.Add(_sytleFourBtn);
        for (int styleBtnListNum = 0; styleBtnListNum < styleBtnList.Count; styleBtnListNum++)
        {
            AddEventClick(styleBtnList[styleBtnListNum]);
            AddEventOver(styleBtnList[styleBtnListNum]);
            AddEventExit(styleBtnList[styleBtnListNum]);
        }
        AddEventClick(_itemObjectBtn);
        AddEventClick(_itemEffectBtn);
        AddEventDown(_itemObjectBtn);
        AddEventUp(_itemEffectBtn);
        AddEventExit(_itemObjectBtn);
        AddEventOver(_itemEffectBtn);
        AddEventExit(_itemEffectBtn);
        AddEventOver(_itemObjectBtn);
        //初始化分类列表
        XmlDocument idXml = new XmlDocument();
        idXml.LoadXml(Resources.Load("Config/Item/ItemList").ToString());
        _itemNumListOneCout = idXml.GetElementsByTagName("ItemList")[0].ChildNodes.Count;
        _itemNumListTwoCout = idXml.GetElementsByTagName("ItemList")[1].ChildNodes.Count;
        XmlNodeList idChild = idXml.SelectSingleNode("ItemCode").ChildNodes;
        foreach (XmlElement a in idChild)
        {
            foreach (XmlElement b in a.ChildNodes)
            {
                _itemBtnNameList.Add(b.Attributes["tip"].Value);
                _itemNumListAll.Add(b.Attributes["id"].Value);
            }
        }
        //定义按钮容器
        _itemBtnList = GetUI("ItemBtnList");
        //实例化按钮
        for (int _itemBtnNameListNum = 0; _itemBtnNameListNum < _itemBtnNameList.Count; _itemBtnNameListNum++)
        {
            GameObject _itemBtn = Instantiate(Resources.Load("UI/ItemTool/itemTypeBtn")) as GameObject;
            _itemBtn.transform.Find("Text").GetComponent<Text>().text = _itemBtnNameList[_itemBtnNameListNum];
            _itemBtn.transform.parent = _itemBtnList.transform;
            _itemBtn.name = "Btn" + _itemBtnNameListNum;
            AddEventClick(_itemBtn);
            AddEventExit(_itemBtn);
            AddEventOver(_itemBtn);
        }
        //实例item容器
        _itemContent = GetUI("ItemImgList").transform.Find("Viewport").transform.Find("Content").transform.gameObject;
        //获取Item配置列表
        _itemList = ItemManager.ItemDataList;
        //实例化每个类型的容器
        for (int _itemNumListAllNum = 0; _itemNumListAllNum < _itemNumListAll.Count; _itemNumListAllNum++)
        {
            List<GameObject> itemPanel = new List<GameObject>();
            _itemPanelList.Add(itemPanel);
        }
        //实例化所有点击图片
        for (int _itemListNum = 0; _itemListNum < _itemList.Count; _itemListNum++)
        {
            if (_itemList[_itemListNum].id.Substring(0, 1) == "1" || _itemList[_itemListNum].id.Substring(0, 1) == "2")
            {
                GameObject _itemImg = Instantiate(Resources.Load("UI/ItemTool/ItemImg")) as GameObject;
                for (int _itemPanelListNum = 0; _itemPanelListNum < _itemPanelList.Count; _itemPanelListNum++)
                {
                        if (_itemList[_itemListNum].id.Substring(0, 4) == _itemNumListAll[_itemPanelListNum])
                        {
                            _itemPanelList[_itemPanelListNum].Add(_itemImg);
                        }
                        _itemImg.transform.parent = _itemContent.transform;
                        _itemImg.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                        _itemImg.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                        _itemImg.GetComponent<Image>().overrideSprite = Resources.Load(_itemList[_itemListNum].thumbnail, typeof(Sprite)) as Sprite;
                        _itemImg.name = _itemList[_itemListNum].id + ":" + _itemList[_itemListNum].classify;
                }
                _itemImg.GetComponent<Button>().onClick.AddListener(delegate () {
                    this.OnClickHandle(_itemImg);
                });
            }

        }
        Open();
    }

    //处理ID分类一级各类别个数

    public override void Open()
    {
        base.Open();
        init();
    }
    protected override void OnEnter(GameObject obj)
    {
       
        if (obj == _itemObjectBtn)
        {
            _itemObjectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
        }
        if (obj == _itemEffectBtn)
        {
            _itemEffectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
        }
        if (obj != _itemEffectBtn && obj != _itemObjectBtn && obj != _sytleOneBtn && obj != _sytleTwoBtn && obj != _sytleThreeBtn && obj != _sytleFourBtn)
        {
            obj.transform.Find("Text").GetComponent<Text>().color = new Color(0.952F, 0.706F, 0.2902F, 1);
        }
        if (obj == _sytleOneBtn || obj == _sytleTwoBtn || obj == _sytleThreeBtn || obj == _sytleFourBtn) {
            for (int styleBtnListNum = 0; styleBtnListNum < styleBtnList.Count; styleBtnListNum++)
            {
                if (styleBtnList[styleBtnListNum] == obj)
                {
                    SetButtonTextColor(styleBtnListNum);
                }
            }
        }
    }
    public void OnClickHandle(GameObject obj) {
        for (int _itemContentNum = 0; _itemContentNum < _itemContent.transform.childCount; _itemContentNum++)
        {
            if (obj == _itemContent.transform.GetChild(_itemContentNum).gameObject)
            {
                if (_itemList[_itemContentNum].type == "ItemObjectGroup")
                {
                    dispatchEvent(new ItemToolPanelEvent(ItemToolPanelEvent.Create_Combination, _itemList[_itemContentNum].id));
                }
                else
                {
                    dispatchEvent(new ItemToolPanelEvent(ItemToolPanelEvent.Create_Item, _itemList[_itemContentNum].id));
                }
            }
        }
    }
    protected override void OnExit(GameObject obj)
    {
        if (obj == _itemObjectBtn)
        {
            if (mainNum == 1)
            {
                _itemObjectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
                _itemEffectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
            }
            else if (mainNum == 2)
            {
                _itemObjectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
                _itemEffectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
            }
        }
        if (obj == _itemEffectBtn)
        {
            if (mainNum == 1)
            {
                _itemObjectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
                _itemEffectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
            }
            else if (mainNum == 2)
            {
                _itemObjectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
                _itemEffectBtn.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
            }
        }
        if (obj != _itemEffectBtn && obj != _itemObjectBtn && obj != _sytleOneBtn && obj != _sytleTwoBtn && obj != _sytleThreeBtn && obj != _sytleFourBtn)
        {
            BtnShowHandle(secNum);
        }
        if (obj == _sytleOneBtn || obj == _sytleTwoBtn || obj == _sytleThreeBtn || obj == _sytleFourBtn)
        {
            SetButtonTextColor(styleNum);
        }
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj == _sytleOneBtn)
        {
            styleNum = 0;
            SetButtonTextColor(0);
            choseStyleHandle(0);
        }
        if (obj == _sytleTwoBtn)
        {
            styleNum = 1;
            SetButtonTextColor(1);
            choseStyleHandle(1);
        }
        if (obj == _sytleThreeBtn)
        {
            styleNum = 2;
            SetButtonTextColor(2);
            choseStyleHandle(2);
        }
        if (obj == _sytleFourBtn)
        {
            styleNum = 3;
            SetButtonTextColor(3);
            choseStyleHandle(3);
        }
        if (obj == _itemObjectBtn)
        {
            for (int styleBtnListNum = 0; styleBtnListNum < styleBtnList.Count; styleBtnListNum++)
            {
                styleBtnList[styleBtnListNum].SetActive(true);
            }
            choseStyleHandle(0);
            mainNum = 1;
            secNum = 0;
            styleNum = 0;
            ShowItemObjectHandle();
            TpyeBtnShowHandle(1);
            SetButtonColor(_itemObjectBtn);
            IntButtonColor(_itemEffectBtn);
            SetButtonTextColor(0);
        }
        if (obj == _itemEffectBtn)
        {
            for (int styleBtnListNum = 0; styleBtnListNum < styleBtnList.Count; styleBtnListNum++)
            {
                styleBtnList[styleBtnListNum].SetActive(false);
            }
            mainNum = 2;
            secNum = 9;
            styleNum = 0;
            ShowItemEffectHandle();
            TpyeBtnShowHandle(2);
            SetButtonColor(_itemEffectBtn);
            IntButtonColor(_itemObjectBtn);
        }
        for (int i = 0; i < _itemBtnList.transform.childCount; i++)
        {
            if(obj == _itemBtnList.transform.GetChild(i).gameObject)
            {
                styleNum = 0;
                secNum = i;
                PanelShowHandle(i);
                BtnShowHandle(i);
                SetButtonTextColor(0);
            }
        }
        

    }
    private void init()
    {
        ShowItemObjectHandle();
        TpyeBtnShowHandle(1);
        IntButtonColor(_itemEffectBtn);
        SetButtonColor(_itemObjectBtn);
    }

    private void ShowItemObjectHandle()
    {
        BtnShowHandle(0);
        PanelShowHandle(0);
    }

    private void ShowItemEffectHandle()
    {
        BtnShowHandle(_itemNumListOneCout);
        PanelShowHandle(_itemNumListOneCout);
    }
    //设置出现的样式风格
    private void choseStyleHandle(int a) {
        for (int showImgListNum = 0; showImgListNum < showImgList.Count; showImgListNum++)
        {
            string itemStlyle = showImgList[showImgListNum].name.Split(':')[1];
            if (a == 0) {
                showImgList[showImgListNum].SetActive(true);
            }
            else {
                if (itemStlyle == a.ToString())
                {
                    showImgList[showImgListNum].SetActive(true);
                }
                else
                {
                    showImgList[showImgListNum].SetActive(false);
                }
            }
        }
        
    }
    //设置出现的item列表
    private void PanelShowHandle(int a)
    {
        showImgList.Clear();
        for (int _itemPanelListNum = 0; _itemPanelListNum < _itemPanelList.Count; _itemPanelListNum++)
        {
            if (_itemPanelListNum == a)
            {
                for (int i = 0; i < _itemPanelList[_itemPanelListNum].Count; i++)
                {
                    _itemPanelList[_itemPanelListNum][i].SetActive(true);
                    showImgList.Add(_itemPanelList[_itemPanelListNum][i]);
                }
            }
            else
            {
                for (int i = 0; i < _itemPanelList[_itemPanelListNum].Count; i++)
                {
                    _itemPanelList[_itemPanelListNum][i].SetActive(false);
                }
            }
        }
    }

    //设置点击的按钮状态
    private void BtnShowHandle(int a)
    {
        for (int _itemBtnListNum = 0; _itemBtnListNum < _itemBtnList.transform.childCount; _itemBtnListNum++)
        {
            if (_itemBtnListNum == a)
            {
                _itemBtnList.transform.GetChild(_itemBtnListNum).Find("Text").GetComponent<Text>().color = new Color(0.952F, 0.706F, 0.2902F, 1);
            }
            else
            {
                _itemBtnList.transform.GetChild(_itemBtnListNum).Find("Text").GetComponent<Text>().color = Color.white;
            }
        }
    }

    private void TpyeBtnShowHandle(int a)
    {
        for (int _itemBtnListNum = 0; _itemBtnListNum < _itemBtnList.transform.childCount; _itemBtnListNum++)
        {
            if (_itemBtnListNum < _itemNumListOneCout)
            {
                if (a == 1)
                {
                    _itemBtnList.transform.GetChild(_itemBtnListNum).gameObject.SetActive(true);
                }
                if (a == 2)
                {
                    _itemBtnList.transform.GetChild(_itemBtnListNum).gameObject.SetActive(false);
                }
            }
            else
            {
                if (a == 1)
                {
                    _itemBtnList.transform.GetChild(_itemBtnListNum).gameObject.SetActive(false);
                }
                if (a == 2)
                {
                    _itemBtnList.transform.GetChild(_itemBtnListNum).gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetButtonColor(GameObject e)
    {
        e.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
    }

    public void IntButtonColor(GameObject e)
    {
        e.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
    }
    private void SetButtonTextColor(int a)
    {
        for (int styleBtnListNum = 0; styleBtnListNum < styleBtnList.Count; styleBtnListNum++)
        {
            if (styleBtnListNum == a || styleBtnListNum == styleNum)
            {
                styleBtnList[styleBtnListNum].GetComponent<Text>().color = new Color(255f / 255F, 185f / 255F, 2f / 255F, 1);
            }
            else
            {
                styleBtnList[styleBtnListNum].GetComponent<Text>().color = new Color(255f / 255F, 255f / 255F, 255f / 255F, 1);
            }
        }
    }

    void Update()
    {
    }
}
