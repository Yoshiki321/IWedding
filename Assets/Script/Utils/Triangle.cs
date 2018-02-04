using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    private Vector2 p1;
    private Vector2 p2;
    private Vector2 p3;

    public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    /// <summary>
    /// 点是否在三角形内 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool Inside(Vector2 p)
    {
        if (Vector2.Distance(p1, p) + Vector2.Distance(p2, p) == Vector2.Distance(p1, p2) ||
            Vector2.Distance(p1, p) + Vector2.Distance(p3, p) == Vector2.Distance(p1, p3) ||
            Vector2.Distance(p3, p) + Vector2.Distance(p2, p) == Vector2.Distance(p3, p2) ||
            (p1.x == p.x && p1.y == p.y) ||
            (p2.x == p.x && p2.y == p.y) ||
            (p3.x == p.x && p3.y == p.y))
        {
            return true;
        }

        float planeAB = (p1.x - p.x) * (p2.y - p.y) - (p2.x - p.x) * (p1.y - p.y);
        float planeBC = (p2.x - p.x) * (p3.y - p.y) - (p3.x - p.x) * (p2.y - p.y);
        float planeCA = (p3.x - p.x) * (p1.y - p.y) - (p1.x - p.x) * (p3.y - p.y);
        return sign(planeAB) == sign(planeBC) && sign(planeBC) == sign(planeCA);
    }

    private float sign(float n)
    {
        return Mathf.Abs(n) / n;
    }
}
