using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvyColumnLine : MonoBehaviour
{
    MeshRenderer mr;
    MeshFilter mf;
    Mesh mesh;

    void Awake()
    {
        mr = gameObject.AddComponent<MeshRenderer>();
        mf = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
    }

    private Color _color = Color.blue;

    public Color color
    {
        set
        {
            _color = value;
            mr.material.color = _color;
        }
        get { return _color; }
    }

    public void CreateMesh(List<Vector3> points, List<int> trianglesList)
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

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.tangents = tangents;
        mesh.normals = normals;
        mf.mesh = mesh;

        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.material = new Material(Shader.Find("Unlit/Color"));
        mr.material.color = Color.blue;
    }
}
