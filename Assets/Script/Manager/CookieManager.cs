using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class CookieManager
{
    public CookieManager()
    {
        XmlDocument xml = new XmlDocument();
        xml. LoadXml(Resources.Load("Config/EffectLampCookie").ToString());

        XmlNode xmlNode = xml.SelectSingleNode("EffectLampCookie");

        _cookieImageList = new Hashtable();
        _cookieList = new Hashtable();
        foreach (XmlNode node in xmlNode)
        {
            _cookieList.Add(node.Attributes["id"].Value, node.Attributes["url"].Value);
            _cookieImageList.Add(node.Attributes["id"].Value, node.Attributes["thumbnail"].Value);
        }
    }

    private static Hashtable _cookieImageList;

    public static Hashtable CookieImageList
    {
        get { return _cookieImageList; }
    }

    private static Hashtable _cookieList;

    public static Hashtable CookieList
    {
        get { return _cookieList; }
    }

    public static Cubemap GetCubemapCookie(string id)
    {
        return Resources.Load(_cookieList[id] as string) as Cubemap;
    }

    public static Texture2D GetTextureCookie(string id)
    {
        return Resources.Load(_cookieList[id] as string) as Texture2D;
    }
}
