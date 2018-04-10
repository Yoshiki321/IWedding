using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;

public class PhotoWallVO : ComponentVO
{
    public string xmlUrl = "";
    public string url = "";
    public Texture2D texture2;
    public Sprite sprite;

    override public void FillFromObject(ComponentVO asset)
    {
        url = (asset as PhotoWallVO).url;
        id = (asset as PhotoWallVO).id;
    }

    override public bool Equals(AssetVO asset)
    {
        PhotoWallVO vo = asset as PhotoWallVO;
        return (
            vo.url == url
            );
    }

    public override AssetVO Clone()
    {
        PhotoWallVO vo = new PhotoWallVO();
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
            url = code.GetString("url");
        }
    }
}
