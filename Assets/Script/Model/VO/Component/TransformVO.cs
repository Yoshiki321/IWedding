using UnityEngine;
using System.Collections;
using System.Xml;

public class TransformVO : ComponentVO
{
    public float rotateX = 0f;
    public float rotateY = 0f;
    public float rotateZ = 0f;

    public float scaleX = 1f;
    public float scaleY = 1f;
    public float scaleZ = 1f;

    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    override public void FillFromObject(ComponentVO asset)
    {
        id = asset.id;
        rotateX = (asset as TransformVO).rotateX;
        rotateY = (asset as TransformVO).rotateY;
        rotateZ = (asset as TransformVO).rotateZ;
        scaleX = (asset as TransformVO).scaleX;
        scaleY = (asset as TransformVO).scaleY;
        scaleZ = (asset as TransformVO).scaleZ;
        x = (asset as TransformVO).x;
        y = (asset as TransformVO).y;
        z = (asset as TransformVO).z;
    }

    override public bool Equals(AssetVO asset)
    {
        TransformVO vo = asset as TransformVO;
        return (
            vo.rotateX == rotateX &&
            vo.rotateY == rotateY &&
            vo.rotateZ == rotateZ &&
            vo.scaleX == scaleX &&
            vo.scaleY == scaleY &&
            vo.scaleZ == scaleZ &&
            vo.x == x &&
            vo.y == y &&
            vo.z == z
            );
    }

    public override AssetVO Clone()
    {
        TransformVO vo = new TransformVO();
        vo.id = id;
        vo.rotateX = rotateX;
        vo.rotateY = rotateY;
        vo.rotateZ = rotateZ;
        vo.scaleX = scaleX;
        vo.scaleY = scaleY;
        vo.scaleZ = scaleZ;
        vo.x = x;
        vo.y = y;
        vo.z = z;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Transform";
            code += " x = " + GetPropertyString(x);
            code += " y = " + GetPropertyString(y);
            code += " z = " + GetPropertyString(z);
            code += " scaleX = " + GetPropertyString(scaleX);
            code += " scaleY = " + GetPropertyString(scaleY);
            code += " scaleZ = " + GetPropertyString(scaleZ);
            code += " rotateX = " + GetPropertyString(rotateX);
            code += " rotateY = " + GetPropertyString(rotateY);
            code += " rotateZ = " + GetPropertyString(rotateZ);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            x = float.Parse(code.Attributes["x"].Value);
            y = float.Parse(code.Attributes["y"].Value);
            z = float.Parse(code.Attributes["z"].Value);
            scaleX = float.Parse(code.Attributes["scaleX"].Value);
            scaleY = float.Parse(code.Attributes["scaleY"].Value);
            scaleZ = float.Parse(code.Attributes["scaleZ"].Value);
            rotateX = float.Parse(code.Attributes["rotateX"].Value);
            rotateY = float.Parse(code.Attributes["rotateY"].Value);
            rotateZ = float.Parse(code.Attributes["rotateZ"].Value);
        }
    }
}
