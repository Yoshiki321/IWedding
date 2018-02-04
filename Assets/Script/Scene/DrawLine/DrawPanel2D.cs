using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;

/// <summary>
/// 将此组件添加到一个Canvas上开启绘制功能
/// </summary>
public class DrawPanel2D : DispatcherEventPanel
{
    private SurfacePlane3D _drawFillPanel;
    private Material _fillMaterial;

    void Start()
    {
        GameObject drawFillPanelObject = new GameObject();
        drawFillPanelObject.name = "drawFillPanel";
        _drawFillPanel = drawFillPanelObject.AddComponent<SurfacePlane3D>();
        _drawFillPanel.TwoSided = true;
        drawFillPanelObject.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        drawFillPanelObject.layer = LayerMask.NameToLayer("UI");
        drawFillPanelObject.transform.parent = transform;

        transform.GetComponent<Canvas>().planeDistance = 101;

        _fillMaterial = new Material(Shader.Find("Unlit/Texture"));
        //fillMaterial = new Material(Shader.Find("Standard"));
        _fillMaterial.SetTextureScale("_MainTex", new Vector2(0.01f, 0.01f));
    }

    public string id = "";
    private string _materialsUpID = "2010";
    private string _materialsDownID = "2010";

    /// <summary>
    /// 设置贴图
    /// </summary>
    /// <param name="id"></param>
    /// <param name="down"></param>
    public void SetMaterial(string id, string down = "")
    {
        _materialsUpID = id;
        if (down == "")
            _materialsDownID = id;
        else
            _materialsDownID = down;
        Material m = TexturesManager.CreateMaterials(id);
        Texture2D t = m.GetTexture("_MainTex") as Texture2D;
        _fillMaterial.SetTexture("_MainTex", t);

        _drawFillPanel.SetMaterial(_fillMaterial);
    }

    /// <summary>
    /// 清除
    /// </summary>
    public void ClearAll()
    {
        foreach (DrawLine line in _drawLines)
        {
            line.Dispose();
            Destroy(line.gameObject.transform.parent.gameObject);
        }

        _nodeLines = new List<DrawNode>();
        _drawLines = new List<DrawLine>();
    }

    /// <summary>
    /// 添加点的列表
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="thickness"></param>
    public void Draw(string nodes, float thickness)
    {
        List<List<Vector2>> list = new List<List<Vector2>>();

        string pointStr = nodes;
        string[] ss = pointStr.Split(';');

        foreach (string s in ss)
        {
            string[] sss = s.Split(',');
            list.Add(new List<Vector2>() {
                    new Vector2(float.Parse(sss[0]), float.Parse(sss[1])),
                    new Vector2(float.Parse(sss[2]), float.Parse(sss[3])),
                    new Vector2(float.Parse(sss[4]), float.Parse(sss[5]))
            });
        }

        this.thickness = thickness;

        ClearAll();

        foreach (List<Vector2> l in list)
        {
            AddPoint(l[0], l[1], l[2]);
        }

        SetMaterial(_materialsUpID);

        UpdateRelation();
        FillPanel();
    }

    private void UpdateRelation()
    {
        foreach (DrawNode node in _nodeLines)
        {
            foreach (DrawNode node1 in _nodeLines)
            {
                if (node != node1 &&
                    node.transform.position.x == node1.transform.position.x &&
                    node.transform.position.y == node1.transform.position.y)
                {
                    node.relationNode = node1;
                    node1.relationNode = node;
                    break;
                }
            }
        }
    }

    private List<DrawNode> _nodeLines = new List<DrawNode>();
    private List<DrawLine> _drawLines = new List<DrawLine>();

