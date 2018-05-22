using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

public class ItemToolPanel : BasePanel
{
    //左侧按钮的所有名字实例
    private List<ItemObject> _btnNameList = new List<ItemObject>();
    //解析的XML文件
    private XmlDocument idXml;
    //物品按钮容器
    private GameObject _itemBtnList;
    //特效按钮容器
    private GameObject _effectBtnList;
    //多级列表容器
    private GameObject _btnChildList;
    //是否打开子集界面
    private bool _isChildBtnListOpen = false;
    //当前子集
    private ItemObject _thisItemObj;
    //当前子集点击按钮的GameObject
    private GameObject _thisItemGObj;
    //点击flag
    private bool clickFlag = false;
    //点击关闭子集
    private GameObject _touBg;
    //现在点击的按钮index
    private int _thisBtnIndex;
    //定义content内容用来置放所有可见的img
    private GameObject _itemContent;
    //定义excel表单配置列表
    private List<ItemData> _itemList;
    //风格样式
    private GameObject _sytleOneBtn;
    private GameObject _sytleTwoBtn;
    private GameObject _sytleThreeBtn;
    private GameObject _sytleFourBtn;
    private List<GameObject> showImgList = new List<GameObject>();
    private List<GameObject> styleBtnList = new List<GameObject>();
    //定义物品和特效的主按钮
    private GameObject _itemObjectBtn;
    private GameObject _itemEffectBtn;
    //动态加载读取到的id列表
    private string[] itemIdListContent;
    //物品的个数
    private int _itemNum;
    //style选择的index
    private int _styleIndex;
    //物品的默认id
    private int _itemInitId;
    //特效的默认id
    private int _effectInitId;

    private void Start()
    {
        //初始化两个大分类按钮
        _itemObjectBtn = GetUI("ItemObjectBtn");
        _itemEffectBtn = GetUI("ItemEffectBtn");
        AddEventFromObject(_itemObjectBtn);
        AddEventFromObject(_itemEffectBtn);
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
            AddEventFromObject(styleBtnList[styleBtnListNum]);
        }
        //实例item容器
        _itemContent = GetUI("ItemImgList").transform.Find("Viewport").transform.Find("Content").transform.gameObject;
        //获取Item配置列表
        _itemList = ItemManager.ItemDataList;

        idXml = new XmlDocument();
        idXml.LoadXml(Resources.Load("Config/Item/ItemList").ToString());
        //物品的菜单配置读取
        XmlNodeList idChild = idXml.SelectSingleNode("ItemCode").ChildNodes[0].ChildNodes;
        //特效的菜单配置读取
        XmlNodeList id2Child = idXml.SelectSingleNode("ItemCode").ChildNodes[1].ChildNodes;
        _itemNum = idChild.Count;
        DynamicErgodic(idChild);
        DynamicErgodic(id2Child);
        print(_btnNameList);
        //实例化所有物品的按钮
        _itemBtnList = GetUI("ItemBtnList");
        //实例化所有的特效按钮
        _effectBtnList = GetUI("EffectBtnList");

