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

    public CollageStruct SetCollage(string name, string tag, object id)
    {
        CollageStruct c = HasCollage(tag);
        if (c != null)
        {
            c.id = id;
            c.name = name;
            return c;
        }
        c = new CollageStruct(name, tag, id);
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
            if (collages[i].name != vo.collages[i].name) return false;
            if (collages[i].tag != vo.collages[i].tag) return false;
            if (collages[i].id != vo.collages[i].id) return false;
        }
        return true;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Collage";
            code += " struct = ";
            code += '"';
            for (int i = 0; i < collages.Count; i++)
            {
                code += collages[i].ToCode() + ((i == collages.Count - 1) ? "" : ";");
            }
            code += '"';
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            string s = code.Attributes["struct"].Value;
            char[] separator = { ';' };
            string[] arr = s.Split(separator);

            foreach (string sc in arr)
            {
                if (sc != "")
                {
                    char[] separator1 = { ',' };
                    string[] arr1 = sc.Split(separator1);
                    SetCollage(arr1[0], arr1[1], arr1[2]);
                }
            }
        }
    }
}

public class CollageStruct
{
    public string name;
    public string tag;
    public object id;
    public float tilingX;
    public float tilingY;
    public float offestX;
    public float offestY;

    public CollageStruct(string name, string url, object id, Vector2 tiling = new Vector2(), Vector2 offest = new Vector2())
    {
        this.name = name;
        this.tag = url;
        this.id = id;
        tilingX = tiling.x;
        tilingY = tiling.y;
        offestX = offest.x;
        offestY = offest.y;
    }

    public CollageStruct Clone()
    {
        return new CollageStruct(name, tag, id, new Vector2(tilingX, tilingY), new Vector2(offestX, offestY));
    }

    public string ToCode()
    {
        return name + "," + tag + "," + id + "," + tilingX + "," + tilingY + "," + offestX + "," + offestY;
    }
}

