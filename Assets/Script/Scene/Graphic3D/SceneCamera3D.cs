using UnityEngine;
using System.Collections;

public class SceneCamera3D : ObjectSprite3D
{
    override public void Instantiate(ObjectVO vo)
    {
        layer = "ObjectSprite3D";

        base.Instantiate(vo);
    }

    public override GameObject model
    {
        set
        {
            SceneComponent[] clist = gameObject.GetComponentsInChildren<SceneComponent>();

            if (clist.Length == 0)
            {
                gameObject.AddComponent<EditorCameraComponent>();
                clist = gameObject.GetComponentsInChildren<SceneComponent>();
            }

            foreach (SceneComponent sc in clist)
            {
                sc.Init(this);
            }
        }
    }

    private void Update()
    {
    }
}
