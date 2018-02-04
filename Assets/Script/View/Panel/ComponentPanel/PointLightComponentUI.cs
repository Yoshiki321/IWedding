using Build3D;
using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLightComponentUI : BaseComponentUI
{
    private SliderUI rangeSlider;
    private SliderUI intensitySlider;
    private ButtonImageUI colorUI;

    private ColorVO _color;

    override public void Init()
    {
        base.Init();

        CreateTitleName("点光源设置");

        rangeSlider = CreateSliderUI("范围", 5, 30, value => { foreach (PointLightComponent pointLight in _pointLights) pointLight.range = value; });
        intensitySlider = CreateSliderUI("强度", 0.1f, 2.5f, value => { foreach (PointLightComponent pointLight in _pointLights) pointLight.intensity = value; });
        colorUI = CreateButtonImageUI("颜色", ColorClickHandle);
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
        foreach (PointLightComponent pointLight in _pointLights) pointLight.color = _color;
        UpdateComponent();
        colorUI.image.color = _color.color;
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            PointLightVO vo = avo as PointLightVO;

            vo.range = rangeSlider.value;
            vo.intensity = intensitySlider.value;
            vo.color = _color;
        }
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<PointLightVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<PointLightVO>().Clone());
            }

            FillComponent();
        }
    }

    private List<PointLightComponent> _pointLights;

    protected override void FillComponent()
    {
        base.FillComponent();

        _pointLights = new List<PointLightComponent>();

        foreach (Item3D item in _items)
        {
            _pointLights.Add(item.GetComponentInChildren<PointLightComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            PointLightVO vo = avo as PointLightVO;
            rangeSlider.value = vo.range;
            intensitySlider.value = vo.intensity;

            _color = vo.color;
            colorUI.image.color = _color.color;
        }

        _fillComponent = false;
    }
}


