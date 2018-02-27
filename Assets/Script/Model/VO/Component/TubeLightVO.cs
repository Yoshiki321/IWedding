using UnityEngine;
using System.Collections;
using System.Xml;

public class TubeLightVO : ComponentVO
{
    public float brightness = 1.2f;
    public float range = 7;
    public float length = 1.4f;
    public Color color = Color.white;

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        brightness = (asset as TubeLightVO).brightness;
        range = (asset as TubeLightVO).range;
        length = (asset as TubeLightVO).length;
        color = (asset as TubeLightVO).color;
    }

    override public bool Equals(AssetVO asset)
    {
        TubeLightVO vo = asset as TubeLightVO;
        return (
            vo.brightness == brightness &&
            vo.range == range &&
            vo.length == length &&
            vo.color == color
            );
    }

    public override AssetVO Clone()
    {
        TubeLightVO vo = new TubeLightVO();
        vo.id = id;
        vo.brightness = brightness;
        vo.range = range;
        vo.length = length;
        vo.color = color;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<TubeLight";
            code += " brightness = " + GetPropertyString(brightness);
            code += " range = " + GetPropertyString(range);
            code += " length = " + GetPropertyString(length);
            code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            brightness = float.Parse(code.Attributes["brightness"].Value);
            range = float.Parse(code.Attributes["range"].Value);
            length = float.Parse(code.Attributes["length"].Value);
            color = ColorUtils.HexToColor(code.Attributes["color"].Value);
        }
    }
}