    /// <summary>
    /// 添加点
    /// </summary>
    /// <param name="f"></param>
    /// <param name="t"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public DrawLine AddPoint(Vector2 f, Vector2 t, Vector2 c)
    {
        GameObject drawLineObject = new GameObject();
        drawLineObject.transform.parent = transform;
        drawLineObject.name = "DrawLineObject";

        GameObject line = new GameObject();
        line.transform.parent = drawLineObject.transform;
        DrawLine drawLine = line.AddComponent<DrawLine>();
        drawLine.SetPoint(f, t, c);

        drawLine.nodeFrom.line = drawLine;
        drawLine.nodeTo.line = drawLine;
        drawLine.nodeCurve.line = drawLine;
        _drawLines.Add(drawLine);
        _nodeLines.Add(drawLine.nodeFrom);
        _nodeLines.Add(drawLine.nodeTo);

        drawLine.AddDown(LineDownHandle);
        drawLine.AddUp(LineUpHandle);
        drawLine.AddRightClick(LineRightClickHandle);
        drawLine.nodeFrom.AddDown(NodeDownHandle);
        drawLine.nodeTo.AddDown(NodeDownHandle);
        drawLine.nodeCurve.AddDown(NodeDownHandle);
        drawLine.nodeFrom.AddUp(NodeUpHandle);
        drawLine.nodeTo.AddUp(NodeUpHandle);
        drawLine.nodeCurve.AddUp(NodeUpHandle);

        drawLine.nodeFrom.transform.position += new Vector3(0, 0, -1);
        drawLine.nodeTo.transform.position += new Vector3(0, 0, -1);
        drawLine.nodeCurve.transform.position += new Vector3(0, 0, -1);

        return drawLine;
    }

    public float TilingX
    {
        set { _drawFillPanel.tilingX = value; }
        get { return _drawFillPanel.tilingX; }
    }

    public float TilingY
    {
        set { _drawFillPanel.tilingY = value; }
        get { return _drawFillPanel.tilingY; }
    }

    public float OffestX
    {
        set { _drawFillPanel.offestX = value; }
        get { return _drawFillPanel.offestX; }
    }

    public float OffestY
    {
        set { _drawFillPanel.offestY = value; }
        get { return _drawFillPanel.offestY; }
    }

    private void FillPanel()
    {
        _drawFillPanel.BuildIrregularGeometry(Triangulator.GetMeshData(GetPanelPoints()));
        _drawFillPanel.SetMaterial(_fillMaterial);
    }

    public void ResetCurve(DrawLine line)
    {
        line.ResetCurve();
        FillPanel();
    }

    private Vector3 _lastMousePoint;
    private DrawNode _drawNode;

