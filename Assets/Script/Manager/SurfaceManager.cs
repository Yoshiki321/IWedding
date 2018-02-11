using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class SurfaceManager
{
    public SurfaceManager()
    {
        //加载ItemXml配置文件
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/Surface/Surface").ToString());
        //解析到XML-第1标签-ItemCode
        XmlNode xmlNode = xml.SelectSingleNode("SurfaceCode");
        //实例化列表用来置放子物体
        _surfaceDrawDataList = new List<SurfaceDrawData>();

        foreach (XmlNode node in xmlNode)
        {
            SurfaceDrawData data = new SurfaceDrawData();
            data.id = node.Attributes["id"].Value;
            data.points = node.Attributes["points"].Value;
            data.thumbnail = node.Attributes["thumbnail"].Value;
            _surfaceDrawDataList.Add(data);
        }
    }

    private static List<SurfaceDrawData> _surfaceDrawDataList;

    public static List<SurfaceDrawData> SurfaceDrawDataList
    {
        get { return _surfaceDrawDataList; }
    }

    public static SurfaceDrawData GetSurfaceDrawData(string id)
    {
        foreach (SurfaceDrawData data in _surfaceDrawDataList)
        {
            if (data.id == id)
            {
                return data;
            }
        }
        return null;
    }
}
