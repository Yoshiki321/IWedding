using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawNode : MonoBehaviour
{
    public DrawLine line;
    public DrawNode relationNode;
    private Image image;

    private void Start()
    {
    }

    public void SetType(bool isLine)
    {
        image = gameObject.AddComponent<Image>();
        image.rectTransform.sizeDelta = new Vector2(20, 20);
        if (isLine)
        {
            image.overrideSprite = Resources.Load("UI/DrawLinePanel/nodep", typeof(Sprite)) as Sprite;
        }
        else
        {
            image.overrideSprite = Resources.Load("UI/DrawLinePanel/nodec", typeof(Sprite)) as Sprite;
        }

        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(20, 20, 20);

        gameObject.layer = LayerMask.NameToLayer("UI");
    }

    public DrawNode Opposition
    {
        get { return line.Opposition(transform.localPosition); }
    }

    public void Move(Vector3 v)
    {
        if (name == "NodeFrom")
            line.MovePoint(v, line.nodeTo.transform.position, line.nodeCurve.transform.position);
        if (name == "NodeTo")
            line.MovePoint(line.nodeFrom.transform.position, v, line.nodeCurve.transform.position);
        if (name == "NodeCurve")
            line.MovePoint(line.nodeFrom.transform.position, line.nodeTo.transform.position, v);
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
        _funDown(this);
    }

    void OnMouseUp()
    {
        _funUp(this);
    }
}
