using UnityEngine;
using System.Collections;
using BuildManager;
using System.Collections.Generic;
using Build3D;

public class TransformComponent : SceneComponent
{
    private AssetSprite _item;

    private bool _isInit = false;

    public override void Init(AssetSprite item)
    {
        if (_isInit) return;
        _isInit = true;

        _item = item;
    }

    private AssetVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value;
            TransformVO vo = value.GetComponentVO<TransformVO>();

            _item.x = vo.x;
            _item.y = vo.y;
            _item.z = vo.z;
            _item.scaleX = vo.scaleX;
            _item.scaleY = vo.scaleY;
            _item.scaleZ = vo.scaleZ;
            _item.rotationX = vo.rotateX;
            _item.rotationY = vo.rotateY;
            _item.rotationZ = vo.rotateZ;
        }
        get { return _vo; }
    }

    public float x
    {
        set { _item.x = value; }
        get { return _item.x; }
    }

    public float y
    {
        set { _item.y = value; }
        get { return _item.y; }
    }

    public float z
    {
        set { _item.z = value; }
        get { return _item.z; }
    }

    public float scaleX
    {
        set { _item.scaleX = value; }
        get { return _item.scaleX; }
    }

    public float scaleY
    {
        set { _item.scaleY = value; }
        get { return _item.scaleY; }
    }

    public float scaleZ
    {
        set { _item.scaleZ = value; }
        get { return _item.scaleZ; }
    }

    public float rotateX
    {
        set { _item.rotationX = value; }
        get { return _item.rotationX; }
    }

    public float rotateY
    {
        set { _item.rotationY = value; }
        get { return _item.rotationY; }
    }

    public float rotateZ
    {
        set { _item.rotationZ = value; }
        get { return _item.rotationZ; }
    }
}
