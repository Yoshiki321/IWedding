using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorCameraComponentUI : BaseComponentUI
{
    public SliderUI postExposure;
    public SliderUI temperature;
    public SliderUI tint;
    public SliderUI hueShift;
    public SliderUI saturation;
    public SliderUI contrast;

    override public void Init()
    {
        base.Init();

        CreateTitleName("画面设置");

        postExposure = CreateSliderUI("曝光度", -2, 2, value => _editorCamera.postExposure = value);
        temperature = CreateSliderUI("温度", -100, 100, value => _editorCamera.temperature = value);
        tint = CreateSliderUI("色彩", -100, 100, value => _editorCamera.tint = value);
        hueShift = CreateSliderUI("色相偏移", -180, 180, value => _editorCamera.hueShift = value);
        saturation = CreateSliderUI("饱和度", -100, 100, value => _editorCamera.saturation = value);
        contrast = CreateSliderUI("对比度", -100, 100, value => _editorCamera.contrast = value);
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            EditorCameraVO vo = avo as EditorCameraVO;
            vo.postExposure = postExposure.value;
            vo.temperature = temperature.value;
            vo.tint = tint.value;
            vo.hueShift = hueShift.value;
            vo.saturation = saturation.value;
            vo.contrast = contrast.value;
        }
    }

    private EditorCameraComponent _editorCamera;

    protected override void FillComponent()
    {
        base.FillComponent();

        foreach (ObjectSprite3D item in _items)
        {
            _editorCamera = item.GetComponentInChildren<EditorCameraComponent>();
        }

        foreach (AssetVO avo in _assets)
        {
            EditorCameraVO vo = avo as EditorCameraVO;
            postExposure.value = vo.postExposure;
            temperature.value = vo.temperature;
            tint.value = vo.tint;
            hueShift.value = vo.hueShift;
            saturation.value = vo.saturation;
            contrast.value = vo.contrast;
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
                _oldAssets.Add(obj.VO.GetComponentVO<EditorCameraVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<EditorCameraVO>().Clone());
            }

            FillComponent();
        }
    }
}