    private DrawNode startNode;
    private DrawNode endNode;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePoint = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_drawLine)
            {
                _drawLine.UpdateCollider();
                _drawLine = null;
            }

            if (_drawNode)
            {
                _drawNode.line.UpdateCollider();
                if (_drawNode.relationNode != null)
                {
                    _drawNode.relationNode.line.UpdateCollider();
                }
                _drawNode = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(GetNodeCode());
        }
    }

    public void AddLine(DrawLine line)
    {
        Vector3 endNode = line.nodeTo.transform.position;
        Vector3 mv = Vector3.Lerp(line.nodeFrom.transform.position, line.nodeTo.transform.position, .5f);
        line.MovePoint(line.nodeFrom.transform.position, mv, Vector3.Lerp(line.nodeFrom.transform.position, mv, .5f));
        DrawLine drawLine = AddPoint(mv, endNode, Vector3.Lerp(mv, endNode, .5f));
        drawLine.UpdateCollider();
        drawLine.nodeFrom.line.UpdateCollider();
        drawLine.nodeTo.line.UpdateCollider();
        UpdateRelation();
        FillPanel();
    }

    public void RemoveLine(DrawLine line)
    {
        line.nodeFrom.relationNode.Move(line.nodeTo.relationNode.transform.position);

        _nodeLines.Remove(line.nodeFrom);
        _nodeLines.Remove(line.nodeTo);
        _drawLines.Remove(line);
        line.Dispose();
        UpdateRelation();
        FillPanel();
    }

    private List<Vector2> GetPanelPoints()
    {
        List<Vector2> list = new List<Vector2>();

        startNode = _drawLines[0].nodeFrom;
        endNode = startNode.relationNode;

        list.AddRange(startNode.line.points);

        while (startNode.Opposition != endNode)
        {
            if (startNode == null || startNode.Opposition == null || startNode.Opposition.relationNode == null) break;
            startNode = startNode.Opposition.relationNode;
            if (startNode == null || startNode.Opposition == null || startNode.Opposition.relationNode == null) break;
            List<Vector2> list2 = startNode.line.GetPoint(startNode.transform.position, startNode.Opposition.transform.position);
            list2.RemoveAt(0);
            list.AddRange(list2);
        }

        list.RemoveAt(0);
        return list;
    }

    private GameObject _drawPanelObject;
    ThickIrregularPlane3D _thickIrregularPlane3D;

    public ItemVO CreateMesh()
    {
        _drawPanelObject = new GameObject("DrawPanel");
        _thickIrregularPlane3D = _drawPanelObject.AddComponent<ThickIrregularPlane3D>();
        _thickIrregularPlane3D.SetPoints(GetPanelPoints(), thickness);
        _thickIrregularPlane3D.upPanelCollage(_materialsUpID);
        _thickIrregularPlane3D.downPanelCollage(_materialsDownID);

        if (id == "")
        {
            id = NumberUtils.GetGuid();
        }
        _thickIrregularPlane3D.id = id;

        _thickIrregularPlane3D.tilingX = _drawFillPanel.tilingX;
        _thickIrregularPlane3D.tilingY = _drawFillPanel.tilingY;
        _thickIrregularPlane3D.offestX = _drawFillPanel.offestX;
        _thickIrregularPlane3D.offestY = _drawFillPanel.offestY;

        _drawPanelObject.AddComponent<ThickIrregularComponent>();
        ItemVO itemvo = AssetsModel.Instance.CreateItemVO("ThickIrregularPlane3D");
        itemvo.model = _drawPanelObject;
        itemvo.topImgTexture = (Texture2D)Resources.Load("TopImg/Stage");
        itemvo.id = _thickIrregularPlane3D.id;

        TransformVO tvo = itemvo.GetComponentVO<TransformVO>();
        tvo.rotateX = (_direction == HORIZONTAL) ? 0 : -90;

        CollageVO cvo = itemvo.GetComponentVO<CollageVO>();
        return itemvo;
    }

    public XmlNode Code
    {
        set
        {
            XmlNode code = value as XmlNode;
            XmlNode drawPanelNode = code.SelectSingleNode("ThickIrregularPlane3D").SelectSingleNode("DrawPanel");
            XmlNode thickIrregularNode = code.SelectSingleNode("ThickIrregularPlane3D").SelectSingleNode("ThickIrregular");
            SetMaterial(thickIrregularNode.Attributes["upCollageId"].Value, thickIrregularNode.Attributes["downCollageId"].Value);
            Draw(drawPanelNode.Attributes["nodes"].Value, float.Parse(thickIrregularNode.Attributes["thickness"].Value));
            direction = drawPanelNode.Attributes["direction"].Value;
            TilingX = float.Parse(thickIrregularNode.Attributes["tilingX"].Value);
            TilingY = float.Parse(thickIrregularNode.Attributes["tilingY"].Value);
            OffestX = float.Parse(thickIrregularNode.Attributes["offestX"].Value);
            OffestY = float.Parse(thickIrregularNode.Attributes["offestY"].Value);
            id = drawPanelNode.Attributes["id"].Value;
        }
        get
        {
            string code = "";
            code += "<ThickIrregularPlane3D>" + "\n";

            code += "   <DrawPanel ";
            code += " id = " + '"' + id + '"';
            code += " nodes = " + '"' + GetNodeCode() + '"';
            code += " direction = " + '"' + _direction + '"';
            code += "/>" + "\n";

            code += "<ThickIrregular";
            code += " upCollageId = " + GetPropertyString(_materialsUpID);
            code += " downCollageId = " + GetPropertyString(_materialsDownID);
            code += " tilingX = " + GetPropertyString(_drawFillPanel.tilingX);
            code += " tilingY = " + GetPropertyString(_drawFillPanel.tilingY);
            code += " offestX = " + GetPropertyString(_drawFillPanel.offestX);
            code += " offestY = " + GetPropertyString(_drawFillPanel.offestY);
            code += " thickness = " + GetPropertyString(_thickness);

            List<Vector2> _list = GetPanelPoints();
            string p = "";
            for (int i = 0; i < _list.Count; i++)
            {
                p += _list[i].x + "," + _list[i].y + ((i == _list.Count - 1) ? "" : ";");
            }
            code += " points = " + GetPropertyString(p);

            code += ">";
            code += "</ThickIrregular>";

            code += "</ThickIrregularPlane3D>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);

            return xml;
        }
    }

    private DrawLine GetDrawLine(Vector3 f, Vector3 t)
    {
        foreach (DrawLine line in _drawLines)
        {
            if (line.nodeFrom.transform.position == f || line.nodeTo.transform.position == t)
            {
                return line;
            }
            if (line.nodeFrom.transform.position == t || line.nodeTo.transform.position == f)
            {
                return line;
            }
        }
        return null;
    }

    public void ChangeLineHandle(float speedX, float speedY)
    {
        float speed = BisectorMoveUtils.GetMoveLineSpeed(_drawLine.bisector, speedX, speedY);
        if (speed == 0) return;

        Bisector fromb = _drawLine.nodeFrom.relationNode.line.bisector;
        Bisector tob = _drawLine.nodeTo.relationNode.line.bisector;

        List<Vector2> list = BisectorMoveUtils.GetLineMovePoint(_drawLine.bisector, fromb, tob, speed);
        Vector2 fromPoint = list[0];
        Vector2 toPoint = list[1];
        _drawLine.MovePoint(fromPoint, toPoint, _drawLine.nodeCurve.transform.position);

        _drawLine.nodeFrom.relationNode.Move(fromPoint);
        _drawLine.nodeTo.relationNode.Move(toPoint);

        _drawLine.nodeFrom.relationNode.line.UpdateCollider();
        _drawLine.nodeTo.relationNode.line.UpdateCollider();

        UpdateRelation();
        FillPanel();
    }

    public string GetNodeCode()
    {
        string code = "";
        for (int i = 0; i < _drawLines.Count; i++)
        {
            code += _drawLines[i].nodeFrom.transform.position.x + "," + _drawLines[i].nodeFrom.transform.position.y + "," +
                _drawLines[i].nodeTo.transform.position.x + "," + _drawLines[i].nodeTo.transform.position.y + "," +
                _drawLines[i].nodeCurve.transform.position.x + "," + _drawLines[i].nodeCurve.transform.position.y + ((i == _drawLines.Count - 1) ? "" : ";");
        }
        return code;
    }

    private float _thickness = 50;

    public float thickness
    {
        set { _thickness = value; }
        get { return _thickness; }
    }

    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";

    private string _direction = "Horizontal";

    public string direction
    {
        set { _direction = value; }
        get { return _direction; }
    }

    protected string GetPropertyString(object value)
    {
        return '"' + value.ToString() + '"';
    }

    #region Loop

    private void LateUpdate()
    {
        LoopLine();
        LoopNode();
    }

    private void LoopNode()
    {
        if (_drawNode != null)
        {
            Vector3 point = _drawNode.transform.position;
            point -= _lastMousePoint - Input.mousePosition;
            point = new Vector3(point.x, point.y);

            _drawNode.Move(point);
            if (_drawNode.relationNode != null && _drawNode.name != "NodeCurve")
            {
                _drawNode.relationNode.Move(point);
            }

            _lastMousePoint = Input.mousePosition;

            FillPanel();
        }
    }

    private void LoopLine()
    {
        if (_drawLine)
        {
            Vector2 p = Input.mousePosition - _lastMousePoint;
            ChangeLineHandle(p.x, p.y);

            _lastMousePoint = Input.mousePosition;
        }
    }

    #endregion;

    #region Event

    private DrawLine _drawLine;
    private Action<DrawLine> _lineRightClick;
    private Action<DrawLine> _lineClick;
    private Action<DrawNode> _nodeClick;

    private void LineDownHandle(DrawLine line)
    {
        _drawLine = line;
        _lineClick(line);
    }

    private void LineUpHandle(DrawLine line)
    {
        if (_drawLine)
        {
            _drawLine.UpdateCollider();
            _drawLine = null;
        }
    }

    private void LineRightClickHandle(DrawLine line)
    {
        _lineRightClick(line);
    }

    public void AddLineRightClick(Action<DrawLine> fun)
    {
        _lineRightClick = fun;
    }

    public void AddLineClick(Action<DrawLine> fun)
    {
        _lineClick = fun;
    }

    public void AddNodeClick(Action<DrawNode> fun)
    {
        _nodeClick = fun;
    }

    public void NodeDownHandle(DrawNode node)
    {
        _drawLine = null;
        _drawNode = node;
        _nodeClick(node);
    }

    public void NodeUpHandle(DrawNode node)
    {
    }

    #endregion;
}
