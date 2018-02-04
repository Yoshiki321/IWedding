using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildVO : AssetVO
{
    public List<LineVO> lines = new List<LineVO>();

    public List<SurfaceVO> surfaces = new List<SurfaceVO>();

    override public AssetVO Clone()
    {
        BuildVO b = new BuildVO();
        b.FillFromBuild(this);
        return b;
    }

    public void FillFromBuild(BuildVO asset)
    {
        this.lines = new List<LineVO>();
        this.surfaces = new List<SurfaceVO>();

        foreach (LineVO vo in asset.lines)
        {
            this.lines.Add(vo.Clone() as LineVO);
        }

        foreach (SurfaceVO vo in asset.surfaces)
        {
            this.surfaces.Add(vo.Clone() as SurfaceVO);
        }
    }
}
