using UnityEngine;
using System.Collections;
using System.Xml;
using BuildManager;
using System.Collections.Generic;

public class CurvyColumnVO : ComponentVO
{
    public List<Vector3> points;

    override public void FillFromObject(ComponentVO asset)
    {
    }

    override public bool Equals(AssetVO asset)
    {
        CurvyColumnVO vo = asset as CurvyColumnVO;
        return (
            true
            );
    }

    public override AssetVO Clone()
    {
        CurvyColumnVO vo = new CurvyColumnVO();
        vo.id = id;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Frame";
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
        }
    }
}
