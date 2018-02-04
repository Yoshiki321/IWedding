using System;
using System.Collections.Generic;
using UnityEngine;
using Build2D;

public class Design
{
//    public static LineData CreateLine(Vector2 from, Vector2 to, float thickness = 10)
//    {
//        LineVO linevo = BuilderModel.Instance.CreateLineVO(from, to, thickness, Factory.CreateCollageVO("1002"), Factory.CreateCollageVO("1002"));
//        LineData lineData = BuilderModel.Instance.CreateLine(linevo);
//        return lineData;
//    }

//    public static SurfaceData CreateSurface(List<LineVO> lines, string name)
//    {
//        SurfaceVO surfaceVO = BuilderModel.Instance.CreateSurfaceVO(lines, name,Factory.CreateCollageVO("1001"), Factory.CreateCollageVO("1001"));
//        SurfaceData surfaceData = BuilderModel.Instance.CreateSurface(surfaceVO);
//        return surfaceData;
//    }

//    public static List<LineVO> CreateLayout(LayoutConstant type, float x, float y, float thickness = 10, string name = "ha")
//    {
//        List<LineVO> lines = new List<LineVO>();

//        thickness = .15f;

//        switch (type)
//        {
//            case LayoutConstant.RECT:
//                lines.Add(CreateLine(new Vector2(x + 3, y - 3), new Vector2(x - 3, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y - 3), new Vector2(x + 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y + 3), new Vector2(x - 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 3, y + 3), new Vector2(x - 3, y - 3), thickness).vo);
//                break;
//            case LayoutConstant.CONCAVE:
//                lines.Add(CreateLine(new Vector2(x - 1, y - 1), new Vector2(x - 1, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 1, y - 3), new Vector2(x - 3, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 3, y - 3), new Vector2(x - 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 3, y + 3), new Vector2(x + 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y + 3), new Vector2(x + 3, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y - 3), new Vector2(x + 1, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 1, y - 3), new Vector2(x + 1, y - 1), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 1, y - 1), new Vector2(x - 1, y - 1), thickness).vo);
//                break;
//            case LayoutConstant.BULGE:
//                lines.Add(CreateLine(new Vector2(x - 1, y - 5), new Vector2(x - 1, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 1, y - 3), new Vector2(x - 3, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 3, y - 3), new Vector2(x - 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 3, y + 3), new Vector2(x + 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y + 3), new Vector2(x + 3, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y - 3), new Vector2(x + 1, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 1, y - 3), new Vector2(x + 1, y - 5), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 1, y - 5), new Vector2(x - 1, y - 5), thickness).vo);
//                break;
//            case LayoutConstant.L:
//                lines.Add(CreateLine(new Vector2(x - 3, y - 3), new Vector2(x, y - 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x, y - 3), new Vector2(x, y), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x, y), new Vector2(x + 3, y), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y), new Vector2(x + 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x + 3, y + 3), new Vector2(x - 3, y + 3), thickness).vo);
//                lines.Add(CreateLine(new Vector2(x - 3, y + 3), new Vector2(x - 3, y - 3), thickness).vo);
//                break;
//        }

//        SurfaceData surfaceData = Design.CreateSurface(lines, name);
//        //BuilderModel.Instance.UpdateSurface2(surfaceData.surface);

//        return lines;
//    }

//    public static void CreateItem(string id, Action<ObjectData> eFun = null)
//    {
//        new LoadItmeVO(id, eFun);
//    }

//    public static void CreateNested(string id, Action<ObjectData> eFun = null)
//    {
//        new LoadNestedVO(id, eFun);
//    }
//}

//class LoadSpotlightVO
//{
//    private Action<ObjectData> eFun;

//    public LoadSpotlightVO(string id, Action<ObjectData> eFun)
//    {
//        this.eFun = eFun;
//        AssetsModel.Instance.CreateItemVO(id, LoadItmevoComplete);
//    }

//    private void LoadItmevoComplete(ItemVO itemvo)
//    {
//        ObjectData itemData = AssetsModel.Instance.CreateItem(itemvo);
//        if (this.eFun != null) this.eFun(itemData);
//    }
//}

//class LoadItmeVO
//{
//    private Action<ObjectData> eFun;

//    public LoadItmeVO(string id, Action<ObjectData> eFun)
//    {
//        this.eFun = eFun;
//        AssetsModel.Instance.CreateItemVO(id, LoadItmevoComplete);
//    }

//    private void LoadItmevoComplete(ItemVO itemvo)
//    {
//        ObjectData itemData = AssetsModel.Instance.CreateItem(itemvo);
//        if (this.eFun != null) this.eFun(itemData);
//    }
}

class LoadNestedVO
{
    private Action<ObjectData> eFun;

    public LoadNestedVO(string id, Action<ObjectData> eFun)
    {
        this.eFun = eFun;
        AssetsModel.Instance.CreateNestedVO(id, LoadNestedvoComplete);
    }

    private void LoadNestedvoComplete(NestedVO nestedvo)
    {
        ObjectData nestedData = AssetsModel.Instance.CreateNested(nestedvo);
        if (this.eFun != null) this.eFun(nestedData);
    }
}
