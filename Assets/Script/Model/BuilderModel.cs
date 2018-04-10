using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;
using Build3D;
using System.Xml;
using BuildManager;

public class BuilderModel : Actor<BuilderModel>
{
    public void Remove(string id)
    {
        RemoveSurface(id);
    }

    private float _height = 8;

    public LineVO CreateLineVO(Vector2 from, Vector2 to, float thickness, CollageVO alongCollage = null, CollageVO inverseCollage = null)
    {
        LineVO linevo = new LineVO();

        linevo.id = NumberUtils.GetGuid();

        linevo.from = from;
        linevo.to = to;

        linevo.thickness = thickness;

        linevo.height = _height;

        return linevo;
    }

    public LineStruct CreateLine(LineVO linevo)
    {
        GameObject _line = new GameObject();
        Line2D line = _line.AddComponent<Line2D>();

        GameObject _from = new GameObject();
        Node2D fromNode = _from.AddComponent<Node2D>();
        fromNode.type = "from";
        fromNode.line = line;
        fromNode.x = linevo.from.x;
        fromNode.y = linevo.from.y;

        _from.name = "from " + linevo.id;
        _from.transform.parent = _line.transform;

        GameObject _to = new GameObject();
        Node2D toNode = _to.AddComponent<Node2D>();
        toNode.type = "to";
        toNode.line = line;
        toNode.x = linevo.to.x;
        toNode.y = linevo.to.y;

        _to.name = "to " + linevo.id;
        _to.transform.parent = _line.transform;

        line.fromNode = fromNode;
        line.toNode = toNode;

        line.Init(linevo);
        line.UpdateVO();

        GameObject _line3 = new GameObject();
        Line3D line3 = _line3.AddComponent<Line3D>();
        line3.Instantiate(linevo);

        LineStruct data = new LineStruct();
        data.line = line;
        data.line3 = line3;
        data.vo = linevo;

        lineDatas.Add(data);

        _line.transform.parent = SceneManager.Instance.Graphics2D.LineLayer.transform;
        _line3.transform.parent = SceneManager.Instance.Graphics3D.LineLayer.transform;

        _from.transform.parent = _line.transform;
        _to.transform.parent = _line.transform;

        _line.transform.localPosition = new Vector3(_line.transform.position.x, _line.transform.position.y, 0);

        return data;
    }

    public void Clear()
    {
        foreach (LineStruct data in lineDatas)
        {
            data.line.Dispose();
            data.line3.Dispose();
        }

        foreach (SurfaceStruct data in surfaceDatas)
        {
            data.surface.Dispose();
            data.surface3.Dispose();
        }

        lineDatas = new List<LineStruct>();
        surfaceDatas = new List<SurfaceStruct>();
    }

    public SurfaceVO CreateSurfaceVO(List<LineVO> lines, string name)
    {
        SurfaceVO surfaceVO = new SurfaceVO();
        surfaceVO.id = NumberUtils.GetGuid();
        surfaceVO.height = _height;
        surfaceVO.linesVO = lines;
        surfaceVO.surfaceName = name;
        return surfaceVO;
    }

    public SurfaceStruct CreateSurface(SurfaceVO surfaceVO)
    {
        foreach (LineVO vo in surfaceVO.linesVO)
        {
            if (GetLineData(vo.id) == null)
            {
                CreateLine(vo);
            }
        }

        GameObject surfaceObject2 = new GameObject();
        Surface2D surface = surfaceObject2.AddComponent<Surface2D>();
        surface.Init(surfaceVO);

        GameObject surfaceObject3 = new GameObject();
        Surface3D surface3 = surfaceObject3.AddComponent<Surface3D>();
        surface3.Init(surfaceVO);

        SurfaceStruct data = new SurfaceStruct();
        data.surface = surface;
        data.surface3 = surface3;
        data.vo = surfaceVO;

        surfaceDatas.Add(data);

        surface.transform.parent = SceneManager.Instance.Graphics2D.SurfaceLayer.transform;
        surface3.transform.parent = SceneManager.Instance.Graphics3D.SurfaceLayer.transform;

        UpdatedDepth();
        UpdateSurface2(surface);
        return data;
    }

