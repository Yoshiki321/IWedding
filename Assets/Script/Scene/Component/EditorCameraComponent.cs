using UnityEngine;
using System.Collections;
using Build3D;
using UnityEngine.PostProcessing;

public class EditorCameraComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

        if (_item.VO.GetComponentVO<EditorCameraVO>() == null)
        {
            _item.VO.AddComponentVO<EditorCameraVO>();
        }

        _postProcessingBehaviour = gameObject.GetComponentInChildren<PostProcessingBehaviour>();
        _postProcessingProfile = _postProcessingBehaviour.profile;

        colorGrading = ColorGradingModel.Settings.defaultSettings;
        colorGrading.tonemapping.tonemapper = ColorGradingModel.Tonemapper.ACES;
    }

    #region colorGrading

    ColorGradingModel.Settings colorGrading;

    private float _postExposure;
    private float _temperature;
    private float _tint;
    private float _hueShift;
    private float _saturation;
    private float _contrast;

    /// <summary>
    /// 曝光度
    /// </summary>
    public float postExposure { set { _postExposure = value; UpdateColorGrading(); } }

    /// <summary>
    /// 冷暖色调
    /// </summary>
    public float temperature { set { _temperature = value; UpdateColorGrading(); } }

    /// <summary>
    /// 色彩
    /// </summary>
    public float tint { set { _tint = value; UpdateColorGrading(); } }

    /// <summary>
    /// 色相偏移
    /// </summary>
    public float hueShift { set { _hueShift = value; UpdateColorGrading(); } }

    /// <summary>
    /// 饱和度
    /// </summary>
    public float saturation { set { _saturation = value; UpdateColorGrading(); } }

    /// <summary>
    /// 对比度
    /// </summary>
    public float contrast { set { _contrast = value; UpdateColorGrading(); } }

    private void UpdateColorGrading()
    {
        colorGrading.basic.postExposure = _postExposure;
        colorGrading.basic.temperature = _temperature;
        colorGrading.basic.tint = _tint;
        colorGrading.basic.hueShift = _hueShift;
        colorGrading.basic.saturation = _saturation;
        colorGrading.basic.contrast = _contrast;
        _postProcessingProfile.colorGrading.settings = colorGrading;
    }

    #endregion

    PostProcessingBehaviour _postProcessingBehaviour;
    PostProcessingProfile _postProcessingProfile;

    private EditorCameraVO _vo;

    override public AssetVO VO
    {
        set
        {
            _vo = value.GetComponentVO<EditorCameraVO>();

            _postExposure = _vo.postExposure;
            _temperature = _vo.temperature;
            _tint = _vo.tint;
            _hueShift = _vo.hueShift;
            _saturation = _vo.saturation;
            _contrast = _vo.contrast;

            UpdateColorGrading();
        }
        get { return _vo; }
    }


    void Update()
    {
    }
}
