using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class DrawShapeManager
{
    public DrawShapeManager()
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/Shape/ShapeDraw").ToString());
        XmlNode xmlNode = xml.SelectSingleNode("ShapeDraw");
        _shapeDrawDataList = new List<ShapeDrawData>();

        foreach (XmlNode node in xmlNode)
        {
            ShapeDrawData data = new ShapeDrawData();
            data.id = node.Attributes["id"].Value;
            data.name = node.Attributes["name"].Value;
            data.thumbnail = node.Attributes["thumbnail"].Value;
            data.nodes = node.Attributes["points"].Value;
            _shapeDrawDataList.Add(data);
        }
    }

    private static List<ShapeDrawData> _shapeDrawDataList;

    public static List<ShapeDrawData> ShapeDrawDataList
    {
        get { return _shapeDrawDataList; }
    }
}
