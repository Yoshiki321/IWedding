using UnityEngine;
using System;

public class PlaneUtils
{
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

