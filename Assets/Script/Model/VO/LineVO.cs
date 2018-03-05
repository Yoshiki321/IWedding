using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LineVO : AssetVO
{
    public float thickness;

    public float height;
    public List<HoleVO> holes = new List<HoleVO>();
    public bool interiorAlong;

    public Vector2 from;
    public Vector2 to;
    public Vector2 afrom;
    public Vector2 ato;
    public Vector2 ifrom;
    public Vector2 ito;

    override public bool Equals(AssetVO asset)
    {
        return (base.Equals(asset) &&
        this.from.Equals((asset as LineVO).from) &&
        this.to.Equals((asset as LineVO).to) &&
        ArrayUtils.Equals((asset as LineVO).holes, holes) &&
        this.thickness == (asset as LineVO).thickness &&
        this.afrom.Equals((asset as LineVO).afrom) &&
        this.ato.Equals((asset as LineVO).ato) &&
        this.ifrom.Equals((asset as LineVO).ifrom) &&
        this.ito.Equals((asset as LineVO).ito) &&
        EqualsComponentVO(asset));
    }

    override public AssetVO Clone()
    {
        LineVO l = new LineVO();
        l.FillFromLine(this);
        return l;
    }

    public void FillFromLine(LineVO asset)
    {
        this.from = asset.from.Clone();
        this.to = asset.to.Clone();
        this.thickness = asset.thickness;
        this.id = asset.id;
        this.interiorAlong = asset.interiorAlong;
        this.height = asset.height;

        this.holes = new List<HoleVO>();
        for (int i = 0; i < asset.holes.Count; i++)
        {
            this.holes.Add(asset.holes[i].Clone());
        }

        this.afrom = asset.afrom.Clone();
        this.ato = asset.ato.Clone();
        this.ifrom = asset.ifrom.Clone();
        this.ito = asset.ito.Clone();

        componentVOList = new List<ComponentVO>();
        for (int i = 0; i < asset.componentVOList.Count; i++)
        {
            componentVOList.Add(asset.componentVOList[i].Clone() as ComponentVO);
        }
    }

    override public XmlNode Code
    {
        get
        {
            string code = "";
            code += "<Line";
            code += " from = " + from.GetCode();
            code += " to = " + to.GetCode();
            code += " thickness = " + GetPropertyString(thickness);
            code += " id = " + GetPropertyString(id);
            code += " interiorAlong = " + GetBoolString(interiorAlong);
            code += " height = " + GetPropertyString(height);
            code += " afrom = " + afrom.GetCode();
            code += " ato = " + ato.GetCode();
            code += " ifrom = " + ifrom.GetCode();
            code += " ito = " + ito.GetCode();
            code += ">";

            //foreach (HoleVO vo in holes)
            //{
            //    code += vo.Code.OuterXml;
            //}

            foreach (ComponentVO vo in componentVOList)
            {
                code += vo.Code;
            }

            code += "</Line>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
        set
        {
            XmlNode code = value;
            from = from.SetCode(code.Attributes["from"].Value);
            to = to.SetCode(code.Attributes["to"].Value);
            thickness = float.Parse(code.Attributes["thickness"].Value);
            id = code.Attributes["id"].Value;
            interiorAlong = (code.Attributes["interiorAlong"].Value == "1") ? true : false;
            height = float.Parse(code.Attributes["height"].Value);
            afrom = afrom.SetCode(code.Attributes["afrom"].Value);
            ato = ato.SetCode(code.Attributes["ato"].Value);
            ifrom = ifrom.SetCode(code.Attributes["ifrom"].Value);
            ito = ito.SetCode(code.Attributes["ito"].Value);

            //XmlNodeList holesNodes = code.SelectNodes("Hole");
            //if (holesNodes != null)
            //{
            //    foreach (XmlNode holesXml in holesNodes)
            //    {
            //        HoleVO hvo = new HoleVO();
            //        hvo.Code = holesXml;
            //        holes.Add(hvo);
            //    }
            //}

            code = code.SelectSingleNode("Collage");
            if (code != null) AddComponentVO<CollageVO>().Code = code;
        }
    }
}
