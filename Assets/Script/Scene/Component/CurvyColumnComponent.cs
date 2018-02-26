using UnityEngine;
using System.Collections;
using Build3D;

public class CurvyColumnComponent : SceneComponent
{
    private bool _isInit = false;

    ItemVO _itemVO;

    CurvyColumn _curvyColumn;
    CurvyColumn curvyColumn
    {
        get
        {
            if (_curvyColumn == null)
            {
                ThickIrregularPlane3D t3 = _item.GetComponentInChildren<ThickIrregularPlane3D>();
                if (t3)
                {
                    _curvyColumn = _itemVO.model.AddComponent<CurvyColumn>();
                    _curvyColumn.SetPoints(t3.upPoints, t3.transform.parent, true);
                    _curvyColumn.layer.transform.localScale = t3.upObj.transform.localScale;
                    _curvyColumn.layer.transform.localPosition = new Vector3();
                }
                else
                {

                }
            }

            return _curvyColumn;
        }
    }

    Item3D _item;

    public override void Init(AssetSprite item)
    {
        if (_isInit) return;
        _isInit = true;

        _item = item as Item3D;
        _itemVO = item.VO as ItemVO;

        if (_itemVO.GetComponentVO<CurvyColumnVO>() == null)
        {
            _itemVO.AddComponentVO<CurvyColumnVO>();
        }
    }

    private bool _enabledCurvyColumn;

    public bool enabledCurvyColumn
    {
        set
        {
            _enabledCurvyColumn = value;
            _curvyColumn.layer.SetActive(value);
        }
    }

    private float _radius;

    public float radius
    {
        set
        {
            if (_radius == value) return;
            _radius = value;
            if (curvyColumn) curvyColumn.radius = _radius;
        }
    }

    private Color _color;

    public Color color
    {
        set
        {
            _color = value;
            if (curvyColumn) curvyColumn.color = _color;
        }

        get { return _color; }
    }

    private CurvyColumnVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<CurvyColumnVO>();
            color = _vo.color;
            radius = _vo.radius;
            enabled = _vo.enabled;
        }
        get { return _vo; }
    }
}
