using UnityEngine;
using System.Collections;

public class CurvyColumnComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<CurvyColumnVO>() == null)
        {
            _item.VO.AddComponentVO<CurvyColumnVO>();
        }


    }

    private AssetVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value;
            CurvyColumnVO vo = value.GetComponentVO<CurvyColumnVO>();
        }
        get { return _vo; }
    }


    private void Update()
    {

    }
}
