using System;
using UnityEngine;
using UnityEngine.UI;

public class TransformUI : BaseUI
{
    public Text nameText;
    public InputField X;
    public InputField Y;
    public InputField Z;

    private Action<string> _actionX;
    private Action<string> _actionY;
    private Action<string> _actionZ;

    public void OnEndEdit(Action<string> actionX, Action<string> actionY, Action<string> actionZ)
    {
        X.onEndEdit.AddListener(TextHandleX);
        Y.onEndEdit.AddListener(TextHandleY);
        Z.onEndEdit.AddListener(TextHandleZ);
        _actionX = actionX;
        _actionY = actionY;
        _actionZ = actionZ;
    }

    private void TextHandleX(string value)
    {
        _actionX(value);
        ui.UpdateComponent();
    }

    private void TextHandleY(string value)
    {
        _actionY(value);
        ui.UpdateComponent();
    }

    private void TextHandleZ(string value)
    {
        _actionZ(value);
        ui.UpdateComponent();
    }
}