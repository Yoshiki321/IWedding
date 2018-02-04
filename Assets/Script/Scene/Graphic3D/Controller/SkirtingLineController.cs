using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build3D;

public class SkirtingLineController : BaseController
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

	public string id;

    private Material _mt;
    private Vector2 _tiling;

    public override void Interaction()
    {
		//Surface3D s3 = BuilderModel3D.GetSurface3DByLineId(id);
        Surface3D s3 = null;
	
		string url;

		if (s3.SkirtingLineData == "Materials/SkirtingLine/Ceiling_Edge_Decor") 
		{
			url = s3.SkirtingLineData = "Materials/SkirtingLine/Ceiling_Edge_Clean";
		} 
		else 
		{
			url = s3.SkirtingLineData = "Materials/SkirtingLine/Ceiling_Edge_Decor";
		}

		for (int i = 0; i < s3.lines.Count; i++) 
		{
			//Line3D l3 = BuilderModel3D.GetLine3DById(s3.lines[i]);
			//Line3D l3 = null;

			//_tiling = l3.ceilingLine3D.transform.GetComponent<Renderer>().material.GetTextureScale("_MainTex");

			//_mt = Resources.Load(url, typeof(Material)) as Material;

			//l3.ceilingLine3D.transform.GetComponent<Renderer>().material = _mt;

			//_mt.SetTextureScale("_MainTex", _tiling);
		}
    } 
}
