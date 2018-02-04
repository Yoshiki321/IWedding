using UnityEngine;
using System.Collections;
using System;

public class DispatcherEventPanel : MonoBehaviour
{
    private EventDispatcher _eventDispatcher;

    private EventDispatcher eventDispatcher
    {
        get
        {
            if (_eventDispatcher == null)
                _eventDispatcher = new EventDispatcher();
            return _eventDispatcher;
        }
    }

    public void addEventListener(String types, EventDispatcher.EventDelegate listener, bool useCapture = false, int priority = 0, bool useWeakReference = false)
    {
        eventDispatcher.addEventListener(types, listener, useCapture, priority, useWeakReference);
    }

    public void removeEventListener(String types, EventDispatcher.EventDelegate listener, bool useCapture = false)
    {
        eventDispatcher.removeEventListener(types, listener, useCapture);
    }

    public bool hasEventListener(String types)
    {
        return eventDispatcher.hasEventListener(types);
    }

    public virtual void dispatchEvent(EventObject evt)
    {
        eventDispatcher.dispatchEvent(evt);
    }

    protected virtual void OnClick(GameObject obj)
    {

    }

    protected virtual void OnDown(GameObject obj)
    {

    }

    protected virtual void OnUp(GameObject obj)
    {

    }

    protected virtual void OnEnter(GameObject obj)
    {

    }

    protected virtual void OnExit(GameObject obj)
    {

    }
    protected void AddEventClick(GameObject o)
    {
        List.Get(o).onClick = OnClick;
    }

    protected void AddEventDown(GameObject o)
    {
        List.Get(o).onDown = OnDown;
    }

    protected void AddEventUp(GameObject o)
    {
        List.Get(o).onUp = OnUp;
    }

    protected void AddEventOver(GameObject o)
    {
        List.Get(o).onEnter = OnEnter;
    }

    protected void AddEventExit(GameObject o)
    {
        List.Get(o).onExit = OnExit;
    }

    protected virtual GameObject GetUI(string name)
    {
        return transform.Find(name).gameObject;
    }

    private bool _visible;

    public bool Visible
    {
        set
        {
            _visible = value;
            gameObject.SetActive(value);
        }
        get { return _visible; }
    }
}
