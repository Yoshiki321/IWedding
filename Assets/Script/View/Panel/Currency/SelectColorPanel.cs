using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColorPanel : BaseWindow
{
    ColorPicker colorPicker;

    public override void Init()
    {
        base.Init();
        colorPicker = GetComponentInChildren<ColorPicker>();
    }

    override public void SetContent(object value)
    {
        if(value == null)return;
        Color((Color)value);
    }

    public void Color(Color value)
    {
        if (value == null) return;
        colorPicker.SetColor(value);
    }

    public ColorPicker.ColorPickerEvent onPicker
    {
        get { return colorPicker.onPicker; }
        set { colorPicker.onPicker = value; }
    }

    protected override void Close()
    {
        base.Close();

        onPicker.RemoveAllListeners();
    }
}