    public void CreateLineByCode(XmlNodeList xml)
    {
        foreach (XmlNode node in xml)
        {
            LineVO vo = new LineVO();
            vo.Code = node;
            CreateLine(vo);
        }
    }

    public void CreateSurfaceByCode(XmlNodeList xml)
    {
        foreach (XmlNode node in xml)
        {
            SurfaceVO vo = new SurfaceVO();
            vo.Code = node;
            CreateSurface(vo);
        }
    }

    public List<SurfaceStruct> surfaceDatas { get; private set; } = new List<SurfaceStruct>();
    public List<LineStruct> lineDatas { get; private set; } = new List<LineStruct>();

    public List<LineStruct> GetLineDataToPoint(Vector2 f, Vector2 t)
    {
        List<LineStruct> rl = new List<LineStruct>();
        for (int i = 0; i < lineDatas.Count; i++)
        {
            if ((lineDatas[i].vo.from.Equals(f) && lineDatas[i].vo.to.Equals(t)) ||
                (lineDatas[i].vo.from.Equals(t) && lineDatas[i].vo.to.Equals(f)))
            {
                rl.Add(lineDatas[i]);
            }
        }
        return rl;
    }

    public List<Node2D> nodes
    {
        get
        {
            List<Node2D> ns = new List<Node2D>();
            foreach (LineStruct l in lineDatas)

            {
                ns.Add(l.line.fromNode);
                ns.Add(l.line.toNode);
            }
            return ns;
        }
    }

    public List<Line2D> lines
    {
        get
        {
            List<Line2D> ls = new List<Line2D>();
            for (int i = 0; i < lineDatas.Count; i++)
            {
                ls.Add(lineDatas[i].line);
            }
            return ls;
        }
    }

    public List<Line3D> line3s
    {
        get
        {
            List<Line3D> ls = new List<Line3D>();
            for (int i = 0; i < lineDatas.Count; i++)
            {
                ls.Add(lineDatas[i].line3);
            }
            return ls;
        }
    }

    public List<Vector2> GetPoints(List<Node2D> arr = null)
    {
        List<Vector2> rp = new List<Vector2>();
        List<Node2D> hp = new List<Node2D>();

        if (arr != null)
        {
            ArrayUtils.CloneArray(arr, hp);
        }

        bool s;
        for (int i = 0; i < nodes.Count; i++)
        {
            s = true;
            for (int j = 0; j < hp.Count; j++)
            {
                if (nodes[i].point.Equals(hp[j].point))
                {
                    s = false;
                    break;
                }
            }
            if (s)
            {
                rp.Add(nodes[i].point);
                hp.Add(nodes[i]);
            }
        }
        return rp;
    }

    public List<Line2D> GetLines(List<Line2D> arr = null)
    {
        List<Line2D> ra = new List<Line2D>();
        for (int i = 0; i < lines.Count; i++)
        {
            Line2D line = lines[i];
            if (!ArrayUtils.Has(arr, line))
            {
                ra.Add(line);
            }
        }
        return ra;
    }

    public LineStruct GetLineData(string id)
    {
        for (int i = 0; i < lineDatas.Count; i++)
        {
            if (lineDatas[i].vo.id == id)
            {
                return lineDatas[i];
            }
        }
        return null;
    }

    public SurfaceStruct GetSurfaceData(string id)
    {
        for (int i = 0; i < surfaceDatas.Count; i++)
        {
            if (surfaceDatas[i].vo.id == id)
            {
                return surfaceDatas[i];
            }
        }
        return null;
    }

    public void RemoveLine(string id)
    {
        for (int i = 0; i < lineDatas.Count; i++)
        {
            if (lineDatas[i].vo.id == id)
            {
                lineDatas[i].line.Dispose();
                lineDatas[i].line3.Dispose();
                lineDatas.Remove(lineDatas[i]);
            }
        }
    }

