using UnityEngine;
using System.Collections;
using System.Xml;

public class PointLightVO : ComponentVO
{
    public float range = 10f;
    public float intensity = 1f;
    public Color color = Color.white;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as PointLightVO).id;
        range = (asset as PointLightVO).range;
        intensity = (asset as PointLightVO).intensity;
        color = (asset as PointLightVO).color;
    }

    override public bool Equals(AssetVO asset)
    {
        PointLightVO vo = asset as PointLightVO;
        return (
            vo.id == id &&
            vo.range == range &&
            vo.intensity == intensity &&
            vo.color.Equals(color)
            );
    }

    public override AssetVO Clone()
    {
        PointLightVO vo = new PointLightVO();
        vo.id = id;
        vo.range = range;
        vo.intensity = intensity;
        vo.color = color;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<PointLight";
            code += " range = " + GetPropertyString(range);
            code += " intensity = " + GetPropertyString(intensity);
            code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            range = code.GetFloat("range");
            intensity = code.GetFloat("intensity");
            color = code.GetColor("color");
        }
    }
}
