using System;
using UnityEngine;
using UnityEngine.UI;

public class DropdownUI : BaseUI
{
    public Text nameText;
    public Dropdown dropdown;

    private Action<int> _action;

    public void OnValueChanged(Action<int> action)
    {
        dropdown.onValueChanged.AddListener(OnValueChanged);
        _action = action;
    }

    private void OnValueChanged(int value)
    {
        _action(value);
        ui.UpdateComponent();
    }
}