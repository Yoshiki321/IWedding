using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GizmoPanel : BasePanel
{
    private Toggle Rotation;
    private Toggle Scale;
    private Toggle Transform;

    void Start()
    {
        Rotation = GetUI("GizmoToggleGroup").transform.Find("Rotation").GetComponent<Toggle>();
        Transform = GetUI("GizmoToggleGroup").transform.Find("Transform").GetComponent<Toggle>();
        Scale = GetUI("GizmoToggleGroup").transform.Find("Scale").GetComponent<Toggle>();

        Rotation.onValueChanged.AddListener(RotationHandle);
        Transform.onValueChanged.AddListener(TransformHandle);
        Scale.onValueChanged.AddListener(ScaleHandle);
    }

    private void RotationHandle(bool value)
    {
        dispatchEvent(new GizmoPanelEvent(GizmoPanelEvent.Rotation));
    }

    private void TransformHandle(bool value)
    {
        dispatchEvent(new GizmoPanelEvent(GizmoPanelEvent.Transform));
    }

    private void ScaleHandle(bool value)
    {
        dispatchEvent(new GizmoPanelEvent(GizmoPanelEvent.Scale));
    }
}
