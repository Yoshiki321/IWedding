using UnityEngine;
using System.Collections;
using System.Xml;

public class BallLampVO : ComponentVO
{
    public string cookieId = "1001";

    public float rotateX = 1f;
    public float rotateY = 1f;
    public float rotateZ = 1f;

    public float range = 8;

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as BallLampVO).id;
        cookieId = (asset as BallLampVO).cookieId;
        rotateX = (asset as BallLampVO).rotateX;
        rotateY = (asset as BallLampVO).rotateY;
        rotateZ = (asset as BallLampVO).rotateZ;
        range = (asset as BallLampVO).range;
    }

    override public bool Equals(AssetVO asset)
    {
        BallLampVO vo = asset as BallLampVO;
        return (
            vo.cookieId == cookieId &&
            vo.rotateX == rotateX &&
            vo.rotateY == rotateY &&
            vo.rotateZ == rotateZ &&
            vo.range == range
            );
    }

    public override AssetVO Clone()
    {
        BallLampVO vo = new BallLampVO();
        vo.id = id;
        vo.cookieId = cookieId;
        vo.rotateX = rotateX;
        vo.rotateY = rotateY;
        vo.rotateZ = rotateZ;
        vo.range = range;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<BallLamp";
            code += " cookieId = " + GetPropertyString(cookieId);
            code += " rotateX = " + GetPropertyString(rotateX);
            code += " rotateY = " + GetPropertyString(rotateY);
            code += " rotateZ = " + GetPropertyString(rotateZ);
            code += " range = " + GetPropertyString(range);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            cookieId = code.GetString("cookieId");
            rotateX = code.GetFloat("rotateX");
            rotateY = code.GetFloat("rotateY");
            rotateZ = code.GetFloat("rotateZ");
            range = code.GetFloat("range");
        }
    }
}
