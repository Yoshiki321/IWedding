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

        SurfaceVO vo = CreateLayout(ce.type, 1, 1, .15f, ce.name);

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

    public SurfaceVO CreateLayout(LayoutConstant type, float x, float y, float thickness = 10, string name = "ha")
    {
        List<LineVO> lines = new List<LineVO>();

        switch (type)
        {
            case LayoutConstant.RECT:
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y - 3), new Vector2(x - 3, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y - 3), new Vector2(x + 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y + 3), new Vector2(x - 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y + 3), new Vector2(x - 3, y - 3), thickness));
                break;

            case LayoutConstant.CONCAVE:
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 1, y - 1), new Vector2(x - 1, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 1, y - 3), new Vector2(x - 3, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y - 3), new Vector2(x - 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y + 3), new Vector2(x + 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y + 3), new Vector2(x + 3, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y - 3), new Vector2(x + 1, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 1, y - 3), new Vector2(x + 1, y - 1), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 1, y - 1), new Vector2(x - 1, y - 1), thickness));
                break;

            case LayoutConstant.BULGE:
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 1, y - 5), new Vector2(x - 1, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 1, y - 3), new Vector2(x - 3, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y - 3), new Vector2(x - 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y + 3), new Vector2(x + 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y + 3), new Vector2(x + 3, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y - 3), new Vector2(x + 1, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 1, y - 3), new Vector2(x + 1, y - 5), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 1, y - 5), new Vector2(x - 1, y - 5), thickness));
                break;

            case LayoutConstant.L:
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y - 3), new Vector2(x, y - 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x, y - 3), new Vector2(x, y), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x, y), new Vector2(x + 3, y), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y), new Vector2(x + 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x + 3, y + 3), new Vector2(x - 3, y + 3), thickness));
                lines.Add(BuilderModel.Instance.CreateLineVO(new Vector2(x - 3, y + 3), new Vector2(x - 3, y - 3), thickness));
                break;
        }

        return BuilderModel.Instance.CreateSurfaceVO(lines, name);
    }
}
