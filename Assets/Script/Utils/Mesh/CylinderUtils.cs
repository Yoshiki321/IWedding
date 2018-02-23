using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 两点画圆柱的静态类
/// </summary>
public class CylinderUtils
{
    private static float _radius;

    public static Mesh CreateCylinder(Vector3 v, Vector3 v1, float radius)
    {
        _radius = radius;

        return null;
    }

    private static Mesh CreateMesh(List<Vector3> points, List<int> trianglesList)
    {
        List<Vector2> uvList = new List<Vector2>();
        foreach (Vector3 v in points)
        {
            uvList.Add(new Vector2(v.x, v.z));
        }

        Vector2[] uvs = new Vector2[uvList.Count];
        Vector3[] normals = new Vector3[uvList.Count];
        Vector4[] tangents = new Vector4[uvList.Count];
        Vector3[] vertices = new Vector3[points.Count];
        int[] triangles = new int[trianglesList.Count];

        for (int i = 0; i < uvList.Count; i++)
        {
            uvs[i] = uvList[i];
            normals[i] = new Vector3(0, 1, 0);
            tangents[i] = new Vector4(-1, 0, 0, -1);
        }

        for (int i = 0; i < points.Count; i++)
        {
            vertices[i] = points[i];
        }

        for (int i = 0; i < trianglesList.Count; i++)
        {
            triangles[i] = trianglesList[i];
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.tangents = tangents;
        mesh.normals = normals;

        return mesh;
    }

    /// <summary>
    /// 绘制顶面
    /// </summary>
    /// <param name="points"></param>
    private static void CreateTop(List<Vector3> points)
    {
        List<int> trianglesList = new List<int>();

        for (int i = 1; i < points.Count; i++)
        {
            trianglesList.Add(0);
            if (i <= points.Count - 2)
            {
                trianglesList.Add(i + 1);
                trianglesList.Add(i);
            }
            else
            {
                trianglesList.Add(1);
                trianglesList.Add(i - 1);
            }
        }
        CreateMesh(points, trianglesList);
    }

    /// <summary>
    /// 创建圆柱侧面
    /// </summary>
    /// <param name="points"></param>
    /// <param name="points1"></param>
    /// <param name="isDouble"></param>
    private static void CreateColumn(List<Vector3> points, List<Vector3> points1, bool isDouble = false)
    {
        List<Vector3> meshPoints = new List<Vector3>();
        meshPoints.AddRange(points);
        meshPoints.AddRange(points1);

        List<int> trianglesList = new List<int>();

        for (int i = 0; i < points.Count; i++)
        {
            int k = 0;
            float min = Mathf.Infinity;
            for (int j = 0; j < points1.Count; j++)
            {
                float d = Vector3.Distance(points[i], points1[j]);
                if (d < min)
                {
                    min = d;
                    k = j;
                }
            }

            if (points.Count + k > meshPoints.Count - 2)
            {
                AddTriangles(trianglesList, i, points.Count + k, points.Count + 1, isDouble);

                if (i + 1 > points.Count - 2)
                {
                    AddTriangles(trianglesList, i, 0, points.Count + k, isDouble);
                }
                else
                {
                    AddTriangles(trianglesList, i, i + 1, points.Count + k, isDouble);
                }
            }
            else
            {
                AddTriangles(trianglesList, i, points.Count + k, points.Count + k + 1, isDouble);

                if (i + 1 > points.Count - 2)
                {
                    AddTriangles(trianglesList, i, 0, points.Count + k, isDouble);
                }
                else
                {
                    AddTriangles(trianglesList, i, i + 1, points.Count + k, isDouble);
                }
            }
        }
        CreateMesh(meshPoints, trianglesList);
    }

    /// <summary>
    /// 添加三角形
    /// </summary>
    /// <param name="trianglesList"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="d"></param>
    private static void AddTriangles(List<int> trianglesList, int x, int y, int z, bool d = true)
    {
        trianglesList.Add(x);
        trianglesList.Add(y);
        trianglesList.Add(z);

        if (d)
        {
            trianglesList.Add(z);
            trianglesList.Add(y);
            trianglesList.Add(x);
        }
    }

    /// <summary>
    /// 获取点与连接点垂直平面上圆的所有点
    /// </summary>
    /// <returns></returns>
    private static List<Vector3> GetNodePoints(Vector3 v, Vector3 v1)
    {
        Vector3 v2 = new Vector3(999f, 999f, 999f);
        Vector3 m = v - v1;
        Vector3 m1 = v - v2;
        Vector3 m2 = Vector3.Cross(m, m1);
        Vector3 m3 = Vector3.MoveTowards(v, v1 + m2, _radius);

        List<Vector3> list = new List<Vector3>();
        for (int j = 0; j < 360; j += 10)
        {
            list.Add(MathUtils3D.RotateRound(m3, v1, v1 - v, j));
        }
        return list;
    }
}
