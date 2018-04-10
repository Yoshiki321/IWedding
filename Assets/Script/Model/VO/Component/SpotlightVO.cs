using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SpotlightVO : ComponentVO
{
    public float volumeBrightness = .2f;
    public float lightBrightness = 1f;
    public float spotAngle = 15f;
    public float angle;
    public Color color = Color.white;
    public string cookieId = "";

    public float fromX;
    public float toX;
    public float fromY;
    public float toY;
    public float timeX;
    public float timeY;
    public int rotateType;

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        volumeBrightness = (asset as SpotlightVO).volumeBrightness;
        lightBrightness = (asset as SpotlightVO).lightBrightness;
        spotAngle = (asset as SpotlightVO).spotAngle;
        angle = (asset as SpotlightVO).angle;
        cookieId = (asset as SpotlightVO).cookieId;
        color = (asset as SpotlightVO).color;
        fromX = (asset as SpotlightVO).fromX;
        toX = (asset as SpotlightVO).toX;
        fromY = (asset as SpotlightVO).fromY;
        toY = (asset as SpotlightVO).toY;
        timeX = (asset as SpotlightVO).timeX;
        timeY = (asset as SpotlightVO).timeY;
        rotateType = (asset as SpotlightVO).rotateType;
    }

    override public bool Equals(AssetVO asset)
    {
        SpotlightVO vo = asset as SpotlightVO;
        return (
            vo.volumeBrightness == volumeBrightness &&
            vo.lightBrightness == lightBrightness &&
            vo.spotAngle == spotAngle &&
            vo.angle == angle &&
            vo.cookieId == cookieId &&
            vo.color.Equals(color) &&
            vo.fromX == fromX &&
            vo.toX == toX &&
            vo.fromY == fromY &&
            vo.toY == toY &&
            vo.timeX == timeX &&
            vo.timeY == timeY &&
            vo.rotateType == rotateType
            );
    }

    public override AssetVO Clone()
    {
        SpotlightVO vo = new SpotlightVO();
        vo.id = id;
        vo.volumeBrightness = volumeBrightness;
        vo.lightBrightness = lightBrightness;
        vo.spotAngle = spotAngle;
        vo.angle = angle;
        vo.cookieId = cookieId;
        vo.color = color;
        vo.fromX = fromX;
        vo.toX = toX;
        vo.fromY = fromY;
        vo.toY = toY;
        vo.timeX = timeX;
        vo.timeY = timeY;
        vo.rotateType = rotateType;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Spotlight";
            code += " volumeBrightness = " + GetPropertyString(volumeBrightness);
            code += " lightBrightness = " + GetPropertyString(lightBrightness);
            code += " spotAngle = " + GetPropertyString(spotAngle);
            code += " angle = " + GetPropertyString(angle);
            code += " cookieId = " + GetPropertyString(cookieId);
            code += " color = " + GetPropertyString(ColorUtils.ColorToHex(color));
            code += " fromX = " + GetPropertyString(fromX);
            code += " toX = " + GetPropertyString(toX);
            code += " fromY = " + GetPropertyString(fromY);
            code += " toY = " + GetPropertyString(toY);
            code += " timeX = " + GetPropertyString(timeX);
            code += " timeY = " + GetPropertyString(timeY);
            code += " startRotate = " + GetPropertyString(rotateType);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            volumeBrightness = code.GetFloat("volumeBrightness");
            lightBrightness = code.GetFloat("lightBrightness");
            spotAngle = code.GetFloat("spotAngle");
            angle = code.GetFloat("angle");
            cookieId = code.GetString("cookieId");
            color = code.GetColor("color");
            fromX = code.GetFloat("fromX");
            toX = code.GetFloat("toX");
            fromY = code.GetFloat("fromY");
            toY = code.GetFloat("toY");
            timeX = code.GetFloat("timeX");
            timeY = code.GetFloat("timeY");
            rotateType = code.GetInt("startRotate");
        }
    }
}
