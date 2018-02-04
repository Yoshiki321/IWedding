using BuildManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    public Vector3 Normal = new Vector3(0, 1, 0);
    Quaternion direction;

    void Start()
    {
        direction = Quaternion.FromToRotation(new Vector3(0, 1, 0), Normal);
    }

    void Update()
    {
        Quaternion q = SceneManager.Instance.Camera3D.transform.rotation * direction;
        Vector3 v = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(v.x, q.eulerAngles.y, v.z);
    }
}
