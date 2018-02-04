using System;
using System.Collections.Generic;
using UnityEngine;

public class Mediators
{
    public virtual void OnRegister()
    {

    }

    public virtual void OnRemove()
    {

    }

	protected void AddViewListener(String types, EventDispatcher.EventDelegate listener)
	{
        panel.addEventListener(types, listener);
	}

    protected void RemoveViewListener(String types, EventDispatcher.EventDelegate listener)
    {
        panel.removeEventListener(types, listener);
    }

	protected void AddContextListener(String types, EventDispatcher.EventDelegate listener)
	{
        CommandMap.Instance.addEventListener(types, listener);
	}

	protected void RemoveContextListener(String types, EventDispatcher.EventDelegate listener)
	{
        CommandMap.Instance.removeEventListener(types, listener);
	}

    public static void DispatcherEvent(EventObject evt)
    {
        CommandMap.Instance.dispatchEvent(evt);
    }

    public BasePanel panel;
}
