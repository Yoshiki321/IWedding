using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class SprinkleVO : ComponentVO
{
    public List<GameObject> list = new List<GameObject>();
    public XmlNode xml;

    public string sprinkleCode = "";
    public List<SprinkleData> dataList = new List<SprinkleData>();

    public float count = 5;

    public string itemId = "1001";

    public void SetData(SprinkleData data)
    {
        dataList.Add(data);
    }

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as SprinkleVO).id;
        itemId = (asset as SprinkleVO).itemId;

        dataList = new List<SprinkleData>();
        SprinkleVO sprinkleVO = (asset as SprinkleVO);
        count = sprinkleVO.count;
        foreach (SprinkleData data in sprinkleVO.dataList)
        {
            dataList.Add(data.Clone());
        }
    }

    override public bool Equals(AssetVO asset)
    {
        SprinkleVO vo = asset as SprinkleVO;

        return (
            dataList.Count == vo.dataList.Count &&
            count == vo.count &&
            itemId == vo.itemId
            );
    }

    public override AssetVO Clone()
    {
        SprinkleVO vo = new SprinkleVO();
        vo.id = id;
        vo.count = count;
        vo.itemId = itemId;

        foreach (SprinkleData data in dataList)
        {
            vo.dataList.Add(data.Clone());
        }
        return vo;
    }

    override public object Code
    {
        get
        {
            string code = "";
            code += "<Sprinkle";
            code += " id = " + GetPropertyString(id);
            code += " count = " + GetPropertyString(count);
            code += " data = ";

            code += '"';
            foreach (GameObject obj in list)
            {
                code += obj.name + "," + obj.transform.position.x + "," + obj.transform.position.y + "," + obj.transform.position.z + "," +
                     obj.transform.rotation.eulerAngles.x + "," + obj.transform.rotation.eulerAngles.y + "," + obj.transform.rotation.eulerAngles.z + ";";
            }
            code += '"';

            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
            id = code.GetString("id");
            count = code.GetFloat("count");
            sprinkleCode = code.GetString("data");
        }
    }
}


public class SprinkleData
{
    public string id;
    public string itemId;
    public List<Vector3> points = new List<Vector3>();
    public List<Vector3> rotations = new List<Vector3>();

    public string ToCode()
    {
        return "";
    }

    public bool Equals(SprinkleData data)
    {
        if (id != data.id) return false;
        if (points.Count != data.points.Count) return false;
        if (rotations.Count != data.rotations.Count) return false;
        if (itemId != data.itemId) return false;
        return true;
    }

    public SprinkleData Clone()
    {
        SprinkleData data = new SprinkleData();
        data.id = id;
        data.itemId = itemId;
        foreach (Vector3 v in points)
        {
            data.points.Add(v.Clone());
        }
        foreach (Vector3 v in rotations)
        {
            data.rotations.Add(v.Clone());
        }
        return data;
    }
}
