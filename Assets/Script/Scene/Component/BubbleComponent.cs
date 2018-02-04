using UnityEngine;
using System.Collections;
using static UnityEngine.ParticleSystem;

public class BubbleComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<BubbleVO>() == null)
        {
            _item.VO.AddComponentVO<BubbleVO>();
        }

        _particle = _item.GetComponentInChildren<ParticleSystem>();
        _particle.transform.parent.gameObject.AddComponent<FaceToCamera>();
        _main = _particle.main;
        _velocityOverLifetime = _particle.velocityOverLifetime;
    }

    private ParticleSystem _particle;
    private MainModule _main;
    private VelocityOverLifetimeModule _velocityOverLifetime;

    private AssetVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value;
            BubbleVO vo = value.GetComponentVO<BubbleVO>();
        }
        get { return _vo; }
    }

    public float density
    {
        set
        {
            _particle.Stop();
            _main.duration = value;
            _particle.Play();
        }
    }

    public float size
    {
        set { _main.startSize = new MinMaxCurve(value, value * 10); }
    }

    public float speed
    {
        set { _main.startSpeed = new MinMaxCurve(value, value * 2); }
    }

    private float _directionX;

    public float directionX
    {
        set
        {
            _directionX = value;
            _velocityOverLifetime.x = _directionX;
            _velocityOverLifetime.xMultiplier = _directionX - 1;
        }

        get { return _directionX; }
    }

    private void Update()
    {

    }
}
