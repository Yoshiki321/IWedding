using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Build2D;
using Build3D;
using System.Xml;
using BuildManager;

public class AssetsModel : Actor<AssetsModel>
{
    public void Clear()
    {
        foreach (ObjectData data in objectDataList)
        {
            data.object2.Dispose();
            data.object3.Dispose();
        }

        objectDataList = new List<ObjectData>();
        itemDataList = new List<ObjectData>();
        nestedDataList = new List<ObjectData>();
    }

    public ObjectData Remove(string id)
    {
        ObjectData objectData = GetObjectData(id);

        if (objectData == null) return null;

        Remove(objectData);

        return RemoveObjectData(id);
    }

    public void Remove(ObjectData objectData)
    {
        if (objectData.object2 is Item2D)
        {
            Item2D item = objectData.object2 as Item2D;
            Item3D item3 = objectData.object3 as Item3D;

            item.Dispose();
            item3.Dispose();
        }

        if (objectData.object2 is Nested2D)
        {
            Nested2D nested = objectData.object2 as Nested2D;
            Nested3D nested3 = objectData.object3 as Nested3D;

            nested.Dispose();
            nested3.Dispose();
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
        }

        ProgressLoadItem(list);
    }

    #region progressLoadItem;

    int _progressItemCurrent = 0;
    int _progressItemTotal = 0;

    public int progressItemCurrent { get { return _progressItemCurrent; } }
    public int progressItemTotal { get { return _progressItemTotal; } }

    List<ItemVO> _progressLoadItem;
    public void ProgressLoadItem(List<ItemVO> list)
    {
        _progressItemCurrent = 0;
        _progressItemTotal = list.Count;
        _progressLoadItem = list;
    }

    public void Update()
    {
        if (_progressLoadItem != null && _progressLoadItem.Count > 0)
        {
            _progressItemCurrent++;

            ObjectData data = CreateItem(_progressLoadItem[0]);
            (data.object2 as Item2D).UpdateSize();
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
            lists.Add(vo);
        }
        return lists;
    }

    public void CreateNested(XmlNodeList xml)
    {
        foreach (XmlNode node in xml)
        {
            NestedVO vo = new NestedVO();
            vo.Code = node;
            CreateNested(vo);
        }
    }

    private void FillObjectVO(ObjectVO vo, string id)
    {
        ItemData itemData = ItemManager.GetItemData(id);
        if (itemData != null)
        {
            vo.modelId = itemData.model;
            vo.topImgId = itemData.topImg;

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

    public ObjectData CreateItem(ItemVO itemvo, bool save = true)
    {
        GameObject _item = new GameObject();
        Item2D item2d = _item.AddComponent<Item2D>();
        item2d.parent = SceneManager.Instance.Graphics2D.ItemLayer.transform;
        item2d.Instantiate(itemvo);

        GameObject _item3 = new GameObject();
        _item3.transform.parent = SceneManager.Instance.Graphics3D.ItemLayer.transform;
        Item3D item3d = _item3.AddComponent<Item3D>();
        item3d.Instantiate(itemvo);

        itemvo.name = itemvo.assetId.ToString();

        ObjectData data = new ObjectData();
        data.object2 = item2d;
        data.object3 = item3d;
        data.vo = itemvo;

        if (save)
        {
            SetObjectData(data);
        }

        return data;
    }

    public ObjectData CreateNested(NestedVO nestedvo, bool save = true)
    {
        GameObject _nested = new GameObject();
        Nested2D nested2d = _nested.AddComponent<Nested2D>();
        nested2d.parent = SceneManager.Instance.Graphics2D.NestedLayer.transform;
        nested2d.Instantiate(nestedvo);

        GameObject _nested3 = new GameObject();
        _nested3.transform.parent = SceneManager.Instance.Graphics3D.NestedLayer.transform;
        Nested3D nested3d = _nested3.AddComponent<Nested3D>();
        nested3d.Instantiate(nestedvo);

        nestedvo.name = nestedvo.assetId.ToString();

        nested2d.height3D = nested3d.sizeY;
        nested2d.width = nested3d.sizeX;
        nested2d.widthZ = nested3d.sizeZ;

        ObjectData data = new ObjectData();
        data.object2 = nested2d;
        data.object3 = nested3d;
        data.vo = nestedvo;

        if (save)
        {
            SetObjectData(data);
        }

        return data;
    }

    //----------------
    //----------------
    //----------------
    //----------------
    //----------------

    private List<ObjectData> objectDataList = new List<ObjectData>();
    private List<ObjectData> itemDataList = new List<ObjectData>();
    private List<ObjectData> nestedDataList = new List<ObjectData>();

    public List<ObjectData> objectDatas
    {
        get { return objectDataList; }
    }

    public List<ObjectData> itemDatas
    {
        get { return itemDataList; }
    }

    public List<ObjectData> nestedDatas
    {
        get { return nestedDataList; }
    }

    public void SetObjectData(ObjectData data)
    {
        objectDataList.Add(data);
        if (data.vo is ItemVO) itemDataList.Add(data);
        if (data.vo is NestedVO) nestedDataList.Add(data);
    }

    public ObjectData GetObjectData(String id)
    {
        foreach (ObjectData objectData in objectDataList)
        {
            if (objectData.vo.id == id)
            {
                return objectData;
            }
        }
        return null;
    }

    public ObjectData GetItemData(String id)
    {
        foreach (ObjectData objectData in itemDataList)
        {
            if (objectData.vo.id == id)
            {
                return objectData;
            }
        }
        return null;
    }

    public ObjectData GetNestedData(String id)
    {
        foreach (ObjectData objectData in nestedDataList)
        {
            if (objectData.vo.id == id)
            {
                return objectData;
            }
        }
        return null;
    }

    public List<ObjectSprite> GetObjectData(List<AssetVO> vos)
    {
        List<ObjectSprite> list = new List<ObjectSprite>();
        foreach (AssetVO vo in vos)
        {
            ObjectData objectData = GetObjectData(vo.id);
            if (objectData != null)
            {
                list.Add(objectData.object3);
            }
        }
        return list;
    }

    public ObjectData RemoveObjectData(String id)
    {
        foreach (ObjectData objectData in objectDataList)
        {
            if (objectData.vo.id == id)
            {
                objectDataList.Remove(objectData);
                if (objectData.vo is ItemVO) itemDataList.Remove(objectData);
                if (objectData.vo is NestedVO) nestedDataList.Remove(objectData);
                return objectData;
            }
        }
        return null;
    }

    public List<ItemVO> GetGroupItem(string id)
    {
        List<ItemVO> list = new List<ItemVO>();

        foreach (ObjectData data in itemDataList)
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

        foreach (ObjectData data in itemDataList)
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

public class ObjectData
{
    public ObjectSprite object2;
    public ObjectSprite object3;
    public ObjectVO vo;
    public string id { get { return vo.id; } }
}
