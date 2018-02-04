using UnityEngine.UI;
using System;
using UnityEngine;

public class ButtonImageUI : BaseUI
{
    public Text buttonText;
    public Button button;
    public Image image;

    private Action<ButtonImageUI> _action;

    public void OnClickButtom(Action<ButtonImageUI> action)
    {
        button.onClick.AddListener(ColorClickHandle);
        _action = action;
    }

    private void ColorClickHandle()
    {
        _action(this);
    }
}
