using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;
using Build3D;

public class UIManager
{
    public static Hashtable uiHashtable = new Hashtable();

    public static void CreateUI(string url, string ui, Type type)
    {
        Mediators m = Activator.CreateInstance(type) as Mediators;
        GameObject obj = GameObject.Instantiate(Resources.Load(url, typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
        obj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        obj.GetComponent<Canvas>().worldCamera = SceneManager.Instance.CameraUI;
        obj.transform.parent = SceneManager.Instance.UILayer.transform;
        Type t = Type.GetType(ui);
        Component c = obj.AddComponent(t);
        c.name = ui;
        uiHashtable.Add(ui, m);
        m.panel = c as BasePanel;
        obj.SetActive(false);
    }

    public static Hashtable panelHashtable = new Hashtable();

    public static void CreatePanel(string url, string ui)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load(url, typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
        obj.transform.parent = SceneManager.Instance.UILayer.transform;
        Type t = Type.GetType(ui);
        Component c = obj.AddComponent(t);
        (c as BaseWindow).Init();
        c.name = ui;
        panelHashtable.Add(ui, c);
        obj.SetActive(false);
    }

    public static BaseWindow OpenPanel(string name, object value = null, Vector3 v = new Vector3())
    {
        BaseWindow obj = panelHashtable[name] as BaseWindow;
        obj.gameObject.SetActive(true);
        obj.SetContent(value);
        obj.transform.Find("View").localPosition = v;
        return obj;
    }

    public static BaseWindow ClosePanel(string name)
    {
        BaseWindow obj = panelHashtable[name] as BaseWindow;
        obj.gameObject.SetActive(false);
        return obj;
    }

    public static Hashtable componentHashtable = new Hashtable();

    public static BaseComponentUI CreateComponent(string url, string ui)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load(url, typeof(GameObject)) as GameObject, new Vector3(), new Quaternion());
        obj.GetComponent<Canvas>().worldCamera = SceneManager.Instance.CameraUI;
        obj.transform.parent = SceneManager.Instance.UILayer.transform.Find("ComponentPanel").transform.Find("View").transform.Find("Viewport").transform.Find("Content");
        Type t = Type.GetType(ui);
        Component c = obj.AddComponent(t);
        c.name = ui;
        BaseComponentUI bc = c as BaseComponentUI;
        componentHashtable.Add(ui, bc);
        obj.SetActive(false);
        bc.Init();
        return bc;
    }

    public static BaseComponentUI CreateComponent(string ui)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("UI/Component/BaseComponentUI", typeof(GameObject)) as GameObject, new Vector3(), new Quaternion());
        obj.GetComponent<Canvas>().worldCamera = SceneManager.Instance.CameraUI;
        obj.transform.parent = SceneManager.Instance.UILayer.transform.Find("ComponentPanel").transform.Find("View").transform.Find("Viewport").transform.Find("Content");
        obj.name = ui;
        Type t = Type.GetType(ui);
        Component c = obj.AddComponent(t);
        c.name = ui;
        BaseComponentUI bc = c as BaseComponentUI;
        componentHashtable.Add(ui, bc);
        obj.SetActive(false);
        bc.Init();
        return bc;
    }

    public static void UpdateComponent(string ui, List<ObjectSprite> items)
    {
        BaseComponentUI obj = componentHashtable[ui] as BaseComponentUI;
        obj.items = items;
    }

    public static BaseComponentUI OpenComponent(string ui, List<ObjectSprite> items)
    {
        ((uiHashtable[UI.ComponentPanel] as Mediators).panel as BasePanel).transform.Find("View").gameObject.SetActive(true);

        BaseComponentUI obj = componentHashtable[ui] as BaseComponentUI;
        obj.gameObject.SetActive(true);
        obj.items = items;
          
        return obj;
    }

    public static BasePanel OpenUI(string bp, object obj = null, float x = float.NaN, float y = float.NaN)
    {
        Mediators m = uiHashtable[bp] as Mediators;

        if (!m.panel.gameObject.activeSelf)
        {
            m.OnRegister();
            m.panel.gameObject.SetActive(true);

            if (obj != null)
            {
                m.panel.content = obj;
            }

            m.panel.Open();

            Transform view = m.panel.transform.Find("View");
            if (view && !float.IsNaN(x) && !float.IsNaN(y))
            {
                view.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
            }
        }

        return m.panel;
    }

    public static BasePanel CloseUI(string bp)
    {
        Mediators m = uiHashtable[bp] as Mediators;

        if (m.panel.gameObject.activeSelf)
        {
            m.OnRemove();
            m.panel.Close();
            m.panel.gameObject.SetActive(false);
        }

        return m.panel;
    }

    public static BasePanel GetUI(string bp)
    {
        Mediators m = uiHashtable[bp] as Mediators;
        return m.panel;
    }

    public static Mediators GetMediators(string bp)
    {
        return uiHashtable[bp] as Mediators;
    }
}
