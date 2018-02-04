using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraCommand : Command
{
    public override void Execute(EventObject e)
    {
        CameraCommandEvent c = (CameraCommandEvent)e;
        CameraManager.ChangeCamera(c.data);
        
        DispatchEvent(new CameraCommandEvent(CameraCommandEvent.UPDATE, c.data));
    }
}
