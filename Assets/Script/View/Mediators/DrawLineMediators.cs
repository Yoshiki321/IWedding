using UnityEngine;
using System.Collections;
using BuildManager;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Build3D;

public class DrawLineMediators : Mediators
{
    public override void OnRegister()
    {
        AddContextListener(FileEvent.LOAD_THICKIRREGULAR_COMPLETE, LoadItemComplete);

        AddViewListener(DrawLinePanelEvent.ADD_ITEM, AddItemHandle);
        AddViewListener(DrawLinePanelEvent.REVISE_ITEM, ReviseItemHandle);
        AddViewListener(DrawLinePanelEvent.SAVE, SaveHandle);
        AddViewListener(DrawLinePanelEvent.LOAD, LoadHandle);
    }

    public override void OnRemove()
    {
        RemoveContextListener(FileEvent.LOAD_THICKIRREGULAR_COMPLETE, LoadItemComplete);

        RemoveViewListener(DrawLinePanelEvent.ADD_ITEM, AddItemHandle);
        RemoveViewListener(DrawLinePanelEvent.SAVE, SaveHandle);
        RemoveViewListener(DrawLinePanelEvent.LOAD, LoadHandle);
    }

    private void LoadItemComplete(EventObject e)
    {
        FileEvent fileEvent = e as FileEvent;
        (panel as DrawLinePanel).drawPanel.Code = fileEvent.obj as XmlNode;
    }

    private void SaveHandle(EventObject e)
    {
        DrawLinePanelEvent dpe = e as DrawLinePanelEvent;
        DispatcherEvent(new FileEvent(FileEvent.SAVE_THICKIRREGULAR, "", "", dpe.code));
    }

    private void LoadHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.LOAD_THICKIRREGULAR));
    }

    private void ReviseItemHandle(EventObject e)
    {
        DrawLinePanelEvent dpe = e as DrawLinePanelEvent;
        ObjectData data = AssetsModel.Instance.GetItemData(dpe.itemVO.id);
        if (data != null)
        {
            Item3D item = data.object3 as Item3D;
            ThickIrregularPlane3D t3 = (item.VO as ItemVO).model.GetComponent<ThickIrregularPlane3D>();
            if (t3 != null)
            {
                string name = t3.id + ".zwkjtiobj";
                string urlName = SceneManager.ProjectModelURL + "\\" + name;
                DispatcherEvent(new FileEvent(FileEvent.SAVE_THICKIRREGULAR, urlName, name, dpe.code));

                t3.Code = dpe.code;
                GameObject.Destroy(dpe.itemVO.model.gameObject);

                (item.VO as ItemVO).GetComponentVO<ThickIrregularVO>().xml = dpe.code;
            }
        }
    }

    private void AddItemHandle(EventObject e)
    {
        DrawLinePanelEvent dpe = e as DrawLinePanelEvent;
        ItemVO itemvo = dpe.itemVO;
        ThickIrregularPlane3D t3 = itemvo.model.GetComponent<ThickIrregularPlane3D>();

        if (AssetsModel.Instance.GetItemData(itemvo.id) != null)
        {
            string id = NumberUtils.GetGuid();
            itemvo.id = id;
            t3.id = id;
        }

        string name = t3.id + ".zwkjtiobj";
        string urlName = SceneManager.ProjectModelURL + "\\" + name;
        DispatcherEvent(new FileEvent(FileEvent.SAVE_THICKIRREGULAR, urlName, name, dpe.code));

        //string code = DES_zaowu.Encoder(dpe.code.OuterXml);
        //byte[] bytes = System.Text.Encoding.Default.GetBytes(code);
        //File.WriteAllBytes(urlName, bytes);

        ThickIrregularVO tvo = itemvo.AddComponentVO<ThickIrregularVO>();
        tvo.url = name;
        tvo.xml = dpe.code;

        itemvo.model.AddComponent<CurvyColumnComponent>();

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
            new List<AssetVO>() { null },
            new List<AssetVO>() { dpe.itemVO }
            ));
    }
}
