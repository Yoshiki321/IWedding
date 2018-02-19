using UnityEngine;
using System.Collections;

public class CurvyColumnComponent : SceneComponent
{
    private bool _isInit = false;

    ItemVO _itemVO;
    CurvyColumn curvyColumn;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        _itemVO = _item.VO as ItemVO;
        if (_itemVO.GetComponentVO<CurvyColumnVO>() == null)
        {
            _itemVO.AddComponentVO<CurvyColumnVO>();
        }

        ThickIrregularPlane3D t3 = _item.GetComponentInChildren<ThickIrregularPlane3D>();
        if (t3)
        {
            curvyColumn = _itemVO.model.AddComponent<CurvyColumn>();
            curvyColumn.SetPoints(t3.upPoints, t3.transform.parent);
            curvyColumn.layer.transform.localScale = t3.upObj.transform.localScale;
        }
        else
        {

        }
    }

    private float _radius;

    public float radius
    {
        set
        {
            _radius = value;
            curvyColumn.radius = _radius;
        }
    }


    private ColorVO _colorVO;

    public ColorVO color
    {
        set
        {
            _colorVO = value;
            curvyColumn.color = _colorVO.color;
        }

        get { return _colorVO; }
    }

    private CurvyColumnVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<CurvyColumnVO>();
            color = _vo.color;
            radius = _vo.radius;
        }
        get { return _vo; }
    }
}
