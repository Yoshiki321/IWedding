using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{

    void Start()
    {

    }

    private GameObject _currentItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Infrared.CurrentInfo.transform)
            {
                _currentItem = Infrared.CurrentInfo.transform.gameObject;

                BaseController bc = _currentItem.GetComponentInParent<BaseController>();
                if (bc)
                {
                    bc.Interaction();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (Infrared.CurrentInfo.transform)
            {
                _currentItem = Infrared.CurrentInfo.transform.gameObject;

                WallController wc = _currentItem.GetComponentInParent<WallController>();
                if (wc)
                {
                    wc.CreateHardWall();
                }

                TableLayoutController tc = _currentItem.GetComponentInParent<TableLayoutController>();
                if (tc)
                {
                    tc.Interaction();
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    _currentItem = Infrared.CurrentInfo.transform.gameObject;

        //    WallController wc = _currentItem.GetComponentInParent<WallController>();
        //    if (wc)
        //    {
        //        wc.CreateFrame();
        //    }

        //    Frame3D frame3D = _currentItem.GetComponentInParent<Frame3D>();
        //    if (frame3D)
        //    {
        //        frame3D.ChangeMaterial();
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    _currentItem = Infrared.CurrentInfo.transform.gameObject;

        //    ItemChangeController icc = _currentItem.GetComponentInParent<ItemChangeController>();
        //    if (icc)
        //    {
        //        icc.Change();
        //    }
        //}
    }
}
