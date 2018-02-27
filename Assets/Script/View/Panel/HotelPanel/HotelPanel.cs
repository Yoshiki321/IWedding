using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class HotelPanel : BasePanel
{
    private GameObject returnBtn;
    private List<HotelData> _hotelList;
    private GameObject _goodsList;
    private List<GameObject> _goodsListContent;
    private List<GameObject> _showHotelsContent;
    private Dropdown _selectCondition_1;
    private Dropdown _selectCondition_2;
    private Dropdown _selectCondition_3;
    private GameObject _goodsView;
    private GameObject _enterHotelBtn;

    void Start()
    {
        returnBtn = GetUI("ReturnBtn");
        AddEventClick(returnBtn);
        //列表视图Content获取
        _goodsList = GetUI("Goodslist").transform.Find("Image").transform.Find("content").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject;
        _selectCondition_1 = GetUI("SelectView").transform.Find("province").transform.Find("Dropdown").GetComponent<Dropdown>();
        _selectCondition_2 = GetUI("SelectView").transform.Find("city").transform.Find("Dropdown").GetComponent<Dropdown>();
        _selectCondition_3 = GetUI("SelectView").transform.Find("district").transform.Find("Dropdown").GetComponent<Dropdown>();
        _goodsListContent = new List<GameObject>();
        _showHotelsContent = new List<GameObject>();
        _goodsView = GetUI("GoodView").transform.Find("Image").transform.Find("content").gameObject;
        _enterHotelBtn = GetUI("GoodView").transform.Find("Image").transform.Find("content").gameObject.transform.Find("Button").gameObject;
        _hotelList = HotelManager.HotelDataList;
        for (int _hotelListNum = 0; _hotelListNum < _hotelList.Count; _hotelListNum++)
        {
            GameObject _HotelBtn = Instantiate(Resources.Load("UI/HotelPanel/HotelBtn")) as GameObject;
            _HotelBtn.transform.Find("Img").GetComponent<Image>().overrideSprite = Resources.Load(_hotelList[_hotelListNum].url, typeof(Sprite)) as Sprite;
            _HotelBtn.transform.parent = _goodsList.transform;
            _HotelBtn.transform.Find("name").GetComponent<Text>().text = _hotelList[_hotelListNum].name;
            _HotelBtn.transform.Find("add").GetComponent<Text>().text = _hotelList[_hotelListNum].address;
            _goodsListContent.Add(_HotelBtn);
            AddEventClick(_HotelBtn);
        }
        _selectCondition_1.onValueChanged.AddListener(SelectCondition_1Handle);
        _selectCondition_2.onValueChanged.AddListener(SelectCondition_2Handle);
        _selectCondition_3.onValueChanged.AddListener(SelectCondition_3Handle);
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj == returnBtn)
        {
            dispatchEvent(new HotelPanelEvent(HotelPanelEvent.CLOSE));
        }
        if (obj == _enterHotelBtn)
        {
            dispatchEvent(new HotelPanelEvent(HotelPanelEvent.ENTERHOTEL));
        }
        for (int _goodsListContentNum = 0; _goodsListContentNum < _goodsListContent.Count; _goodsListContentNum++)
        {
            if (obj == _goodsListContent[_goodsListContentNum])
            {
                obj.GetComponent<Image>().color = new Color(251F / 255F, 165f / 255F, 11f / 255F, 1);
                _goodsView.transform.Find("Image").GetComponent<Image>().overrideSprite = Resources.Load(_hotelList[_goodsListContentNum].hotelimg, typeof(Sprite)) as Sprite;
                _goodsView.transform.Find("name").GetComponent<Text>().text = _hotelList[_goodsListContentNum].name;
                _goodsView.transform.Find("size").GetComponent<Text>().text = _hotelList[_goodsListContentNum].size;
                _goodsView.transform.Find("date").GetComponent<Text>().text = _hotelList[_goodsListContentNum].date;
                _enterHotelBtn.SetActive(true);
            }
            else {
                _goodsListContent[_goodsListContentNum].GetComponent<Image>().color = new Color(99F / 255F, 99F / 255F, 99F / 255F, 1);
            }
        }
    }

    private void SelectCondition_1Handle(int value)
    {
    }

    private void SelectCondition_2Handle(int value)
    {
     
    }

    private void SelectCondition_3Handle(int value)
    {
            switch (value)
            {
                case 0:
                    ShowAllHotelHandle();
                    break;
                case 1:
                    SelectDistrictHandle("玄武区");
                    break;
                case 2:
                    SelectDistrictHandle("鼓楼区");
                    break;
                case 3:
                    SelectDistrictHandle("秦淮区");
                    break;
                case 4:
                    SelectDistrictHandle("建邺区");
                    break;
                case 5:
                    SelectDistrictHandle("栖霞区");
                    break;
                case 6:
                    SelectDistrictHandle("雨花区");
                    break;
                case 7:
                    SelectDistrictHandle("浦口区");
                    break;
                case 8:
                    SelectDistrictHandle("江宁区");
                    break;
                case 9:
                    SelectDistrictHandle("六合区");
                    break;
                case 10:
                    SelectDistrictHandle("溧水区");
                    break;
                case 11:
                    SelectDistrictHandle("高淳区");
                    break;
        }
    }

    private void SelectDistrictHandle(string v) {
        for (int _hotelListNum = 0; _hotelListNum < _hotelList.Count; _hotelListNum++)
        {
            if (_hotelList[_hotelListNum].district == v)
            {
                _goodsListContent[_hotelListNum].SetActive(true);
            }
            else
            {
                _goodsListContent[_hotelListNum].SetActive(false);
            }
        }
    }

    private void ShowAllHotelHandle()
    {
        for (int _hotelListNum = 0; _hotelListNum < _hotelList.Count; _hotelListNum++)
        {
                _goodsListContent[_hotelListNum].SetActive(true);
        }
    }

    void Update()
    {
    }
}
