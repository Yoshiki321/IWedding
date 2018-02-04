using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class VectorExtend
{
    public static string GetCode(this Vector2 vector2)
    {
        string s = "";
        s += '"';
        s += vector2.x;
        s += ",";
        s += vector2.y;
        s += '"';
        return s;
    }

    public static Vector2 SetCode(this Vector2 vector2, string code)
    {
        string[] a = Regex.Split(code, ",");
        vector2.x = float.Parse(a[0]);
        vector2.y = float.Parse(a[1]);
        return vector2;
    }

    public static Vector2 Clone(this Vector2 vector2)
    {
        Vector2 v = new Vector2();
        v.x = vector2.x;
        v.y = vector2.y;
        return v;
    }

    public static bool IsNaN(this Vector3 vector3)
    {
        if (float.IsNaN(vector3.x) || float.IsNaN(vector3.y) || float.IsNaN(vector3.z))
        {
            return true;
        }
        return false;
    }

    public static bool IsInfinity(this Vector3 vector3)
    {
        if (float.IsInfinity(vector3.x) || float.IsInfinity(vector3.y) || float.IsInfinity(vector3.z))
        {
            return true;
        }
        return false;
    }

    public static Vector2 Null(this Vector2 vector2)
    {
        vector2.x = 999999999;
        vector2.y = 999999999;
        return vector2;
    }

    public static bool IsNull(this Vector2 vector2)
    {
        if (vector2.x == 999999999 && vector2.y == 999999999)
        {
            return true;
        }
        return false;
    }

    public static Vector3 Clone(this Vector3 vector3)
    {
        Vector3 v = new Vector3();
        v.x = vector3.x;
        v.y = vector3.y;
        v.z = vector3.z;
        return v;
    }

    public static Vector3 Null(this Vector3 vector)
    {
        vector.x = 999999999;
        vector.y = 999999999;
        vector.z = 999999999;
        return vector;
    }

    public static bool IsNull(this Vector3 vector)
    {
        if (vector.x == 999999999 && vector.y == 999999999)
        {
            return true;
        }
        return false;
    }

    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static Vector2 Round(this Vector2 vector)
    {
        return new Vector2(float.Parse(vector.x.ToString("#0.0000")), float.Parse(vector.y.ToString("#0.0000")));
    }

    public static Vector3 ToVector3(this Vector2 vector)
    {
        return new Vector3(vector.x, vector.y);
    }
}
