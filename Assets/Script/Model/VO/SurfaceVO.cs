using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;
using System.Xml;

public class SurfaceVO : AssetVO
{
    public float height;
    public string surfaceName;

    private List<LineVO> _linesVO = new List<LineVO>();

    private List<Vector2> _points;

    public List<LineVO> linesVO
    {
        set
        {
            _linesVO = value;

            Update();
        }
        get { return _linesVO; }
    }

    public List<Vector2> points
    {
        get { return _points; }
    }

    private List<LineVO> _tempList;
    private List<LineVO> _drawTempList;

    public void Update()
    {
        _tempList = new List<LineVO>();
        _drawTempList = new List<LineVO>();

        ArrayUtils.CloneArray(_linesVO, _tempList);
        ArrayUtils.CloneArray(_linesVO, _drawTempList);

        _points = new List<Vector2>();
        _points.Add(_drawTempList[0].from.Clone());

        Vector2 to = new Vector2();
        to = to.Null();

        for (int i = 1; i < _drawTempList.Count; i++)
        {
            if (to.IsNull())
            {
                _tempList.Remove(_drawTempList[0]);
                to = _drawTempList[0].to;
            }
            else
            {
                ArrayList t = GetFrom(to);
                if (t != null)
                {
                    to = (Vector2)t[0];
                    _tempList.Remove((LineVO)t[1]);
                }
                else
                {
                    to.Null();
                }
            }
            if (to != null)
            {
                if (!ArrayUtils.HasVector2(_points, to))
                {
                    _points.Add(to.Clone());
                }
            }
            else
            {
                if (!ArrayUtils.HasVector2(_points, _drawTempList[0].from))
                {
                    _points.Add(_drawTempList[0].from.Clone());
                }
            }
        }
    }

    private ArrayList GetFrom(Vector2 p)
    {
        ArrayList list = new ArrayList();
        foreach (LineVO vo in _tempList)
        {
            if (vo.from.Equals(p))
            {
                list.Add(vo.to);
                list.Add(vo);
                return list;
            }
            if (vo.to.Equals(p))
            {
                list.Add(vo.from);
                list.Add(vo);
                return list;
            }
        }
        return null;
    }

    override public bool Equals(AssetVO asset)
    {
        return (EqualsSurface((SurfaceVO)asset));
    }

    public bool EqualsSurface(SurfaceVO asset)
    {
        return (base.Equals(asset) &&
        this.height == asset.height &&
        this.surfaceName == asset.surfaceName &&
        EqualsLine(asset));
    }

    private bool EqualsLine(SurfaceVO asset)
    {
        if (asset.linesVO.Count != linesVO.Count)
        {
            return false;
        }

        for (int i = 0;i < asset.linesVO.Count;i++)
        {
            if(!asset.linesVO[i].Equals(linesVO[i]))
            {
                return false;
            }
        }

        return true;
    }

    override public AssetVO Clone()
    {
        SurfaceVO l = new SurfaceVO();
        l.FillFromSurface(this);
        return (AssetVO)l;
    }

    public void FillFromSurface(SurfaceVO asset)
    {
        id = asset.id;
        height = asset.height;
        surfaceName = asset.surfaceName;

        _linesVO = new List<LineVO>();

        for (int i = 0; i < asset.linesVO.Count; i++)
        {
            _linesVO.Add(asset.linesVO[i].Clone() as LineVO);
        }

        componentVOList = new List<ComponentVO>();
        for (int i = 0; i < asset.componentVOList.Count; i++)
        {
            componentVOList.Add(asset.componentVOList[i].Clone() as ComponentVO);
        }

        Update();
    }

    override public XmlNode Code
    {
        get
        {
            string code = "";
            code += "<Surface ";
            code += "id = " + GetPropertyString(id);
            code += " height = " + GetPropertyString(height);
            code += " surfaceName = " + GetPropertyString(surfaceName);

            code += " lines = ";
            code += '"';
            foreach (LineVO linevo in _linesVO)
            {
                code += linevo.id + ",";
            }
            code = code.Substring(0, code.Length - 1);
            code += '"';
            code += ">";

            foreach (ComponentVO vo in componentVOList)
            {
                code += vo.Code;
            }

            code += "</Surface>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
        set
        {
            XmlNode code = value;
            id = code.Attributes["id"].Value;
            height = float.Parse(code.Attributes["height"].Value);
            surfaceName = code.Attributes["surfaceName"].Value;

            List<LineVO> vos = new List<LineVO>();
            string lines = code.Attributes["lines"].Value;
            string[] lineIds = lines.Split(',');
            foreach (string id in lineIds)
            {
                vos.Add(BuilderModel.Instance.GetLineData(id).vo);
            }

            linesVO = vos;

            code = code.SelectSingleNode("Collage");
            if (code != null) AddComponentVO<CollageVO>().Code = code;

            Update();
        }
    }
}
