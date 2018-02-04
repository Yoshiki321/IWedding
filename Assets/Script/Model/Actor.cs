using UnityEngine;
using System.Collections;

public abstract class Actor<T> where T : new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    protected void Dispatch(EventObject evt)
    {
        CommandMap.Instance.dispatchEvent(evt);
    }
}
