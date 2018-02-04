using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Triangle3D
{
    public Vector3 v1 = new Vector3();
    public Vector3 v2 = new Vector3();
    public Vector3 v3 = new Vector3();

    public Triangle3D(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
    }

    public ArrayList MaxSidePoint
    {
        get
        {
            object[] d = new object[3]{
                new {d = Vector3.Distance(v1,v2),v1 = v1,v2 = v2,v3 = v3},
                new {d = Vector3.Distance(v2,v3),v1 = v2,v2 = v3,v3 = v1},
                new {d = Vector3.Distance(v1,v3),v1 = v1,v2 = v3,v3 = v2}
            };

            Array.Sort(d);
            Hashtable tb = (Hashtable)d[2];

            return new ArrayList { tb["v1"], tb["v2"], tb["v3"] };
        }
    }

    public float maxSideLong()
    {
        float[] d = new float[3] {
            Vector3.Distance (v1, v2),
            Vector3.Distance (v2, v3),
            Vector3.Distance (v1, v3)
        };
        Array.Sort(d);

        return (float)d[0];
    }

    public float width
    {
        get
        {
            float[] d = new float[3] {
                v1.x,
                v2.x,
                v3.x
            };
            Array.Sort(d);

            return d[d.Length - 1] - d[0];
        }
    }

    public float height
    {
        get
        {
            float[] d = new float[3] {
                v1.z,
                v2.z,
                v3.z
            };
            Array.Sort(d);

            return d[d.Length - 1] - d[0];
        }
    }
}

