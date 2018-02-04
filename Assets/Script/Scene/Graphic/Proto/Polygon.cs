using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon
{
    private List<Vector2> _points;

    private List<Triangle> _triangle;

    /// <summary>
    /// 创建一个多边形 
    /// </summary>
    public Polygon()
    {

    }

    public List<Vector2> points
    {
        set { _points = value; }
        get { return _points; }
    }

    public void triangulate()
    {
        _triangle = CutTriangulate();
    }

    /// <summary>
    /// 获取多边形内所有的三角形 
    /// </summary>
    public List<Triangle> triangles
    {
        get { return _triangle; }
    }

    /// <summary>
    /// 点是否在多边形内 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool inside(Vector2 p)
    {
        for (int i = 0; i < _triangle.Count; i++)
        {
            if (_triangle[i].Inside(p))
            {
                return true;
            }
        }
        return false;
    }

    private const float EPSILON = 9999999999f;

    /// <summary>
    /// 分割出所有的三角形 
    /// </summary>
    /// <returns></returns>
    private List<Triangle> CutTriangulate()
    {
        return Triangulator.GetMeshData(_points).triangleList;
    }
}
