using UnityEngine;
using System.Collections;
using System.Xml;
using Build3D;
using System.Collections.Generic;

public class RelationVO : ComponentVO
{
    public string itemId = "";
    public List<GameObject> relationItems = new List<GameObject>();

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as RelationVO).id;
        itemId = (asset as RelationVO).itemId;
    }

    override public bool Equals(AssetVO asset)
    {
        RelationVO vo = asset as RelationVO;
        return (
            vo.itemId == itemId
            );
    }

    public override AssetVO Clone()
    {
        RelationVO vo = new RelationVO();
        vo.id = id;
        vo.itemId = itemId;
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Relation";
            code += " itemId = " + GetPropertyString(itemId);
            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            itemId = code.Attributes["itemId"].Value;
        }
    }
}
