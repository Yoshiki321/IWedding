using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectPanel : BasePanel
{
    private GameObject _content;

    private void Awake()
    {
        _content = GetUI("View/ScrollImg/Image/Content");
    }

    public void UpdateItem(List<ObjectData> lists)
    {
        foreach (Transform t in _content.transform)
        {
            Destroy(t.gameObject);
        }

        images = new List<Image>();
        items = new List<GameObject>();

        foreach (string id in AssetsModel.Instance.GetGroupList())
        {
            List<ItemVO> list = AssetsModel.Instance.GetGroupItem(id);

            CreateGroupName(list, id);
        }

        foreach (ObjectData item in lists)
        {
            if ((item.vo as ItemVO).groupId == "")
            {
                CreateTextName(item.vo, 0);
            }
        }
    }

    public void CreateGroupName(List<ItemVO> list, string name)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("UI/ItemSelectText",
              typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
        obj.name = name;
        obj.transform.parent = _content.transform;
        obj.layer = gameObject.layer;
        nameText = obj.GetComponentInChildren<Text>();
        nameText.gameObject.layer = gameObject.layer;
        nameText.text = name;

        items.Add(obj);
        images.Add(obj.transform.Find("Image").GetComponent<Image>());
        Destroy(obj.GetComponent<List>());

        foreach (ItemVO vo in list)
        {
            CreateTextName(vo, 1);
        }
    }

    List<string> openNameList = new List<string>();
    List<Image> images = new List<Image>();
    List<GameObject> items = new List<GameObject>();

    Text nameText;
    RectTransform nameTextRect;

    public void CreateTextName(ObjectVO vo, float level)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("UI/ItemSelectText",
                typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
        obj.name = vo.id;
        obj.transform.parent = _content.transform;
        obj.layer = gameObject.layer;
        nameText = obj.GetComponentInChildren<Text>();
        nameText.gameObject.layer = gameObject.layer;
        nameText.text = vo.name;
		nameText.fontSize = 18;
        nameTextRect = nameText.gameObject.GetComponent<RectTransform>();
        nameTextRect.position = new Vector3(nameTextRect.position.x + level * 40, nameTextRect.position.y-5.5f, nameTextRect.position.z);

        items.Add(obj);
        images.Add(obj.transform.Find("Image").GetComponent<Image>());
        obj.GetComponent<Button>().onClick.AddListener(delegate () {
            this.OnClick(obj);
        });
    }

    public void UpdateTextName()
    {
        ClearSelect();
        foreach (GameObject obj in items)
        {
            foreach (Item3D item in Mouse3Manager.selectionItem)
            {
                if(item.VO.id == obj.name)
                {
                    SelectItem(obj);
                    break;
                }
            }
        }
    }

    public void ClearSelect()
    {
        foreach (Image image in images)
        {
            image.color = new Color(0,0,0,0);
        }
    }

    public void SelectItem(GameObject obj)
    {
        obj.transform.Find("Image").GetComponent<Image>().color = ColorUtils.HexToColor("474747FF");
    }

    private GameObject _lastObj;

    protected override void OnClick(GameObject obj)
    {
        ClearSelect();
        SelectItem(obj);

        //if (ArrayUtils.Has(AssetsModel.Instance.GetGroupList(), obj.name))
        //{

        //}

        if (obj == _lastObj)
        {
            dispatchEvent(new ItemSelectPanelEvent(ItemSelectPanelEvent.FOCUSON_SELECTION, obj.name));
            return;
        }

        _lastObj = obj;
        dispatchEvent(new ItemSelectPanelEvent(ItemSelectPanelEvent.SELECT_ITEM, obj.name));
        Invoke("DoubleClick", .4f);
    }

    private void DoubleClick()
    {
        _lastObj = null;
    }
}
