using UnityEngine;
using System.Collections;
using Build3D;
using System.Collections.Generic;

public class RelationComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        _itemvo = _item.VO as ItemVO;
        if (_itemvo.GetComponentVO<RelationVO>() == null)
        {
            _itemvo.AddComponentVO<RelationVO>();
        }
    }

    private ItemVO _itemvo;
    private RelationVO _vo;
    private Vector3 _point;

    private string _itemId = "hhheeeda";
    private string _relationId;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<RelationVO>();

            itemId = _vo.itemId;
        }
        get { return _vo; }
    }

    public string itemId
    {
        set
        {
            if (value == "")
            {
                return;
            }
            if (_itemId == value)
            {
                return;
            }

            foreach (GameObject data in _vo.relationItems)
            {
                Destroy(data);
            }

            _itemId = value;

            ItemData itemData = ItemManager.GetItemData(_itemId);
            GameObject model = Instantiate(Resources.Load(itemData.model)) as GameObject;
            GameObject relationBox = gameObject.transform.Find("Relation").gameObject;
            _point = relationBox.transform.position;
            model.transform.parent = relationBox.transform;
            _vo.relationItems.Add(model);
            model.layer = gameObject.layer;
            Transform[] ts = model.GetComponentsInChildren<Transform>();
            foreach (Transform t in ts)
            {
                t.gameObject.layer = gameObject.layer;
            }
            model.transform.localPosition = Vector3.zero;
        }
    }

    public string relationId
    {
        set
        {
            _relationId = value;
        }
    }

    private void OnDestroy()
    {
        if (_vo.relationItems != null)
        {
            foreach (GameObject data in _vo.relationItems)
            {
                Destroy(data);
            }
        }
    }
}
