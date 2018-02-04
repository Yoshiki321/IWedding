using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldUI : BaseUI
{
    public Text nameText;
    public InputField text;

    private Action<string> _action;

    public void OnEndEdit(Action<string> action)
    {
        text.onEndEdit.AddListener(TextHandle);
        _action = action;
    }

    private void TextHandle(string value)
    {
        _action(value);
        ui.UpdateComponent();
    }
}