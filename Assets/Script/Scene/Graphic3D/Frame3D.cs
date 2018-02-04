using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame3D : MonoBehaviour
{
    private GameObject _item;

    public void Create(WallPlaneVO vo, Vector3 point)
    {
        _item = new GameObject();

        _item.name = "Frame1";
        transform.name = _item.name;

        _item.transform.parent = transform;
        _item.transform.position = point;

        float _a3 = PlaneUtils.Angle(vo.from, vo.to);
        if (_a3 == 0)
        {
            _a3 = 180;
        }
        else if (_a3 == 180)
        {
            _a3 = 0;
        }

        _item.transform.rotation = Quaternion.Euler(_item.transform.eulerAngles.x, _a3, _item.transform.eulerAngles.z);

        ChangeMaterial();
    }

    public void ChangeMaterial()
    {
        string url = "Item/Frame/Frame1/" + names[Random.Range(0, 7)];

        _item.transform.Find("Picture").GetComponent<MeshRenderer>().material = Resources.Load(url, typeof(Material)) as Material;
    }

    private string[] names = new string[7]{"Studio/Studio1","Studio/Studio2","Studio/Studio3",
        "Milano/Milano1","Milano/Milano2","Milano/Milano3","Milano/Milano4"};
}
