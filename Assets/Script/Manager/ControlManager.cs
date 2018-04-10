using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;
using Build3D;
using BuildManager;

public class ControlManager : EventDispatcher
{
    public List<AssetVO> oldAssetsVO = new List<AssetVO>();
    public List<AssetVO> newAssetsVO = new List<AssetVO>();

    private bool _enabledBuild;

    public bool EnabledBuild
    {
        get { return _enabledBuild; }
        set
        {
            _enabledBuild = value;
            foreach (LineStruct data in BuilderModel.Instance.lineDatas)
            {
                data.line.enabled = value;
            }
            foreach (SurfaceStruct data in BuilderModel.Instance.surfaceDatas)
            {
                data.surface.enabled = value;
            }
            foreach (NestedStruct data in BuilderModel.Instance.nestedDatas)
            {
                data.nested2.enabled = value;
            }
        }
    }

    private bool _enabledAssets;

    public bool EnabledAssets
    {
        get { return _enabledAssets; }
        set
        {
            _enabledAssets = value;
            foreach (ItemStruct data in AssetsModel.Instance.itemDatas)
            {
                data.item2.enabled = value;
            }
        }
    }

    private CorePanel corePanel;

    public ControlManager()
    {
        corePanel = UIManager.GetUI(UI.CorePanel) as CorePanel;
    }

    private bool CanDispatchVO()
    {
        if (oldAssetsVO == null || newAssetsVO == null)
        {
            return false;
        }

        if (oldAssetsVO.Count == 0 || newAssetsVO.Count == 0)
        {
            return false;
        }

        if (newAssetsVO.Count != oldAssetsVO.Count)
        {
            return false;
        }

        for (int i = 0; i < newAssetsVO.Count; i++)
        {
            if (newAssetsVO[i] != null && oldAssetsVO[i] == null)
            {
                return true;
            }

            if (newAssetsVO[i] != null && oldAssetsVO[i] == null)
            {
                return true;
            }

            if (!newAssetsVO[i].Equals(oldAssetsVO[i]))
            {
                return true;
            }
        }

        return false;
    }

    #region Mouse

    protected float mouseMoveX;
    protected float mouseMoveY;
    protected float mouseWheel;
    protected float _lastDownX;
    protected float _lastDownY;

    /// <summary>
    /// 初始化鼠标
    /// </summary>
    protected void InitialMousePoint()
    {
        if (Input.touchCount == 0)
        {
            _lastDownX = Input.mousePosition.x;
            _lastDownY = Input.mousePosition.y;
        }
    }

    #endregion

    #region Loop

    public bool loop { set; get; } = true;

    public void LateUpdate()
    {
        if (!loop) return;

        if (Input.touchCount == 0)
        {
            mouseMoveX = (Input.mousePosition.x - _lastDownX) * Time.deltaTime * 1;
            mouseMoveY = (Input.mousePosition.y - _lastDownY) * Time.deltaTime * 1;
        }

        if (mouseMoveX > 5)
            mouseMoveX = 0;
        if (mouseMoveY > 5)
            mouseMoveY = 0;

        mouseWheel = 0;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            mouseWheel = -.5f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            mouseWheel = .5f;
        }

        LoopNode();
        LoopLine();
        LoopSurface();
        LoopItem();
        LoopNested();
        LoopPlane();

        InitialMousePoint();

        if (Input.GetMouseButtonDown(1))
        {
            _movePlane = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _movePlane = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _sorptionIsInit = false;
            _sorptionPointToPointList = new List<Vector2>();
            _sorptionPointToLineList = new List<Bisector>();
            _sorptionPointToPointLineList = new List<Vector2>();
            _sorptionIsLineForPointToPointLineList = new List<Vector2>();

            if (MouseManager.SelectedSurface)
            {
                if (lineTool) lineTool.line = null;
                corePanel.SetActiveLineTool(false);
            }
            else
            {
                if (MouseManager.SelectedLine)
                {
                    if (lineTool) lineTool.line = MouseManager.SelectedLine;
                    corePanel.SetActiveLineTool(true);
                }
            }

            if (MouseManager.SelectedPlane)
            {
                if (lineTool) lineTool.line = null;
                corePanel.SetActiveLineTool(false);
            }

            if (operateSurface)
            {
                UpdateHoleForMoveLine(operateSurface);
                newAssetsVO.Add(operateSurface.VO);
                UpdateSurface();
            }

            if (operateLine)
            {
                UpdateHoleForMoveLine(operateLine.surface);
                newAssetsVO.Add(operateLine.surface.VO);
                UpdateSurface();
            }

            if (operateItem)
            {
                ItemReleaseHandle();
            }

            if (operateNode)
            {
                newAssetsVO.Add(operateNode.line.surface.VO);
                UpdateNode();
            }

            if (operateNested)
            {
                UpdateNestedOnLine(operateNested);
                newAssetsVO.Add(operateNested.VO);
                UpdateNested();
            }

            operateLine = null;
            operateNode = null;
            oldAssetsVO = new List<AssetVO>();
            newAssetsVO = new List<AssetVO>();
        }

