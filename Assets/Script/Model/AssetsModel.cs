using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Build2D;
using Build3D;
using System.Xml;
using BuildManager;
using System.IO;

public class AssetsModel : Actor<AssetsModel>
{
    public void Clear()
    {
        foreach (ItemStruct data in itemDatas)
        {
            data.item2.Dispose();
            data.item3.Dispose();
        }
        itemDatas = new List<ItemStruct>();
    }

    public ItemStruct Remove(string id)
    {
        ItemStruct objectData = GetItemData(id);

        if (objectData == null) return null;

        Remove(objectData);

        return RemoveData(id);
    }

    public void Remove(ItemStruct objectData)
    {
        if (objectData.item2 is Item2D)
        {
            Item2D item = objectData.item2 as Item2D;
            Item3D item3 = objectData.item3 as Item3D;

            item.Dispose();
            item3.Dispose();
        }
    }

    public ItemVO CreateItemVO(string id)
    {
        ItemVO itemvo = new ItemVO();
        FillObjectVO(itemvo, id);
        itemvo.AddComponentVO<TransformVO>();
        return itemvo;
    }

    public void CreateNestedVO(string id, Action<NestedVO> eFun)
    {
        NestedVO nestedvo = new NestedVO();
        FillObjectVO(nestedvo, id);
        nestedvo.AddComponentVO<TransformVO>();
        eFun(nestedvo);
    }

    public void CreateItem(XmlNodeList xml)
    {
        List<ItemVO> list = new List<ItemVO>();
        foreach (XmlNode node in xml)
        {
            ItemVO vo = new ItemVO();
            vo.Code = node;
            list.Add(vo);

            if (vo.name == "undefined")
            {
                ItemData itemData = ItemManager.GetItemData(vo.assetId);
                if (itemData != null)
                {
                    vo.name = itemData.name;
                }
            }
        }

        ProgressLoadItem(list);
    }

    #region progressLoadItem;

    public int progressItemCurrent { get; private set; } = 0;

    public int progressItemTotal { get; private set; } = 0;

    List<ItemVO> _progressLoadItem;
    public void ProgressLoadItem(List<ItemVO> list)
    {
        progressItemCurrent = 0;
        progressItemTotal = list.Count;
        _progressLoadItem = list;
    }

    public void Update()
    {
        if (_progressLoadItem != null && _progressLoadItem.Count > 0)
        {
            progressItemCurrent++;

            ItemStruct data = CreateItem(_progressLoadItem[0]);
            (data.item2 as Item2D).UpdateSize();
            _progressLoadItem.RemoveAt(0);
        }
    }

    #endregion;

    public void CreateItem(XmlNodeList xml, string combination = "")
    {
        foreach (XmlNode node in xml)
        {
            ItemVO vo = new ItemVO();
            vo.Code = node;
            vo.groupId = combination;
            vo.id = NumberUtils.GetGuid();
            CreateItem(vo);
        }
    }

    public List<AssetVO> CreateItemVO(XmlNodeList xml, string combination = "")
    {
        List<AssetVO> lists = new List<AssetVO>();
        foreach (XmlNode node in xml)
        {
            ItemVO vo = new ItemVO();
            vo.Code = node;
            vo.groupId = combination;
            vo.id = NumberUtils.GetGuid();

            if (vo.name == "undefined")
            {
                ItemData itemData = ItemManager.GetItemData(vo.assetId);
                if (itemData != null)
                {
                    vo.name = itemData.name;
                }
            }

            lists.Add(vo);
        }
        return lists;
    }

    private void FillObjectVO(ObjectVO vo, string id)
    {
        ItemData itemData = ItemManager.GetItemData(id);
        if (itemData != null)
        {
            vo.modelId = itemData.model;
            vo.topImgId = itemData.topImg;
            vo.name = itemData.name;

            foreach (ComponentVO cvo in itemData.componentVOs)
            {
                vo.AddComponentVO(cvo);
            }
        }

        vo.assetId = id;
        vo.id = NumberUtils.GetGuid();
    }

    //----------------
    //----------------
    //----------------
    //----------------
    //----------------

    public ItemStruct CreateItem(ItemVO itemvo, bool save = true)
    {
        GameObject _item = new GameObject();
        Item2D item2d = _item.AddComponent<Item2D>();
        item2d.parent = SceneManager.Instance.Graphics2D.ItemLayer.transform;
        item2d.Instantiate(itemvo);

        GameObject _item3 = new GameObject();
        _item3.transform.parent = SceneManager.Instance.Graphics3D.ItemLayer.transform;
        Item3D item3d = _item3.AddComponent<Item3D>();
        item3d.Instantiate(itemvo);

        ItemStruct data = new ItemStruct();
        data.item2 = item2d;
        data.item3 = item3d;
        data.vo = itemvo;

        if (save)
        {
            itemDatas.Add(data);
        }

        return data;
    }

