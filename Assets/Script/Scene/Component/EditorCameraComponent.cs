using UnityEngine;
using System.Collections;
using Build3D;
using UnityEngine.Rendering.PostProcessing;

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

        _postProcessingBehaviour = GameObject.Find("Post-process Volume").GetComponent<PostProcessVolume>();
        _postProcessingProfile = _postProcessingBehaviour.profile;

        colorGrading = _postProcessingProfile.settings[0] as ColorGrading;
        colorGrading.tonemapper.value = Tonemapper.ACES;
    }

    #region colorGrading

    ColorGrading colorGrading;

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
        colorGrading.postExposure.value = _postExposure;
        colorGrading.temperature.value = _temperature;
        colorGrading.tint.value = _tint;
        colorGrading.hueShift.value = _hueShift;
        colorGrading.saturation.value = _saturation;
        colorGrading.contrast.value = _contrast;
        _postProcessingProfile.settings[0] = colorGrading;
    }

    #endregion

    PostProcessVolume _postProcessingBehaviour;
    PostProcessProfile _postProcessingProfile;

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
