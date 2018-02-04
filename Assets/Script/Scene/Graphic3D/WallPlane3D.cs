using UnityEngine;
using System.Collections;
using Build3D;

public class WallPlane3D : RectPenetratePlane3D
{
    public void SetCollage(string id)
    {
        SetMaterial(TexturesManager.CreateMaterials(id));
    }
}
