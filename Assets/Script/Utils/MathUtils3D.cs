using System;
using UnityEngine;

public class MathUtils3D
{
    public static Vertex crossProduct(Vertex param1, Vertex param2)
    {
        Vertex v1 = new Vertex();
        v1.x = param1.y * param2.z - param1.z * param2.y;
        v1.y = param1.z * param2.x - param1.x * param2.z;
        v1.z = param1.x * param2.y - param1.y * param2.x;

        float v2 = Mathf.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        v1.x = v1.x / v2;
        v1.y = v1.y / v2;
        v1.z = v1.z / v2;
        return v1;
    }

    public static Vertex substract(Vertex param1, Vertex param2)
    {
        Vertex v = new Vertex();
        v.x = param1.x - param2.x;
        v.y = param1.y - param2.y;
        v.z = param1.z - param2.z;
        return v;
    }
}

