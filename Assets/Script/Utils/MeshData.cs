using System;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    private Vector2[] _uvs;

    public Vector2[] uvs
    {
        set { _uvs = value; }
        get { return _uvs; }
    }

    private Vector3[] _vertices;

    public Vector3[] vertices
    {
        set { _vertices = value; }
        get { return _vertices; }
    }

    private int[] _triangles;

    public int[] triangles
    {
        set { _triangles = value; }
        get { return _triangles; }
    }

    private List<Triangle> _triangleList;

    public List<Triangle> triangleList
    {
        set { _triangleList = value; }
        get { return _triangleList; }
    }
}


