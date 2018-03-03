using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseSurfaceMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(ChooseSurfacePanelEvent.CREATE_SURFACE, CreatSurfaceHandle);
        AddViewListener(ChooseSurfacePanelEvent.HARD, CreateDoorHandle);
        AddViewListener(ChooseSurfacePanelEvent.EXIT, ExitHandle);
    }

    public override void OnRemove()
    {
        RemoveViewListener(ChooseSurfacePanelEvent.CREATE_SURFACE, CreatSurfaceHandle);
        RemoveViewListener(ChooseSurfacePanelEvent.HARD, CreateDoorHandle);
        RemoveViewListener(ChooseSurfacePanelEvent.EXIT, ExitHandle);
    }

    private void CreatSurfaceHandle(EventObject e)
    {
        ChooseSurfacePanelEvent ce = (ChooseSurfacePanelEvent)e;

        SurfaceVO vo = CreateLayout(ce.points, 1, 1, .4f, ce.name);

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_SURFACE,
            new List<AssetVO>() { null },
            new List<AssetVO>() { vo }
            ));
    }

    private void ExitHandle(EventObject e)
    {
        UIManager.CloseUI(panel.name);
    }

    private void CreateDoorHandle(EventObject e)
    {
        ChooseSurfacePanelEvent ce = (ChooseSurfacePanelEvent)e;
        AssetsModel.Instance.CreateNestedVO(ce.name, LoadNestedvoComplete);
    }

    private void LoadNestedvoComplete(NestedVO nestedvo)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.ADD_NESTED,
            new List<AssetVO>() { null },
            new List<AssetVO>() { nestedvo }
            ));
    }

    public SurfaceVO CreateLayout(string type, float x, float y, float thickness = 10, string name = "ha")
    {
        List<LineVO> lines = new List<LineVO>();
        string[] LineList = type.Split(';');
        List<string[]> layoutLineList = new List<string[]>();
        for (int i = 0; i < LineList.Length; i++)
        {
            layoutLineList.Add(LineList[i].Split(','));
        }
        for (int layoutLineListNum = 0; layoutLineListNum < layoutLineList.Count; layoutLineListNum++)
        {
            float a = float.Parse(layoutLineList[layoutLineListNum][0]);
            float b = float.Parse(layoutLineList[layoutLineListNum][1]);
            float c = float.Parse(layoutLineList[layoutLineListNum][2]);
            float d = float.Parse(layoutLineList[layoutLineListNum][3]);

            lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + a, y + b), new Vector2(x + c, y + d), thickness));

        }
        return BuilderModel.Instance.CreateSurfaceVO(lines,name);
    }
}
