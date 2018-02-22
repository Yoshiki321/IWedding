using UnityEngine.UI;
using System;
using UnityEngine;

public class TitleButtonUI : BaseUI
{
    public Text buttonText;
    public Text titleText;
    public Button button;

    private Action<TitleButtonUI> _action;

    public void OnClickButtom(Action<TitleButtonUI> action)
    {
        button.onClick.AddListener(ClickHandle);
        _action = action;
    }

    private void ClickHandle()
    {
        _action(this);
        ui.UpdateComponent();
    }
}
