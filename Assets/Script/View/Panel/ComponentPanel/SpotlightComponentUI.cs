using Build3D;
using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotlightComponentUI : BaseComponentUI
{
    private SliderUI angleSlider;
    private SliderUI volumeBrightness;
    private SliderUI lightBrightness;
    private SliderUI spotAngle;

    private ButtonImageUI colorUI;
    private ButtonImageUI cookieUI;

    private SliderUI fromSlider;
    private SliderUI toSlider;
    private SliderUI timeSlider;
    private DropdownUI rotateTypeUI;

    private Color _color;
    private string _cookieId;

    override public void Init()
    {
        base.Init();

        CreateTitleName("射灯设置");

        angleSlider = CreateSliderUI("角度", 0, 360, value => { foreach (SpotlightComponent spotlight in _spotlights) spotlight.Angle = value; });
        volumeBrightness = CreateSliderUI("体积光强度", 0, 1, value => { foreach (SpotlightComponent spotlight in _spotlights) spotlight.VolumeBrightness = value; });
        lightBrightness = CreateSliderUI("光强度", 0.5f, 1.5f, value => { foreach (SpotlightComponent spotlight in _spotlights) spotlight.LightBrightness = value; });
        spotAngle = CreateSliderUI("照射范围", 10, 30, value => { foreach (SpotlightComponent spotlight in _spotlights) spotlight.SpotAngle = value; });

        fromSlider = CreateSliderUI("起始角度", 0, 360);
        toSlider = CreateSliderUI("结束角度", 0, 360);
        timeSlider = CreateSliderUI("旋转时间", 1, 5);
        rotateTypeUI = CreateDropdownUI("旋转类型", new List<string> { "无", "左右旋转", "上下旋转" }, RotateTypeHandle);

        colorUI = CreateButtonImageUI("颜色", ColorClickHandle);
        cookieUI = CreateButtonImageUI("样式", CookieClickHandle);

        UpdateRotateType();
    }

    private void RotateTypeHandle(int value)
    {
        foreach (SpotlightComponent spotlight in _spotlights)
            spotlight.rotateType = value;
        UpdateRotateType();
        DispatchUpdate();
    }

    private void UpdateRotateType()
    {
        if (rotateTypeUI.dropdown.value == 0)
        {
            fromSlider.SetActive(false);
            toSlider.SetActive(false);
            timeSlider.SetActive(false);
        }
        else
        {
            fromSlider.SetActive(true);
            toSlider.SetActive(true);
            timeSlider.SetActive(true);
        }

        UpdateHeight();
    }

    private void ColorClickHandle(ButtonImageUI ui)
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _color,
         colorUI.button.transform.position) as SelectColorPanel;
        sp.onPicker.AddListener(UpdateColor);
    }

    private void CookieClickHandle(ButtonImageUI ui)
    {
        SelectTexturePanel sp = UIManager.OpenPanel(Panel.SelectTexturePanel, CookieManager.CookieImageList,
            cookieUI.button.transform.position - new Vector3(30, 0)) as SelectTexturePanel;
        sp.getTextue += UpdateCookie;
        sp.selectItem = _cookieId;
    }

    private void UpdateColor(Color color)
    {
        _color = color;
        foreach (SpotlightComponent spotlight in _spotlights) spotlight.Color = _color;
        UpdateComponent();
        colorUI.image.color = _color;
    }

    private void UpdateCookie(string id)
    {
        _cookieId = id;
        foreach (SpotlightComponent spotlight in _spotlights) spotlight.Cookie = _cookieId;
        UpdateComponent();
        cookieUI.image.color = _color;
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            SpotlightVO vo = avo as SpotlightVO;
            vo.rotateType = rotateTypeUI.dropdown.value;
            if (rotateTypeUI.dropdown.value == 1)
            {
                vo.fromX = 0;
                vo.fromY = fromSlider.value;
                vo.toX = 0;
                vo.toY = toSlider.value;
                vo.timeX = 0;
                vo.timeY = timeSlider.value;
            }
            if (rotateTypeUI.dropdown.value == 2)
            {
                vo.fromY = 0;
                vo.fromX = fromSlider.value;
                vo.toY = 0;
                vo.toX = toSlider.value;
                vo.timeY = 0;
                vo.timeX = timeSlider.value;
            }

            vo.angle = angleSlider.value;
            vo.volumeBrightness = volumeBrightness.value;
            vo.lightBrightness = lightBrightness.value;
            vo.spotAngle = spotAngle.value;
            vo.color = _color;
            vo.cookieId = _cookieId;
        }
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<SpotlightVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<SpotlightVO>().Clone());
            }

            FillComponent();
        }
    }

    private List<SpotlightComponent> _spotlights;

    protected override void FillComponent()
    {
        base.FillComponent();

        _spotlights = new List<SpotlightComponent>();

        foreach (Item3D item in _items)
        {
            _spotlights.Add(item.GetComponentInChildren<SpotlightComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            SpotlightVO vo = avo as SpotlightVO;
            angleSlider.value = vo.angle;
            volumeBrightness.value = vo.volumeBrightness;
            lightBrightness.value = vo.lightBrightness;
            spotAngle.value = vo.spotAngle;

            rotateTypeUI.dropdown.value = vo.rotateType;
            if (vo.rotateType == 0)
            {
                fromSlider.value = 0;
                toSlider.value = 0;
                timeSlider.value = 0;
            }
            if (vo.rotateType == 1)
            {
                fromSlider.value = vo.fromY;
                toSlider.value = vo.toY;
                timeSlider.value = vo.timeY;
            }
            if (vo.rotateType == 2)
            {
                fromSlider.value = vo.fromX;
                toSlider.value = vo.toX;
                timeSlider.value = vo.timeX;
            }

            _color = vo.color;
            colorUI.image.color = _color;
            _cookieId = vo.cookieId;
            cookieUI.image.color = _color;
        }

        _fillComponent = false;
    }
}


