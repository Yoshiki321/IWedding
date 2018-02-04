using UnityEngine;
using System.Collections;
using BuildManager;

public class GizmoPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(GizmoPanelEvent.Transform, TransformHandle);
        AddViewListener(GizmoPanelEvent.Scale, ScaleHandle);
        AddViewListener(GizmoPanelEvent.Rotation, RotationHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(GizmoPanelEvent.Transform, TransformHandle);
        RemoveViewListener(GizmoPanelEvent.Scale, ScaleHandle);
        RemoveViewListener(GizmoPanelEvent.Rotation, RotationHandle);
    }

    private void TransformHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.ActiveGizmoType = RTEditor.GizmoType.Translation;
    }

    private void ScaleHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.ActiveGizmoType = RTEditor.GizmoType.Scale;
    }

    private void RotationHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.ActiveGizmoType = RTEditor.GizmoType.Rotation;
    }
}
