using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWall3D : MonoBehaviour
{
	private GameObject _item;

	public void Create(WallPlaneVO vo)
	{
		//_item = new GameObject();

		//Vector2 vc = (VO.from + VO.to) / 2;

		//_item.transform.parent = VO.parent;
		//_item.transform.position = new Vector3 (vc.x, 0, vc.y);

		//float _a3 = PlaneUtils.Angle(VO.from, VO.to);
		//if (_a3 == 0)
		//{
		//	_a3 = 180;
		//}
		//else if (_a3 == 180)
		//{
		//	_a3 = 0;
		//}

		//_item.transform.rotation = Quaternion.Euler (_item.transform.eulerAngles.x, _a3, _item.transform.eulerAngles.z);

		//float _d = Vector3.Distance (VO.from, VO.to);

		//MeshFilter rm = _item.transform.Find ("Right").gameObject.GetComponent<MeshFilter> ();
		//MeshFilter lm = _item.transform.Find ("Left").gameObject.GetComponent<MeshFilter> ();
		//MeshFilter mm = _item.transform.Find ("Middle").gameObject.GetComponent<MeshFilter> ();

		//float sx = (rm.mesh.bounds.max.y-rm.mesh.bounds.min.y) +
		//	(lm.mesh.bounds.max.y- lm.mesh.bounds.min.y) + 
		//	(mm.mesh.bounds.max.y- mm.mesh.bounds.min.y);
		//float sy = (mm.mesh.bounds.max.z- mm.mesh.bounds.min.z);

		//_item.transform.localScale = new Vector3 (_d / sx, VO.height / sy, 1);
	}
}
