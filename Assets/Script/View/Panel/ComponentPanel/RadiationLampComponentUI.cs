using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiationLampComponentUI : BaseComponentUI
{
    private SliderUI brightness;
    private SliderUI spacing;
    private SliderUI rotateSpeed;

    private ButtonImageUI colorUI;
    private ColorVO _color;

    override public void Init()
    {
        base.Init();

        CreateTitleName("旋转灯设置");

        brightness = CreateSliderUI("光线强度", 0.1f, 1, value => { foreach (RadiationLampComponent radiationLamp in _radiationLamps) radiationLamp.brightness = value; });
        rotateSpeed = CreateSliderUI("旋转角度", 0, 5, value => { foreach (RadiationLampComponent radiationLamp in _radiationLamps) radiationLamp.rotateSpeed = value; });
        spacing = CreateSliderUI("灯光间距", 10, 80, value => { foreach (RadiationLampComponent radiationLamp in _radiationLamps) radiationLamp.spacing = value; });
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
        foreach (RadiationLampComponent radiationLamp in _radiationLamps) radiationLamp.color = _color;
        UpdateComponent();
        colorUI.image.color = _color.color;
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO vo in _assets)
        {
            RadiationLampVO rvo = vo as RadiationLampVO;
            rvo.rotateSpeed = rotateSpeed.value;
            rvo.brightness = brightness.value;
            rvo.color = _color;
            rvo.spacing = spacing.value;
        }
    }

    private List<RadiationLampComponent> _radiationLamps;

    protected override void FillComponent()
    {
        base.FillComponent();

        _radiationLamps = new List<RadiationLampComponent>();

        foreach (Item3D item in _items)
        {
            _radiationLamps.Add(item.GetComponentInChildren<RadiationLampComponent>());
        }

        foreach (AssetVO vo in _assets)
        {
            RadiationLampVO rvo = vo as RadiationLampVO;
            brightness.value = rvo.brightness;
            rotateSpeed.value = rvo.rotateSpeed;
            spacing.value = rvo.spacing;

            _color = rvo.color;
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
                _oldAssets.Add(obj.VO.GetComponentVO<RadiationLampVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<RadiationLampVO>().Clone());
            }

            FillComponent();
        }
    }
}


