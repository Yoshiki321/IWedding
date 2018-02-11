using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;

public class FlowerWallVO : ComponentVO
{
    public string assetId;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as FlowerWallVO).id;
        assetId = (asset as FlowerWallVO).assetId;
    }

    override public bool Equals(AssetVO asset)
    {
        FlowerWallVO vo = asset as FlowerWallVO;
        return (
            vo.id == id &&
            vo.assetId == assetId
            );
    }

    public override AssetVO Clone()
    {
        FlowerWallVO vo = new FlowerWallVO();
        vo.id = id;
        vo.assetId = assetId;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Frame";
            code += " assetId = " + GetPropertyString(assetId);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            assetId = code.Attributes["assetId"].Value;
        }
    }
}