    public void RemoveSurface(string id)
    {
        for (int i = 0; i < surfaceDatas.Count; i++)
        {
            if (surfaceDatas[i].vo.id == id)
            {
                foreach (LineVO vo in surfaceDatas[i].vo.linesVO)
                {
                    RemoveLine(vo.id);
                }

                surfaceDatas[i].surface.Dispose();
                surfaceDatas[i].surface3.Dispose();
                surfaceDatas.Remove(surfaceDatas[i]);
            }
        }
    }

    public void UpdatedDepth()
    {
        for (int i = 0; i < surfaceDatas.Count; i++)
        {
            var v = surfaceDatas[i].surface.transform.position;
            v.z = (i + 1) * -.01f;
            surfaceDatas[i].surface.transform.position = v;
        }
    }

    public void UpdateLine2(Line2D line)
    {
        LineStruct lineData = GetLineData(line.VO.id);
        lineData.line.UpdateVO();
        lineData.line3.VO = line.VO as LineVO;
    }

    public void UpdateSurface2(Surface2D surface)
    {
        foreach (Line2D l in surface.lines)
        {
            UpdateLine2(l);
        }

        SurfaceStruct surfaceData = GetSurfaceData(surface.VO.id);
        surfaceData.surface.UpdateVO();
        surfaceData.surface3.VO = surface.VO as SurfaceVO;
    }

    public void SetLinesTransparent(bool b)
    {
        foreach (Line3D l in line3s)
        {
            l.transparent = b;
            l.UpdateCollage();
        }
    }

    #region Nested

    public List<NestedStruct> nestedDatas { get; private set; } = new List<NestedStruct>();

    public void CreateNested(XmlNodeList xml)
    {
        foreach (XmlNode node in xml)
        {
            NestedVO vo = new NestedVO();
            vo.Code = node;
            CreateNested(vo);
        }
    }

    public NestedStruct CreateNested(NestedVO nestedvo, bool save = true)
    {
        GameObject _nested = new GameObject();
        Nested2D nested2d = _nested.AddComponent<Nested2D>();
        nested2d.parent = SceneManager.Instance.Graphics2D.NestedLayer.transform;
        nested2d.Instantiate(nestedvo);

        GameObject _nested3 = new GameObject();
        _nested3.transform.parent = SceneManager.Instance.Graphics3D.NestedLayer.transform;
        Nested3D nested3d = _nested3.AddComponent<Nested3D>();
        nested3d.Instantiate(nestedvo);

        nestedvo.name = nestedvo.assetId.ToString();

        nested2d.height3D = nested3d.sizeY;
        nested2d.width = nested3d.sizeX;
        nested2d.widthZ = nested3d.sizeZ;

        NestedStruct data = new NestedStruct();
        data.nested2 = nested2d;
        data.nested3 = nested3d;
        data.vo = nestedvo;

        if (save)
        {
            nestedDatas.Add(data);
        }

        return data;
    }

    public NestedStruct GetNestedData(string id)
    {
        foreach (NestedStruct objectData in nestedDatas)
        {
            if (objectData.vo.id == id)
            {
                return objectData;
            }
        }
        return null;
    }

    public void RemoveNested(NestedStruct data)
    {
        Nested2D nested = data.nested2 as Nested2D;
        Nested3D nested3 = data.nested3 as Nested3D;

        nested.Dispose();
        nested3.Dispose();
    }

    #endregion
}

public class LineStruct
{
    public Line2D line;
    public Line3D line3;
    public LineVO vo;

    public string id
    {
        get { return vo.id; }
    }
}

public class SurfaceStruct
{
    public Surface2D surface;
    public Surface3D surface3;
    public SurfaceVO vo;

    public string id
    {
        get { return vo.id; }
    }
}

public class NestedStruct
{
    public Nested2D nested2;
    public Nested3D nested3;
    public NestedVO vo;
    public string id { get { return vo.id; } }
}