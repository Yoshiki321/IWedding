using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 有厚度的不规则面
/// </summary>
public class ThickIrregularPlane3D : MonoBehaviour
{
    public string id;
    private List<Vector2> _list;
    private float _thickness;

    private string _upCollageId;
    private string _downCollageId;

    public List<Vector2> list
    {
        get { return _list; }
    }

    private List<Vector3> _upPoints;
    private List<Vector3> _downPoints;

    public List<Vector3> upPoints
    {
        get { return _upPoints; }
    }

    public List<Vector3> downPoints
    {
        get { return _downPoints; }
    }

    public void upPanelCollage(string id)
    {
        _upCollageId = id;
        _upPanel.SetCollage(id);
    }

    public void downPanelCollage(string id)
    {
        _downCollageId = id;
        _downPanel.SetCollage(id);
    }

    private SurfacePlane3D _upPanel;
    private SurfacePlane3D _downPanel;

    private BoxCollider _boxCollider;

    private GameObject _upObj;
    private GameObject _downObj;

    public GameObject upObj
    {
        get { return _upObj; }
    }

    public GameObject downObj
    {
        get { return _downObj; }
    }

    /// <summary>
    ///  添加一个面的按顺序的点
    /// </summary>
    /// <param name="list"></param>
    public void SetPoints(List<Vector2> list, float thickness = 50, string upid = "0003", string downid = "0003")
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        if (_boxCollider == null)
        {
            _boxCollider = gameObject.AddComponent<BoxCollider>();
            _boxCollider.enabled = false;
        }

        _list = list;
        _thickness = thickness;

        List<Vector3> meshPoints = new List<Vector3>();
        _upPoints = new List<Vector3>();
        _downPoints = new List<Vector3>();
        foreach (Vector2 v in list)
        {
            _upPoints.Add(new Vector3(v.x, thickness, v.y));
            _downPoints.Add(new Vector3(v.x, 0, v.y));
        }
        meshPoints.AddRange(_upPoints);
        meshPoints.AddRange(_downPoints);

        _upObj = new GameObject("UpObj");
        _downObj = new GameObject("DownObj");
        _upPanel = _upObj.AddComponent<SurfacePlane3D>();
        _downPanel = _downObj.AddComponent<SurfacePlane3D>();
        _upPanel.TwoSided = true;
        _downPanel.TwoSided = true;
        _upPanel.BuildIrregularGeometry(Triangulator.GetMeshData(list, thickness));
        _downPanel.InversionTexture = true;
        _downPanel.BuildIrregularGeometry(Triangulator.GetMeshData(list));

        _upObj.tag = "Collage0";
        _downObj.tag = "Collage1";

        _upPanel.SetCollage(upid);
        _downPanel.SetCollage(downid);
        _upPanel.tilingX = 0.005f;
        _upPanel.tilingY = 0.005f;
        _downPanel.tilingX = 0.005f;
        _downPanel.tilingY = 0.005f;

        //_upPanel.TilingAccordingScale = true;
        //_downPanel.TilingAccordingScale = true;

        GameObject obj = new GameObject("ThickObj");
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        mr.material = new Material(Shader.Find("Standard"));

        _upObj.transform.parent = transform;
        _downObj.transform.parent = transform;
        obj.transform.parent = transform;

        _upObj.layer = gameObject.layer;
        _downObj.layer = gameObject.layer;
        obj.layer = gameObject.layer;

        _upObj.transform.localEulerAngles = new Vector3();
        _downObj.transform.localEulerAngles = new Vector3();
        obj.transform.localEulerAngles = new Vector3();

        _upObj.transform.localPosition = new Vector3();
        _downObj.transform.localPosition = new Vector3();
        obj.transform.localPosition = new Vector3();

        _upObj.transform.localScale = new Vector3(.01f, .01f, .01f);
        _downObj.transform.localScale = new Vector3(.01f, .01f, .01f);
        obj.transform.localScale = new Vector3(.01f, .01f, .01f);

        _upObj.AddComponent<MeshCollider>();
        _downObj.AddComponent<MeshCollider>();
        obj.AddComponent<MeshCollider>();

        List<Vector2> uvList = new List<Vector2>();
        foreach (Vector3 v in meshPoints)
        {
            uvList.Add(new Vector2(v.x, v.z));
        }

        List<int> trianglesList = new List<int>();
        for (int i = 0; i < meshPoints.Count / 2; i++)
        {
            if (i == meshPoints.Count / 2 - 1)
            {
                trianglesList.Add(i);
                trianglesList.Add(0);
                trianglesList.Add(meshPoints.Count / 2);
                trianglesList.Add(i);
                trianglesList.Add(meshPoints.Count / 2 + i);
                trianglesList.Add(meshPoints.Count / 2);
                trianglesList.Add(meshPoints.Count / 2);
                trianglesList.Add(0);
                trianglesList.Add(i);
                trianglesList.Add(meshPoints.Count / 2);
                trianglesList.Add(meshPoints.Count / 2 + i);
                trianglesList.Add(i);
            }
            else
            {
                trianglesList.Add(i);
                trianglesList.Add(i + 1);
                trianglesList.Add(meshPoints.Count / 2 + 1 + i);
                trianglesList.Add(i);
                trianglesList.Add(meshPoints.Count / 2 + i);
                trianglesList.Add(meshPoints.Count / 2 + 1 + i);
                trianglesList.Add(meshPoints.Count / 2 + 1 + i);
                trianglesList.Add(i + 1);
                trianglesList.Add(i);
                trianglesList.Add(meshPoints.Count / 2 + 1 + i);
                trianglesList.Add(meshPoints.Count / 2 + i);
                trianglesList.Add(i);
            }
        }

