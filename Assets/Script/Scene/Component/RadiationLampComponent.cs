using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

public class RadiationLampComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<RadiationLampVO>() == null)
        {
            _item.VO.AddComponentVO<RadiationLampVO>();
        }

        lightbeams = transform.GetComponentsInChildren<VolumetricLightBeam>();

        foreach (VolumetricLightBeam lb in lightbeams)
        {
            lb.noiseEnabled = true;
            lb.fadeEndFromLight = false;

            lights.Add(lb.gameObject.GetComponent<Light>(), 0);
        }
    }

    Dictionary<Light, float> lights = new Dictionary<Light, float>();

    VolumetricLightBeam[] lightbeams;

    List<Material> materialList = new List<Material>();

    public float rotateSpeed = 0;

    private RadiationLampVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<RadiationLampVO>();

            color = _vo.color;

            rotateSpeed = _vo.rotateSpeed;

            brightness = _vo.brightness;
        }
        get { return _vo; }
    }

    private void Update()
    {
        #region hit;

        RaycastHit hitInfo;
        LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");

        foreach (VolumetricLightBeam lb in lightbeams)
        {
            GameObject spot = lb.transform.gameObject;
            Vector3 fwd = spot.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(spot.transform.position, fwd, out hitInfo, layer))
            {
                float d = Vector3.Distance(spot.transform.position, hitInfo.point);
                if (d != lb.fadeEnd)
                {
                    d /= lb.gameObject.transform.localScale.x;
                    lb.fadeEnd = d + .1f;
                    lb.GenerateGeometry();
                }
            }
        }

        #endregion;

        #region rotate;

        gameObject.transform.Rotate(new Vector3(0, rotateSpeed, 0));

        #endregion;

        #region animation;

        if (_animationDrop == 0)
        {
            foreach (VolumetricLightBeam lb in lightbeams)
            {
                lb.gameObject.GetComponent<Light>().enabled = true;
            }
        }
        if (_animationDrop == 1)
        {
        }
        if (_animationDrop == 2)
        {

        }

        #endregion;
    }

    private float _brightness;

    public float brightness
    {
        set
        {
            _brightness = value;

            foreach (VolumetricLightBeam lb in lightbeams)
            {
                lb.alphaOutside = value;
                lb.alphaInside = value;
                lb.GenerateGeometry();
            }
        }
    }

    private float _spacing;

    public float spacing
    {
        set
        {
            _spacing = value;

            foreach (VolumetricLightBeam lb in lightbeams)
            {
                Vector3 rv = new Vector3(_spacing,
                                        lb.gameObject.transform.parent.localRotation.eulerAngles.y,
                                        lb.gameObject.transform.parent.localRotation.eulerAngles.z);

                lb.gameObject.transform.parent.localRotation = Quaternion.Euler(rv);
            }
        }
        get { return _spacing; }
    }

    private int _animationDrop;

    public int animationDrop
    {
        set
        {
            _animationDrop = value;


        }
        get { return _animationDrop; }
    }

    private ColorVO _colorVO;

    public ColorVO color
    {
        set
        {
            _colorVO = value;

            Light[] lights = GetComponentsInChildren<Light>();
            foreach (Light light in lights)
            {
                light.color = value.color;
            }

            foreach (VolumetricLightBeam lb in lightbeams)
            {
                lb.GenerateGeometry();
            }

            foreach (Transform obj in transform)
            {
                Color c = new Color();
                c.r = value.color.r * .7f;
                c.g = value.color.g * .7f;
                c.b = value.color.b * .7f;
                c.a = .7f;
                if (obj.Find("Sphere01") != null) obj.Find("Sphere01").GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", c);
                if (obj.GetComponent<Light>() != null) obj.GetComponent<Light>().color = value.color;
            }
        }
    }
}
