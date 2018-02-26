using UnityEngine.UI;
using System;
using UnityEngine;

public class TextureUI : BaseUI
{
    public Text text;
    public Button texture;
    public Button color;

    private Action<TextureUI> _action;
    private Action<TextureUI> _actionColor;

    public void OnClickColor(Action<TextureUI> action)
    {
        color.onClick.AddListener(ColorClickHandle);
        _actionColor = action;
    }

    public void OnClickTexture(Action<TextureUI> action)
    {
        texture.onClick.AddListener(TextureClickHandle);
        _action = action;
    }

    private void ColorClickHandle()
    {
        _actionColor(this);
    }

    private void TextureClickHandle()
    {
        _action(this);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
