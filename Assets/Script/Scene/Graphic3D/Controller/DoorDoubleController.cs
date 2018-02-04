using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDoubleController : BaseController
{

    private GameObject _right;
    private GameObject _left;

    void Start()
    {
        _right = transform.Find("DoorRight").gameObject;
        _left = transform.Find("DoorLeft").gameObject;
    }

    private bool _open;

    public void OpenDoor()
    {
        _open = true;
        transform.GetComponent<BoxCollider>().isTrigger = true;
        loop = true;
    }

    public void CloseDoor()
    {
        _open = false;
        transform.GetComponent<BoxCollider>().isTrigger = false;
        loop = true;
    }

    private bool loop;
    private int c = 0;

    void Update()
    {
        if (!loop) return;

        if (_open)
        {
            if (c >= 30)
            {
                _right.transform.localRotation = Quaternion.Euler(-90, 0, -120);
                _left.transform.localRotation = Quaternion.Euler(-90, 0, 120);
                loop = false;
                c = 30;
            }
            else
            {
                _right.transform.eulerAngles = new Vector3(_right.transform.eulerAngles.x, _right.transform.eulerAngles.y, _right.transform.eulerAngles.z - 4);
                _left.transform.eulerAngles = new Vector3(_left.transform.eulerAngles.x, _left.transform.eulerAngles.y, _left.transform.eulerAngles.z + 4);
                c++;
            }
        }
        else
        {
            if (c <= 0)
            {
                _right.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                _left.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                loop = false;
                c = 0;
            }
            else
            {
                _right.transform.eulerAngles = new Vector3(_right.transform.eulerAngles.x, _right.transform.eulerAngles.y, _right.transform.eulerAngles.z + 4);
                _left.transform.eulerAngles = new Vector3(_left.transform.eulerAngles.x, _left.transform.eulerAngles.y, _left.transform.eulerAngles.z - 4);
                c--;
            }
        }
    }

    public override void Interaction()
    {
        if (_open)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
}
