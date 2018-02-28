using BuildManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ItemVO : ObjectVO
{
    public string groupId = "";

    override public AssetVO Clone()
    {
        ItemVO vo = new ItemVO();
        vo.FillFromObject(this);
        vo.groupId = groupId;
        return vo;
    }

    override public XmlNode Code
    {
        get
        {
            string code = "";
            code += "<Item";
            code += " id = " + GetPropertyString(id);
            code += " assetId = " + GetPropertyString(assetId);
            code += " groupId = " + GetPropertyString(groupId);
            code += ">";

            foreach (ComponentVO vo in componentVOList)
            {
                code += vo.Code;
            }

            code += "</Item>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
        set
        {
            XmlNode code = value;
            id = code.Attributes["id"].Value;

            assetId = code.Attributes["assetId"].Value;

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

            groupId = (code.Attributes["groupId"] != null) ? code.Attributes["groupId"].Value : "";

            XmlNode xml;
            xml = code.SelectSingleNode("Transform");
            if (xml != null) AddComponentVO<TransformVO>().Code = xml;

            xml = code.SelectSingleNode("Spotlight");
            if (xml != null) AddComponentVO<SpotlightVO>().Code = xml;

            xml = code.SelectSingleNode("MultipleSpotlight");
            if (xml != null) AddComponentVO<MultipleSpotlightVO>().Code = xml;

            xml = code.SelectSingleNode("BallLamp");
            if (xml != null) AddComponentVO<BallLampVO>().Code = xml;

            xml = code.SelectSingleNode("RadiationLamp");
            if (xml != null) AddComponentVO<RadiationLampVO>().Code = xml;

            xml = code.SelectSingleNode("TubeLight");
            if (xml != null) AddComponentVO<TubeLightVO>().Code = xml;

            xml = code.SelectSingleNode("Frame");
            if (xml != null) AddComponentVO<FrameVO>().Code = xml;

            xml = code.SelectSingleNode("Bubble");
            if (xml != null) AddComponentVO<BubbleVO>().Code = xml;

            xml = code.SelectSingleNode("Smoke");
            if (xml != null) AddComponentVO<SmokeVO>().Code = xml;

            xml = code.SelectSingleNode("Collage");
            if (xml != null) AddComponentVO<CollageVO>().Code    = xml;

            xml = code.SelectSingleNode("PointLight");
            if (xml != null) AddComponentVO<PointLightVO>().Code = xml;
            
            xml = code.SelectSingleNode("Relation");
            if (xml != null) AddComponentVO<RelationVO>().Code = xml;

            xml = code.SelectSingleNode("ThickIrregular");
            if (xml != null) AddComponentVO<ThickIrregularVO>().Code = xml;

            xml = code.SelectSingleNode("FlowerWall");
            if (xml != null) AddComponentVO<FlowerWallVO>().Code = xml;
            
            xml = code.SelectSingleNode("CurvyColumn");
            if (xml != null) AddComponentVO<CurvyColumnVO>().Code = xml;

            xml = code.SelectSingleNode("Sprinkle");
            if (xml != null) AddComponentVO<SprinkleVO>().Code = xml;
        }
    }

    protected IEnumerator LoadText(string url, Action<byte[]> cb)
    {
        WWW www = new WWW(url);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            cb.Invoke(www.bytes);
            www.Dispose();
        }
    }
}
