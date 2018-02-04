using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    void Start()
    {

    }

    public GameObject obj;

    void Update()
    {
        //Ray ray = SceneManager.Instance.HoverCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //LayerMask layer = 1 << LayerMask.NameToLayer("Plane3D");
        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo, 9999999, layer))
        //{
        //    if (obj)
        //    {
        //        obj.transform.position = new Vector3(hitInfo.point.x, obj.transform.position.y, hitInfo.point.z);
        //    }
        //}
    }
}