        if (MouseManager.DownSurface && MouseManager.SelectedSurface && MouseManager.DownSurface == MouseManager.SelectedSurface)
        {
            if (operateSurface == null)
            {
                operateSurface = MouseManager.SelectedSurface;
                oldAssetsVO.Add(operateSurface.VO.Clone());
            }
        }
        else
        {
            if (operateSurface != null)
            {
                operateSurface = null;
            }
        }

        if (MouseManager.DownPlane)
        {
            LoopPlane();
        }

        if (MouseManager.DownNested && MouseManager.SelectedNested && MouseManager.DownNested == MouseManager.SelectedNested)
        {
            if (operateNested == null)
            {
                operateNested = MouseManager.SelectedNested;
                oldAssetsVO.Add(operateNested.VO.Clone());
            }
        }
        else
        {
            if (operateNested != null)
            {
                operateNested = null;
            }
        }

        if (MouseManager.DownItem && MouseManager.SelectedItem && MouseManager.DownItem == MouseManager.SelectedItem)
        {
            if (operateItem == null)
            {
                operateItem = MouseManager.SelectedItem;
                ItemSaveOldHandle();
            }
        }
        else
        {
            if (operateItem != null)
            {
                operateItem = null;
            }
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (MouseManager.SelectedItem)
        //    {
        //        MouseManager.SelectedItem.transform.Rotate(0, 0, 5);
        //        dispatchEvent(new ControlManagerEvent(ControlManagerEvent.CHANGE_ITEM, new List<ObjectSprite> { MouseManager.SelectedItem }));
        //    }
        //}

        RemoveAuxiliaryLine();
        //UpdateLineRelationship();
    }

    #endregion

    #region Plane

    private bool _movePlane;

    private void LoopPlane()
    {
        if (_movePlane)
        {
            if (SceneManager.Instance.Camera2D.transform.position.y - mouseMoveY > 40 ||
                SceneManager.Instance.Camera2D.transform.position.y + mouseMoveY < -40 ||
                SceneManager.Instance.Camera2D.transform.position.x - mouseMoveX > 30 ||
                SceneManager.Instance.Camera2D.transform.position.x + mouseMoveX < -30)
            {
                return;
            }

            SceneManager.Instance.Camera2D.transform.Translate(Vector3.left * mouseMoveX, Space.World);
            SceneManager.Instance.Camera2D.transform.Translate(Vector3.up * -mouseMoveY, Space.World);
            lineTool.UpdateLine();
        }

        if (mouseWheel != 0)
        {
            if (SceneManager.Instance.Camera2D.orthographicSize - mouseWheel < 3.5f ||
                SceneManager.Instance.Camera2D.orthographicSize - mouseWheel > 15.5f)
            {
                return;
            }

            SceneManager.Instance.Camera2D.orthographicSize -= mouseWheel;
            lineTool.UpdateLine();
        }
    }

    #endregion

    #region Surface

    private void UpdateSurface()
    {


        if (CanDispatchVO())
        {
            dispatchEvent(new ControlManagerEvent(ControlManagerEvent.CHANGE_SURFACE,
                new List<ObjectSprite>() { operateSurface },
                oldAssetsVO,
                newAssetsVO
                ));
        }
    }

    public Surface2D operateSurface { set; get; }

    private void LoopSurface()
    {
        if (operateSurface)
        {
            ChangeSurfaceHandle(operateSurface, mouseMoveX, mouseMoveY);
            lineTool.UpdateLine();
        }
    }

    public void ChangeSurfaceHandle(Surface2D surface, float speedX, float speedY)
    {
        if (speedX == 0 && speedY == 0) return;

        Line2D line;
        Vector2 fsp, tsp;
        float spx = speedX;
        float spy = speedY;
        for (int i = 0; i < surface.lines.Count; i++)
        {
            line = surface.lines[i];

            if (!line.fromNode.mousePoint.IsNull()) line.fromNode.mousePoint = line.fromNode.point;
            if (!line.toNode.mousePoint.IsNull()) line.toNode.mousePoint = line.toNode.point;

            line.fromNode.mousePoint = line.fromNode.mousePoint + new Vector2(speedX, speedY);
            line.toNode.mousePoint = line.toNode.mousePoint + new Vector2(speedX, speedY);

            fsp = GetSorptionPoint(line.fromNode, true, false, surface);
            tsp = GetSorptionPoint(line.toNode, true, false, surface);

            if (!fsp.Equals(line.fromNode.mousePoint))
            {
                spx = fsp.x - line.fromNode.point.x;
                spy = fsp.y - line.fromNode.point.y;
                break;
            }
            else if (!tsp.Equals(line.toNode.mousePoint))
            {
                spx = tsp.x - line.toNode.point.x;
                spy = tsp.y - line.toNode.point.y;
                break;
            }
        }

        for (int i = 0; i < surface.lines.Count; i++)
        {
            line = surface.lines[i];

            MoveNode(line.fromNode, line.fromNode.point + new Vector2(spx, spy), false);
            MoveNode(line.toNode, line.toNode.point + new Vector2(spx, spy), false);

            line.fromNode.mousePoint.Null();
            line.toNode.mousePoint.Null();
        }

        //for each(var item: Item in AssetsModel.instance.items){
        //	if (item.surface == surface)
        //	{
        //		item.x += spx;
        //		item.y += spy;
        //	}

        //	item.updateItemVO();
        //}

        //UpdateLineMoveHandle(BuilderModel.Instance.lines);

        //BuilderModel.Instance.UpdateSurface2(surface);
        surface.UpdateVO();

        UpdateNestedSurfaceMoveHandle(surface);
    }

