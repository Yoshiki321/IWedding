using UnityEngine;
using System.Collections;
using Build3D;
using System.IO;
using BuildManager;
using System.Collections.Generic;

public class FlowerWallComponent : SceneComponent
{
    private bool _isInit = false;

    private DrawPlane drawPlane;
    private Material panelMaterial;

    private List<GameObject> _objectList;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<FlowerWallVO>() == null)
        {
            _item.VO.AddComponentVO<FlowerWallVO>();
        }

        drawPlane = _item.GetComponentInChildren<DrawPlane>();

        panelMaterial = new Material(Shader.Find("Standard"));
        RenderingModeUnits.SetMaterialRenderingMode(panelMaterial, RenderingModeUnits.RenderingMode.Transparent);
        drawPlane.SetMaterial(panelMaterial);

        _objectList = new List<GameObject>();

        Editor();
    }

    private bool _editor;

    public void Editor()
    {
        _editor = !_editor;

        SceneManager.EnabledEditorObjectSelection(_editor);
        drawPlane.isOperate = !_editor;
    }

    public void Clear()
    {
        foreach (GameObject obj in _objectList)
        {
            Destroy(obj);
        }

        _objectList = new List<GameObject>();
    }

    bool _visible;

    public bool visible
    {
        set
        {
            _visible = value;

            if (value)
            {
                _color.a = 1;
            }
            else
            {
                _color.a = 0;
            }

            panelMaterial.color = _color;
            drawPlane.SetMaterial(panelMaterial);
        }
        get { return _visible; }
    }

    Color _color;

    public ColorVO color
    {
        set
        {
            _color = value.color;
            _color.a = _visible ? 1 : 0;
            panelMaterial.color = _color;
            drawPlane.SetMaterial(panelMaterial);
        }
    }

    public void Fill()
    {
        Clear();

        List<float> xx = new List<float>();
        List<float> yy = new List<float>();
        List<float> zz = new List<float>();
        foreach (DrawLine line in drawPlane.drawLines)
        {
            foreach (Vector3 v in line.points)
            {
                xx.Add(v.x);
                yy.Add(v.y);
                zz.Add(v.z);
            }
        }

        float maxx = 0;
        for (int i = 0; i < xx.Count; i++) maxx = Mathf.Max(maxx, xx[i]);

        float maxy = 0;
        for (int i = 0; i < yy.Count; i++) maxy = Mathf.Max(maxy, yy[i]);

        float maxz = 0;
        for (int i = 0; i < zz.Count; i++) maxz = Mathf.Max(maxz, zz[i]);

        float minx = 0;
        for (int i = 0; i < xx.Count; i++) minx = Mathf.Min(minx, xx[i]);

        float miny = 0;
        for (int i = 0; i < yy.Count; i++) miny = Mathf.Min(miny, yy[i]);

        float minz = 0;
        for (int i = 0; i < zz.Count; i++) minz = Mathf.Min(minz, zz[i]);

        float countX = Mathf.Round((maxx - minx) / 18);
        float countY = Mathf.Round((maxy - miny) / 18);

        float mx = ((maxx - 1) - (minx + 1)) / (countX - 1);
        float my = ((maxy - 1) - (miny + 1)) / (countY - 1);
        float dx = maxx;
        float dy = maxy;
        int j = 0;
        for (int i = 0; i < countX * countY; i++)
        {
            float tdx = dx;
            float tdy = dy;
            bool isIn = false;
            foreach (Triangle t in drawPlane.meshData.triangleList)
            {
                if (t.Inside(new Vector2(dx, dy)))
                {
                    isIn = true;
                    break;
                }
                if (t.Inside(new Vector2(dx + 1, dy + 1)))
                {
                    tdx += 1;
                    tdy += 1;
                    isIn = true;
                    break;
                }
                if (t.Inside(new Vector2(dx + 1, dy - 1)))
                {
                    tdx += 1;
                    tdy -= 1;
                    isIn = true;
                    break;
                }
                if (t.Inside(new Vector2(dx - 1, dy + 1)))
                {
                    tdx -= 1;
                    tdy += 1;
                    isIn = true;
                    break;
                }
                if (t.Inside(new Vector2(dx - 1, dy - 1)))
                {
                    tdx -= 1;
                    tdy -= 1;
                    isIn = true;
                    break;
                }
            }

            if (isIn)
            {
                GameObject obj = GameObject.Instantiate(Resources.Load("Item/FlowerGroup/FlowerGroup") as GameObject);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(tdx, tdy, 0);
                obj.transform.localScale = new Vector3(90f, 90f, 90f);
                obj.transform.localRotation = Quaternion.Euler(UnityEngine.Random.value * 360, -90f, -90f);

                float r = UnityEngine.Random.Range(0f, 1f);
                float g = UnityEngine.Random.Range(0f, 1f);
                float b = UnityEngine.Random.Range(0f, 1f);
                Color color = new Color(r, g, b);

                obj.GetComponentInChildren<MeshRenderer>().material.color = color;
                obj.layer = gameObject.layer;
                Transform[] ts = obj.GetComponentsInChildren<Transform>();
                foreach (Transform t in ts)
                {
                    t.gameObject.layer = gameObject.layer;
                }

                _objectList.Add(obj);
            }

            j++;
            dx -= mx;
            if (j == countX)
            {
                dx = maxx;
                dy -= my;
                j = 0;
            }
        }
    }

    private FlowerWallVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<FlowerWallVO>();
            visible = _vo.visible;
            visible = _vo.visible;
            color = _vo.color;
        }
        get { return _vo; }
    }
}
