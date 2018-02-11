using UnityEngine;
using System.Collections;
using Build3D;
using System.Collections.Generic;

public class TubeLightComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite item)
    {
        if (_isInit) return;
        _isInit = true;

        _item = item as Item3D;

        if (_item.VO.GetComponentVO<TubeLightVO>() == null)
        {
            _item.VO.AddComponentVO<TubeLightVO>();
        }

        Destroy(_item.GetComponentInChildren<BoxCollider>());

        _box = _item.gameObject.AddComponent<CapsuleCollider>();
        _tubeLight = item.GetComponentInChildren<TubeLight>();

        UpdateShadowPlanes();
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        _box.radius = _tubeLight.m_Radius;
        _box.height = _tubeLight.m_Length;
    }

    private Item3D _item;
    private CapsuleCollider _box;

    private TubeLight _tubeLight;

    private TubeLightVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<TubeLightVO>();

            Brightness = _vo.brightness;
            Color = _vo.color;
            Length = _vo.length;
            Range = _vo.range;

            UpdateCollider();
        }
        get { return _vo; }
    }

    public void UpdateShadowPlanes()
    {
        //TubeLightShadowPlane[] m_ShadowPlanes = new TubeLightShadowPlane[BuilderModel.Instance.lineDatas.Count];
        //for(int i = 0;i < BuilderModel.Instance.lineDatas.Count;i++)
        //{
        //    m_ShadowPlanes[i] = (BuilderModel.Instance.lineDatas[i].line3 as Line3D).insidePlane.gameObject.GetComponent<TubeLightShadowPlane>();
        //}
        //_tubeLight.maxPlanes = BuilderModel.Instance.lineDatas.Count;
        //_tubeLight.m_ShadowPlanes = m_ShadowPlanes;
    }

    private ColorVO _colorVO;

    public ColorVO Color
    {
        set
        {
            _colorVO = value;
            _tubeLight.m_Color = value.color;
            _tubeLight.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", value.color);
        }

        get { return _colorVO; }
    }

    private float _brightness;

    public float Brightness
    {
        set
        {
            _brightness = value;
            _tubeLight.m_Intensity = value;
        }
        get { return _brightness; }
    }

    public float Length
    {
        set
        {
            _tubeLight.m_Length = value;
        }
    }

    public float Range
    {
        set
        {
            _tubeLight.m_Range = value;
        }
    }
}
