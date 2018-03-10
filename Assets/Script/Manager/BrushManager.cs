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
    Action<Vector3, Vector3> clickFun;

    public void SetItem(string id, Action<Vector3, Vector3> f)
    {
        itemId = id;

        SceneManager.EnabledEditorObjectSelection(false);

        if (item != null)
        {
            GameObject.Destroy(item);
        }

        if (id == "")
        {
            SceneManager.EnabledEditorObjectSelection(true);
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
            item = GameObject.Instantiate(Resources.Load(itemData.model)) as GameObject;
            item.transform.parent = box.transform;
            item.transform.localPosition = new Vector3();
            item.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            MeshRenderer[] mrs = item.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in mrs)
            {
                Material m = mr.material;
                m.color = new Color(m.color.r, m.color.g, m.color.b, .5f);
                RenderingModeUnits.SetMaterialRenderingMode(m, RenderingModeUnits.RenderingMode.Transparent);
            }

            Transform[] ts = item.GetComponentsInChildren<Transform>();
            foreach (Transform t in ts)
            {
                t.gameObject.layer = LayerMask.NameToLayer("Water");
            }
        }
    }

    void ClearBox()
    {
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

        CapsuleCollider[] capsuleBoxs = item.GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider capsuleBox in capsuleBoxs)
        {
            GameObject.Destroy(capsuleBox);
        }
    }

    public string GetItem()
    {
        return itemId;
    }

    bool down;

    public void Update()
    {
        if (item == null || itemId == "") return;

        ClearBox();

        if (SceneManager.InEditorArea(Input.mousePosition))
        {
            Ray ray = SceneManager.Instance.Camera3D.ScreenPointToRay(Input.mousePosition);
            LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 9999999))
            {
                box.transform.position = hit.point;
                if (hit.transform.GetComponentInParent<SurfacePlane3D>() == null)
                {
                    if (itemId == "10070017" || itemId == "10070018")
                    {
                        box.transform.LookAt(hit.point - hit.normal);
                        box.transform.Translate(Vector3.back * 0.05f);
                    }
                }
                else
                {
                    box.transform.rotation = Quaternion.Euler(new Vector3());
                }
                Vector3 v = box.transform.rotation.eulerAngles;

                if (Input.GetMouseButtonDown(0))
                {
                    down = true;
                }
                if (Input.GetMouseButtonUp(0) && down)
                {
                    down = false;
                    clickFun(box.transform.position, new Vector3(0, v.y - 90, 0));
                }
            }
        }
    }
}
