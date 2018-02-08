using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawNode : MonoBehaviour
{
    public DrawLine line;
    public DrawNode relationNode;

    private void Start()
    {
    }

    public void SetType(bool isLine)
    {
        GameObject obj;
        if (isLine)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        else
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        obj.transform.parent = transform;
        obj.transform.localScale = new Vector3(12f, 12f, 12f);

        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(20, 20, 20);

        gameObject.layer = transform.parent.gameObject.layer;
        obj.layer = gameObject.layer;
    }

    public DrawNode Opposition
    {
        get { return line.Opposition(transform.localPosition); }
    }

    public void Move(Vector3 v)
    {
        if (name == "NodeFrom")
            line.MovePoint(v, line.nodeTo.transform.localPosition, line.nodeCurve.transform.localPosition);
        if (name == "NodeTo")
            line.MovePoint(line.nodeFrom.transform.localPosition, v, line.nodeCurve.transform.localPosition);
        if (name == "NodeCurve")
            line.MovePoint(line.nodeFrom.transform.localPosition, line.nodeTo.transform.localPosition, v);
    }

    private Action<DrawNode> _funDown;
    private Action<DrawNode> _funUp;

    public void AddDown(Action<DrawNode> fun)
    {
        _funDown = fun;
    }

    public void AddUp(Action<DrawNode> fun)
    {
        _funUp = fun;
    }

    void OnMouseDown()
    {
        _funDown?.Invoke(this);
    }

    void OnMouseUp()
    {
        _funUp?.Invoke(this);
    }
}
