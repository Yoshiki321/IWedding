using UnityEngine;
using System.Collections;
using BuildManager;
using System.Collections.Generic;
using Build3D;

public class WindBellLineComponent : SceneComponent
{
    private AssetSprite _item;

    private bool _isInit = false;

    public override void Init(AssetSprite item)
    {
        if (_isInit) return;
        _isInit = true;

        _item = item;

        if (_item.VO.GetComponentVO<WindBellLineVO>() == null)
        {
            _item.VO.AddComponentVO<WindBellLineVO>();
        }
    }

    private WindBellLineVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<WindBellLineVO>();

            _length = _vo.length;
            _color = _vo.color;
            _count = _vo.count;
            _itemRadius = _vo.itemRadius;
            _radius = _vo.radius;

            Fill();
        }
        get { return _vo; }
    }

    private List<GameObject> _itemList = new List<GameObject>();

    private void Fill()
    {
        foreach (GameObject obj in _itemList)
        {
            Destroy(obj);
        }
        _itemList = new List<GameObject>();

        List<Vector3> list = PlaneUtils.GetNodePoints(transform.position,
            new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), _radius, _count);

        Vector3 vh = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

        foreach (Vector3 v in list)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            obj.transform.gameObject.layer = transform.gameObject.layer;
            obj.transform.parent = transform;
            obj.transform.localScale = new Vector3(_itemRadius, _length, _itemRadius);
            obj.transform.position = v;
            obj.GetComponent<MeshRenderer>().material.color = _color;
            _itemList.Add(obj);

            //float ylength = obj.GetComponent<MeshFilter>().mesh.bounds.size.y / 2 * transform.lossyScale.y;
            //Vector3 vv = new Vector3(v.x, v.y + ylength, v.z);

            //obj = new GameObject();
            //CurvyColumn cc = obj.AddComponent<CurvyColumn>();
            //List<Vector3> vl = new List<Vector3>();
            //vl.Add(vv);
            //vl.Add(vh);
            //cc.SetPoints(vl, transform);
            //cc.radius = 0.01f;
            //cc.color = Color.white;
        }
    }

    private float _itemRadius;

    public float ItemRadius
    {
        set { _itemRadius = value; Fill(); }
        get { return _itemRadius; }
    }

    private float _radius;

    public float Radius
    {
        set { _radius = value; Fill(); }
        get { return _radius; }
    }

    private float _length;

    public float Length
    {
        set { _length = value; Fill(); }
        get { return _length; }
    }

    private int _count;

    public int Count
    {
        set { _count = value; Fill(); }
        get { return _count; }
    }

    private Color _color;

    public Color Color
    {
        set { _color = value; Fill(); }
        get { return _color; }
    }
}
