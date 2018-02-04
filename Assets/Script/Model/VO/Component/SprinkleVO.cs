using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class SprinkleVO : ComponentVO
{
    public List<GameObject> list = new List<GameObject>();
    public XmlNode xml;

    public string sprinkleCode;
    public List<SprinkleData> dataList = new List<SprinkleData>();

    public void SetData(SprinkleData data)
    {
        dataList.Add(data);
    }

    override public void FillFromObject(ComponentVO asset)
    {
        id = (asset as SprinkleVO).id;

        dataList = new List<SprinkleData>();
        SprinkleVO sprinkleVO = (asset as SprinkleVO);
        foreach (SprinkleData data in sprinkleVO.dataList)
        {
            dataList.Add(data.Clone());
        }
    }

    override public bool Equals(AssetVO asset)
    {
        SprinkleVO vo = asset as SprinkleVO;

        return (
            dataList.Count == vo.dataList.Count
            );
    }

    public override AssetVO Clone()
    {
        SprinkleVO vo = new SprinkleVO();
        vo.id = id;

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
            code += " data = ";

            foreach (GameObject obj in list)
            {
                code += obj.name + "," + obj.transform.position.x + "," + obj.transform.position.y + "," + obj.transform.position.z + "," +
                     obj.transform.rotation.eulerAngles.x + "," + obj.transform.rotation.eulerAngles.y + "," + obj.transform.rotation.eulerAngles.z + ";";
            }

            code += "/>";

            return code;
        }
        set
        {
            XmlNode code = value as XmlNode;
        }
    }
}


public class SprinkleData
{
    public string id;
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
        return true;
    }

    public SprinkleData Clone()
    {
        SprinkleData data = new SprinkleData();
        data.id = id;
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
