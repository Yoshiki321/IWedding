using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;

public class FrameVO : ComponentVO
{
    public string xmlUrl = "";
    public string url = "";
    public Texture2D texture2;
    public Sprite sprite;

    override public void FillFromObject(ComponentVO asset)
    {
        url = (asset as FrameVO).url;
        id = (asset as FrameVO).id;
    }

    override public bool Equals(AssetVO asset)
    {
        FrameVO vo = asset as FrameVO;
        return (
            vo.url == url
            );
    }

    public override AssetVO Clone()
    {
        FrameVO vo = new FrameVO();
        vo.id = id;
        vo.url = url;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Frame";
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
