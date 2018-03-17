using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorptionManager
{
    private static bool _dotToLineEnabled = true;
    private static bool _dotToDotEnabled = true;

    public static bool dotToLineEnabled
    {
        set { _dotToLineEnabled = value; }

    }
    public static bool dotToDotEnabled
    {
        set { _dotToDotEnabled = value; }
    }

    /// <summary>
    /// 水平垂直上的点
    /// </summary>
    /// <param name="node"></param>
    /// <param name="nodes"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Hashtable NodeToPointLine(Vector2 node, List<Vector2> nodes, float range = 10)
    {
        if (!_dotToDotEnabled) return null;

        List<Bisector> pointLines = new List<Bisector>();
        float distance;

        for (int i = 0; i < nodes.Count; i++)
        {
            pointLines.Add(new Bisector(nodes[i], nodes[i] + (new Vector2(0, 1))));
            pointLines.Add(new Bisector(nodes[i], nodes[i] + (new Vector2(1, 0))));
        }

        Bisector vb = null;
        Bisector hb = null;
        Vector2 rp = node.Clone();

        for (int i = 0; i < pointLines.Count; i++)
        {
            distance = pointLines[i].Distance(node);

            //水平
            if (pointLines[i].k == 0 && distance <= range)
            {
                hb = pointLines[i];
            }

            //垂直
            if (float.IsNaN(pointLines[i].k) && distance <= range)
            {
                vb = pointLines[i];
            }
        }

        Hashtable h = new Hashtable();

        if (vb != null || hb != null)
        {
            if (hb != null) rp.y = hb.b;
            if (vb != null) rp.x = vb.a;

            h.Add("p", rp);
            h.Add("vb", vb);
            h.Add("hb", hb);

            return h;
        }

        h.Add("p", new Vector2().Null());
        h.Add("vb", null);
        h.Add("hb", null);

        return h;
    }

    /// <summary>
    /// 点对点吸附
    /// </summary>
    /// <param name="point"></param>
    /// <param name="points"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Vector2 PointToPoint(Vector2 point, List<Vector2> points, float range = 10)
    {
        if (!_dotToDotEnabled) return new Vector2().Null();

        float distance;
        for (int i = 0; i < points.Count; i++)
        {
            distance = Vector2.Distance(points[i], point);
            if (distance <= range)
            {
                return points[i];
            }
        }

        return new Vector2().Null();
    }

    /// <summary>
    /// 点对线吸附 
    /// </summary>
    /// <param name="point"></param>
    /// <param name="bisectors"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Hashtable PointToLine(Vector2 point, List<Bisector> bisectors, float range = 10)
    {
        if (!_dotToLineEnabled) return null;

        Bisector bisector;
        float distance;

        List<Hashtable> sortLines = new List<Hashtable>();

        Hashtable hashtable = new Hashtable();

        for (int i = 0; i < bisectors.Count; i++)
        {
            bisector = bisectors[i];
            distance = bisector.Distance(point);

            if (distance <= range)
            {
                hashtable = new Hashtable();
                hashtable.Add("b", bisector);
                hashtable.Add("d", distance);
                sortLines.Add(hashtable);
            }
        }

        sortLines.Sort(delegate (Hashtable px, Hashtable py)
        {
            float d1 = (float)px["d"];
            float d2 = (float)py["d"];
            return d1.CompareTo(d2);
        });

        if (sortLines.Count > 0)
        {
            List<Bisector> ls = new List<Bisector>();

            foreach (Hashtable h in sortLines)
            {
                ls.Add((Bisector)h["b"]);
            }

            Vector2 p;

            if (sortLines.Count > 1)
            {

                Bisector b1 = (Bisector)sortLines[0]["b"];
                Bisector b2 = (Bisector)sortLines[1]["b"];

                hashtable = new Hashtable();

                p = b1.Intersection(b2);

                if (float.IsNaN(p.x) || float.IsNaN(p.y))
                {

                    hashtable.Add("p", b1.VerticalIntersection(point));
                    hashtable.Add("b", ls);

                    return hashtable;
                }
                else
                {

                    hashtable.Add("p", p);
                    hashtable.Add("b", ls);

                    return hashtable;
                }
            }
            else
            {
                Bisector b1 = (Bisector)sortLines[0]["b"];

                p = b1.VerticalIntersection(point);

                if (!p.IsNull())
                {
                    hashtable = new Hashtable();
                    hashtable.Add("p", p);
                    hashtable.Add("b", ls);

                    return hashtable;
                }
            }
        }

        hashtable = new Hashtable();
        hashtable.Add("p", new Vector2().Null());
        hashtable.Add("b", null);

        return hashtable;
    }
}
