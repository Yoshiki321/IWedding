using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Build3D;

public class TubeLightComponentUI : BaseComponentUI
{
    private SliderUI brightnessSlider;
    private SliderUI rangeSlider;
    private SliderUI lengthSlider;
    private ButtonImageUI colorUI;
    private Color _color;

    override public void Init()
    {
        base.Init();

        CreateTitleName("管灯设置");

        brightnessSlider = CreateSliderUI("强度", 0.5f, 2, value => { foreach (TubeLightComponent tubelight in _tubelights) tubelight.Brightness = value; });
        rangeSlider = CreateSliderUI("范围", 1, 8, value => { foreach (TubeLightComponent tubelight in _tubelights) tubelight.Range = value; });
        lengthSlider = CreateSliderUI("长度", 0.08f, 5, value => { foreach (TubeLightComponent tubelight in _tubelights) tubelight.Length = value; });
        colorUI = CreateButtonImageUI("颜色", ColorClickHandle);
    }

    private void ColorClickHandle(ButtonImageUI ui)
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _color,
         colorUI.button.transform.position) as SelectColorPanel;
        sp.onPicker.AddListener(UpdateColor);
    }

    private void UpdateColor(Color color)
    {
        _color = color;
        foreach (TubeLightComponent tubelight in _tubelights) tubelight.Color = _color;
        UpdateComponent();
        colorUI.image.color = _color;
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            TubeLightVO vo = avo as TubeLightVO;

            vo.range = rangeSlider.value;
            vo.length = lengthSlider.value;
            vo.brightness = brightnessSlider.value;
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
                _oldAssets.Add(obj.VO.GetComponentVO<TubeLightVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<TubeLightVO>().Clone());
            }

            FillComponent();
        }
    }

    private List<TubeLightComponent> _tubelights;

    protected override void FillComponent()
    {
        base.FillComponent();

        _tubelights = new List<TubeLightComponent>();

        foreach (Item3D item in _items)
        {
            _tubelights.Add(item.GetComponentInChildren<TubeLightComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            TubeLightVO vo = avo as TubeLightVO;
            rangeSlider.value = vo.range;
            lengthSlider.value = vo.length;
            brightnessSlider.value = vo.brightness;

            _color = vo.color;
            colorUI.image.color = _color;
        }

        _fillComponent = false;
    }
}
