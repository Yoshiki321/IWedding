using System;
using UnityEngine;

public class BasePanel : DispatcherEventPanel
{
    public object content;

    public virtual void Open()
    {
    }

    public virtual void Close()
    {
        content = null;
    }

    public void CloseSelf()
    {
        UIManager.CloseUI(name);
    }
}
