using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : BaseController 
{
	public SurfacePlane3D surfacePlane3D;

	private bool _open;

	public override void Interaction()
	{
		if (_open)
		{
			_open = false;
            //surfacePlane3D.SetCollage("1001");
		}
		else
		{
			_open = true;
            //surfacePlane3D.SetCollage("1004");
		}
	}
}
