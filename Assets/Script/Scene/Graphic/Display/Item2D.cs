using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Build2D
{
    public class Item2D : ObjectSprite2D
    {
        public Vector2 mousePointLT = new Vector2();
        public Vector2 mousePointLB = new Vector2();
        public Vector2 mousePointRT = new Vector2();
        public Vector2 mousePointRB = new Vector2();

        void Update()
        {
            mousePointLT = localLT;
            mousePointRT = localRT;
            mousePointLB = localLB;
            mousePointRB = localRB;
        }

        public Vector2 localRT
        {
            get { return new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, 0); }
        }

        public Vector2 localLT
        {
            get { return new Vector3(transform.position.x - width / 2, transform.position.y + height / 2, 0); }
        }

        public Vector2 localRB
        {
            get { return new Vector3(transform.position.x + width / 2, transform.position.y - height / 2, 0); }
        }

        public Vector2 localLB
        {
            get { return new Vector3(transform.position.x - width / 2, transform.position.y - height / 2, 0); }
        }

        public Line2D line;
        public Surface2D surface;

        public override AssetVO VO
        {
            set
            {
                base.VO = value;

                this.gameObject.name = "Item2 " + _vo.id;
            }
        }

        override public void UpdateSize()
        {
            if (_textureObject == null) return;

            BoxCollider[] boxs = (VO as ItemVO).model.GetComponentsInChildren<BoxCollider>();
            Vector3 v = Vector3.zero;
            foreach (BoxCollider b in boxs)
            {
                v += b.center;
            }
            v /= boxs.Length;

            if (float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z))
            {
                _textureObjectOffest = Vector3.zero;
            }
            else
            {
                _textureObjectOffest = new Vector3(v.x * 4.79f, v.z * 4.79f, 0);
                UpdatePosition();
            }

            if (width != 0 && height != 0 && (VO as ObjectVO).sizeX != 0 && (VO as ObjectVO).sizeY != 0 && (VO as ObjectVO).sizeZ != 0)
            {
                //_textureObject.transform.localScale = new Vector3(_textureObject.transform.localScale.x / width * ((VO as ObjectVO).sizeX) * VO.GetComponentVO<TransformVO>().scaleX,
                //                                  _textureObject.transform.localScale.y / height * ((VO as ObjectVO).sizeZ) * VO.GetComponentVO<TransformVO>().scaleZ, 1);

                _textureObject.transform.localScale = new Vector3(100 * (((VO as ObjectVO).sizeX) / _texture.width) * VO.GetComponentVO<TransformVO>().scaleX,
                                                  100 * (((VO as ObjectVO).sizeZ) / _texture.height) * VO.GetComponentVO<TransformVO>().scaleZ, 1);
            }
            else
            {
                _textureObject.transform.localScale = new Vector3(.4f, .4f, .4f);
            }
        }
    }
}
