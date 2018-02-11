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

    public void Fill()
    {
        Clear();

        List<float> xx = new List<float>();
        List<float> yy = new List<float>();
        List<float> zz = new List<float>();
        foreach (DrawLine line in drawPlane.drawLines)
        {
            Vector3 from = drawPlane.plane.transform.InverseTransformPoint(line.nodeFrom.transform.TransformPoint(line.nodeFrom.transform.localPosition));
            Vector3 to = drawPlane.plane.transform.InverseTransformPoint(line.nodeFrom.transform.TransformPoint(line.nodeTo.transform.localPosition));
            Vector3 curve = drawPlane.plane.transform.InverseTransformPoint(line.nodeFrom.transform.TransformPoint(line.nodeCurve.transform.localPosition));

            xx.Add(line.nodeFrom.transform.localPosition.x);
            xx.Add(line.nodeTo.transform.localPosition.x);
            xx.Add(line.nodeCurve.transform.localPosition.x);
            yy.Add(line.nodeFrom.transform.localPosition.y);
            yy.Add(line.nodeTo.transform.localPosition.y);
            yy.Add(line.nodeCurve.transform.localPosition.y);
            zz.Add(line.nodeFrom.transform.localPosition.z);
            zz.Add(line.nodeTo.transform.localPosition.z);
            zz.Add(line.nodeCurve.transform.localPosition.z);
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
        }
        get { return _vo; }
    }
}
