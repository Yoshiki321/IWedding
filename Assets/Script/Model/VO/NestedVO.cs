using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class NestedVO : ObjectVO
{
    public List<string> lines = new List<string>();

    public string lineId;

    override public AssetVO Clone()
    {
        NestedVO vo = new NestedVO();
        vo.FillFromObject(this);

        vo.lines = new List<string>();
        foreach (string id in this.lines)
        {
            vo.lines.Add(id);
        }

        vo.lineId = lineId;
        return vo;
    }

    override public XmlNode Code
    {
        get
        {
            string code = "";
            code += "<Nested";
            code += " id = " + GetPropertyString(id);
            code += " assetId = " + GetPropertyString(assetId);
            code += " lineId = " + GetPropertyString(lineId);
            code += ">";

            foreach (ComponentVO vo in componentVOList)
            {
                code += vo.Code;
            }

            code += "</Nested>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
        set
        {
            XmlNode code = value;
            id = code.Attributes["id"].Value;
            assetId = code.Attributes["assetId"].Value;
            lineId = code.Attributes["lineId"].Value;

            ItemData itemData = ItemManager.GetItemData(assetId);
            if (itemData != null)
            {
                modelId = itemData.model;
                topImgId = itemData.topImg;
            }
            else
            {
                model = new GameObject();
            }

            XmlNode transformXml = code.SelectSingleNode("Transform");
            if (transformXml != null) AddComponentVO<TransformVO>().Code = transformXml;
        }
    }
}
