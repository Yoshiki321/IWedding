using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListAssetsExtension
{
    public static ArrayList ToArrayList(this List<ObjectSprite> assets)
    {
        ArrayList list = new ArrayList();
        foreach (ObjectSprite vo in assets)
        {
            list.Add(vo);
        }
        return list;
    }

    public static ArrayList ToArrayList(this List<AssetVO> assets)
    {
        ArrayList list = new ArrayList();
        foreach (AssetVO vo in assets)
        {
            list.Add(vo);
        }
        return list;
    }

    public static List<AssetVO> Clone(this List<AssetVO> assets)
    {
        List<AssetVO> list = new List<AssetVO>();

        foreach (AssetVO vo in assets)
        {
            list.Add(vo.Clone());
        }

        return list;
    }

    public static bool EqualsComponentsVO(this List<AssetVO> assets, List<AssetVO> newAssets)
    {
        if (assets.Count != newAssets.Count)
        {
            return false;
        }

        for (int i = 0; i < assets.Count; i++)
        {
            if (assets[i].EqualsComponentVO(newAssets[i]) == false)
            {
                return false;
            }
        }

        return true;
    }

    public static bool EqualsAsstesVO(this List<AssetVO> assets, List<AssetVO> newAssets)
    {
        if (assets.Count != newAssets.Count)
        {
            return false;
        }

        for (int i = 0; i < assets.Count; i++)
        {
            if (assets[i].Equals(newAssets[i]) == false)
            {
                return false;
            }
        }

        return true;
    }
}
