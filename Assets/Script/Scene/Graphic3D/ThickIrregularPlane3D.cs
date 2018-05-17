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
    private float _thickness;

    private string _upCollageId;
    private string _downCollageId;

    public List<Vector2> list { get; private set; }

    public List<Vector3> upPoints { get; private set; }

    public List<Vector3> downPoints { get; private set; }

    public void UpPanelCollage(string id)
    {
        _upCollageId = id;
        _upPanel.SetCollage(id);
    }

    public void DownPanelCollage(string id)
    {
        _downCollageId = id;
        _downPanel.SetCollage(id);
    }

    private SurfacePlane3D _upPanel;
    private SurfacePlane3D _downPanel;

    private BoxCollider _boxCollider;

    public GameObject upObj { get; private set; }

    public GameObject downObj { get; private set; }

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

        this.list = list;
        _thickness = thickness;

        List<Vector3> meshPoints = new List<Vector3>();
        upPoints = new List<Vector3>();
        downPoints = new List<Vector3>();
        foreach (Vector2 v in list)
        {
            upPoints.Add(new Vector3(v.x, thickness, v.y));
            downPoints.Add(new Vector3(v.x, 0, v.y));
        }
        meshPoints.AddRange(upPoints);
        meshPoints.AddRange(downPoints);

        upObj = new GameObject("UpObj");
        downObj = new GameObject("DownObj");
        _upPanel = upObj.AddComponent<SurfacePlane3D>();
        _downPanel = downObj.AddComponent<SurfacePlane3D>();
        _upPanel.TwoSided = true;
        _downPanel.TwoSided = true;
        _upPanel.BuildIrregularGeometry(Triangulator.GetMeshData(list));
        _downPanel.InversionTexture = true;
        _downPanel.BuildIrregularGeometry(Triangulator.GetMeshData(list));

        upObj.tag = "Collage0";
        downObj.tag = "Collage1";

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

        upObj.transform.parent = transform;
        downObj.transform.parent = transform;
        obj.transform.parent = transform;

        upObj.layer = gameObject.layer;
        downObj.layer = gameObject.layer;
        obj.layer = gameObject.layer;

        upObj.transform.localEulerAngles = new Vector3();
        downObj.transform.localEulerAngles = new Vector3();
        obj.transform.localEulerAngles = new Vector3();

        upObj.transform.localPosition = new Vector3(0,thickness/100,0);
        downObj.transform.localPosition = new Vector3();
        obj.transform.localPosition = new Vector3();

        upObj.transform.localScale = new Vector3(.01f, .01f, .01f);
        downObj.transform.localScale = new Vector3(.01f, .01f, .01f);
        obj.transform.localScale = new Vector3(.01f, .01f, .01f);

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

        upObj.AddComponent<MeshCollider>();
        downObj.AddComponent<MeshCollider>();
        obj.AddComponent<MeshCollider>();

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
            XmlNode thickIrregularNode = code.SelectSingleNode("ThickIrregular");

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
            for (int i = 0; i < list.Count; i++)
            {
                p += list[i].x + "," + list[i].y + ((i == list.Count - 1) ? "" : ";");
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
        if (value == null) value = "";
        return '"' + value.ToString() + '"';
    }
}
