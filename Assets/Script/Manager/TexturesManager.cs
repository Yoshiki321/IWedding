using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class TexturesManager
{
    public TexturesManager()
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/PlaneTextures").ToString());

        XmlNode xmlNode = xml.SelectSingleNode("PlaneTextures");

        _collageImageList = new Hashtable();
        _collageDataList = new List<CollageData>();
        foreach (XmlNode node in xmlNode)
        {
            CollageData c = new CollageData();
            c.id = node.Attributes["id"].Value;
            c.materialsUrl = node.Attributes["url"].Value;
            c.thumbnailUrl = node.Attributes["thumbnail"].Value;
            _collageDataList.Add(c);

            _collageImageList.Add(c.id, c.thumbnailUrl);
        }
    }

    private static Hashtable _collageImageList;

    public static Hashtable CollageImageList
    {
        get { return _collageImageList; }
    }

    private static List<CollageData> _collageDataList;

    public static List<CollageData> CollageDataList
    {
        get { return _collageDataList; }
    }

    public static CollageData GetCollageData(string id)
    {
        foreach (CollageData c in _collageDataList)
        {
            if (c.id == id)
            {
                return c;
            }
        }
        return null;
    }

    public static Material CreateMaterials(string id)
    {
        return Resources.Load(GetCollageData(id).materialsUrl) as Material;
    }
}
