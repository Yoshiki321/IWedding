using System.Xml;
using UnityEngine;

public static class XmlNodeExtension
{
    public static float GetFloat(this XmlNode xmlNode, string name)
    {
        if (xmlNode.Attributes[name] == null)
        {
            return 0;
        }
        return float.Parse(xmlNode.Attributes[name].Value);
    }

    public static int GetInt(this XmlNode xmlNode, string name)
    {
        if (xmlNode.Attributes[name] == null)
        {
            return 0;
        }
        return int.Parse(xmlNode.Attributes[name].Value);
    }

    public static bool GetBool(this XmlNode xmlNode, string name)
    {
        if (xmlNode.Attributes[name] == null)
        {
            return false;
        }
        return xmlNode.Attributes[name].Value == "1";
    }

    public static string GetString(this XmlNode xmlNode, string name)
    {
        if (xmlNode.Attributes[name] == null)
        {
            return "";
        }
        return xmlNode.Attributes[name].Value;
    }

    public static Color GetColor(this XmlNode xmlNode, string name)
    {
        if (xmlNode.Attributes[name] == null)
        {
            return Color.black;
        }
        return ColorUtils.HexToColor(xmlNode.Attributes[name].Value);
    }
}
