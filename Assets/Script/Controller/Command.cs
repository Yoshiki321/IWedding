using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public virtual void Execute(EventObject e)
    {
    }

    protected void DispatchEvent(EventObject evt)
    {
        CommandMap.DispatcherEvent(evt);
    }
}
