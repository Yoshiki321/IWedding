using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using Build3D;
using BuildManager;

public class CeilingLine3D
{
    private GameObject _item;

    public string id;

    public string inside;

    private Vector3 _v3;
    private float _a3;
    private Vector3 _fv3;
    private Vector3 _tv3;
    private Vector2 _fv2;
    private Vector2 _tv2;
    private float _d;

    public void Init(LineVO linevo)
    {
        _item = Resources.Load("Item/CeilingLine/CeilingLine1") as GameObject;
        _item = GameObject.Instantiate(_item, new Vector3(), new Quaternion());

        //this.gameObject.layer = LayerMask.NameToLayer("Graphic3D");
        _item.transform.gameObject.layer = LayerMask.NameToLayer("Graphic3D");
        _item.transform.parent = SceneManager.Instance.Graphics3D.SkirtingLayer.transform;

        _item.AddComponent<MeshCollider>().convex = true;

        lineVO = linevo;
    }

    private LineVO _linevo;

    public LineVO lineVO
    {
        set
        {
            _linevo = value;

            id = _linevo.id;

            inside = _linevo.interiorAlong ? "a" : "i";

            if (inside == "a")
            {
                _fv3 = new Vector3(_linevo.afrom.x, _linevo.height - .01f, _linevo.afrom.y);
                _tv3 = new Vector3(_linevo.ato.x, _linevo.height - .01f, _linevo.ato.y);
                _fv2 = new Vector2(_linevo.afrom.x, _linevo.afrom.y);
                _tv2 = new Vector2(_linevo.ato.x, _linevo.ato.y);
            }
            else
            {
                _fv3 = new Vector3(_linevo.ifrom.x, _linevo.height - .01f, _linevo.ifrom.y);
                _tv3 = new Vector3(_linevo.ito.x, _linevo.height - .01f, _linevo.ito.y);
                _fv2 = new Vector2(_linevo.ifrom.x, _linevo.ifrom.y);
                _tv2 = new Vector2(_linevo.ito.x, _linevo.ito.y);
            }

            _v3 = (_fv3 + _tv3) / 2;
            _a3 = PlaneUtils.Angle(_fv2, _tv2);

            _d = Vector3.Distance(_fv3, _tv3);

            _item.transform.position = _v3;

            if (inside == "a")
            {
                _item.transform.rotation = Quaternion.Euler(_item.transform.eulerAngles.x, 360 - _a3, _item.transform.eulerAngles.z);
            }
            else
            {
                _item.transform.rotation = Quaternion.Euler(_item.transform.eulerAngles.x, 360 - _a3 - 180, _item.transform.eulerAngles.z);
            }

            float sx = _item.gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x;

            _tiling = new Vector3(_d / sx, 1);
            _item.transform.localScale = new Vector3(_d / sx, 1, 1);

            _item.GetComponent<SkirtingLineController>().id = id;

            _item.GetComponent<Renderer>().material.SetTextureScale("_MainTex", _tiling);
        }
    }

    public void Dispose()
    {
        GameObject.Destroy(_item);
    }

    private Vector2 _tiling;

    public Vector2 Tiling
    {
        get { return _tiling; }
    }

    public Transform transform
    {
        get { return _item.transform; }
    }
}