        for (int i = 0; i < _btnNameList.Count; i++)
        {
           
            GameObject _itemBtn = Instantiate(Resources.Load("UI/ItemTool/itemTypeBtn")) as GameObject;
            _itemBtn.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = (_btnNameList[i].tip.ToString());
            if (i < _itemNum)
            {
                _itemBtn.transform.parent = _itemBtnList.transform;

            }
            else
            {
                _itemBtn.transform.parent = _effectBtnList.transform;
            }
            _itemBtn.name = "Btn" + _btnNameList[i] + i;
            AddEventFromObject(_itemBtn);
        }
        _touBg = GetUI("TouBg");
        _touBg.SetActive(false);
        AddEventFromObject(_touBg);
        //所有的按钮
        SwitchMainMenu(1);
        SetButtonTextColor(styleBtnList[0]);
        InstaItemImg("0");
        Open();
    }

    //动态无限遍历
    private void DynamicErgodic(XmlNodeList list,int a = 0, ItemObject io = null)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ItemObject obj = new ItemObject();
            obj.tip = list[i].Attributes["tip"].Value;
            if (list[i].Attributes["id"] != null)
            {
                obj.id = list[i].Attributes["id"].Value;
            }
            if (a == 0)
            {
                _btnNameList.Add(obj);
            }
            else
            {
                io.childList.Add(obj);
            }
            if (list[i].SelectSingleNode("Item") != null) {
                XmlNodeList nlist = list[i].ChildNodes;
                if (nlist.Count != 0) DynamicErgodic(nlist, 1, obj);
            }
        }
    }
    //物体绑定鼠标进入、移除、以及点击事件
    private void AddEventFromObject(GameObject gobj)
    {
        AddEventClick(gobj);
        AddEventExit(gobj);
        AddEventOver(gobj);
    }

    public override void Open()
    {
        base.Open();
        init();
    }
    //鼠标进入的事件
    protected override void OnEnter(GameObject obj)
    {
        if (obj == _sytleOneBtn || obj == _sytleTwoBtn || obj == _sytleThreeBtn || obj == _sytleFourBtn)
        {
            SetButtonTextColor(obj);
        }
        else if (obj == _itemObjectBtn || obj == _itemEffectBtn || obj == _sytleFourBtn || obj == _touBg) {

        }
        else
        {
            SetButtonTextColor(obj);
        }
    }
    //鼠标移除的事件
    protected override void OnExit(GameObject obj)
    {
        if (obj == _sytleOneBtn || obj == _sytleTwoBtn || obj == _sytleThreeBtn || obj == _sytleFourBtn)
        {
            for (int i = 0; i < styleBtnList.Count; i++)
            {
                if (i != _styleIndex)
                {
                    IntButtonTextColor(styleBtnList[i]);
                }
            }
        }
        else if (obj == _itemObjectBtn || obj == _itemEffectBtn || obj == _touBg)
        {

        }
        else if (obj.transform.parent == _itemBtnList.transform)
        {
            for (int i = 0; i < _itemBtnList.transform.childCount; i++)
            {
                if (i != _thisBtnIndex)
                {
                    IntButtonTextColor(_itemBtnList.transform.GetChild(i).gameObject);
                }
                else
                {
                    SetButtonTextColor(_itemBtnList.transform.GetChild(i).gameObject);
                }
            }
        }
        else if (obj.transform.parent == _effectBtnList.transform)
        {
            for (int i = 0; i < _effectBtnList.transform.childCount; i++)
            {
                if (i != _thisBtnIndex - _itemNum)
                {
                    IntButtonTextColor(_effectBtnList.transform.GetChild(i).gameObject);
                }
                else
                {
                    SetButtonTextColor(_effectBtnList.transform.GetChild(i).gameObject);
                }
            }
        }
        else
        {
            IntButtonTextColor(obj);
        }
    }
    //鼠标按下的事件
    protected override void OnDown(GameObject obj)
    {
    }
    //鼠标抬起的事件
    protected override void OnUp(GameObject obj)
    {
    }
    //鼠标点击的事件
    protected override void OnClick(GameObject obj)
    {
        //点击透明背景取消子集
        if (obj == _touBg)
        {
            if (_btnChildList) Destroy(_btnChildList);
            _isChildBtnListOpen = false;
            _touBg.SetActive(false);
        }
        //同级切换选择
        if (obj.transform.parent == _itemBtnList.transform && obj != _thisItemGObj)
        {
            _isChildBtnListOpen = false;
        }
        if (obj.transform.parent == _effectBtnList.transform && obj != _thisItemGObj)
        {
            _isChildBtnListOpen = false;
        }
        //子集点击判断
        if (_isChildBtnListOpen)
        {
            ItemClick(obj, _thisItemObj);
            return;
        }
        //实例化按钮点击判断
        for (int i = 0; i < _btnNameList.Count; i++)
        {
            if (!_isChildBtnListOpen)
            {
                if (obj.name == "Btn" + _btnNameList[i] + i)
                {
                    _thisBtnIndex = i;
                    _thisItemGObj = obj;
                    _touBg.SetActive(true);
                    _thisItemObj = _btnNameList[i];
                    if (_btnChildList) Destroy(_btnChildList);
                    _btnChildList = Instantiate(Resources.Load("UI/ItemTool/BtnChildList")) as GameObject;
                    _isChildBtnListOpen = true;
                    _btnChildList.transform.parent = _itemBtnList.transform.parent.transform;
                    if (i < _itemNum)
                    {
                        _btnChildList.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10F, -197F - 30 * i);
                    }
                    else
                    {
                        _btnChildList.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10F, -197F - 30 * (i- _itemNum));
                    }
                    for (int ii = 0; ii < ((_btnNameList[i]).childList).Count; ii++)
                    {
                        GameObject _childBtn = Instantiate(Resources.Load("UI/ItemTool/ChildBtn")) as GameObject;
                        _childBtn.GetComponent<UnityEngine.UI.Text>().text = ((ItemObject)(_btnNameList[i].childList[ii])).tip;
                        _childBtn.name = "Btn" + ii;
                        _childBtn.transform.parent = _btnChildList.transform;
                        AddEventFromObject(_childBtn);
                    }
                }
            }
        }
        if (obj.transform.parent == _itemBtnList.transform)
        {
            choseStyleHandle(0);
            for (int i = 0; i < styleBtnList.Count; i++)
            {
                if (i != _styleIndex)
                {
                    IntButtonTextColor(styleBtnList[i]);
                }
            }
            for (int i = 0; i < _itemBtnList.transform.childCount; i++)
            {
                if (i != _thisBtnIndex)
                {
                    IntButtonTextColor(_itemBtnList.transform.GetChild(i).gameObject);
                }
                else
                {
                    SetButtonTextColor(_itemBtnList.transform.GetChild(i).gameObject);
                }
            }
        }
        else if (obj.transform.parent == _effectBtnList.transform)
        {
            choseStyleHandle(0);
            for (int i = 0; i < styleBtnList.Count; i++)
            {
                if (i != _styleIndex)
                {
                    IntButtonTextColor(styleBtnList[i]);
                }
            }
            for (int i = 0; i < _effectBtnList.transform.childCount; i++)
            {
                if (i != _thisBtnIndex - _itemNum)
                {
                    IntButtonTextColor(_effectBtnList.transform.GetChild(i).gameObject);
                }
                else
                {
                    SetButtonTextColor(_effectBtnList.transform.GetChild(i).gameObject);
                }
            }
        }
        //物品主按钮点击
        if (obj == _itemObjectBtn)
        {
            InstaItemImg("0");
            SwitchMainMenu(1);
        }
        //特效主按钮点击
        if (obj == _itemEffectBtn)
        {
            InstaItemImg("1");
            SwitchMainMenu(2);
        }
        //风格点击
        if (obj == _sytleOneBtn)
        {
            choseStyleHandle(0);
        }
        if (obj == _sytleTwoBtn)
        {
            choseStyleHandle(1);
        }
        if (obj == _sytleThreeBtn)
        {
            choseStyleHandle(2);
        }
        if (obj == _sytleFourBtn)
        {
            choseStyleHandle(3);
        }
    }
    //无限遍历点击子集
    public void ItemClick(GameObject obj,ItemObject io)
    {
        for (int i = 0; i < io.childList.Count; i++)
        {
            if (obj.name == "Btn" + i)
            {
                string itemIdListString;
                
                _touBg.SetActive(true);
                for (int a = 0; a < _btnChildList.transform.childCount; a++) { Destroy(_btnChildList.transform.GetChild(a).gameObject); }
                if (((ItemObject)(io.childList)[i]).childList.Count == 0) {
                    _touBg.SetActive(false);
                    _isChildBtnListOpen = false;
                    Destroy(_btnChildList);
                    string id = ((ItemObject)(io.childList)[i]).id;
                    XmlDocument idListXml = new XmlDocument();
                    idListXml.LoadXml(Resources.Load("Config/Item/itemId").ToString());
                    XmlNodeList itemIdList = idListXml.SelectSingleNode("ItemCode").ChildNodes;
                    for (int iiln = 0; iiln < itemIdList.Count; iiln++)
                    {
                        if (itemIdList[iiln].Attributes["id"].Value == id)
                        {
                            itemIdListString = itemIdList[iiln].Attributes["list"].Value;
                            itemIdListContent = itemIdListString.Split(',');
                        }
                    }
                    //实例选中的物品图片
                    for (int b = 0; b < _itemContent.transform.childCount; b++) { Destroy(_itemContent.transform.GetChild(b).gameObject); }
                    for (int _itemListNum = 0; _itemListNum < _itemList.Count; _itemListNum++)
                    {
                        if (itemIdListContent != null)
                        {
                            for (int idIndex = 0; idIndex < itemIdListContent.Length; idIndex++)
                            {
                                if (_itemList[_itemListNum].id == itemIdListContent[idIndex])
                                {
                                    GameObject _itemImg = Instantiate(Resources.Load("UI/ItemTool/ItemImg")) as GameObject;
                                    _itemImg.transform.parent = _itemContent.transform;
                                    _itemImg.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                                    _itemImg.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                                    _itemImg.GetComponent<Image>().overrideSprite = Resources.Load(_itemList[_itemListNum].thumbnail, typeof(Sprite)) as Sprite;
                                    _itemImg.name = _itemList[_itemListNum].id + ":" + _itemList[_itemListNum].classify;
                                    _itemImg.GetComponent<Button>().onClick.AddListener(delegate () {
                                        this.OnClickHandle(_itemImg);
                                    });
                                }
                            }
                        }
                    }
                    return;
                }
                if (_btnChildList) Destroy(_btnChildList);
                _btnChildList = Instantiate(Resources.Load("UI/ItemTool/BtnChildList")) as GameObject;
                _btnChildList.transform.parent = _itemBtnList.transform.parent.transform;
                if (_thisBtnIndex < _itemNum)
                {
                    _btnChildList.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10F, -197F - 30 * _thisBtnIndex);
                }
                else
                {
                    _btnChildList.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(10F, -197F - 30 * (_thisBtnIndex - _itemNum));
                }
                for (int ii = 0; ii < ((ItemObject)(io.childList)[i]).childList.Count;ii++)
                {
                    GameObject _childBtn = Instantiate(Resources.Load("UI/ItemTool/ChildBtn")) as GameObject;
                    _childBtn.GetComponent<UnityEngine.UI.Text>().text = ((ItemObject)((ItemObject)io.childList[ii]).childList[ii]).tip;
                    print(_childBtn.GetComponent<UnityEngine.UI.Text>().text);
                    _childBtn.name = "Btn" + ii;
                    _childBtn.transform.parent = _btnChildList.transform;
                    AddEventFromObject(_childBtn);
                    _thisItemObj = ((ItemObject)((ItemObject)io.childList[ii]).childList[ii]);
                    if (ii == ((ItemObject)(io.childList)[i]).childList.Count - 1)
                    {
                        return;
                    }
                }
            }
            if (i == io.childList.Count - 1)
            {
                ItemClick(obj, _thisItemObj);
            }
        }
        

    }

   

    public void OnClickHandle(GameObject obj)
    {
        for (int _itemContentNum = 0; _itemContentNum < _itemContent.transform.childCount; _itemContentNum++)
        {
            if (obj == _itemContent.transform.GetChild(_itemContentNum).gameObject)
            {
                string _itemId = obj.name.Split(':')[0];
                if (_itemList[_itemContentNum].type == "ItemObjectGroup")
                {
                    dispatchEvent(new ItemToolPanelEvent(ItemToolPanelEvent.Create_Combination, _itemId));
                }
                else
                {
                    dispatchEvent(new ItemToolPanelEvent(ItemToolPanelEvent.Create_Item, _itemId));
                }
            }
        }
    }

    //切换两个大类的方法
    private void SwitchMainMenu(int a)
    {
        _styleIndex = 0;
        choseStyleHandle(0);
        styleBtnList[0].GetComponent<UnityEngine.UI.Text>().color = new Color(255f / 255F, 185f / 255F, 2f / 255F, 1);
        //两个按钮容器显示
        if (a == 1)
        {
            _thisBtnIndex = 0;
            for (int i = 0; i < _itemBtnList.transform.childCount; i++)
            {
                if (i == 0)
                {
                    SetButtonTextColor(_itemBtnList.transform.GetChild(i).gameObject);
                }
                else
                {
                    IntButtonTextColor(_itemBtnList.transform.GetChild(i).gameObject);
                }
            }
            _itemBtnList.SetActive(true);
            _effectBtnList.SetActive(false);
            IntButtonColor(_itemEffectBtn);
            SetButtonColor(_itemObjectBtn);
        }
        else
        {
            _thisBtnIndex = _itemNum;
            for (int i = 0; i < _effectBtnList.transform.childCount; i++)
            {
                if (i == 0)
                {
                    SetButtonTextColor(_effectBtnList.transform.GetChild(i).gameObject);
                }
                else
                {
                    IntButtonTextColor(_effectBtnList.transform.GetChild(i).gameObject);
                }
            }
            _itemBtnList.SetActive(false);
            _effectBtnList.SetActive(true);
            SetButtonColor(_itemEffectBtn);
            IntButtonColor(_itemObjectBtn);
        }
    }
    //图形按钮的颜色设置
    public void SetButtonColor(GameObject e)
    {
        e.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 1);
    }
    //图形按钮的颜色恢复
    public void IntButtonColor(GameObject e)
    {
        e.transform.Find("bgImg").GetComponent<Image>().color = new Color(26f / 255F, 26f / 255F, 26f / 255F, 0);
    }

    private void SetButtonTextColor(GameObject obj)
    {
        if (obj.transform.Find("Text"))
        {
            obj.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().color = new Color(255f / 255F, 185f / 255F, 2f / 255F, 1);
        }
        else
        {
            obj.GetComponent<UnityEngine.UI.Text>().color = new Color(255f / 255F, 185f / 255F, 2f / 255F, 1);
        }
    }

    private void IntButtonTextColor(GameObject obj)
    {
        {
            if (obj.transform.Find("Text"))
            {
                obj.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().color = new Color(255f / 255F, 255f / 255F, 255f / 255F, 1);
            }
            else
            {
                obj.GetComponent<UnityEngine.UI.Text>().color = new Color(255f / 255F, 255f / 255F, 255f / 255F, 1);
            }
        }
    }

    private void choseStyleHandle(int a)
    {
        _styleIndex = a;
        styleBtnList[a].GetComponent<UnityEngine.UI.Text>().color = new Color(255F / 255F, 185f / 255F, 2f / 255F, 1);
        for (int showImgListNum = 0; showImgListNum < _itemContent.transform.childCount; showImgListNum++)
        {
            string itemStlyle = _itemContent.transform.GetChild(showImgListNum).name.Split(':')[1];
            if (a == 0)
            {
                _itemContent.transform.GetChild(showImgListNum).gameObject.SetActive(true);
            }
            else
            {
                if (itemStlyle == a.ToString())
                {
                    _itemContent.transform.GetChild(showImgListNum).gameObject.SetActive(true);
                }
                else
                {
                    _itemContent.transform.GetChild(showImgListNum).gameObject.SetActive(false);
                }
            }
        }

    }
    //动态实例化
    private void InstaItemImg(string a) {
        string id = a;
        string itemIdListString;
        XmlDocument idListXml = new XmlDocument();
        idListXml.LoadXml(Resources.Load("Config/Item/itemId").ToString());
        XmlNodeList itemIdList = idListXml.SelectSingleNode("ItemCode").ChildNodes;
        for (int iiln = 0; iiln < itemIdList.Count; iiln++)
        {
            if (itemIdList[iiln].Attributes["id"].Value == id)
            {
                itemIdListString = itemIdList[iiln].Attributes["list"].Value;
                itemIdListContent = itemIdListString.Split(',');
            }
        }
        for (int b = 0; b < _itemContent.transform.childCount; b++) { Destroy(_itemContent.transform.GetChild(b).gameObject); }
        for (int _itemListNum = 0; _itemListNum < _itemList.Count; _itemListNum++)
        {
            if (itemIdListContent != null)
            {
                for (int idIndex = 0; idIndex < itemIdListContent.Length; idIndex++)
                {
                    if (_itemList[_itemListNum].id == itemIdListContent[idIndex])
                    {
                        GameObject _itemImg = Instantiate(Resources.Load("UI/ItemTool/ItemImg")) as GameObject;
                        _itemImg.transform.parent = _itemContent.transform;
                        _itemImg.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                        _itemImg.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                        _itemImg.GetComponent<Image>().overrideSprite = Resources.Load(_itemList[_itemListNum].thumbnail, typeof(Sprite)) as Sprite;
                        _itemImg.name = _itemList[_itemListNum].id + ":" + _itemList[_itemListNum].classify;
                        _itemImg.GetComponent<Button>().onClick.AddListener(delegate () {
                            this.OnClickHandle(_itemImg);
                        });
                    }
                }
            }
        }
    }
    //初始
    private void init()
    {
    }
    //逐帧
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            clickFlag = false;
            if (!clickFlag)
            {
                if (_btnChildList) Destroy(_btnChildList);
                _isChildBtnListOpen = false;
                _touBg.SetActive(false);
            }
        }
    }
}
