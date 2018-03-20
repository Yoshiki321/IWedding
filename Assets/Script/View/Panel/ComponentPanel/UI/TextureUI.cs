using UnityEngine.UI;
using System;
using UnityEngine;

public class TextureUI : BaseUI
{
    public Text text;
    public Button url;
    public Button texture;
    public Button color;

    private Action<TextureUI> _actionURL;
    private Action<TextureUI> _actionTexture;
    private Action<TextureUI> _actionColor;

    public void OnClickColor(Action<TextureUI> action)
    {
        color.onClick.AddListener(ColorClickHandle);
        _actionColor = action;
    }

    public void OnClickTexture(Action<TextureUI> action)
    {
        texture.onClick.AddListener(TextureClickHandle);
        _actionTexture = action;
    }

    public void OnClickURL(Action<TextureUI> action)
    {
        url.onClick.AddListener(URLClickHandle);
        _actionURL = action;
    }

    private void ColorClickHandle()
    {
        _actionColor(this);
    }

    private void TextureClickHandle()
    {
        _actionTexture(this);
    }

    private void URLClickHandle()
    {
        _actionURL(this);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
