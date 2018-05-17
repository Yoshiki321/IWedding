using UnityEngine;
using System;
using System.Collections.Generic;

public class PlaneUtils
{
    /// <summary>
    /// 获取点与连接点垂直平面上圆的所有点
    /// </summary>
    /// <returns></returns>
    public static List<Vector3> GetNodePoints(Vector3 v, Vector3 v1, float radius, int count)
    {
        Vector3 v2 = new Vector3(999f, 999f, 999f);
        Vector3 m = v - v1;
        Vector3 m1 = v - v2;
        Vector3 m2 = Vector3.Cross(m, m1);
        Vector3 m3 = Vector3.MoveTowards(v, v1 + m2, radius);

        List<Vector3> list = new List<Vector3>();
        for (int j = 0; j < 360; j += 360 / count)
        {
            list.Add(MathUtils3D.RotateRound(m3, v1, v1 - v, j));
        }
        return list;
    }

    /// <summary>
    /// 计算两点之间的角度 
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="center"></param>
    /// <returns></returns>
    public static float Angle(Vector2 p1, Vector2 p2, bool center = false)
    {
        float x = p1.x - p2.x;
        float y = p1.y - p2.y;
        float rr = Mathf.Atan2(y, x) / Mathf.PI * 180;

        if (center)
        {
            if (rr < 0)
            {
                rr += 360;
            }
        }
        if (rr == 360) rr = 0;
        return rr;
    }

    /// <summary>
    /// 通过点 角度 距离 求另一个点 
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="angle"></param>
    /// <param name="leng"></param>
    /// <returns></returns>
    public static Vector2 AngleDistanceGetPoint(Vector2 p1, float angle, float leng)
    {
        float a = (angle - 180) * Mathf.PI / 180;
        float cx = leng * Mathf.Cos(a);
        float cy = leng * Mathf.Sin(a);
        Vector2 p2 = new Vector2(p1.x + cx, p1.y + cy);
        return p2;
    }
}

