using BuildManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private Vector3 _f;
    private Vector3 _t;
    private Vector3 _c;

    LineRenderer lineRenderer;

    public DrawNode nodeFrom;
    public DrawNode nodeTo;
    public DrawNode nodeCurve;
    public Bisector bisector;

    public List<Vector2> points = new List<Vector2>();
    private List<GameObject> boxs = new List<GameObject>();

    public void ResetCurve()
    {
        MovePoint(_f, _t, Vector3.Lerp(_f, _t, .5f));
    }

    public void SetPoint(Vector3 f, Vector3 t, Vector3 c)
    {
        bisector = new Bisector(f, t);
        _f = f;
        _t = t;

        if (c.IsNull())
        {
            _c = Vector3.Lerp(f, t, .5f);
        }
        else
        {
            _c = c;
        }

        _isInStraight = bisector.PointInStraight(c);

        gameObject.layer = transform.parent.gameObject.layer;

        GameObject nodef = new GameObject();
        GameObject nodet = new GameObject();
        GameObject nodec = new GameObject();

        nodef.transform.parent = gameObject.transform;
        nodet.transform.parent = gameObject.transform;
        nodec.transform.parent = gameObject.transform;

        nodeFrom = nodef.AddComponent<DrawNode>();
        nodeTo = nodet.AddComponent<DrawNode>();
        nodeCurve = nodec.AddComponent<DrawNode>();
        nodeFrom.SetType(true);
        nodeTo.SetType(true);
        nodeCurve.SetType(false);

        nodeFrom.transform.localPosition = new Vector3(0, 0, 0);
        nodeTo.transform.localPosition = new Vector3(0, 0, 0);
        nodeCurve.transform.localPosition = new Vector3(0, 0, 0);

        nodeFrom.transform.localPosition = f.ToVector2();
        nodeTo.transform.localPosition = t.ToVector2();
        nodeCurve.transform.localPosition = _c.ToVector2();

        nodeFrom.transform.localScale = new Vector3(1, 1, 1);
        nodeTo.transform.localScale = new Vector3(1, 1, 1);
        nodeCurve.transform.localScale = new Vector3(1, 1, 1);

        gameObject.name = "DrawLine";
        nodef.name = "NodeFrom";
        nodet.name = "NodeTo";
        nodec.name = "NodeCurve";

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 10 * transform.parent.parent.localScale.x;
        lineRenderer.useWorldSpace = false;

        UpdateLine();
        UpdateCollider();
    }

    private bool _isInStraight = true;

    public void MovePoint(Vector3 f, Vector3 t, Vector3 c)
    {
        if (f.IsNaN() == true || t.IsNaN() == true || c.IsNaN() == true ||
           f.IsInfinity() == true || t.IsInfinity() == true || c.IsInfinity() == true)
        {
            return;
        }
        if (f == nodeFrom.transform.localPosition && t == nodeTo.transform.localPosition && c == nodeCurve.transform.localPosition)
        {
            return;
        }
        _isInStraight = bisector.PointInStraight(c);

        _f = f;
        _t = t;
        _c = c;

        if (_isInStraight)
        {
            nodeFrom.transform.localPosition = f.ToVector2();
            nodeTo.transform.localPosition = t.ToVector2();
            nodeCurve.transform.localPosition = Vector3.Lerp(f, t, .5f).ToVector2();
        }
        else
        {
            nodeFrom.transform.localPosition = f.ToVector2();
            nodeTo.transform.localPosition = t.ToVector2();
            nodeCurve.transform.localPosition = c.ToVector2();
        }
        bisector = new Bisector(_f, _t);

        UpdateLine();
    }

    public DrawNode Opposition(Vector3 v)
    {
        if (v.ToVector2() == _f.ToVector2()) return nodeTo;
        if (v.ToVector2() == _t.ToVector2()) return nodeFrom;
        return null;
    }

    public List<Vector2> GetPoint(Vector3 f, Vector3 t)
    {
        List<Vector2> list = new List<Vector2>();
        foreach (Vector2 v in points)
        {
            list.Add(v);
        }

        if (list[0] != f.ToVector2())
        {
            list.Reverse();
        }
        return list;
    }

    public void UpdateLine()
    {
        points = new List<Vector2>();

        _f = nodeFrom.transform.localPosition;
        _t = nodeTo.transform.localPosition;
        _c = nodeCurve.transform.localPosition;
        bisector = new Bisector(_f, _t);

        if (_isInStraight)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, _f);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, _t);
            points.Add(_f.ToVector2());
            points.Add(_t.ToVector2());
        }
        else
        {
            int curveCount = 100;
            lineRenderer.SetPosition(0, _f);
            points.Add(_f.ToVector2());

            lineRenderer.positionCount = curveCount + 1;
            for (int i = 1; i < curveCount; i++)
            {
                float a = PlaneUtils.Angle(bisector.center, _c);
                float d = Vector2.Distance(bisector.center, _c) * 2;
                Vector2 v = PlaneUtils.AngleDistanceGetPoint(bisector.center, a, d);
                Vector3 c = Bezier.BezierCurve(_f, v, _t, i * 0.01f);
                lineRenderer.SetPosition(i, c);
                points.Add(c.ToVector2());
            }
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, _t);
            points.Add(_t.ToVector2());
        }

        lineRenderer.material = new Material(Shader.Find("Standard"));
        DrawBound(2);
    }

    public void DrawBound(int style)
    {
        Color drawColor;
        switch (style)
        {
            case 1:
                drawColor = Color.cyan;
                break;
            case 2:
                drawColor = new Color(0.07F, 0.07F, 0.07F, 1);
                break;
            default:
                drawColor = Color.cyan;
                break;
        }
        lineRenderer.material.color = drawColor;
    }

    public void UpdateCollider()
    {
        transform.localPosition = new Vector3();
        transform.localRotation = Quaternion.Euler(new Vector3());

        foreach (GameObject b in boxs)
        {
            Destroy(b);
        }
        boxs = new List<GameObject>();

        if (points.Count > 2)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 f = new Vector3();
                Vector3 t = new Vector3();

                if (i == points.Count - 1)
                {
                }
                else
                {
                    f = points[i].ToVector3();
                    t = points[i + 1].ToVector3();
                }

                GameObject obj = new GameObject();
                obj.transform.parent = transform.parent;
                DrawLineCollider collider = obj.AddComponent<DrawLineCollider>();
                collider.AddCollider(f, t);
                boxs.Add(obj);
                collider.AddMouseDown(AddColliderDown);
                collider.AddMouseUp(AddColliderUp);
                collider.AddRightMouseUp(AddColliderRightClick);
                collider.AddMouseOver(AddColliderOver);
                collider.AddMouseOut(AddColliderOut);
                obj.layer = gameObject.layer;
                obj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            GameObject obj = new GameObject();
            obj.transform.parent = transform.parent;
            DrawLineCollider collider = obj.AddComponent<DrawLineCollider>();
            collider.AddCollider(points[0], points[1]);
            boxs.Add(obj);
            collider.AddMouseDown(AddColliderDown);
            collider.AddMouseUp(AddColliderUp);
            collider.AddRightMouseUp(AddColliderRightClick);
            collider.AddMouseOver(AddColliderOver);
            collider.AddMouseOut(AddColliderOut);
            obj.layer = gameObject.layer;
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void AddColliderDown()
    {
        _funDown?.Invoke(this);
    }

    private void AddColliderUp()
    {
        _funUp?.Invoke(this);
    }

    private void AddColliderRightClick()
    {
        _funRightClick?.Invoke(this);
    }

    private void AddColliderOver()
    {
        DrawBound(1);
    }

    private void AddColliderOut()
    {
        DrawBound(2);
    }

    private Action<DrawLine> _funDown;
    private Action<DrawLine> _funUp;
    private Action<DrawLine> _funRightClick;

    public void AddDown(Action<DrawLine> fun)
    {
        _funDown = fun;
    }

    public void AddUp(Action<DrawLine> fun)
    {
        _funUp = fun;
    }

    public void AddRightClick(Action<DrawLine> fun)
    {
        _funRightClick = fun;
    }

    RaycastHit hitInfo;

    //private void Update()
    //{
    //    DrawBound(2);

    //    Ray ray = SceneManager.Instance.Camera3D.ScreenPointToRay(Input.mousePosition);
    //    LayerMask layer = 1 << LayerMask.NameToLayer("Water");
    //    if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layer))
    //    {
    //        if (hitInfo.transform.gameObject == gameObject)
    //        {
    //            DrawBound(1);
    //        }

    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            _funDown(this);
    //        }

    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            _funUp(this);
    //        }
    //    }
    //}

    //void OnMouseOver()
    //{
    //    DrawBound(1);

    //    if (Input.GetMouseButton(1))
    //    {
    //        _funRightClick(this);
    //    }
    //}

    //void OnMouseDown()
    //{
    //    _funDown(this);
    //}

    //void OnMouseUp()
    //{
    //    _funUp(this);
    //}

    public void Dispose()
    {
        foreach (GameObject b in boxs)
        {
            Destroy(b);
        }

        Destroy(nodeFrom.gameObject);
        Destroy(nodeTo.gameObject);
        Destroy(nodeCurve.gameObject);

        Destroy(gameObject);
    }
}
