using UnityEngine;
using System.Collections;
using static UnityEngine.ParticleSystem;

public class PointLightComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite item)
    {
        if (_isInit) return;
        _isInit = true;

        if (item.VO.GetComponentVO<PointLightVO>() == null)
        {
            item.VO.AddComponentVO<PointLightVO>();
        }

        _lights = item.GetComponentsInChildren<Light>();
    }

    private Light[] _lights;
    private AssetVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value;
            PointLightVO vo = value.GetComponentVO<PointLightVO>();

            range = vo.range;
            intensity = vo.intensity;
            color = vo.color;
        }
        get { return _vo; }
    }

    private void UpdateLightItem()
    {
        foreach (Light light in _lights)
        {
            if (light.gameObject.GetComponent<MeshRenderer>())
            {
                Material material = light.gameObject.GetComponent<MeshRenderer>().material;
                material.color = _color;
                material.SetColor("_EmissionColor", _color);
            }
        }
    }

    private float _range;

    public float range
    {
        set
        {
            _range = value;
            foreach (Light light in _lights)
            {
                light.range = value;
            }
            UpdateLightItem();
        }
    }

    private float _intensity;

    public float intensity
    {
        set
        {
            _intensity = value;
            foreach (Light light in _lights)
            {
                light.intensity = value;
            }
            UpdateLightItem();
        }
    }

    private Color _color;

    public Color color
    {
        set
        {
            _color = value;
            foreach (Light light in _lights)
            {
                light.color = value;
            }
            UpdateLightItem();
        }
    }

    private void Update()
    {

    }
}
