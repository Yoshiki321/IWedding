using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class SprinkleManager
{
    public SprinkleManager()
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/Item/SprinkleItem").ToString());

        XmlNode xmlNode = xml.SelectSingleNode("SprinkleItem");

        _sprinkleImageList = new Hashtable();
        _sprinkleList = new Hashtable();
        foreach (XmlNode node in xmlNode)
        {
            _sprinkleList.Add(node.Attributes["id"].Value, node.Attributes["model"].Value);
            _sprinkleImageList.Add(node.Attributes["id"].Value, node.Attributes["thumbnail"].Value);
        }
    }

    private static Hashtable _sprinkleImageList;

    public static Hashtable SprinkleImageList
    {
        get { return _sprinkleImageList; }
    }

    private static Hashtable _sprinkleList;

    public static Hashtable SprinkleList
    {
        get { return _sprinkleList; }
    }

    public static string GetModel(string id)
    {
        return _sprinkleList[id] as string;
    }

    public static string GetThumbnail(string id)
    {
        return _sprinkleList[id] as string;
    }
}