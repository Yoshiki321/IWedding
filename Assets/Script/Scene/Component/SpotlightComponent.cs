using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

public class SpotlightComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<SpotlightVO>() == null)
        {
            _item.VO.AddComponentVO<SpotlightVO>();
        }

        _light = spotlight.GetComponent<Light>();
        _lightBeam = _item.GetComponentInChildren<VolumetricLightBeam>();
        _lightBeam.noiseEnabled = true;
        VolumeBrightness = .2f;
    }

    private void Update()
    {
        if (!_lightBeam) return;

        RaycastHit hitInfo;
        LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");

        Vector3 fwd = spot.transform.TransformDirection(Vector3.back);
        if (Physics.Raycast(spot.transform.position, fwd, out hitInfo, 100f, layer))
        {
            float d = Vector3.Distance(spot.transform.position, hitInfo.point);
            if (d != _lightBeam.fadeEnd)
            {
                _lightBeam.fadeEnd = d + .1f;
                _lightBeam.GenerateGeometry();
            }
        }
    }

    public GameObject glass;
    public GameObject spotlight;
    public GameObject spot;
    public GameObject lightbeam;
    public Texture2D lightbeamTexture;
    private Light _light;

    private VolumetricLightBeam _lightBeam;

    private SpotlightVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<SpotlightVO>();

            VolumeBrightness = _vo.volumeBrightness;
            LightBrightness = _vo.lightBrightness;
            SpotAngle = _vo.spotAngle;
            Angle = _vo.angle;
            Color = _vo.color;
            Cookie = _vo.cookieId;

            fromX = _vo.fromX;
            fromY = _vo.fromY;
            toX = _vo.toX;
            toY = _vo.toY;
            timeX = _vo.timeX;
            timeY = _vo.timeY;

            if (_vo.rotateType != 0)
            {
                StartRotate();
            }
            else
            {
                StopRotate();
            }
        }
        get { return _vo; }
    }

    private float _volumeBrightness;

    public float VolumeBrightness
    {
        set
        {
            _volumeBrightness = value;
            _lightBeam.alphaOutside = value;
            _lightBeam.alphaInside = value;
            _lightBeam.GenerateGeometry();
        }
        get { return _volumeBrightness; }
    }

    private float _lightBrightness;

    public float LightBrightness
    {
        set
        {
            _lightBrightness = value;
            _light.intensity = value;
        }
        get { return _lightBrightness; }
    }

    private float _spotAngle;

    public float SpotAngle
    {
        set
        {
            _spotAngle = value;
            _light.spotAngle = value;
        }
        get { return _spotAngle; }
    }

    public float Angle
    {
        set { spot.transform.localRotation = Quaternion.Euler(value, spot.transform.localRotation.y, spot.transform.localRotation.z); }
        get { return spot.transform.localRotation.eulerAngles.x; }
    }

    private Color _color;

    public Color Color
    {
        set
        {
            _color = value;
            _light.color = value;
            spot.GetComponent<Light>().color = value;

            _lightBeam.GenerateGeometry();

            Color c = new Color();
            c.r = value.r * .7f;
            c.g = value.g * .7f;
            c.b = value.b * .7f;
            c.a = .7f;
            glass.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", c);
        }

        get { return _color; }
    }

    private int _rotateType;

    public int rotateType
    {
        set { _rotateType = value; }
        get { return _rotateType; }
    }

    public string Cookie
    {
        set
        {
            Texture2D texture2 = CookieManager.GetTextureCookie(value);
            _light.cookie = texture2;
        }
    }

    public float fromY;
    public float toY;
    public float fromX;
    public float toX;
    public float timeY;
    public float timeX;

    public void SetData(SpotRotateYoYoData data)
    {
        fromX = data.fromX;
        fromY = data.fromY;
        toX = data.toX;
        toY = data.toY;
        timeX = data.timeX;
        timeY = data.timeY;
        rotateType = data.rotateType;

        if (rotateType != 0)
        {
            StartRotate();
        }
        else
        {
            StopRotate();
        }
    }

    public void StopRotate()
    {
        iTween.Stop(gameObject);
        iTween.Stop(spot);
    }

    public void StartRotate()
    {
        if (rotateType == 1)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0,
                fromY,
                0);
            Hashtable args = new Hashtable();
            args.Add("y", toY);
            args.Add("islocal", true);
            args.Add("time", timeY);
            //args.Add("delay", 0.1f);
            args.Add("easeType", iTween.EaseType.easeInOutExpo);
            args.Add("loopType", iTween.LoopType.pingPong);
            iTween.RotateTo(gameObject, args);
        }

        if (rotateType == 2)
        {
            spot.transform.localRotation = Quaternion.Euler(fromX,
                spot.transform.localRotation.eulerAngles.y,
                spot.transform.localRotation.eulerAngles.z);
            Hashtable args1 = new Hashtable();
            args1.Add("x", toX);
            args1.Add("islocal", true);
            args1.Add("time", timeX);
            //args.Add("delay", 0.1f);
            args1.Add("easeType", iTween.EaseType.easeInOutExpo);
            args1.Add("loopType", iTween.LoopType.pingPong);
            iTween.RotateTo(spot, args1);
        }
    }
}


public class SpotRotateYoYoData
{
    public float fromX;
    public float fromY;
    public float toX;
    public float toY;
    public float timeX;
    public float timeY;

    public int rotateType;
}
