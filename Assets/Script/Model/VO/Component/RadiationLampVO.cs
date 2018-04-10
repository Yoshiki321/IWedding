using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

public class RadiationLampVO : ComponentVO
{
    public float brightness = .5f;
    public float rotateSpeed = 2f;
    public float spacing = 30f;
    public Color color = Color.white;

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        brightness = (asset as RadiationLampVO).brightness;
        rotateSpeed = (asset as RadiationLampVO).rotateSpeed;
        color = (asset as RadiationLampVO).color;
        spacing = (asset as RadiationLampVO).spacing;
    }

    override public bool Equals(AssetVO asset)
    {
        RadiationLampVO vo = asset as RadiationLampVO;
        return (
            vo.brightness == brightness &&
            vo.spacing == spacing &&
            vo.color.Equals(color) &&
            vo.rotateSpeed == rotateSpeed
            );
    }

    public override AssetVO Clone()
    {
        RadiationLampVO vo = new RadiationLampVO();
        vo.id = id;
        vo.brightness = brightness;
        vo.rotateSpeed = rotateSpeed;
        vo.spacing = spacing;
        vo.color = color;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<RadiationLamp";
            code += " brightness = " + GetPropertyString(brightness);
            code += " rotateSpeed = " + GetPropertyString(rotateSpeed);
            code += " spacing = " + GetPropertyString(spacing);
            code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            brightness = code.GetFloat("brightness");
            rotateSpeed = code.GetFloat("rotateSpeed");
            spacing = code.GetFloat("spacing");
            color = code.GetColor("color");
        }
    }
}
