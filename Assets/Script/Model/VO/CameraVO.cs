using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CameraVO : ObjectVO
{
    override public AssetVO Clone()
    {
        CameraVO vo = new CameraVO();
        vo.FillFromObject(this);
        return vo;
    }

    override public XmlNode Code
    {
        get
        {
            string code = "";
            code += "<Camera";
            code += " id = " + GetPropertyString(id);
            code += ">";

            foreach (ComponentVO vo in componentVOList)
            {
                code += vo.Code;
            }

            code += "</Camera>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
        set
        {
            XmlNode code = value;
            id = code.Attributes["id"].Value;

            XmlNode xml = code.SelectSingleNode("EditorCamera");
            GetComponentVO<EditorCameraVO>().Code = xml;
        }
    }
}
