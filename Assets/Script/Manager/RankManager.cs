using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Build3D;

/// <summary>
/// 对齐管理
/// </summary>
public class RankManager
{
    #region transform

    /// <summary>
    /// 将列表里所有道具的x等于列表一个道具的x
    /// </summary>
    /// <param name="list"></param>
    public static void RankPositionX(List<Item3D> list)
    {
        RankTransform(list, "x");
    }

    /// <summary>
    /// 将列表里所有道具的y等于列表一个道具的y
    /// </summary>
    /// <param name="list"></param>
    public static void RankPositionY(List<Item3D> list)
    {
        RankTransform(list, "y");
    }

    /// <summary>
    /// 将列表里所有道具的z等于列表一个道具的z
    /// </summary>
    /// <param name="list"></param>
    public static void RankPositionZ(List<Item3D> list)
    {
        RankTransform(list, "z");
    }

    /// <summary>
    /// 将列表里所有道具的ScaleX等于列表一个道具的ScaleX
    /// </summary>
    /// <param name="list"></param>
    public static void RankScaleX(List<Item3D> list)
    {
        RankTransform(list, "sx");
    }

    /// <summary>
    /// 将列表里所有道具的ScaleY轴等于列表一个道具的ScaleY
    /// </summary>
    /// <param name="list"></param>
    public static void RankScaleY(List<Item3D> list)
    {
        RankTransform(list, "sy");
    }

    /// <summary>
    /// 将列表里所有道具的ScaleZ等于列表一个道具的ScaleZ
    /// </summary>
    /// <param name="list"></param>
    public static void RankScaleZ(List<Item3D> list)
    {
        RankTransform(list, "sz");
    }

    /// <summary>
    /// 将列表里所有道具的RotationX等于列表一个道具的RotationX
    /// </summary>
    /// <param name="list"></param>
    public static void RankRotationX(List<Item3D> list)
    {
        RankTransform(list, "rx");
    }

    /// <summary>
    /// 将列表里所有道具的RotationY等于列表一个道具的RotationY
    /// </summary>
    /// <param name="list"></param>
    public static void RankRotationY(List<Item3D> list)
    {
        RankTransform(list, "ry");
    }

    /// <summary>
    /// 将列表里所有道具的RotationZ等于列表一个道具的RotationZ
    /// </summary>
    /// <param name="list"></param>
    public static void RankRotationZ(List<Item3D> list)
    {
        RankTransform(list, "rz");
    }

    private static void RankTransform(List<Item3D> list, string value)
    {
        if (list.Count <= 1) return;
        float X = list[0].x;
        float Y = list[0].y;
        float Z = list[0].z;
        float sX = list[0].scaleX;
        float sY = list[0].scaleY;
        float sZ = list[0].scaleZ;
        float rX = list[0].rotationX;
        float rY = list[0].rotationY;
        float rZ = list[0].rotationZ;
        foreach (Item3D item in list)
        {
            if (value == "x") item.x = X;
            if (value == "y") item.y = Y;
            if (value == "z") item.z = Z;
            if (value == "sx") item.scaleX = sX;
            if (value == "sy") item.scaleY = sY;
            if (value == "sz") item.scaleZ = sZ;
            if (value == "rx") item.rotationX = rX;
            if (value == "ry") item.rotationY = rY;
            if (value == "rz") item.rotationZ = rZ;
        }
    }

    #endregion
}
