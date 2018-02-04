using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColorPanel : BaseWindow
{
    public delegate void RetureTextuePosition(ColorVO pos);
    public event RetureTextuePosition getPos;

    ColorManager colorManager;

    public override void Init()
    {
        base.Init();

        colorManager = GetComponentInChildren<ColorManager>();

        colorManager.GetColor(GetColor);
    }

    void OnEnabled()
    {
        colorManager.GetColor(GetColor);
    }

    void GetColor(ColorVO cvo)
    {
        getPos?.Invoke(cvo);
    }

    override public void SetContent(object value)
    {
        colorManager.SetColor(value as ColorVO);
    }

    void Update()
    {

    }

    protected override void Close()
    {
        getPos = null;
    }
}
