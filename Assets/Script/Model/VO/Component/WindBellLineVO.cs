using UnityEngine;
using System.Collections;
using System.Xml;

public class WindBellLineVO : ComponentVO
{
    public int count = 30;
    public float length = 1f;
    public float radius = 0.3f;
    public float itemRadius = 0.01f;
    public Color color = Color.white;

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        count = (asset as WindBellLineVO).count;
        length = (asset as WindBellLineVO).length;
        color = (asset as WindBellLineVO).color;
        itemRadius = (asset as WindBellLineVO).itemRadius;
        radius = (asset as WindBellLineVO).radius;
    }

    override public bool Equals(AssetVO asset)
    {
        WindBellLineVO vo = asset as WindBellLineVO;
        return (
            vo.id == id &&
            vo.count == count &&
            vo.length == length &&
            vo.radius == radius &&
            vo.itemRadius == itemRadius &&
            vo.color == color
            );
    }

    public override AssetVO Clone()
    {
        WindBellLineVO vo = new WindBellLineVO();
        vo.id = id;
        vo.count = count;
        vo.length = length;
        vo.color = color;
        vo.radius = radius;
        vo.itemRadius = itemRadius;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<WindBellLine";
            code += " count = " + GetPropertyString(count);
            code += " length = " + GetPropertyString(length);
            code += " radius = " + GetPropertyString(radius);
            code += " itemRadius = " + GetPropertyString(itemRadius);
            code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            count = code.GetInt("count");
            length = code.GetFloat("length");
            color = code.GetColor("color");
            color = code.GetColor("radius");
            color = code.GetColor("itemRadius");
        }
    }
}
