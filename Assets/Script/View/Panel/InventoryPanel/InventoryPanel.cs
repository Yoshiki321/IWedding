using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : BasePanel
{
    private GameObject _content;

    void Awake()
    {
        _content = GetUI("View/ScrollImg/Image/Content");
    }

    public void UpdateItem(List<ItemStruct> lists)
    {
        foreach (Transform t in _content.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (ItemStruct item in lists)
        {
            CreateTextName(item.vo);
        }
    }

    public void CreateTextName(ItemVO vo)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("UI/InventoryPanel/InventoryColumn",
              typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
        obj.name = name;
        obj.transform.parent = _content.transform;
        obj.layer = gameObject.layer;
        InventoryColumn column = obj.GetComponent<InventoryColumn>();
        column.Name.text = vo.assetId;
        column.Count.text = "1";
        column.Price.text = "1";
    }

    void Update()
    {

    }
}