    public Node2D operateNode { set; get; }

    public void SetOperateNode(Node2D node)
    {
        oldAssetsVO.Add(node.line.surface.VO.Clone());
        operateNode = node;
    }

    private void LoopNode()
    {
        if (operateNode)
        {
            ChangeNodeHandle(operateNode, mouseMoveX, mouseMoveY);
            lineTool.UpdateLine();
        }
    }

    public void ChangeNodeHandle(Node2D node, float speedX, float speedY, bool relation = true)
    {
        if (speedX == 0 && speedY == 0) return;

        MoveNode(node, node.point + new Vector2(speedX, speedY), relation);

        node.line.surface.UpdateVO();
    }

    private void UpdateNode()
    {
        if (CanDispatchVO())
        {
            dispatchEvent(new ControlManagerEvent(ControlManagerEvent.CHANGE_SURFACE,
                new List<ObjectSprite>() { operateNode },
                oldAssetsVO,
                newAssetsVO
                ));
        }
    }

    #endregion

    #region Line

    private LineTool lineTool
    {
        get { return corePanel.lineTool; }
    }

    public void SetOperateLine(Line2D line)
    {
        oldAssetsVO.Add(line.surface.VO.Clone());
        operateLine = line;
    }

    private void LoopLine()
    {
        if (operateLine)
        {
            ChangeLineHandle(operateLine, mouseMoveX, mouseMoveY);
            lineTool.UpdateLine();
        }
    }

    private Bisector _limitFromBisector;
    private Bisector _limitToBisector;

    public Line2D operateLine { set; get; }

    public void ChangeLineLength(Line2D line, float length)
    {
        oldAssetsVO.Add(line.surface.VO.Clone());

        float d = line.interiorLength / 2 - length / 2;

        Vector2 f = PlaneUtils.AngleDistanceGetPoint(line.interiorFrom, line.angle, d);
        Vector2 t = PlaneUtils.AngleDistanceGetPoint(f, line.angle, length);
        Vector2 c = Vector2.Lerp(f, t, .5f);
        Vector2 ff = new Vector2();
        Vector2 tt = new Vector2();

        foreach (Node2D n in line.fromNode.containNodes)
        {
            Bisector b = new Bisector(f.Round(), PlaneUtils.AngleDistanceGetPoint(f, n.line.angle, n.line.interiorLength).Round());
            Bisector bb;
            if (n.line.interiorAlong)
            {
                bb = b.Translation(n.line.thickness / -2);
            }
            else
            {
                bb = b.Translation(n.line.thickness / 2);
            }
            ff = line.bisector.Intersection(bb);

            b = new Bisector(ff, PlaneUtils.AngleDistanceGetPoint(ff, n.line.angle, n.line.interiorLength));
            MoveNode(n.opposite, n.opposite.containNodes[0].line.bisector.Intersection(b), true);
        }

        foreach (Node2D n in line.toNode.containNodes)
        {
            Bisector b = new Bisector(t.Round(), PlaneUtils.AngleDistanceGetPoint(t, n.line.angle, n.line.interiorLength).Round());
            Bisector bb;
            if (n.line.interiorAlong)
            {
                bb = b.Translation(n.line.thickness / -2);
            }
            else
            {
                bb = b.Translation(n.line.thickness / 2);
            }
            tt = line.bisector.Intersection(bb);

            b = new Bisector(tt, PlaneUtils.AngleDistanceGetPoint(tt, n.line.angle, n.line.interiorLength));
            MoveNode(n.opposite, n.opposite.containNodes[0].line.bisector.Intersection(b), true);
        }

        MoveNode(line.fromNode, ff, true);
        MoveNode(line.toNode, tt, true);

        line.surface.UpdateVO();

        newAssetsVO.Add(line.surface.VO.Clone());

        UpdateSurface();
    }

