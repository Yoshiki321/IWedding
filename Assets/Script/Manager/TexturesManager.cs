using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class TexturesManager
{
    public TexturesManager()
    {
        InitFloorTextures();
        InitPlaneTextures();
    }

    private void InitFloorTextures()
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/FloorTextures").ToString());

        XmlNode xmlNode = xml.SelectSingleNode("FloorTextures");

        FloorImageList = new Hashtable();
        FloorDataList = new List<CollageData>();
        foreach (XmlNode node in xmlNode)
        {
            CollageData c = new CollageData();
            c.id = node.Attributes["id"].Value;
            c.materialsUrl = node.Attributes["url"].Value;
            c.thumbnailUrl = node.Attributes["thumbnail"].Value;
            FloorDataList.Add(c);

            FloorImageList.Add(c.id, c.thumbnailUrl);
        }
    }

    public static Hashtable FloorImageList { get; private set; }
    public static List<CollageData> FloorDataList { get; private set; }

    private void InitPlaneTextures()
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/PlaneTextures").ToString());

        XmlNode xmlNode = xml.SelectSingleNode("PlaneTextures");

        CollageImageList = new Hashtable();
        CollageDataList = new List<CollageData>();
        foreach (XmlNode node in xmlNode)
        {
            CollageData c = new CollageData();
            c.id = node.Attributes["id"].Value;
            c.materialsUrl = node.Attributes["url"].Value;
            c.thumbnailUrl = node.Attributes["thumbnail"].Value;
            CollageDataList.Add(c);

            CollageImageList.Add(c.id, c.thumbnailUrl);
        }
    }

    public static Hashtable CollageImageList { get; private set; }
    public static List<CollageData> CollageDataList { get; private set; }

    public static CollageData GetData(string id)
    {
        foreach (CollageData c in CollageDataList)
        {
            if (c.id == id)
            {
                return c;
            }
        }
        foreach (CollageData c in FloorDataList)
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
        if (GetData(id) == null)
        {
            return Resources.Load(GetData("F0001").materialsUrl) as Material;
        }
        return Resources.Load(GetData(id).materialsUrl) as Material;
    }
}
