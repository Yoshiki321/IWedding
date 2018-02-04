using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;

namespace Build3D
{
    public class Nested3D : ObjectSprite3D
    {
		override public void Instantiate(ObjectVO vo)
		{
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            base.Instantiate(vo);

            gameObject.AddComponent<MeshRenderer>();
		}

		public override AssetVO VO
		{
			set
			{
                //if (_vo != null && value.Equals(_vo)) return;

                base.VO = value;

				this.gameObject.name = "nested3 " + _vo.id;
			}
		}

        override public float rotationY
        {
            set { transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, value, transform.localRotation.eulerAngles.z); }
            get { return transform.localRotation.eulerAngles.y; }
        }
    }
}
