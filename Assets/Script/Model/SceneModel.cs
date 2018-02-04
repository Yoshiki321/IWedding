using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneModel : Actor<SceneModel>
{
    private CameraVO _cameraVO;

    public SceneModel()
    {
        _cameraVO = new CameraVO();
    }

    public CameraVO CameraVO
    {
        set { _cameraVO = value; }
        get { return _cameraVO; }
    }
}
