using UnityEngine;
using System.Collections;
using System.Xml;

public class MultipleSpotlightVO : ComponentVO
{
    public GameObject spotlight;

    public int upSpotCount = 5;
    public int downSpotCount = 4;

    public float upSpacing = .3f;
    public float downSpacing = .3f;
    public float verticalSpacing = .5f;

    public float upFromY;
    public float upToY;
    public float upFromX;
    public float upToX;
    public float upTimeY;
    public float upTimeX;

    public float downFromY;
    public float downToY;
    public float downFromX;
    public float downToX;
    public float downTimeY;
    public float downTimeX;

    public bool startRotate;

    public MultipleSpotlightVO()
    {
        //spotlight = Resources.Load(Factory.GetItemModelUrl("Spotlight")) as GameObject;
    }

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        spotlight = (asset as MultipleSpotlightVO).spotlight;
        upSpotCount = (asset as MultipleSpotlightVO).upSpotCount;
        downSpotCount = (asset as MultipleSpotlightVO).downSpotCount;
        upSpacing = (asset as MultipleSpotlightVO).upSpacing;
        downSpacing = (asset as MultipleSpotlightVO).downSpacing;
        verticalSpacing = (asset as MultipleSpotlightVO).verticalSpacing;
        upFromY = (asset as MultipleSpotlightVO).upFromY;
        upToY = (asset as MultipleSpotlightVO).upToY;
        upFromX = (asset as MultipleSpotlightVO).upFromX;
        upToX = (asset as MultipleSpotlightVO).upToX;
        upTimeY = (asset as MultipleSpotlightVO).upTimeY;
        upTimeX = (asset as MultipleSpotlightVO).upTimeX;
        downFromY = (asset as MultipleSpotlightVO).downFromY;
        downToY = (asset as MultipleSpotlightVO).downToY;
        downFromX = (asset as MultipleSpotlightVO).downFromX;
        downToX = (asset as MultipleSpotlightVO).downToX;
        downTimeY = (asset as MultipleSpotlightVO).downTimeY;
        downTimeX = (asset as MultipleSpotlightVO).downTimeX;
        startRotate = (asset as MultipleSpotlightVO).startRotate;
    }

    override public bool Equals(AssetVO asset)
    {
        MultipleSpotlightVO vo = asset as MultipleSpotlightVO;
        return (
            vo.upSpotCount == upSpotCount &&
            vo.downSpotCount == downSpotCount &&
            vo.upSpacing == upSpacing &&
            vo.downSpacing == downSpacing &&
            vo.verticalSpacing == verticalSpacing &&
            vo.upFromY == upFromY &&
            vo.upToY == upToY &&
            vo.upFromX == upFromX &&
            vo.upToX == upToX &&
            vo.upTimeY == upTimeY &&
            vo.upTimeX == upTimeX &&
            vo.downFromY == downFromY &&
            vo.downToY == downToY &&
            vo.downFromX == downFromX &&
            vo.downToX == downToX &&
            vo.downTimeY == downTimeY &&
            vo.downTimeX == downTimeX &&
            vo.startRotate == startRotate
            );
    }

    public override AssetVO Clone()
    {
        MultipleSpotlightVO vo = new MultipleSpotlightVO();
        vo.id = id;
        vo.upSpotCount = upSpotCount;
        vo.downSpotCount = downSpotCount;
        vo.upSpacing = upSpacing;
        vo.downSpacing = downSpacing;
        vo.verticalSpacing = verticalSpacing;
        vo.upFromY = upFromY;
        vo.upToY = upToY;
        vo.upFromX = upFromX;
        vo.upToX = upToX;
        vo.upTimeY = upTimeY;
        vo.upTimeX = upTimeX;
        vo.downFromY = downFromY;
        vo.downToY = downToY;
        vo.downFromX = downFromX;
        vo.downToX = downToX;
        vo.downTimeY = downTimeY;
        vo.downTimeX = downTimeX;
        vo.startRotate = startRotate;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<MultipleSpotlight";
            code += " upSpotCount = " + GetPropertyString(upSpotCount);
            code += " downSpotCount = " + GetPropertyString(downSpotCount);
            code += " upSpacing = " + GetPropertyString(upSpacing);
            code += " downSpacing = " + GetPropertyString(downSpacing);
            code += " verticalSpacing = " + GetPropertyString(verticalSpacing);
            code += " upFromY = " + GetPropertyString(upFromY);
            code += " upToY = " + GetPropertyString(upToY);
            code += " upFromX = " + GetPropertyString(upFromX);
            code += " upToX = " + GetPropertyString(upToX);
            code += " upTimeY = " + GetPropertyString(upTimeY);
            code += " upTimeX = " + GetPropertyString(upTimeX);
            code += " downFromY = " + GetPropertyString(downFromY);
            code += " downToY = " + GetPropertyString(downToY);
            code += " downFromX = " + GetPropertyString(downFromX);
            code += " downToX = " + GetPropertyString(downToX);
            code += " downTimeY = " + GetPropertyString(downTimeY);
            code += " downTimeX = " + GetPropertyString(downTimeX);
            code += " startRotate = " + GetBoolString(startRotate);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            upSpotCount = int.Parse(code.Attributes["upSpotCount"].Value);
            downSpotCount = int.Parse(code.Attributes["downSpotCount"].Value);
            upSpacing = float.Parse(code.Attributes["upSpacing"].Value);
            downSpacing = float.Parse(code.Attributes["downSpacing"].Value);
            verticalSpacing = float.Parse(code.Attributes["verticalSpacing"].Value);
            upFromY = float.Parse(code.Attributes["upFromY"].Value);
            upToY = float.Parse(code.Attributes["upToY"].Value);
            upFromX = float.Parse(code.Attributes["upFromX"].Value);
            upToX = float.Parse(code.Attributes["upToX"].Value);
            upTimeY = float.Parse(code.Attributes["upTimeY"].Value);
            upTimeX = float.Parse(code.Attributes["upTimeX"].Value);
            downFromY = float.Parse(code.Attributes["downFromY"].Value);
            downToY = float.Parse(code.Attributes["downToY"].Value);
            downFromX = float.Parse(code.Attributes["downFromX"].Value);
            downToX = float.Parse(code.Attributes["downToX"].Value);
            downTimeY = float.Parse(code.Attributes["downTimeY"].Value);
            downTimeX = float.Parse(code.Attributes["downTimeX"].Value);
            startRotate = (code.Attributes["startRotate"].Value == "1");
        }
    }
}
