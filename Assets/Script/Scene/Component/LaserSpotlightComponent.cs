using UnityEngine;
using System.Collections;

public class LaserSpotlightComponent : SceneComponent
{
    private bool _isInit = false;

    public override void Init(AssetSprite _item)
    {
        if (_isInit) return;
        _isInit = true;

    }

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 3, 0));
    }
}