    public void ChangeLineHandle(Line2D line, float speedX, float speedY)
    {
        float speed = BisectorMoveUtils.GetMoveLineSpeed(line.bisector, mouseMoveX, mouseMoveY);
        if (speed == 0) return;

        List<Bisector> l = LineMoveCondition(line);
        _limitFromBisector = l[0];
        _limitToBisector = l[1];

        line.fromNode.mousePoint = line.fromNode.point;
        line.toNode.mousePoint = line.toNode.point;

        List<Vector2> movePoints = BisectorMoveUtils.GetLineMovePoint(new Bisector(line.fromNode.mousePoint, line.toNode.mousePoint), _limitFromBisector, _limitToBisector, speed);
        line.fromNode.mousePoint = movePoints[0];
        line.toNode.mousePoint = movePoints[1];

        Vector2 dfp;
        Vector2 dtp;
        Vector2 fsp;
        Vector2 tsp;

        fsp = GetSorptionPoint(line.fromNode, true, true, line.surface);
        tsp = GetSorptionPoint(line.toNode, true, true, line.surface);

        if (!fsp.Equals(line.fromNode.mousePoint))
        {
            Bisector b = new Bisector(fsp, line.bisector.DerivedTo(fsp));
            movePoints = BisectorMoveUtils.GetLineMovePoint(b, _limitFromBisector, _limitToBisector, 0);
            dfp = movePoints[0];
            dtp = movePoints[1];
        }
        else if (!tsp.Equals(line.toNode.mousePoint))
        {
            Bisector b = new Bisector(line.bisector.DerivedFrom(tsp), tsp);
            movePoints = BisectorMoveUtils.GetLineMovePoint(b, _limitFromBisector, _limitToBisector, 0);
            dfp = movePoints[0];
            dtp = movePoints[1];
        }
        else
        {
            dfp = line.fromNode.mousePoint;
            dtp = line.toNode.mousePoint;
        }

        bool can = true;
        float canDistance = 1;

        foreach (Node2D n in line.fromNode.containNodes)
        {
            if (Vector2.Distance(n.opposite.point, dfp) < canDistance)
            {
                can = false;
            }
        }
        foreach (Node2D n in line.toNode.containNodes)
        {
            if (Vector2.Distance(n.opposite.point, dtp) < canDistance)
            {
                can = false;
            }
        }
        if (Vector2.Distance(line.toNode.opposite.point, dtp) < canDistance)
        {
            can = false;
        }
        if (Vector2.Distance(line.fromNode.opposite.point, dfp) < canDistance)
        {
            can = false;
        }
        if (Vector2.Distance(dfp, dtp) < canDistance)
        {
            can = false;
        }

        if (can)
        {
            MoveNode(line.fromNode, dfp, true);
            MoveNode(line.toNode, dtp, true);
        }

        line.surface.UpdateVO();

        UpdateNestedLineMoveHandle(line);
        return;
    }

    protected List<Bisector> LineMoveCondition(Line2D line)
    {
        Bisector fromBisector, toBisector;

        if (line.fromNode.containNodes.Count == 1)
        {
            fromBisector = line.fromNode.containNodes[0].line.bisector;
            if (Mathf.Abs(line.bisector.k - fromBisector.k) < 0.3 || (float.IsNaN(line.bisector.k) && float.IsNaN(fromBisector.k)))
            {
                fromBisector = line.bisector.verticalFrom;
            }
        }
        else
        {
            fromBisector = IsLine(line.fromNode);
        }

        if (line.toNode.containNodes.Count == 1)
        {
            toBisector = line.toNode.containNodes[0].line.bisector;
            if (Mathf.Abs(line.bisector.k - toBisector.k) < 0.3 || (float.IsNaN(line.bisector.k) && float.IsNaN(toBisector.k)))
            {
                toBisector = line.bisector.verticalTo;
            }
        }
        else
        {
            toBisector = IsLine(line.toNode);
        }

        if (line.fromNode.containNodes.Count == 0 && line.toNode.containNodes.Count == 0)
        {
            fromBisector = null;
            toBisector = null;

            return new List<Bisector>() { fromBisector, toBisector };
        }

        if (fromBisector == null) fromBisector = line.bisector.verticalFrom;
        if (toBisector == null) toBisector = line.bisector.verticalTo;

        return new List<Bisector>() { fromBisector, toBisector };
    }

