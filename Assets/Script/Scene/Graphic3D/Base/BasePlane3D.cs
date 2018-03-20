using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePlane3D : MonoBehaviour
{
    protected Vector2[] uvs;
    protected Vector3[] normals;
    protected Vector4[] tangents;
    protected Vector3[] vertices;
    protected int[] triangles;

    private List<Vector2> _uvsList;
    private List<Vector3> _verticesList;
    private List<int> _trianglesList;

    protected int _nextVertexIndex;
    protected int _currentIndex;

    public BasePlane3D()
    {
        Reset();
    }

    public void Reset()
    {
        _uvsList = new List<Vector2>();
        _verticesList = new List<Vector3>();
        _trianglesList = new List<int>();
    }

    /// <summary>
    /// 设置墙体皮肤
    /// </summary>
    /// <param name="string"></param>
    public void SetMaterial(Material m)
    {
        if (_mr == null) return;
        _mt = m;
        _mr.material = _mt;
        UpdateTiling();
    }

    private bool _tilingAccordingScale = false;

    public bool TilingAccordingScale
    {
        set { _tilingAccordingScale = value; }
        get { return _tilingAccordingScale; }
    }

    private float _tilingX = 1;

    public float tilingX
    {
        set
        {
            _tilingX = value;
            UpdateTiling();
        }
        get { return _tilingX; }
    }

    private float _tilingY = 1;

    public float tilingY
    {
        set
        {
            _tilingY = value;
            UpdateTiling();
        }
        get { return _tilingY; }
    }

    private float _offestX = 0;

    public float offestX
    {
        set
        {
            _offestX = value;
            UpdateOffest();
        }
        get { return _offestX; }
    }

    private float _offestY = 0;

    public float offestY
    {
        set
        {
            _offestY = value;
            UpdateOffest();
        }
        get { return _offestY; }
    }

    private void UpdateOffest()
    {
        _mr.material.SetTextureOffset("_MainTex", new Vector2(_offestX, _offestY));
    }

    private void UpdateTiling()
    {
        if (_tilingAccordingScale)
        {
            _mr.material.SetTextureScale("_MainTex", gameObject.transform.localScale);
        }
        else
        {
            _mr.material.SetTextureScale("_MainTex", new Vector2(_tilingX, _tilingY));
        }
    }

    public Material material
    {
        get { return _mr.material; }
    }

    private bool _inversionTexture;

    public bool InversionTexture
    {
        get { return _inversionTexture; }
        set { _inversionTexture = value; }
    }

    private bool _twoSided;

    public bool TwoSided
    {
        get { return _twoSided; }
        set { _twoSided = value; }
    }

    public void UpdateTexture()
    {
        for (int i = 0; i < _verticesList.Count; i++)
        {
            Vector3 v = _verticesList[i];
            _uvsList.Add(new Vector2(v.x, v.z));
        }
    }

    public void AddVertexData(float px, float py, float pz)
    {
        _verticesList.Add(new Vector3(px, py, pz));
    }

    public void AddIndexData(int index0, int index1, int index2)
    {
        if (_twoSided)
        {
            _trianglesList.Add(index0);
            _trianglesList.Add(index1);
            _trianglesList.Add(index2);
            _trianglesList.Add(index0);
            _trianglesList.Add(index2);
            _trianglesList.Add(index1);
            _nextVertexIndex += 6;
        }
        else
        {
            if (!_inversionTexture)
            {
                _trianglesList.Add(index0);
                _trianglesList.Add(index1);
                _trianglesList.Add(index2);
            }
            else
            {
                _trianglesList.Add(index0);
                _trianglesList.Add(index2);
                _trianglesList.Add(index1);
            }

            _nextVertexIndex += 3;
        }
    }

    protected virtual void DrawBuildGeometry()
    {
    }

    private Material _mt;
    private MeshRenderer _mr;
    private MeshFilter _mf;
    private Mesh _mesh;

    public void BuildGeometry()
    {
        Reset();
        DrawBuildGeometry();
        UpdateTexture();

        uvs = new Vector2[_uvsList.Count];
        normals = new Vector3[_uvsList.Count];
        tangents = new Vector4[_uvsList.Count];
        vertices = new Vector3[_verticesList.Count];
        triangles = new int[_trianglesList.Count];

        for (int i = 0; i < _uvsList.Count; i++)
        {
            uvs[i] = _uvsList[i];

            if (!_inversionTexture)
            {
                normals[i] = new Vector3(0, 1, 0);
            }
            else
            {
                normals[i] = new Vector3(0, -1, 0);
            }

            tangents[i] = new Vector4(-1, 0, 0, -1);
        }

        for (int i = 0; i < _verticesList.Count; i++)
        {
            vertices[i] = _verticesList[i];
        }

        for (int i = 0; i < _trianglesList.Count; i++)
        {
            triangles[i] = _trianglesList[i];
        }

        _mr = gameObject.GetComponent<MeshRenderer>();
        if (_mr == null)
        {
            _mr = gameObject.AddComponent<MeshRenderer>();
        }

        _mf = gameObject.GetComponent<MeshFilter>();
        if (_mf == null)
        {
            _mf = gameObject.AddComponent<MeshFilter>();
        }

        _mesh = new Mesh();
        _mf.mesh = _mesh;
        //_mesh.RecalculateNormals();
        //_mesh.RecalculateBounds();
        //_mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;

        UpdatePlane();
    }

    private void UpdatePlane()
    {
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;
        _mesh.tangents = tangents;
        _mesh.normals = normals;
    }
}
