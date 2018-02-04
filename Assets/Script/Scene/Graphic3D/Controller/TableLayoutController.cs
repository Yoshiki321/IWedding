using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLayoutController : BaseController 
{
	private Vector3 _point3;

	public override void Interaction()
	{
		_point3 = MeshUtils3D.BoundSizeBoxCollider (transform.gameObject);

		GameObject c = new GameObject();
		GameObject c1 = new GameObject();
		GameObject c2 = new GameObject();
        
		GameObject o1 = new GameObject();
		GameObject d = new GameObject();

		c.transform.parent = transform;
		c1.transform.parent = transform;
		c2.transform.parent = transform;

		o1.transform.parent = transform;
		d.transform.parent = transform;

		Vector3 vc = MeshUtils3D.BoundSizeBoxCollider (c);
		Vector3 vc1 = MeshUtils3D.BoundSizeBoxCollider (c1);

		c.transform.position = RandomPoint("r");
		c1.transform.position = new Vector3 (c.transform.position.x, c.transform.position.y+vc.y, c.transform.position.z);
		c2.transform.position = new Vector3 (c.transform.position.x, c1.transform.position.y+vc1.y, c.transform.position.z);

		o1.transform.position = RandomPoint("l");
		d.transform.position = RandomPoint("m");

		c.transform.Rotate (0, Random.Range (0, 360), 0);
		c1.transform.Rotate (0, Random.Range (0, 360), 0);
		c2.transform.Rotate (0, Random.Range (0, 360), 0);
		o1.transform.Rotate (0, Random.Range (0, 360), 0);
		d.transform.Rotate (0, Random.Range (0, 360), 0);
	}

	private float rx;

	private Vector3 RandomPoint(string t)
	{
		if(t == "r"){
			rx = Random.Range (-_point3.x / 3, -_point3.x / 4);
		}
		if(t == "l"){
			rx = Random.Range (_point3.x / 3, _point3.x / 4);
		}
		if(t == "m"){
			rx = Random.Range (-_point3.x / 5, _point3.x / 5);
		}

		float rz = Random.Range (-_point3.z / 4, _point3.z / 4);

		Vector3 v = new Vector3 (transform.position.x + rx, _point3.y, transform.position.z + rz);

		return v;
	}
}
