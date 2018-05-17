using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvyColumn : MonoBehaviour
{
    private List<Vector3> _meshPoints = new List<Vector3>();

    private Transform _parentLayer;

    public GameObject layer { get; private set; }

    public void SetPoints(List<Vector3> value, Transform parentLayer, bool isLink = false)
    {
        objectList = new List<GameObject>();

        _parentLayer = parentLayer;

        if (layer == null)
        {
            layer = new GameObject();
            layer.name = "CurvyColumn";
            layer.transform.parent = parentLayer;
            layer.transform.rotation = parentLayer.rotation;
            layer.transform.position = new Vector3();
        }

        _meshPoints = value;

        for (int i = 0; i < _meshPoints.Count; i++)
        {
            if (i == _meshPoints.Count - 1 && !isLink) return;

            if (i == _meshPoints.Count - 1 && isLink)
            {
                List<Vector3> list = PlaneUtils.GetNodePoints(_meshPoints[i], _meshPoints[0], _radius, 10);
                List<Vector3> list1 = PlaneUtils.GetNodePoints(_meshPoints[0], _meshPoints[i], _radius, 10);

                CreateColumn(list, list1);

                list.Insert(0, _meshPoints[i]);
                CreateTop(list);

                list1.Insert(0, _meshPoints[0]);
                CreateTop(list1);
            }
            else
            {
                List<Vector3> list = PlaneUtils.GetNodePoints(_meshPoints[i], _meshPoints[i + 1], _radius, 10);
                List<Vector3> list1 = PlaneUtils.GetNodePoints(_meshPoints[i + 1], _meshPoints[i], _radius, 10);

                CreateColumn(list, list1);

                list.Insert(0, _meshPoints[i]);
                CreateTop(list);

                list1.Insert(0, _meshPoints[i + 1]);
                CreateTop(list1);
            }

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Color"));
            sphere.GetComponent<MeshRenderer>().material.color = _color;
            sphere.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            if (layer != null)
            {
                sphere.transform.parent = layer.transform;
            }
            else
            {
                sphere.transform.parent = gameObject.transform.parent;
            }

            sphere.transform.localScale = new Vector3(_radius * 2f, _radius * 2f, _radius * 2f);
            sphere.layer = _parentLayer.gameObject.layer;
            sphere.transform.localRotation = Quaternion.Euler(new Vector3());
            sphere.transform.localPosition = _meshPoints[i];
            objectList.Add(sphere);
        }
    }

    public static bool IsParallel(Vector3 lhs, Vector3 rhs)
    {
        float value = Vector3.Dot(lhs.normalized, rhs.normalized);
        if (Mathf.Abs(value) == 1)
            return true;
        return false;
    }

    private List<GameObject> objectList;

    private void CreateColumn(List<Vector3> points, List<Vector3> points1, bool isDouble = false)
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

    private void AddTriangles(List<int> trianglesList, int x, int y, int z, bool d = true)
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

    private void CreateTop(List<Vector3> points)
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

    private void CreateMesh(List<Vector3> points, List<int> trianglesList)
    {
        GameObject obj = new GameObject();
        CurvyColumnLine line = obj.AddComponent<CurvyColumnLine>();
        line.CreateMesh(points, trianglesList);
        line.color = _color;

        objectList.Add(obj);

        if (_parentLayer != null)
        {
            obj.transform.parent = layer.transform;
        }
        else
        {
            obj.transform.parent = gameObject.transform.parent;
        }

        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.layer = _parentLayer.gameObject.layer;
        obj.transform.localRotation = Quaternion.Euler(new Vector3());
        obj.transform.localPosition = new Vector3();
    }

    private float _radius = 1.5f;

    public float radius
    {
        set
        {
            _radius = value;

            foreach (GameObject obj in objectList)
            {
                Destroy(obj);
            }

            SetPoints(_meshPoints, _parentLayer, true);
        }
        get { return _radius; }
    }

    private Color _color = Color.blue;

    public Color color
    {
        set
        {
            _color = value;

            foreach (GameObject obj in objectList)
            {
                obj.GetComponentInChildren<MeshRenderer>().material.color = _color;
            }
        }
        get { return _color; }
    }
}
