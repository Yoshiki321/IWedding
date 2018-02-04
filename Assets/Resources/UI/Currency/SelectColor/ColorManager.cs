using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ColorManager : MonoBehaviour
{
    RectTransform rt;

    private ColorRGB CRGB;
    private ColorPanel CP;
    private ColorCircle CC;

    public Slider sliderCRGB;
    public Image colorShow;


    void OnDisable()
    {
        if (CC != null) CC.getPos -= CC_getPos;
    }

    private void CC_getPos(Vector2 pos)
    {
        Color getColor = CP.GetColorByPosition(pos);
        colorShow.color = getColor;

        ColorVO vo = new ColorVO();
        vo.color = getColor;
        vo.slider = _sliderValue;
        vo.position = CP.colorPosition;

        acc?.Invoke(vo);
    }
    
    public void SetColor(ColorVO vo)
    {
        CC.rt.anchoredPosition = vo.position;
        sliderCRGB.value = vo.slider;

        OnCRGBValueChanged(vo.slider);
    }

    Action<ColorVO> acc;

    public void GetColor(Action<ColorVO> tt)
    {
        acc = tt;
    }

    private void Awake()
    {
        rt = GetComponent<RectTransform>();

        CRGB = GetComponentInChildren<ColorRGB>();
        CP = GetComponentInChildren<ColorPanel>();
        CC = GetComponentInChildren<ColorCircle>();

        sliderCRGB.onValueChanged.AddListener(OnCRGBValueChanged);
    }

    // Use this for initialization
    void Start()
    {
        CC.getPos += CC_getPos;
    }

    private void OnEnable()
    {
        if (CC != null) CC.getPos += CC_getPos;
    }

    private float _sliderValue = 0;

    void OnCRGBValueChanged(float value)
    {
        _sliderValue = value;

        Color endColor = CRGB.GetColorBySliderValue(value);
        CP.SetColorPanel(endColor);
        CC.setShowColor();
    }
}
