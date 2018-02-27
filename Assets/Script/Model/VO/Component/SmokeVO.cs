using UnityEngine;
using System.Collections;
using System.Xml;

public class SmokeVO : ComponentVO
{
    public float size = 1.5f;
    public float speed = 0f;
    public Color startColor = Color.white;
    public Color endColor = Color.white;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as SmokeVO).id;
        size = (asset as SmokeVO).size;
        speed = (asset as SmokeVO).speed;
        startColor = (asset as SmokeVO).startColor;
        endColor = (asset as SmokeVO).endColor;
    }

    override public bool Equals(AssetVO asset)
    {
        SmokeVO vo = asset as SmokeVO;
        return (
            vo.size == size &&
            vo.startColor.Equals(startColor) &&
            vo.endColor.Equals(endColor) &&
            vo.speed == speed
            );
    }

    public override AssetVO Clone()
    {
        SmokeVO vo = new SmokeVO();
        vo.id = id;
        vo.size = size;
        vo.speed = speed;
        vo.startColor = startColor;
        vo.endColor = endColor;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Smoke";
            code += " size = " + GetPropertyString(size);
            code += " speed = " + GetPropertyString(speed);
            code += " startColor = " + GetPropertyString(ColorUtils.ColorToHex(startColor));
            code += " endColor = " + GetPropertyString(ColorUtils.ColorToHex(endColor));
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            size = float.Parse(code.Attributes["size"].Value);
            speed = float.Parse(code.Attributes["speed"].Value);
            startColor = ColorUtils.HexToColor(code.Attributes["startColor"].Value);
            endColor = ColorUtils.HexToColor(code.Attributes["endColor"].Value);
        }
    }
}
