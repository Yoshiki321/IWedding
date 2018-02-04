using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ColorVO
{
    public Color color = Color.white;
    public float slider = 0;
    public Vector2 position = new Vector2(14, 245);

    public ColorVO Clone()
    {
        ColorVO c = new ColorVO();
        c.color = color;
        c.slider = slider;
        c.position = position;
        return c;
    }

    public bool Equals(ColorVO asset)
    {
        return (
            asset.color == color &&
            asset.slider == slider &&
            asset.position.Equals(position)
            );
    }

    public void SetCode(string code)
    {
        string[] sArray = Regex.Split(code, ",", RegexOptions.IgnoreCase);
        color = ColorUtils.HexToColor(sArray[0]);
        slider = float.Parse(sArray[1]);
        position.x = float.Parse(sArray[2]);
        position.y = float.Parse(sArray[3]);
    }

    public string ToCode()
    {
        return '"' + ColorUtils.ColorToHex(color) + "," + slider + "," + position.x + "," + position.y + '"';
    }
}
