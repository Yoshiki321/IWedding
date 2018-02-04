using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : BaseUI
{
    public Text nameText;
    private Slider _slider;
    private InputField _text;

    private Action<float> _sliderAction;

    public Slider slider
    {
        set
        {
            _slider = value;
            slider.onValueChanged.AddListener(SliderHandle);
        }
        get { return _slider; }
    }

    public InputField text
    {
        set
        {
            _text = value;
            text.onEndEdit.AddListener(TextHandle);
        }
        get { return _text; }
    }

    public void SetActive(bool value)
    {
        nameText.transform.parent.parent.gameObject.SetActive(value);
    }

    public void SliderValueChanged(Action<float> action)
    {
        _sliderAction = action;
    }

    public float value
    {
        set
        {
            text.text = value.ToString();
            slider.value = value;
        }
        get { return slider.value; }
    }

    private void SliderHandle(float value)
    {
        _sliderAction?.Invoke(value);
        text.text = value.ToString();
        ui.UpdateComponent();
    }

    private void TextHandle(string value)
    {
        float f = float.Parse(value);
        if (f < slider.minValue)
            f = slider.minValue;
        if (f > slider.maxValue)
            f = slider.maxValue;
        slider.value = f;
        SliderHandle(f);
    }
}