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

        _light = item.GetComponentInChildren<Light>();
    }

    private Light _light;
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

    public float range
    {
        set { _light.range = value; }
    }

    public float intensity
    {
        set { _light.intensity = value; }
    }

    public Color color
    {
        set { _light.color = value; }
    }

    private void Update()
    {

    }
}
