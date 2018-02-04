using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentModelEvent : EventObject
{
    public static string DOCUMENT_NAME_CHANGED = "documentNameChanged";
    public static string DOCUMENT_EDITED = "documentEdited";
    public static string DOCUMENT_CREATED = "documentCreated";
    public static string OBJECTS_UPDATED = "objectsUpdated";

    public static string VALIDATE_OBJECT = "validateObject";

    public static string OBJECTS_COLLECTION_UPDATED = "objectsColelctionUpdated";
    public static string OBJECTS_FILLED = "objectsFilled";
    public static string CLIPBOARD_UPDATED = "clipboardUpdated";

    public AssetVO asset;

    public EventObject Clone()
    {
        return new DocumentModelEvent(type);
    }

    public DocumentModelEvent(string types, AssetVO asset = null, bool bubbles = false, bool cancelable = false)
                : base(types, bubbles, cancelable)
    {
        this.asset = asset;
    }
}
