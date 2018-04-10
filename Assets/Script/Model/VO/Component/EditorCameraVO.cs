using UnityEngine;
using System.Collections;
using System.Xml;

public class EditorCameraVO : ComponentVO
{
    public float postExposure = 0f;
    public float temperature = 0f;
    public float tint = 0f;
    public float hueShift = 0f;
    public float saturation = 1.3f;
    public float contrast = 1.3f;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as EditorCameraVO).id;
        postExposure = (asset as EditorCameraVO).postExposure;
        temperature = (asset as EditorCameraVO).temperature;
        tint = (asset as EditorCameraVO).tint;
        hueShift = (asset as EditorCameraVO).hueShift;
        saturation = (asset as EditorCameraVO).saturation;
        contrast = (asset as EditorCameraVO).contrast;
    }

    override public bool Equals(AssetVO asset)
    {
        EditorCameraVO vo = asset as EditorCameraVO;
        return (
            vo.postExposure == postExposure &&
            vo.temperature == temperature &&
            vo.tint == tint &&
            vo.hueShift == hueShift &&
            vo.contrast == contrast &&
            vo.saturation == saturation
            );
    }

    public override AssetVO Clone()
    {
        EditorCameraVO vo = new EditorCameraVO();
        vo.id = id;
        vo.postExposure = postExposure;
        vo.temperature = temperature;
        vo.tint = tint;
        vo.hueShift = hueShift;
        vo.contrast = contrast;
        vo.saturation = saturation;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<EditorCamera";
            code += " postExposure = " + GetPropertyString(postExposure);
            code += " temperature = " + GetPropertyString(temperature);
            code += " tint = " + GetPropertyString(tint);
            code += " hueShift = " + GetPropertyString(hueShift);
            code += " contrast = " + GetPropertyString(contrast);
            code += " saturation = " + GetPropertyString(saturation);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            postExposure = code.GetFloat("postExposure");
            temperature = code.GetFloat("temperature");
            tint = code.GetFloat("tint");
            hueShift = code.GetFloat("hueShift");
            contrast = code.GetFloat("contrast");
            saturation = code.GetFloat("saturation");
        }
    }
}
