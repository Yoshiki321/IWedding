using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MeshUtils3D
{
    public static Vector3 MeshSize(GameObject gameobject)
    {
        MeshFilter[] ms = gameobject.GetComponentsInChildren<MeshFilter>();
        float[] xs = new float[ms.Length];
        float[] ys = new float[ms.Length];
        float[] zs = new float[ms.Length];

        for (int i = 0; i < ms.Length; i++)
        {
            xs[i] = ms[i].mesh.bounds.size.x;
            ys[i] = ms[i].mesh.bounds.size.y;
            zs[i] = ms[i].mesh.bounds.size.z;
        }

        float maxx = 0;
        for (int i = 0; i < xs.Length; i++)
        {
            maxx = Mathf.Max(maxx, xs[i]);
        }

        float maxy = 0;
        for (int i = 0; i < ys.Length; i++)
        {
            maxy = Mathf.Max(maxy, ys[i]);
        }

        float maxz = 0;
        for (int i = 0; i < zs.Length; i++)
        {
            maxz = Mathf.Max(maxz, zs[i]);
        }

        return new Vector3(maxx, maxy, maxz);
    }

    public static Vector3 BoundSizeBoxCollider(GameObject gameobject)
    {
        if (gameobject == null)
        {
            Debug.Log("BoundSizeBoxCollider----null");
            return Vector3.zero;
        }

        BoxCollider[] mc = gameobject.GetComponentsInChildren<BoxCollider>();
        BoxCollider[] mp = gameobject.GetComponentsInParent<BoxCollider>();
        BoxCollider[] ms = new BoxCollider[mc.Length + mp.Length];

        if (mc.Length == 0 && mp.Length == 0)
        {
            return Vector3.zero;
        }

        int j = 0;
        foreach (BoxCollider box in mc)
        {
            ms[j] = box;
            j++;
        }
        foreach (BoxCollider box in mp)
        {
            ms[j] = box;
            j++;
        }

        float[] maxxs = new float[ms.Length];
        float[] maxys = new float[ms.Length];
        float[] maxzs = new float[ms.Length];
        float[] minxs = new float[ms.Length];
        float[] minys = new float[ms.Length];
        float[] minzs = new float[ms.Length];

        for (int i = 0; i < ms.Length; i++)
        {
            maxxs[i] = ms[i].center.x + ms[i].size.x / 2;
            maxys[i] = ms[i].center.y + ms[i].size.y / 2;
            maxzs[i] = ms[i].center.z + ms[i].size.z / 2;
            minxs[i] = ms[i].center.x - ms[i].size.x / 2;
            minys[i] = ms[i].center.y - ms[i].size.y / 2;
            minzs[i] = ms[i].center.z - ms[i].size.z / 2;
        }

        float maxx = 0;
        for (int i = 0; i < maxxs.Length; i++) maxx = Mathf.Max(maxx, maxxs[i]);

        float maxy = 0;
        for (int i = 0; i < maxys.Length; i++) maxy = Mathf.Max(maxy, maxys[i]);

        float maxz = 0;
        for (int i = 0; i < maxzs.Length; i++) maxz = Mathf.Max(maxz, maxzs[i]);

        float minx = 0;
        for (int i = 0; i < minxs.Length; i++) minx = Mathf.Min(minx, minxs[i]);

        float miny = 0;
        for (int i = 0; i < minys.Length; i++) miny = Mathf.Min(miny, minys[i]);

        float minz = 0;
        for (int i = 0; i < minzs.Length; i++) minz = Mathf.Min(minz, minzs[i]);

        float xx = maxx - minx;
        float yy = maxy - miny;
        float zz = maxz - minz;

        return new Vector3(xx * gameobject.transform.localScale.x, yy * gameobject.transform.localScale.y, zz * gameobject.transform.localScale.z);
    }

    public static Vector3 BoundSizeRenderer(GameObject gameobject)
    {
        Renderer[] ms = gameobject.GetComponentsInChildren<Renderer>();
        float[] xs = new float[ms.Length];
        float[] ys = new float[ms.Length];
        float[] zs = new float[ms.Length];

        for (int i = 0; i < ms.Length; i++)
        {
            xs[i] = ms[i].bounds.size.x;
            ys[i] = ms[i].bounds.size.y;
            zs[i] = ms[i].bounds.size.z;
        }

        float maxx = 0;
        for (int i = 0; i < xs.Length; i++)
        {
            maxx = Mathf.Max(maxx, xs[i]);
        }

        float maxy = 0;
        for (int i = 0; i < ys.Length; i++)
        {
            maxy = Mathf.Max(maxy, ys[i]);
        }

        float maxz = 0;
        for (int i = 0; i < zs.Length; i++)
        {
            maxz = Mathf.Max(maxz, zs[i]);
        }

        return new Vector3(maxx, maxy, maxz);
    }

    public static string MeshToString(MeshFilter mf, Vector3 scale)
    {
        Mesh mesh = mf.mesh;
        Material[] sharedMaterials = mf.GetComponent<Renderer>().sharedMaterials;
        Vector2 textureOffset = mf.GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
        Vector2 textureScale = mf.GetComponent<Renderer>().material.GetTextureScale("_MainTex");

        StringBuilder stringBuilder = new StringBuilder().Append("mtllib design.mtl")
            .Append("\n")
            .Append("g ")
            .Append(mf.name)
            .Append("\n");

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vector = vertices[i];
            stringBuilder.Append(string.Format("v {0} {1} {2}\n", vector.x * scale.x, vector.y * scale.y, vector.z * scale.z));
        }

        stringBuilder.Append("\n");

        Dictionary<int, int> dictionary = new Dictionary<int, int>();

        if (mesh.subMeshCount > 1)
        {
            int[] triangles = mesh.GetTriangles(1);

            for (int j = 0; j < triangles.Length; j += 3)
            {
                if (!dictionary.ContainsKey(triangles[j]))
                {
                    dictionary.Add(triangles[j], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 1]))
                {
                    dictionary.Add(triangles[j + 1], 1);
                }

                if (!dictionary.ContainsKey(triangles[j + 2]))
                {
                    dictionary.Add(triangles[j + 2], 1);
                }
            }
        }

        for (int num = 0; num != mesh.uv.Length; num++)
        {
            Vector2 vector2 = Vector2.Scale(mesh.uv[num], textureScale) + textureOffset;

            if (dictionary.ContainsKey(num))
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", mesh.uv[num].x, mesh.uv[num].y));
            }
            else
            {
                stringBuilder.Append(string.Format("vt {0} {1}\n", vector2.x, vector2.y));
            }
        }

        for (int k = 0; k < mesh.subMeshCount; k++)
        {
            stringBuilder.Append("\n");

            if (k == 0)
            {
                stringBuilder.Append("usemtl ").Append("Material_design").Append("\n");
            }

            if (k == 1)
            {
                stringBuilder.Append("usemtl ").Append("Material_logo").Append("\n");
            }

            int[] triangles2 = mesh.GetTriangles(k);

            for (int l = 0; l < triangles2.Length; l += 3)
            {
                stringBuilder.Append(string.Format("f {0}/{0} {1}/{1} {2}/{2}\n", triangles2[l] + 1, triangles2[l + 2] + 1, triangles2[l + 1] + 1));
            }
        }

        return stringBuilder.ToString();
    }
}
