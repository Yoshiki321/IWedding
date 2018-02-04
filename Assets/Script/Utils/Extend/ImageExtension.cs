using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class ImageExtension
{
    public static void Select(this Image image,bool value)
    {
        if (value)
        {
            image.color = ColorUtils.HexToColor("5F7BCAFF");
        }
        else
        {
            image.color = ColorUtils.HexToColor("FFFFFFFF");
        }
    }
}
