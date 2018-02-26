using UnityEngine;
using System.Collections;
using static UnityEngine.ParticleSystem;

public class SmokeComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<SmokeVO>() == null)
        {
            _item.VO.AddComponentVO<SmokeVO>();
        }

        //StartColor = ColorUtils.HexToColor("3D4A4B9B");
        //EndColor = ColorUtils.HexToColor("3D4A4B9B");

        _particle = _item.GetComponentInChildren<ParticleSystem>();
        _main = _particle.main;
    }

    private ParticleSystem _particle;
    private MainModule _main;

    private AssetVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value;
            SmokeVO vo = value.GetComponentVO<SmokeVO>();

            size = vo.size;
            speed = vo.speed;
            StartColor = vo.startColor;
            EndColor = vo.endColor;
        }
        get { return _vo; }
    }

    public float size
    {
        set { _main.startSize = new MinMaxCurve(value, value * 2); }
    }

    public float speed
    {
        set { _main.startSpeed = new MinMaxCurve(value); }
    }

    private Color _startColorVO = new Color();
    private Color _endColorVO = new Color();

    public Color StartColor
    {
        set
        {
            _startColorVO = value;
            _main.startColor = new MinMaxGradient(_startColorVO, _endColorVO);
        }
    }

    public Color EndColor
    {
        set
        {
            _endColorVO = value;
            _main.startColor = new MinMaxGradient(_startColorVO, _endColorVO);
        }
    }

    private void Update()
    {

    }
}
