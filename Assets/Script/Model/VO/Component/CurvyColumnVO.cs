using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;
using System.Collections.Generic;

public class CurvyColumnVO : ComponentVO
{
    public List<Vector3> points;
    public Color color = Color.white; 
    public float radius = 1.5f;
    public bool enabled = false;

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        color = (asset as CurvyColumnVO).color;
        radius = (asset as CurvyColumnVO).radius;
        enabled = (asset as CurvyColumnVO).enabled;
    }

    override public bool Equals(AssetVO asset)
    {
        CurvyColumnVO vo = asset as CurvyColumnVO;
        return (
             vo.color == color &&
             vo.radius == radius && 
             vo.enabled == enabled
            );
    }

    public override AssetVO Clone()
    {
        CurvyColumnVO vo = new CurvyColumnVO();
        vo.id = id;
        vo.color = color;
        vo.radius = radius;
        vo.enabled = enabled;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<CurvyColumn";
            code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
            code += " radius = " + GetPropertyString(radius);
            code += " enabled = " + GetBoolString(enabled);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            color = ColorUtils.HexToColor(code.Attributes["color"].Value);
            radius = float.Parse(code.Attributes["radius"].Value);
            enabled = code.Attributes["enabled"].Value == "1";
        }
    }
}
