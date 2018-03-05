using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;

namespace Build2D
{
    public class Nested2D : ObjectSprite2D
    {
        public string lineId;

        public float length;

        private List<string> _lines = new List<string>();

        override public void Start()
        {
            base.Start();
            gameObject.transform.localScale = new Vector3(1, 1, 0);
        }

        void Update()
        {

        }

        public List<string> lines
        {
            set { _lines = value; }
            get { return _lines; }
        }

        protected float _width = 1;
        protected float _widthZ = 1;
        protected float _height = 1;

        public float width
        {
            set
            {
                _width = value;
                UpdateNow();
            }
            get { return _width; }
        }

        public float widthZ
        {
            set { _widthZ = value; }
            get { return _widthZ; }
        }

        public float height
        {
            set
            {
                _height = value;
                UpdateNow();
            }
            get { return _height; }
        }

        private float _height3D;

        public float height3D
        {
            set { _height3D = value; }
            get { return _height3D; }
        }

        private float _y3d;

        public float y3d
        {
            set { _y3d = value; }
            get { return _y3d; }
        }

        public void UpdateNow()
        {

        }

        public override AssetVO VO
        {
            set
            {
                base.VO = value;

                NestedVO vo = value as NestedVO;
                lines = new List<string>();
                foreach (string id in vo.lines)
                {
                    lines.Add(id);
                }

                lineId = vo.lineId;

                this.gameObject.name = "Nested2 " + VO.id;
            }
        }

        override public void UpdateVO()
        {
            base.UpdateVO();

            NestedVO nvo = VO as NestedVO;
            nvo.lines = new List<string>();
            foreach (string id in this.lines)
            {
                nvo.lines.Add(id);
            }

            nvo.lineId = lineId;
        }
    }
}
