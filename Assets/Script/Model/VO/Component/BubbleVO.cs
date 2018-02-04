using UnityEngine;
using System.Collections;
using System.Xml;

public class BubbleVO : ComponentVO
{
    public float size = 0.03f;
    public float speed = 1f;
    public float density = .5f;
    public float directionX = 1f;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as BubbleVO).id;
        size = (asset as BubbleVO).size;
        speed = (asset as BubbleVO).speed;
        density = (asset as BubbleVO).density;
        directionX = (asset as BubbleVO).directionX;
    }

    override public bool Equals(AssetVO asset)
    {
        BubbleVO vo = asset as BubbleVO;
        return (
            vo.size == size &&
            vo.density == density &&
            vo.directionX == directionX &&
            vo.speed == speed
            );
    }

    public override AssetVO Clone()
    {
        BubbleVO vo = new BubbleVO();
        vo.id = id;
        vo.size = size;
        vo.speed = speed;
        vo.density = density;
        vo.directionX = directionX;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Bubble";
            code += " size = " + GetPropertyString(size);
            code += " speed = " + GetPropertyString(speed);
            code += " density = " + GetPropertyString(density);
            code += " directionX = " + GetPropertyString(directionX);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            size = float.Parse(code.Attributes["size"].Value);
            speed = float.Parse(code.Attributes["speed"].Value);
            speed = float.Parse(code.Attributes["density"].Value);
            directionX = float.Parse(code.Attributes["directionX"].Value);
        }
    }
}
