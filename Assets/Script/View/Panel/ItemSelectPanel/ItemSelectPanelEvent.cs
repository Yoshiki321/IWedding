using UnityEngine;
using System.Collections;

public class ItemSelectPanelEvent : EventObject
{
    public static string SELECT_ITEM = "ItemSelectPanelEvent_Select_Item";
    public static string FOCUSON_SELECTION = "ItemSelectPanelEvent_FocusOn_Selection";

    public string name;

    public ItemSelectPanelEvent(string types, string name)
		: base(types)
	{
        this.name = name;
    }
}
