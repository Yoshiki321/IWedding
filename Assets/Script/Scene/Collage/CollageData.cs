using UnityEngine;
using System.Collections;
using static RenderingModeUnits;

public class CollageData
{
    public string id;
    public string materialsUrl;
    public string thumbnailUrl;

    public void FillFromCollageData(CollageData collage)
    {
        id = (collage as CollageData).id;
        materialsUrl = (collage as CollageData).materialsUrl;
        thumbnailUrl = (collage as CollageData).thumbnailUrl;
    }

    public bool Equals(CollageData collage)
    {
        return true;
    }

    public CollageData Clone()
    {
        return new CollageData();
    }
}
