using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtension
{
    public static void Alpha(this GameObject gameObject, float value = 1)
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;

        MeshRenderer[] mrs = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            Material m = mr.material;
            m.color = new Color(m.color.r, m.color.g, m.color.b, value);

            if (value == 1)
            {
                RenderingModeUnits.SetMaterialRenderingMode(m, RenderingModeUnits.RenderingMode.Opaque);
            }
            else
            {
                RenderingModeUnits.SetMaterialRenderingMode(m, RenderingModeUnits.RenderingMode.Transparent);
            }
        }
    }

    public static void AddBoxCollider(this GameObject gameObject, bool remove = false, bool enabled = true)
    {
        BoxCollider[] bList = gameObject.GetComponentsInChildren<BoxCollider>();

        if (!remove)
        {
            if (bList.Length > 0)
            {
                return;
            }
        }

        foreach (BoxCollider boxc in bList)
        {
            GameObject.Destroy(boxc);
        }

        Quaternion lastq = gameObject.transform.localRotation;
        Vector3 lasts = gameObject.transform.localScale;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        Renderer[] rList = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in rList)
        {
            if (r.bounds.size.x == 0 || r.bounds.size.y == 0 || r.bounds.size.z == 0)
            {
                continue;
            }

            if (r.gameObject.layer == LayerMask.NameToLayer("TransparentFX"))
            {
                continue;
            }

            BoxCollider box = gameObject.AddComponent<BoxCollider>();
            box.center = new Vector3(r.bounds.center.x - gameObject.transform.localPosition.x,
                r.bounds.center.y - gameObject.transform.localPosition.y,
                r.bounds.center.z - gameObject.transform.localPosition.z);
            box.size = new Vector3(r.bounds.size.x,
                r.bounds.size.y,
                r.bounds.size.z);

            box.enabled = enabled;
        }

        gameObject.transform.localRotation = lastq;
        gameObject.transform.localScale = lasts;
    }

    public static Vector3 GetBoxColliderCenter(this GameObject gameObject)
    {
        Dictionary<string, float> h = GetCollider(gameObject);
        return new Vector3((h["maxx"] + h["minx"]) / 2, (h["maxy"] + h["miny"]) / 2, (h["maxz"] + h["minz"]) / 2);
    }

    public static Vector3 GetBoxColliderSize(this GameObject gameObject)
    {
        Dictionary<string, float> h = GetCollider(gameObject);
        return new Vector3(h["maxx"] - h["minx"], h["maxy"] - h["miny"], h["maxz"] - h["minz"]);
    }

    private static Dictionary<string, float> GetCollider(GameObject gameobject)
    {
        BoxCollider[] mss = gameobject.GetComponents<BoxCollider>();
        BoxCollider[] msc = gameobject.GetComponentsInChildren<BoxCollider>();

        BoxCollider[] ms = new BoxCollider[mss.Length + msc.Length];
        int j = 0;
        foreach (BoxCollider b in mss)
        {
            ms[j] = b;
            j++;
        }
        foreach (BoxCollider b in msc)
        {
            ms[j] = b;
            j++;
        }
        float[] maxxs = new float[ms.Length];
        float[] maxys = new float[ms.Length];
        float[] maxzs = new float[ms.Length];
        float[] minxs = new float[ms.Length];
        float[] minys = new float[ms.Length];
        float[] minzs = new float[ms.Length];

        for (int i = 0; i < ms.Length; i++)
        {
            maxxs[i] = ms[i].center.x + ms[i].size.x / 2;
            maxys[i] = ms[i].center.y + ms[i].size.y / 2;
            maxzs[i] = ms[i].center.z + ms[i].size.z / 2;
            minxs[i] = ms[i].center.x - ms[i].size.x / 2;
            minys[i] = ms[i].center.y - ms[i].size.y / 2;
            minzs[i] = ms[i].center.z - ms[i].size.z / 2;
        }

        float maxx = 0;
        for (int i = 0; i < maxxs.Length; i++) maxx = Mathf.Max(maxx, maxxs[i]);

        float maxy = 0;
        for (int i = 0; i < maxys.Length; i++) maxy = Mathf.Max(maxy, maxys[i]);

        float maxz = 0;
        for (int i = 0; i < maxzs.Length; i++) maxz = Mathf.Max(maxz, maxzs[i]);

        float minx = 0;
        for (int i = 0; i < minxs.Length; i++) minx = Mathf.Min(minx, minxs[i]);

        float miny = 0;
        for (int i = 0; i < minys.Length; i++) miny = Mathf.Min(miny, minys[i]);

        float minz = 0;
        for (int i = 0; i < minzs.Length; i++) minz = Mathf.Min(minz, minzs[i]);

        Dictionary<string, float> h = new Dictionary<string, float>();
        h.Add("maxx", maxx);
        h.Add("maxy", maxy);
        h.Add("maxz", maxz);
        h.Add("minx", minx);
        h.Add("miny", miny);
        h.Add("minz", minz);

        return h;
    }
}
