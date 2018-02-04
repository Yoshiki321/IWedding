using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bisector
{
    public float k;
    public float b;
    public float a;

    public Vector2 intX;
    public Vector2 intY;

    public Vector2 from;
    public Vector2 to;

    public Bisector(Vector2 p1 = new Vector2(), Vector2 p2 = new Vector2())
    {
        CountOblique(p1, p2);
        from = p1;
        to = p2;
        InitBorders(p1, p2);
    }

    public Bisector Clone()
    {
        return new Bisector(from, to);
    }

    public Bisector Translation(float value)
    {
        float d = value * Mathf.Sqrt((Mathf.Pow(k, 2) + 1));

        Bisector b = new Bisector();

        if (float.IsNaN(k))
        {
            if (angle == 90)
            {
                b = new Bisector(new Vector2(from.x + value, from.y), new Vector2(to.x + value, to.y));
            }
            else
            {
                b = new Bisector(new Vector2(from.x - value, from.y), new Vector2(to.x - value, to.y));
            }
        }
        else if (k == 0)
        {
            if (angle == 180)
            {
                b = new Bisector(new Vector2(from.x, from.y + value), new Vector2(to.x, to.y + value));
            }
            else
            {
                b = new Bisector(new Vector2(from.x, from.y - value), new Vector2(to.x, to.y - value));
            }
        }
        else
        {
            if (angle >= 90 && angle < 270)
            {
                b = new Bisector(new Vector2(from.x, from.y + d), new Vector2(to.x, to.y + d));
            }
            else
            {
                b = new Bisector(new Vector2(from.x, from.y - d), new Vector2(to.x, to.y - d));
            }
        }

        return b;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    protected void InitBorders(Vector2 p1, Vector2 p2)
    {
        intX = p1;
        intY = p2;
    }

    /// <summary>
    /// 点是否在线段上
    /// </summary>
    /// <param name="p"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public bool PointInStraight(Vector2 p, float offset = 0.1f)
    {
        if (k != 0 && Mathf.Abs(from.y - to.y) > offset)
        {
            if (p.y < from.y && p.y < to.y) return false;
            if (p.y > from.y && p.y > to.y) return false;
        }

        if (!float.IsNaN(k) && Mathf.Abs(from.x - to.x) > offset)
        {
            if (p.x < from.x && p.x < to.x) return false;
            if (p.x > from.x && p.x > to.x) return false;
        }

        return PointIn(p, offset);
    }

    /// <summary>
    /// 点是否在直线上 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public bool PointIn(Vector2 p, float offset = 0)
    {
        if (Mathf.Abs(Mathf.Round(p.y) - Mathf.Round(k * p.x + b)) <= offset || Mathf.Abs(Mathf.Round(p.x) - Mathf.Round((p.y - b) / k)) <= offset) return true;
        if (float.IsNaN(k) && Mathf.Abs(p.x - from.x) <= offset) return true;
        if (k == 0f && !float.IsNaN(k) && Mathf.Abs(p.y - from.y) <= offset) return true;
        if (p.x == a) return true;
        return false;
    }

    /// <summary>
    /// 计算kba
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    private void CountOblique(Vector2 p1, Vector2 p2)
    {
        if (p1.x - p2.x == 0)
        {
            k = float.NaN;
            b = float.NaN;
            a = p1.x;
        }
        else if (p1.y - p2.y == 0)
        {
            k = 0;
            b = p1.y;
            a = float.NaN;
        }
        else
        {
            k = (p1.y - p2.y) / (p1.x - p2.x);
            b = p1.y - k * p1.x;
            a = -b / k;
        }
    }

    /// <summary>
    /// 根据一个点取对应点
    /// </summary>
    /// <param name="p"></param>
    /// <param name="same"></param>
    /// <returns></returns>
    public Vector2 Other(Vector2 p, bool same = true)
    {
        if (same)
        {
            if (p.Equals(from))
            {
                return to;
            }
            else if (p.Equals(to))
            {
                return from;
            }
            return new Vector2().Null();
        }
        else
        {
            float d1 = Vector2.Distance(from, p);
            float d2 = Vector2.Distance(to, p);
            return d1 < d2 ? to : from;
        }
    }

    /// <summary>
    /// 一个点按终点到起点的方向移动 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public Vector2 DerivedFrom(Vector2 t)
    {
        return PlaneUtils.AngleDistanceGetPoint(t, angleReverse, length);
    }

    /// <summary>
    /// 一个点按起点到终点的方向移动 
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public Vector2 DerivedTo(Vector2 f)
    {
        return PlaneUtils.AngleDistanceGetPoint(f, angle, length);
    }

    /// <summary>
    /// 点到线的距离
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public float Distance(Vector2 p)
    {
        if (float.IsNaN(k))
        {
            return Mathf.Abs(p.x - from.x);
        }
        else
        {
            return Mathf.Abs(k * p.x - p.y + b) / Mathf.Sqrt(Mathf.Pow(k, 2) + 1);
        }
    }

    /// <summary>
    /// 长度 
    /// </summary>
    public float length
    {
        get { return Vector2.Distance(from, to); }
    }

    /// <summary>
    /// 起点到终点的角度 
    /// </summary>
    public float angle
    {
        get { return PlaneUtils.Angle(from, to, true); }
    }

    /// <summary>
    /// 终点到起点的角度 
    /// </summary>
    public float angleReverse
    {
        get { return PlaneUtils.Angle(to, from, true); }
    }

    /// <summary>
    /// 是否成直线
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public bool IsStraightLine(Bisector b)
    {
        return (((int)angle == (int)b.angle ||
            (int)angle == (int)b.angleReverse) &&
            PointIn(b.from) && PointIn(b.to));
    }

    /// <summary>
    /// 中心点
    /// </summary>
    public Vector2 center
    {
        get { return Vector2.Lerp(from, to, .5f); }
    }

    /// <summary>
    /// 经过终点的垂直线 
    /// </summary>
    public Bisector verticalTo
    {
        get { return GetVertical(to); }
    }

    /// <summary>
    /// 经过起点的垂直线 
    /// </summary>
    public Bisector verticalFrom
    {
        get { return GetVertical(from); }
    }

    private Bisector vertical
    {
        get
        {
            Bisector vBisector = Clone();
            if (float.IsNaN(k))
            {
                vBisector.k = 0;
            }
            else if (k == 0)
            {
                vBisector.k = float.NaN;
            }
            else
            {
                vBisector.k = -(1 / k);
            }
            return vBisector;
        }
    }

    /// <summary>
    /// 经过点的垂直线 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public Bisector GetVertical(Vector2 p)
    {
        Bisector vBisector = vertical;
        vBisector.b = p.y - vertical.k * p.x;
        vBisector.a = p.x;
        return vBisector;
    }

    /// <summary>
    /// 可与线相交的垂直最近点 
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Vector2 VerticalIntersection(Vector2 point)
    {
        if ((float.IsNaN(k) && ((point.y <= from.y && point.y >= to.y) ||
            (point.y >= from.y && point.y <= to.y))) ||
            (k == 0 && ((point.x <= from.x && point.x >= to.x) ||
            (point.x >= from.x && point.x <= to.x))) ||
            k != 0)
        {
            Bisector b = new Bisector();
            b.k = verticalFrom.k;
            b.b = point.y - verticalFrom.k * point.x;
            b.a = point.x;
            Vector2 p = Intersection(b);
            if (PointInStraight(p))
            {
                return p;
            }
        }

        return new Vector2().Null();
    }

    /// <summary>
    /// 求夹角 |k1-k2|/(1+k1k2) 
    /// </summary>
    /// <param name="bisector"></param>
    /// <returns></returns>
    public float IncludedAngle(Bisector bisector)
    {
        if (bisector == null) return float.NaN;

        if (1 + k * bisector.k == 0)
            return 90;
        if ((float.IsNaN(k) && bisector.k == 0) || (float.IsNaN(bisector.k) && k == 0))
            return 90;
        return MathUtils.ConvertToDegree(Mathf.Atan(Mathf.Abs((k - bisector.k) / (1 + k * bisector.k))));
    }

    /// <summary>
    /// 直线相交点 
    /// </summary>
    /// <param name="bisector"></param>
    /// <returns></returns>
    public Vector2 Intersection(Bisector bisector)
    {
        if (bisector == null) return new Vector2(float.NaN, float.NaN);

        Vector2 rp;
        float n1, n2, n3;
        if (float.IsNaN(k))
        {
            rp = new Vector2(a, bisector.k * a + bisector.b);
        }
        else if (float.IsNaN(bisector.k))
        {
            rp = new Vector2(bisector.a, k * bisector.a + b);
        }
        else
        {
            n1 = bisector.b - b;
            n2 = k - bisector.k;
            if (n2 == 0)
            {
                if (n1 == 0)
                {
                    rp = new Vector2(float.NaN, float.NaN);
                }
                else
                {
                    rp = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
                }
            }
            else
            {
                n3 = n1 / n2;
                rp = new Vector2(Mathf.Round(100000 * n3) / 100000, Mathf.Round(100000 * (k * n3 + b)) / 100000);
            }
        }

        if (float.IsNaN(rp.x) || float.IsNaN(rp.y))
        {
            return new Vector2(float.NaN, float.NaN);
        }

        return rp;
    }

    /// <summary>
    /// 线段交叉点 
    /// </summary>
    /// <param name="bisector"></param>
    /// <returns></returns>
    public Vector2 IntersectionSegment(Bisector bisector)
    {
        if (bisector.from.Equals(from)) return from;
        if (bisector.from.Equals(to)) return to;
        if (bisector.to.Equals(from)) return from;
        if (bisector.to.Equals(to)) return to;

        Vector2 rp = Intersection(bisector);
        if ((PointInStraight(rp) && bisector.PointInStraight(rp)) && !rp.Equals(from) && !rp.Equals(to)
            && !rp.Equals(bisector.from) && !rp.Equals(bisector.to))
        {
            return rp;
        }
        return new Vector2(float.NaN, float.NaN);
    }
}
