using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class AssetVO
{
    public string name = "undefined";

    public bool isDefault = false;

    public bool isNull = false;

    private string _id;

    public string id
    {
        set
        {
            _id = value;

            foreach (ComponentVO c in componentVOList)
            {
                c.id = _id;
            }
        }
        get { return _id; }
    }

    public virtual bool Equals(AssetVO asset)
    {
        return (asset.id == this.id);
    }

    public virtual bool EqualsComponentVO(AssetVO asset)
    {
        if (componentVOList.Count != asset.componentVOList.Count)
        {
            return false;
        }

        for (int i = 0; i < componentVOList.Count; i++)
        {
            if (componentVOList[i].GetType() != asset.componentVOList[i].GetType())
            {
                return false;
            }
            if (!componentVOList[i].Equals(asset.componentVOList[i]))
            {
                return false;
            }
        }

        return true;
    }

    private XmlNode _code;

    public virtual XmlNode Code
    {
        get { return null; }
        set { _code = value; }
    }

    public virtual AssetVO Clone()
    {
        return null;
    }

    protected List<ComponentVO> componentVOList = new List<ComponentVO>();

    public List<ComponentVO> ComponentsVO
    {
        get { return componentVOList; }
    }

    public ComponentVO AddComponentVO(ComponentVO vo)
    {
        vo.id = id;
        componentVOList.Add(vo);
        return vo;
    }

    public T AddComponentVO<T>() where T : ComponentVO, new()
    {
        T t = new T();
        t.id = id;
        componentVOList.Add(t);
        return t;
    }

    public T GetComponentVO<T>() where T : ComponentVO
    {
        for (int i = 0; i < componentVOList.Count; i++)
        {
            if (componentVOList[i] is T)
            {
                return componentVOList[i] as T;
            }
        }
        return null;
    }

    public ComponentVO GetComponentVO(Type type)
    {
        for (int i = 0; i < componentVOList.Count; i++)
        {
            if (componentVOList[i].GetType() == type)
            {
                return componentVOList[i];
            }
        }
        return null;
    }

    protected string GetPropertyString(object value)
    {
        return '"' + value.ToString() + '"';
    }

    protected string GetBoolString(bool value)
    {
        return '"' + (value ? "1" : "0") + '"';
    }
}
