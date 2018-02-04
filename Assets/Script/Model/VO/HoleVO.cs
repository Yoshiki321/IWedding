using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class HoleVO
{
    protected string GetPropertyString(object obj)
    {
        return '"' + obj.ToString() + '"';
    }

    public HoleVO Clone()
    {
        HoleVO holeData = new HoleVO();
        holeData.holeAlongX = holeAlongX;
        holeData.holeInverseX = holeInverseX;
        holeData.holeWidth = holeWidth;
        holeData.holeHeight = holeHeight;
        holeData.holeType = holeType;
        holeData.holeX = holeX;
        holeData.holeY = holeY;
        holeData.nestedId = nestedId;
        return holeData;
    }

    public XmlNode Code
    {
        get
        {
            string code = "";
            code += "<Hole";
            code += " holeAlongX = " + GetPropertyString(holeAlongX);
            code += " holeInverseX = " + GetPropertyString(holeInverseX);
            code += " holeWidth = " + GetPropertyString(holeWidth);
            code += " holeHeight = " + GetPropertyString(holeHeight);
            code += " holeType = " + GetPropertyString(holeType);
            code += " holeX = " + GetPropertyString(holeX);
            code += " holeY = " + GetPropertyString(holeY);
            code += " nestedId = " + GetPropertyString(nestedId);
            code += "/>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
        set
        {
            XmlNode code = value;
            holeAlongX = float.Parse(code.Attributes["holeAlongX"].Value);
            holeInverseX = float.Parse(code.Attributes["holeInverseX"].Value);
            holeWidth = float.Parse(code.Attributes["holeWidth"].Value);
            holeHeight = float.Parse(code.Attributes["holeHeight"].Value);
            holeType = float.Parse(code.Attributes["holeType"].Value);
            holeX = float.Parse(code.Attributes["holeX"].Value);
            holeY = float.Parse(code.Attributes["holeY"].Value);
            nestedId = code.Attributes["nestedId"].Value;
        }
    }

    public string nestedId;

    private float _holeWidth;

    public float holeWidth
    {
        set { _holeWidth = value; }
        get { return _holeWidth; }

    }

    private float _holeHeight;

    public float holeHeight
    {
        set { _holeHeight = value; }
        get { return _holeHeight; }
    }

    private float _holeAlongX;

    public float holeAlongX
    {

        set { _holeAlongX = value; }
        get { return _holeAlongX; }
    }

    private float _holeInverseX;

    public float holeInverseX
    {
        set { _holeInverseX = value; }
        get { return _holeInverseX; }
    }

    public float holeAlongX3
    {
        get { return holeAlongX - holeWidth / 2; }
    }

    public float holeInverseX3
    {
        get { return holeInverseX - holeWidth / 2; }
    }

    private float _holeType;

    public float holeType
    {
        set { _holeType = value; }
        get { return _holeType; }
    }

    private float _holeY;

    public float holeY
    {
        set { _holeY = value; }
        get { return _holeY; }
    }

    private float _holeX;

    public float holeX
    {
        set { _holeX = value; }
        get { return _holeX; }
    }
}
