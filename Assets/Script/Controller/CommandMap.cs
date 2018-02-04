using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class CommandMap : EventDispatcher
{
    private static CommandMap instance;

    public static CommandMap Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CommandMap();
            }
            return instance;
        }
    }

    public static void MapEvent(string eid,Type type)
    {
        Command command = Activator.CreateInstance(type) as Command;
        Instance.addEventListener(eid, command.Execute);
    }

    public static void DispatcherEvent(EventObject evt)
    {
        Instance.dispatchEvent(evt);
    }
}