        Vector2[] uvs = new Vector2[uvList.Count];
        Vector3[] normals = new Vector3[uvList.Count];
        Vector4[] tangents = new Vector4[uvList.Count];
        Vector3[] vertices = new Vector3[meshPoints.Count];
        int[] triangles = new int[trianglesList.Count];

        for (int i = 0; i < uvList.Count; i++)
        {
            uvs[i] = uvList[i];
            normals[i] = new Vector3(0, 1, 0);
            tangents[i] = new Vector4(-1, 0, 0, -1);
        }

        for (int i = 0; i < meshPoints.Count; i++)
        {
            vertices[i] = meshPoints[i];
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

        if (gameObject.transform.parent)
        {
            Quaternion lastq = gameObject.transform.parent.localRotation;
            Vector3 lasts = gameObject.transform.parent.localScale;
            gameObject.transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.parent.localScale = new Vector3(1, 1, 1);
            Renderer[] rList = gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in rList)
            {
                if (r.bounds.size.x == 0 || r.bounds.size.y == 0 || r.bounds.size.z == 0)
                {
                    continue;
                }

                _boxCollider.center = new Vector3(r.bounds.center.x - gameObject.transform.parent.localPosition.x,
                    r.bounds.center.y - gameObject.transform.parent.localPosition.y,
                    r.bounds.center.z - gameObject.transform.parent.localPosition.z);
                _boxCollider.size = new Vector3(r.bounds.size.x,
                    r.bounds.size.y,
                    r.bounds.size.z);
            }

            gameObject.transform.parent.localRotation = lastq;
            gameObject.transform.parent.localScale = lasts;
        }
        else
        {
            Quaternion lastq = gameObject.transform.localRotation;
            Vector3 lasts = gameObject.transform.localScale;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            Renderer[] rList = gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in rList)
            {
                if (r.bounds.size.x == 0 || r.bounds.size.y == 0 || r.bounds.size.z == 0)
                {
                    continue;
                }

                _boxCollider.center = new Vector3(r.bounds.center.x - gameObject.transform.localPosition.x,
                    r.bounds.center.y - gameObject.transform.localPosition.y,
                    r.bounds.center.z - gameObject.transform.localPosition.z);
                _boxCollider.size = new Vector3(r.bounds.size.x,
                    r.bounds.size.y,
                    r.bounds.size.z);
            }

            gameObject.transform.localRotation = lastq;
            gameObject.transform.localScale = lasts;
        }
    }

    private float _tilingX = 1;

    public float tilingX
    {
        set
        {
            _tilingX = value;
            _upPanel.tilingX = value;
            _downPanel.tilingX = value;
        }
    }

    private float _tilingY = 1;

    public float tilingY
    {
        set
        {
            _tilingY = value;
            _upPanel.tilingY = value;
            _downPanel.tilingY = value;
        }
    }

    private float _offestX = 0;

    public float offestX
    {
        set
        {
            _offestX = value;
            _upPanel.offestX = value;
            _downPanel.offestX = value;
        }
    }

    private float _offestY = 0;

    public float offestY
    {
        set
        {
            _offestY = value;
            _upPanel.offestY = value;
            _downPanel.offestY = value;
        }
    }

    public XmlNode Code
    {
        set
        {
            XmlNode code = value as XmlNode;
            XmlNode thickIrregularNode = code.SelectSingleNode("ThickIrregularPlane3D").SelectSingleNode("ThickIrregular");

            string points = thickIrregularNode.Attributes["points"].Value;
            List<Vector2> list = new List<Vector2>();
            string[] p = points.Split(';');
            foreach (string s1 in p)
            {
                string[] s2 = s1.Split(',');
                list.Add(new Vector2(float.Parse(s2[0]), float.Parse(s2[1])));
            }

            float thickness = float.Parse(thickIrregularNode.Attributes["thickness"].Value);
            string upCollageId = thickIrregularNode.Attributes["upCollageId"].Value;
            string downCollageId = thickIrregularNode.Attributes["downCollageId"].Value;

            SetPoints(list, thickness, upCollageId, downCollageId);

            tilingX = float.Parse(thickIrregularNode.Attributes["tilingX"].Value);
            tilingY = float.Parse(thickIrregularNode.Attributes["tilingY"].Value);
            offestX = float.Parse(thickIrregularNode.Attributes["offestX"].Value);
            offestY = float.Parse(thickIrregularNode.Attributes["offestY"].Value);
        }
        get
        {
            string code = "";
            code += "<ThickIrregular";
            code += " id = " + GetPropertyString(id);
            code += " upCollageId = " + GetPropertyString(_upCollageId);
            code += " downCollageId = " + GetPropertyString(_downCollageId);
            code += " tilingX = " + GetPropertyString(_tilingX);
            code += " tilingY = " + GetPropertyString(_tilingY);
            code += " offestX = " + GetPropertyString(_offestX);
            code += " offestY = " + GetPropertyString(_offestY);
            code += " thickness = " + GetPropertyString(_thickness);

            string p = "";
            for (int i = 0; i < _list.Count; i++)
            {
                p += _list[i].x + "," + _list[i].y + ((i == _list.Count - 1) ? "" : ";");
            }
            code += " points = " + GetPropertyString(p);

            code += ">";
            code += "</ThickIrregular>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(code);
            return xml;
        }
    }

    protected string GetPropertyString(object value)
    {
        return '"' + value.ToString() + '"';
    }
}
