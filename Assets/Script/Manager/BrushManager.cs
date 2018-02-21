using UnityEngine;
using System.Collections;
using BuildManager;
using System;

public class BrushManager
{
    public enum BrushMode
    {
        Place,
        Direct,
    }

    GameObject item;
    GameObject box;

    public BrushManager()
    {
        box = new GameObject();
        _brushMode = BrushMode.Place;
    }

    BrushMode _brushMode;

    public BrushMode brushMode
    {
        set
        {
            _brushMode = value;
            SetItem("", null);
        }
        get { return _brushMode; }
    }

    string itemId;
    Action<Vector3> clickFun;

    public void SetItem(string id, Action<Vector3> f)
    {
        itemId = id;

        if (item != null)
        {
            GameObject.Destroy(item);
        }

        if (id == "")
        {
            return;
        }

        ItemData itemData = ItemManager.GetItemData(id);

        if (itemData == null)
        {
            return;
        }

        clickFun = f;

        if (itemData != null)
        {
            item = GameObject.Instantiate(Resources.Load(itemData.model), new Vector3(), new Quaternion()) as GameObject;
            item.transform.parent = box.transform;
            MeshRenderer[] mrs = item.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in mrs)
            {
                Material m = mr.material;
                m.color = new Color(m.color.r, m.color.g, m.color.b, .5f);
                RenderingModeUnits.SetMaterialRenderingMode(m, RenderingModeUnits.RenderingMode.Transparent);
            }

            BoxCollider[] boxs = item.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider box in boxs)
            {
                GameObject.Destroy(box);
            }

            MeshCollider[] meshBoxs = item.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider meshBox in meshBoxs)
            {
                GameObject.Destroy(meshBox);
            }

            Transform[] ts = item.GetComponentsInChildren<Transform>();
            foreach (Transform t in ts)
            {
                t.gameObject.layer = LayerMask.NameToLayer("ObjectSprite3D");
            }
        }
    }

    public string GetItem()
    {
        return itemId;
    }

    public void Update()
    {
        if (item == null || itemId == "") return;

        if ((Input.mousePosition.x > 350 && Input.mousePosition.x < 1520) &&
             (Input.mousePosition.y < 1000))
        {
            Ray ray = SceneManager.Instance.Camera3D.ScreenPointToRay(Input.mousePosition);
            LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 9999999))
            {
                Vector3 pos = hit.point;
                //pos.y += 0.5f;

                item.transform.localPosition = pos;
                //box.transform.LookAt(hit.point - hit.normal);

                if (Input.GetMouseButton(0))
                {
                    clickFun(pos);
                }
            }
        }
    }
}
