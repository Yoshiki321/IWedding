using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class CollageVO : ComponentVO
{
    public List<CollageStruct> collages = new List<CollageStruct>();

    public void ResetCollage()
    {
        collages = new List<CollageStruct>();
    }

    public CollageStruct HasCollage(string tag)
    {
        foreach (CollageStruct c in collages)
        {
            if (c.tag == tag)
            {
                return c;
            }
        }
        return null;
    }

    public CollageStruct SetCollage(string name, string tag, string id, Color color = new Color(), string url = "")
    {
        CollageStruct c = HasCollage(tag);
        if (c != null)
        {
            c.id = id;
            c.url = url;
            c.name = name;
            return c;
        }

        c = new CollageStruct();
        c.name = name;
        c.tag = tag;
        c.id = id;
        c.url = url;
        c.color = color;

        collages.Add(c);
        return c;
    }

    public CollageStruct GetCollageByUrl(string url)
    {
        foreach (CollageStruct c in collages)
        {
            if (c.tag == url)
            {
                return c;
            }
        }
        return null;
    }

    public CollageStruct GetCollageByName(string name)
    {
        foreach (CollageStruct c in collages)
        {
            if (c.name == name)
            {
                return c;
            }
        }
        return null;
    }

    public override AssetVO Clone()
    {
        CollageVO vo = new CollageVO();
        vo.id = id;
        vo.collages = CollageClone(collages);
        return vo;
    }

    public List<CollageStruct> CollageClone(List<CollageStruct> cs)
    {
        List<CollageStruct> cc = new List<CollageStruct>();
        foreach (CollageStruct c in collages)
        {
            cc.Add(c.Clone());
        }
        return cc;
    }

    override public void FillFromObject(ComponentVO asset)
    {
        collages = (asset as CollageVO).collages;
        id = (asset as CollageVO).id;
    }

    override public bool Equals(AssetVO asset)
    {
        CollageVO vo = asset as CollageVO;
        if (collages.Count != vo.collages.Count)
        {
            return false;
        }
        for (int i = 0; i < collages.Count; i++)
        {
            if (collages[i].name != vo.collages[i].name ||
                collages[i].tag != vo.collages[i].tag ||
                collages[i].id != vo.collages[i].id ||
                collages[i].url != vo.collages[i].url ||
                collages[i].color != vo.collages[i].color ||
                collages[i].tilingX != vo.collages[i].tilingX ||
                collages[i].tilingY != vo.collages[i].tilingY ||
                collages[i].offestX != vo.collages[i].offestX ||
                collages[i].offestY != vo.collages[i].offestY
                )
                return false;
        }
        return true;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Collage>";
            for (int i = 0; i < collages.Count; i++)
            {
                code += "   " + collages[i].ToCode() + ((i == collages.Count - 1) ? "" : "\n");
            }
            code += "</Collage>";
            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            XmlNodeList list = code.SelectNodes("CollageStruct");
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    SetCollage(
                        node.Attributes["name"].Value,
                        node.Attributes["tag"].Value,
                        node.Attributes["id"].Value,
                        ColorUtils.HexToColor(node.Attributes["color"].Value),
                        node.Attributes["url"] != null ? node.Attributes["url"].Value : "");
                    //node.Attributes["tilingX"].Value;
                    //node.Attributes["tilingY"].Value;
                    //node.Attributes["offestX"].Value;
                    //node.Attributes["offestY"].Value;
                }
            }
            else
            {
                if (code.Attributes["struct"] != null)
                {
                    string s = code.Attributes["struct"].Value;
                    char[] separator = { ';' };
                    string[] arr = s.Split(separator);

                    foreach (string sc in arr)
                    {
                        if (sc != "")
                        {
                            char[] separator1 = { ',' };
                            string[] arr1 = sc.Split(separator1);
                            SetCollage(arr1[0], arr1[1], arr1[2], Color.white);
                        }
                    }
                }
            }
        }
    }
}

public class CollageStruct
{
    public string name;
    public string tag;
    public string id;
    public string url;
    public Color color = Color.white;
    public float tilingX;
    public float tilingY;
    public float offestX;
    public float offestY;

    public CollageStruct Clone()
    {
        CollageStruct c = new CollageStruct();
        c.name = name;
        c.tag = tag;
        c.id = id;
        c.url = url;
        c.color = color;
        c.tilingX = tilingX;
        c.tilingY = tilingY;
        c.offestX = offestX;
        c.offestY = offestY;
        return c;
    }

    public string ToCode()
    {
        string code = "";
        code += "<CollageStruct";
        code += " name = " + GetPropertyString(name);
        code += " tag = " + GetPropertyString(tag);
        code += " id = " + GetPropertyString(id);
        code += " url = " + GetPropertyString(url);
        code += " tilingX = " + GetPropertyString(tilingX);
        code += " tilingY = " + GetPropertyString(tilingY);
        code += " offestX = " + GetPropertyString(offestX);
        code += " offestY = " + GetPropertyString(offestY);
        code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
        code += "/>";
        return code;
    }

    protected string GetPropertyString(object value)
    {
        return '"' + value.ToString() + '"';
    }
}

