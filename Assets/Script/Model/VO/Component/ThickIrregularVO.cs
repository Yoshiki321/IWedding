using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;

public class ThickIrregularVO : ComponentVO
{
    public string url = "";
    public XmlNode xml;

    override public void FillFromObject(ComponentVO asset)
    {
        url = (asset as ThickIrregularVO).url;
        id = (asset as ThickIrregularVO).id;
        xml = (asset as ThickIrregularVO).xml.Clone();
    }

    override public bool Equals(AssetVO asset)
    {
        ThickIrregularVO vo = asset as ThickIrregularVO;
        return (
            vo.url == url
            );
    }

    public override AssetVO Clone()
    {
        ThickIrregularVO vo = new ThickIrregularVO();
        vo.id = id;
        vo.url = url;
        if (xml != null) vo.xml = xml.Clone();
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<ThickIrregular";
            code += " url = " + GetPropertyString(url);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            url = code.Attributes["url"].Value;
        }
    }
}