    /// <summary>
    /// 如果点周围的线多于1条且可以形成直线，则延形成的直线移动
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    Bisector IsLine(Node2D node)
    {
        List<Node2D> nodes = node.containNodes;
        if (nodes.Count > 1)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (nodes[i] != nodes[j] && nodes[i] != node && nodes[j] != node &&
                        nodes[i].line.bisector.IsStraightLine(nodes[j].line.bisector))
                    {
                        return nodes[i].line.bisector;
                    }
                }
            }
        }
        return null;
    }

    #endregion

    #region Sorption

    private bool _sorptionIsInit;
    private List<Vector2> _sorptionPointToPointList = new List<Vector2>();
    private List<Bisector> _sorptionPointToLineList = new List<Bisector>();
    private List<Vector2> _sorptionPointToPointLineList = new List<Vector2>();
    private List<Vector2> _sorptionIsLineForPointToPointLineList = new List<Vector2>();

    /// <summary>
    /// 获取吸附点
    /// </summary>
    /// <param name="node">移动点</param>
    /// <param name="canPointLine">是否检测所有点的水平垂直线</param>
    /// <param name="isLine">是否拖动的线</param>
    /// <param name="surface"></param>
    /// <param name="bisector"></param>
    /// <returns></returns>
    private Vector2 GetSorptionPoint(Node2D node, bool canPointLine = true, bool isLine = false, Surface2D surface = null, Bisector bisector = null)
    {
        List<Node2D> surfacePoints = new List<Node2D>();
        List<Line2D> surfaceLines = new List<Line2D>();

        if (surface)
        {
            ArrayUtils.CloneArray(surface.nodes, surfacePoints);
            ArrayUtils.CloneArray(surface.lines, surfaceLines);
        }

        if (!_sorptionIsInit)
        {
            surfacePoints.Add(node);
            _sorptionPointToPointList = BuilderModel.Instance.GetPoints(surfacePoints);
            _sorptionIsLineForPointToPointLineList = BuilderModel.Instance.GetPoints(surfacePoints);

            for (int j = 0; j < node.containNodes.Count; j++)
            {
                surfaceLines.Add(node.containNodes[j].line);
            }
            surfaceLines.Add(node.line);

            List<Line2D> lines = BuilderModel.Instance.GetLines(surfaceLines);
            for (int j = 0; j < lines.Count; j++)
            {
                _sorptionPointToLineList.Add(lines[j].bisector);
            }

            surfacePoints.Add(node.opposite);
            _sorptionPointToPointLineList = BuilderModel.Instance.GetPoints(surfacePoints);
            _sorptionIsInit = true;
        }

        Hashtable o;
        Vector2 p = SorptionManager.PointToPoint(node.mousePoint, _sorptionPointToPointList, .2f);
        if (p.IsNull())
        {
            o = SorptionManager.PointToLine(node.mousePoint, _sorptionPointToLineList, .2f);
            p = (Vector2)o["p"];
            List<Bisector> bs = (List<Bisector>)o["b"];
            if (bs != null) SetAuxiliaryLine(bs[0]);
        }

        if (canPointLine)
        {
            if (isLine)
            {
                o = SorptionManager.NodeToPointLine(node.mousePoint, _sorptionPointToPointLineList, .2f);
                p = (Vector2)o["p"];
            }
            else
            {
                o = SorptionManager.NodeToPointLine(node.mousePoint, _sorptionIsLineForPointToPointLineList, .2f);
                p = (Vector2)o["p"];
            }
            if (p.IsNull() && bisector != null)
            {
                p = bisector.VerticalIntersection(p);
            }
            if ((Bisector)o["vb"] != null) SetAuxiliaryLine((Bisector)o["vb"]);
            if ((Bisector)o["hb"] != null) SetAuxiliaryLine((Bisector)o["hb"]);
        }

        if (p.IsNull())
        {
            p = node.mousePoint;
        }

        return p;
    }

    public List<AuxiliaryLine> auxiliaryLines = new List<AuxiliaryLine>();

    public void SetAuxiliaryLine(Bisector b)
    {
    }

    public void RemoveAuxiliaryLine()
    {
        //auxiliaryLines = new List<AuxiliaryLine>();
    }

    public Item2D operateItem { set; get; }

    private void LoopItem()
    {
        if (operateItem)
        {
            ChangeItemHandle(operateItem, mouseMoveX, mouseMoveY);
        }
    }

    public void ChangeItemHandle(Item2D item, float speedX, float speedY)
    {
        FindItemSurface(item);

        List<Bisector> bisectors = new List<Bisector>();

        if (item.surface)
        {
            for (int i = 0; i < item.surface.lines.Count; i++)
            {
                bisectors.Add(item.surface.lines[i].interiorAlong ? item.surface.lines[i].bisectorAlong : item.surface.lines[i].bisectorInverse);
            }
        }

        item.mousePointRT.x += speedX;
        item.mousePointRT.y += speedY;
        item.mousePointRB.x += speedX;
        item.mousePointRB.y += speedY;
        item.mousePointLB.x += speedX;
        item.mousePointLB.y += speedY;
        item.mousePointLT.x += speedX;
        item.mousePointLT.y += speedY;

        Hashtable rtvo = SorptionManager.PointToLine(item.mousePointRT, bisectors, .2f);
        Hashtable rbvo = SorptionManager.PointToLine(item.mousePointRB, bisectors, .2f);
        Hashtable lbvo = SorptionManager.PointToLine(item.mousePointLB, bisectors, .2f);
        Hashtable ltvo = SorptionManager.PointToLine(item.mousePointLT, bisectors, .2f);

        Vector2 rt = (Vector2)rtvo["p"];
        Vector2 rb = (Vector2)rbvo["p"];
        Vector2 lb = (Vector2)lbvo["p"];
        Vector2 lt = (Vector2)ltvo["p"];

        List<Bisector> rtvobs = (List<Bisector>)rtvo["b"];
        List<Bisector> rbvobs = (List<Bisector>)rbvo["b"];
        List<Bisector> lbvobs = (List<Bisector>)lbvo["b"];
        List<Bisector> ltvobs = (List<Bisector>)ltvo["b"];

        if (rtvobs != null && rtvobs.Count > 1)
        {
            rb.Null(); lb.Null(); lt.Null();
        }
        else if (rbvobs != null && rbvobs.Count > 1)
        {
            rt.Null(); lb.Null(); lt.Null();
        }
        else if (lbvobs != null && lbvobs.Count > 1)
        {
            rb.Null(); rt.Null(); lt.Null();
        }
        else if (ltvobs != null && ltvobs.Count > 1)
        {
            rb.Null(); lb.Null(); rt.Null();
        }

        Vector3 mp = item.transform.position;
        if (!rt.IsNull())
        {
            if (mp.x == item.transform.position.x) mp.x += rt.x - item.localRT.x;
            if (mp.y == item.transform.position.y) mp.y += rt.y - item.localRT.y;
        }
        if (!rb.IsNull())
        {
            if (mp.x == item.transform.position.x) mp.x += rb.x - item.localRB.x;
            if (mp.y == item.transform.position.y) mp.y += rb.y - item.localRB.y;
        }
        if (!lb.IsNull())
        {
            if (mp.x == item.transform.position.x) mp.x += lb.x - item.localLB.x;
            if (mp.y == item.transform.position.y) mp.y += lb.y - item.localLB.y;
        }
        if (!lt.IsNull())
        {
            if (mp.x == item.transform.position.x) mp.x += lt.x - item.localLT.x;
            if (mp.y == item.transform.position.y) mp.y += lt.y - item.localLT.y;
        }

        if (rt.IsNull() && rb.IsNull() && lb.IsNull() && lt.IsNull())
        {
            item.x = item.x + speedX;
            item.y = item.y + speedY;
        }
        else
        {
            item.x = mp.x;
            item.y = mp.y;
        }

        List<AssetVO> newAssetVOs = new List<AssetVO>();
        newAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));

        dispatchEvent(new ControlManagerEvent(ControlManagerEvent.TRANSFORM_MOVE,
         new List<ObjectSprite>() { operateItem },
         oldAssetsVO,
         newAssetVOs
         ));
    }

    private void ItemSaveOldHandle()
    {
        oldAssetsVO = new List<AssetVO>();
        oldAssetsVO.Add(AssetsModel.Instance.GetTransformVO(operateItem));
    }

    private void ItemReleaseHandle()
    {
        newAssetsVO = new List<AssetVO>();
        newAssetsVO.Add(AssetsModel.Instance.GetTransformVO(operateItem));

        List<ObjectSprite> list = new List<ObjectSprite>() { operateItem };
        dispatchEvent(new ControlManagerEvent(ControlManagerEvent.TRANSFORM_RELEASE,
            list,
            oldAssetsVO,
            newAssetsVO
            ));
    }

    private void FindItemSurface(Item2D item)
    {
        foreach (SurfaceStruct surfaceData in BuilderModel.Instance.surfaceDatas)
        {
            if (surfaceData.surface.polygon.inside(item.transform.position))
            {
                item.surface = surfaceData.surface;
            }
        }

        if (item.line)
        {
            item.surface = item.line.surface;
        }
    }

    public Nested2D operateNested { set; get; }

    private void LoopNested()
    {
        if (operateNested)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            ChangeNestedHandle(operateNested, hit.point);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="point"></param>
    /// <param name="type">0:内墙 1:中墙 2:外墙</param>
    /// <returns></returns>
    private NearWallClass GetNearWallPoint(List<Line2D> lines, Vector2 point, int type)
    {
        List<Hashtable> ds = new List<Hashtable>();
        for (int i = 0; i < lines.Count; i++)
        {
            Line2D uLine = lines[i];
            Vector2 xp = new Vector2().Null();

            switch (type)
            {
                case 0:
                    xp = uLine.interiorBisectorN.VerticalIntersection(point);
                    break;
                case 1:
                    xp = uLine.bisector.VerticalIntersection(point);
                    break;
                case 2:
                    xp = uLine.ExteriorBisector.VerticalIntersection(point);
                    break;
            }

            if (!xp.IsNull())
            {
                Hashtable h = new Hashtable();
                h.Add("p", xp);
                h.Add("d", Vector2.Distance(xp, point));
                h.Add("l", uLine);
                ds.Add(h);
            }
        }

        ds.Sort(delegate (Hashtable px, Hashtable py)
        {
            float d1 = (float)px["d"];
            float d2 = (float)py["d"];
            return d1.CompareTo(d2);
        });

        if (ds.Count > 0)
        {
            NearWallClass nc = new NearWallClass();
            Hashtable p = (Hashtable)ds[0];
            nc.line = (Line2D)p["l"];
            nc.point = (Vector2)p["p"];

            return nc;
        }
        else
        {
            return null;
        }
    }

    public class NearWallClass
    {
        public Vector2 point;
        public Line2D line;
    }

    public void UpdateNestedHandle(Nested2D nested, Vector2 xp)
    {
        Line2D line = BuilderModel.Instance.GetLineData(nested.lineId).line;

        float df = Vector2.Distance(xp, line.from);
        float dt = Vector2.Distance(xp, line.to);
        if (df < nested.width / 2 && dt < nested.width / 2)
        {
        }
        else
        {
            if (df < nested.width / 2)
            {
                xp = PlaneUtils.AngleDistanceGetPoint(line.from, line.bisector.angle, nested.width / 2);
            }
            if (dt < nested.width / 2)
            {
                xp = PlaneUtils.AngleDistanceGetPoint(line.to, line.bisector.angleReverse, nested.width / 2);
            }
        }

        nested.transform.rotation = Quaternion.Euler(nested.transform.rotation.x, nested.transform.rotation.y, line.bisector.angle);
        nested.widthZ = 10;

        nested.x = xp.x;
        nested.y = xp.y;
        //nested.y = -.01f;

        nested.UpdateVO();

        //UpdateNestedOnLine(nested, xp);
    }

    public void UpdateNestedOnLine(Nested2D nested)
    {
        Vector2 xp = new Vector2(nested.x, nested.y);

        if (nested.lineId == "")
        {
            foreach (Line2D l in BuilderModel.Instance.lines)
            {
                if (l.bisector.PointInStraight(xp))
                {
                    nested.lineId = l.VO.id;
                }
            }
        }

        RemoveNested(nested);

        nested.lines = new List<string>();

        if (nested.lineId == "" || nested.lineId == null) return;

        AddHole(OverlapLine(BuilderModel.Instance.GetLineData(nested.lineId).line), xp, nested);
    }

    private void ChangeNestedHandle(Nested2D nested, Vector2 point)
    {
        //Debug.Log(new Vector2(point.x, point.y).ToString("f4"));
        if (nested)
        {
            NearWallClass nearWallClass = GetNearWallPoint(BuilderModel.Instance.lines, point, 1);

            if (nearWallClass != null)
            {
                nested.lineId = nearWallClass.line.VO.id;

                UpdateNestedHandle(nested, nearWallClass.point);
            }
        }
    }

    private void RemoveNested(Nested2D nested)
    {
        for (int i = 0; i < nested.lines.Count; i++)
        {
            LineStruct data = BuilderModel.Instance.GetLineData(nested.lines[i]);

            if (data == null) continue;
            Line2D l2 = data.line;
            Line3D l3 = data.line3;
            for (int j = 0; j < l2.holeDatas.Count; j++)
            {
                if (l2.holeDatas[j].nestedId == nested.id)
                {
                    l2.RemoveHole(l2.holeDatas[j]);
                }
                BuilderModel.Instance.UpdateLine2(l2);
            }
        }
    }

    private List<Line2D> OverlapLine(Line2D line)
    {
        Line2D l;
        List<Line2D> lines = new List<Line2D>();
        for (int i = 0; i < BuilderModel.Instance.lines.Count; i++)
        {
            l = BuilderModel.Instance.lines[i];
            if (l != line)
            {
                if (l.bisector.angle == line.bisector.angle || l.bisector.angle == line.bisector.angleReverse)
                {
                    if ((l.from.Equals(line.from) && l.to.Equals(line.to)) ||
                        (l.to.Equals(line.from) && l.from.Equals(line.to)))
                    {
                        lines.Add(l);
                    }
                    else
                    {
                        if ((l.bisector.PointInStraight(line.to) && !line.to.Equals(l.from) && !line.to.Equals(l.to)) ||
                            (l.bisector.PointInStraight(line.from) && !line.from.Equals(l.from) && !line.from.Equals(l.to)) ||
                            (line.bisector.PointInStraight(l.to) && !l.to.Equals(line.from) && !l.to.Equals(line.to)) ||
                            (line.bisector.PointInStraight(l.from) && !l.from.Equals(line.from) && !l.from.Equals(line.to)))
                        {
                            lines.Add(l);
                        }
                    }
                }
            }
        }

        lines.Add(line);
        return lines;
    }

    private void UpdateHoleForMoveLine(Surface2D surface)
    {
        foreach (Line2D line in surface.lines)
        {
            for (int i = 0; i < BuilderModel.Instance.nestedDatas.Count; i++)
            {
                Nested2D nested = BuilderModel.Instance.nestedDatas[i].nested2 as Nested2D;
                if (nested != null)
                {
                    foreach (string lid in nested.lines)
                    {
                        if (BuilderModel.Instance.GetLineData(lid).line == line)
                        {
                            UpdateNestedOnLine(nested);
                        }
                    }
                }
            }
        }
    }

    private void AddHole(List<Line2D> lines, Vector2 xp, Nested2D nested)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Line2D l = lines[i];

            HoleVO h = GetHoleData(l, xp, nested.id, nested.width, nested.height3D, nested.y3d);
            if (h == null)
            {
                continue;
            }

            nested.lines.Add(l.VO.id);
            l.AddHole(h);

            if (nested.lineId == l.VO.id)
            {
                nested.length = h.holeX;
            }

            BuilderModel.Instance.UpdateLine2(l);
        }

        nested.UpdateVO();
    }

    public HoleVO GetHoleData(Line2D line, Vector2 point, string id, float width, float height, float y3d)
    {
        Bisector ba = line.interiorAlong ? line.bisectorAlongN : line.bisectorAlong;
        Bisector bi = line.interiorAlong ? line.bisectorInverse : line.bisectorInverseN;

        Vector2 pn = new Vector2(point.x, point.y);
        Vector2 pp = ba.VerticalIntersection(pn);
        Vector2 pp1 = bi.VerticalIntersection(pn);

        if (pp.IsNull() || pp1.IsNull())
        {
            return null;
        }

        HoleVO h = new HoleVO();
        h.nestedId = id;
        h.holeWidth = width;
        h.holeHeight = height;
        h.holeAlongX = Vector2.Distance(pp, ba.from);
        h.holeInverseX = Vector2.Distance(pp1, bi.from);
        h.holeX = Vector2.Distance(pn, line.bisector.from);
        h.holeY = y3d;
        h.holeType = 1;

        return h;
    }

    private void UpdateNestedSurfaceMoveHandle(Surface2D surface)
    {
        foreach (Line2D l in surface.lines)
        {
            UpdateNestedLineMoveHandle(l);
        }
    }

    private void UpdateNestedLineMoveHandle(Line2D line)
    {
        if (line.holeDatas != null)
        {
            List<string> ls = new List<string>();
            foreach (HoleVO hd in line.holeDatas)
            {
                ls.Add(hd.nestedId);
            }

            foreach (string id in ls)
            {
                UpdateNestedMoveHandle(id);
            }
        }
    }

    private void UpdateNestedMoveHandle(string nestedId)
    {
        Nested2D nested = BuilderModel.Instance.GetNestedData(nestedId).nested2 as Nested2D;

        if (!nested || nested.lineId == "" || nested.lineId == null) return;

        Line2D line2 = BuilderModel.Instance.GetLineData(nested.lineId).line;
        Vector2 p = PlaneUtils.AngleDistanceGetPoint(line2.from, line2.bisector.angle, nested.length);

        UpdateNestedHandle(nested, p);
    }

    private void UpdateNested()
    {
        if (CanDispatchVO())
        {
            dispatchEvent(new ControlManagerEvent(ControlManagerEvent.CHANGE_NESTED,
                new List<ObjectSprite>() { operateNested },
                oldAssetsVO,
                newAssetsVO
                ));
        }
    }

    #endregion

    /// <summary>
    /// 移动点
    /// </summary>
    /// <param name="node">移动的点</param>
    /// <param name="point">目标点</param>
    /// <param name="relation">关联点是否一起移动</param>
    public void MoveNode(Node2D node, Vector2 point, bool relation = true)
    {
        List<Line2D> lines = new List<Line2D>();
        if (relation)
        {
            foreach (Node2D n in node.containNodes)
            {
                n.point = point;
                if (!ArrayUtils.Has(lines, n.line))
                {
                    lines.Add(n.line);
                }
            }
        }

        node.point = point;
        if (!ArrayUtils.Has(lines, node.line))
        {
            lines.Add(node.line);
        }
    }

    /// <summary>
    /// 刷新所有线的内部关系线 
    /// </summary>
    private void UpdateLineRelationship()
    {
        for (int i = 0; i < BuilderModel.Instance.lineDatas.Count; i++)
        {
            BuilderModel.Instance.lineDatas[i].line.UpdateNow();
            BuilderModel.Instance.lineDatas[i].line3.VO = BuilderModel.Instance.lineDatas[i].line.VO as LineVO;
        }

        for (int i = 0; i < BuilderModel.Instance.surfaceDatas.Count; i++)
        {
            (BuilderModel.Instance.surfaceDatas[i].surface.VO as SurfaceVO).Update();
            BuilderModel.Instance.surfaceDatas[i].surface3.VO = BuilderModel.Instance.surfaceDatas[i].surface.VO as SurfaceVO;
        }
    }
}