    public List<ItemStruct> itemDatas { get; private set; } = new List<ItemStruct>();

    public ItemStruct GetItemData(String id)
    {
        foreach (ItemStruct objectData in itemDatas)
        {
            if (objectData.vo.id == id)
            {
                return objectData;
            }
        }
        return null;
    }


    public List<ObjectSprite> GetItemData(List<AssetVO> vos)
    {
        List<ObjectSprite> list = new List<ObjectSprite>();
        foreach (AssetVO vo in vos)
        {
            ItemStruct objectData = GetItemData(vo.id);
            if (objectData != null)
            {
                list.Add(objectData.item3);
            }
        }
        return list;
    }

    public ItemStruct RemoveData(String id)
    {
        foreach (ItemStruct objectData in itemDatas)
        {
            if (objectData.vo.id == id)
            {
                itemDatas.Remove(objectData);
                return objectData;
            }
        }
        return null;
    }

    public List<ItemVO> GetGroupItem(string id)
    {
        List<ItemVO> list = new List<ItemVO>();

        foreach (ItemStruct data in itemDatas)
        {
            if ((data.vo as ItemVO).groupId == id)
            {
                list.Add(data.vo as ItemVO);
            }
        }

        return list;
    }

    public List<string> GetGroupList()
    {
        List<string> list = new List<string>();

        foreach (ItemStruct data in itemDatas)
        {
            if ((data.vo as ItemVO).groupId != "")
            {
                if (!ArrayUtils.Has(list, (data.vo as ItemVO).groupId))
                {
                    list.Add((data.vo as ItemVO).groupId);
                }
            }
        }

        return list;
    }

    public TransformVO GetTransformVO(ObjectSprite3D item)
    {
        TransformVO vo = new TransformVO();
        TransformComponent transformComponent = item.GetComponentInChildren<TransformComponent>();
        if (transformComponent == null)
        {
            transformComponent = item.GetComponent<TransformComponent>();
        }
        if (transformComponent == null)
        {
            return vo;
        }
        vo.id = item.VO.id;
        vo.x = transformComponent.x;
        vo.y = transformComponent.y;
        vo.z = transformComponent.z;
        vo.scaleX = transformComponent.scaleX;
        vo.scaleY = transformComponent.scaleY;
        vo.scaleZ = transformComponent.scaleZ;
        vo.rotateX = transformComponent.rotateX;
        vo.rotateY = transformComponent.rotateY;
        vo.rotateZ = transformComponent.rotateZ;
        return vo;
    }

    public TransformVO GetTransformVO(Item2D item)
    {
        TransformVO vo = new TransformVO();
        TransformVO itemvo = item.VO.GetComponentVO<TransformVO>();
        vo.id = item.VO.id;
        vo.x = item.x;
        vo.y = itemvo.y;
        vo.z = item.y;
        vo.scaleX = itemvo.scaleX;
        vo.scaleY = itemvo.scaleY;
        vo.scaleZ = itemvo.scaleZ;
        vo.rotateX = item.rotationX;
        vo.rotateY = item.rotationZ;
        vo.rotateZ = itemvo.rotateZ;
        return vo;
    }

    public SpotlightVO GetSpotlightVO(Item3D item)
    {
        SpotlightVO vo = new SpotlightVO();
        SpotlightComponent spotlightComponent = item.GetComponentInChildren<SpotlightComponent>();
        vo.angle = spotlightComponent.Angle;
        vo.volumeBrightness = spotlightComponent.VolumeBrightness;
        vo.color = spotlightComponent.Color;
        vo.fromX = spotlightComponent.fromX;
        vo.fromY = spotlightComponent.fromY;
        vo.toX = spotlightComponent.toX;
        vo.toY = spotlightComponent.toY;
        vo.timeX = spotlightComponent.timeX;
        vo.timeY = spotlightComponent.timeY;
        vo.rotateType = spotlightComponent.rotateType;
        return vo;
    }
}

//----------------
//----------------
//----------------
//----------------
//----------------

public class ItemStruct
{
    public Item2D item2;
    public Item3D item3;
    public ItemVO vo;
    public string id { get { return vo.id; } }
}
