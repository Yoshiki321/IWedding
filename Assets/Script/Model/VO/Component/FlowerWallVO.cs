using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;

public class FlowerWallVO : ComponentVO
{
    public string assetId = "";
    public ColorVO color = new ColorVO();
    public bool visible = true;
    public XmlNode panelCode;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as FlowerWallVO).id;
        assetId = (asset as FlowerWallVO).assetId;
        visible = (asset as FlowerWallVO).visible;
        color = (asset as FlowerWallVO).color;
        panelCode = (asset as FlowerWallVO).panelCode;
    }

    override public bool Equals(AssetVO asset)
    {
        FlowerWallVO vo = asset as FlowerWallVO;
        return (
            vo.id == id &&
            vo.assetId == assetId &&
            vo.visible == visible &&
            vo.panelCode == panelCode &&
            vo.color.Equals(color)
            );
    }

    public override AssetVO Clone()
    {
        FlowerWallVO vo = new FlowerWallVO();
        vo.id = id;
        vo.assetId = assetId;
        vo.visible = visible;
        vo.panelCode = panelCode;
        vo.color = color;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<FlowerWall";
            code += " assetId = " + GetPropertyString(assetId);
            code += " visible = " + GetBoolString(visible);
            code += " color = " + color.ToCode();
            code += ">";
            code += panelCode.OuterXml;
            code += "</FlowerWall>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            assetId = code.Attributes["assetId"].Value;
            visible = code.Attributes["visible"].Value == "1";
            color.SetCode(code.Attributes["color"].Value);
            panelCode = code;
        }
    }
}
