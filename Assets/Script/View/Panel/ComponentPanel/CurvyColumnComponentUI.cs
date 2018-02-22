using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurvyColumnComponentUI : BaseComponentUI
{
    private SliderUI radius;
    private ButtonImageUI colorUI;
    private ColorVO _color;
    private Toggle toggle;

    override public void Init()
    {
        base.Init();

        CreateTitleName("灯带设置");

        toggle = CreateToggle("启用灯带", EnabledCurvyColumnHandle);
        radius = CreateSliderUI("半径", 1, 20, value => { foreach (CurvyColumnComponent curvyColumn in _curvyColumns) curvyColumn.radius = value; });
        colorUI = CreateButtonImageUI("样式", ColorClickHandle);
    }

    private void EnabledCurvyColumnHandle(bool value)
    {
        radius.gameObject.SetActive(value);
        colorUI.gameObject.SetActive(value);

        foreach (CurvyColumnComponent curvyColumn in _curvyColumns) curvyColumn.enabledCurvyColumn = value;

        UpdateHeight();
    }

    private void ColorClickHandle(ButtonImageUI ui)
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _color,
        colorUI.button.transform.position) as SelectColorPanel;
        sp.getPos += UpdateColor;
    }

    private void UpdateColor(ColorVO color)
    {
        _color = color;
        foreach (CurvyColumnComponent curvyColumn in _curvyColumns) curvyColumn.color = _color;
        UpdateComponent();
        colorUI.image.color = _color.color;
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            CurvyColumnVO vo = avo as CurvyColumnVO;
            vo.radius = radius.value;
            vo.color = _color;
            vo.enabled = toggle.isOn;
        }
    }

    private List<CurvyColumnComponent> _curvyColumns;

    protected override void FillComponent()
    {
        base.FillComponent();

        _curvyColumns = new List<CurvyColumnComponent>();

        foreach (Item3D item in _items)
        {
            _curvyColumns.Add(item.GetComponentInChildren<CurvyColumnComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            CurvyColumnVO vo = avo as CurvyColumnVO;
            radius.value = vo.radius;
            toggle.isOn = vo.enabled;
            EnabledCurvyColumnHandle(toggle.isOn);
            _color = vo.color;
            colorUI.image.color = _color.color;
        }

        _fillComponent = false;
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<CurvyColumnVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<CurvyColumnVO>().Clone());
            }

            FillComponent();
        }
    }
}


