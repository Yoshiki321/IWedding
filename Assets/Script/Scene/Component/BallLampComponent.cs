using UnityEngine;
using System.Collections;
using Build3D;

public class BallLampComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<BallLampVO>() == null)
        {
            _item.VO.AddComponentVO<BallLampVO>();
        }

        _light = gameObject.GetComponentInChildren<Light>();
    }

    Light _light;

    public float rotateX = 0;
    public float rotateY = 0;
    public float rotateZ = 0;

    private BallLampVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<BallLampVO>();

            rotateX = _vo.rotateX;
            rotateY = _vo.rotateY;
            rotateZ = _vo.rotateZ;

            cookie = _vo.cookieId;
            range = _vo.range;
        }
        get { return _vo; }
    }

    public float range
    {
        set
        {
            _light.range = value;
        }
    }

    public string cookie
    {
        set
        {
            Cubemap texture2 = CookieManager.GetCubemapCookie(value);
            _light.cookie = texture2;
        }
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(rotateX, rotateY, rotateZ));
    }
}
