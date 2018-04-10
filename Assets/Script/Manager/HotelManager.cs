using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using Build3D;

public class HotelManager
{
    public HotelManager()
    {
        //加载ItemXml配置文件
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load("Config/Hotel/Hotel").ToString());
        //解析到XML-第1标签-ItemCode
        XmlNode xmlNode = xml.SelectSingleNode("HotelCode");
        //实例化列表用来置放子物体
        HotelDataList = new List<HotelData>();

        foreach (XmlNode node in xmlNode)
        {
            HotelData data = new HotelData();
            data.id = node.Attributes["id"].Value;
            data.province = node.Attributes["province"].Value;
            data.city = node.Attributes["city"].Value;
            data.district = node.Attributes["district"].Value;
            data.name = node.Attributes["name"].Value;
            data.address = node.Attributes["address"].Value;
            data.url = node.Attributes["url"].Value;
            data.size = node.Attributes["size"].Value;
            data.date = node.Attributes["date"].Value;
            data.hotelimg = node.Attributes["hotelimg"].Value;
            HotelDataList.Add(data);
        }
    }

    public static List<HotelData> HotelDataList { get; private set; }

    public static HotelData GetHotelData(string id)
    {
        foreach (HotelData data in HotelDataList)
        {
            if (data.id == id)
            {
                return data;
            }
        }
        return null;
    }

}
