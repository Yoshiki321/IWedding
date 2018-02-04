using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToolMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(CameraToolPanelEvent.Into2D, Into2DHandle);
        AddViewListener(CameraToolPanelEvent.Into3D, Into3DHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(CameraToolPanelEvent.Into2D, Into2DHandle);
        RemoveViewListener(CameraToolPanelEvent.Into3D, Into3DHandle);
    }

    private void Into2DHandle(EventObject e)
    {
        DispatcherEvent(new CameraCommandEvent(CameraCommandEvent.CHANGE, CameraFlags.Two));

        SceneManager.Instance.mouseManager.ClearSelect();
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }

    private void Into3DHandle(EventObject e)
    {
        DispatcherEvent(new CameraCommandEvent(CameraCommandEvent.CHANGE, CameraFlags.Fly));

        SceneManager.Instance.mouseManager.ClearSelect();
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }
}
