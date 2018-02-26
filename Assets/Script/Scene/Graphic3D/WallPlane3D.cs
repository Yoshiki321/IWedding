using UnityEngine;
using System.Collections;
using Build3D;

public class WallPlane3D : RectPenetratePlane3D
{
    public void SetCollage(CollageStruct collageStruct)
    {
        Material m = TexturesManager.CreateMaterials(collageStruct.id as string);
        m.color = collageStruct.color;
        SetMaterial(m);
    }

    public void SetCollage(string id)
    {
        SetMaterial(TexturesManager.CreateMaterials(id));
    }
}
