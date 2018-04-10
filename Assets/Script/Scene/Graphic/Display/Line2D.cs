using BuildManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Build2D
{
    public class Line2D : BuildSprite
    {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;

        public void Init(LineVO linevo)
        {
            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
            _meshCollider = gameObject.AddComponent<MeshCollider>();

            _meshRenderer.material = new Material(Shader.Find("Standard"));

            path = new Path2D();

            gameObject.layer = LayerMask.NameToLayer("Graphic2D");

            InitScale();
            VO = linevo;
        }

        public Path2D path { get; private set; }

        public Vector2 from
        {
            set
            {
                path.from = value.Round();

                fromNode.x = value.x;
                fromNode.y = value.y;
            }
            get { return path.from; }
        }

        public Vector2 to
        {
            set
            {
                path.to = value.Round();

                toNode.x = value.x;
                toNode.y = value.y;
            }
            get { return path.to; }
        }

        public Vector2 interiorFrom
        {
            get
            {
                if (interiorAlong)
                {
                    return _alongFrom;
                }

                return _inverseFrom;
            }
        }

        public Vector2 interiorTo
        {
            get
            {
                if (interiorAlong)
                {
                    return _alongTo;
                }

                return _inverseTo;
            }
        }

        public Bisector interiorBisector
        {
            get
            {
                if (interiorAlong)
                {
                    return bisectorAlong;
                }

                return bisectorInverse;
            }
        }

        public float thickness
        {
            get { return path.thickness; }
        }

        public Bisector bisector
        {
            get { return path.bisector; }
        }

        public Bisector bisectorAlong
        {
            get { return path.bisectorAlong; }
        }

        public Bisector bisectorInverse
        {
            get { return path.bisectorInverse; }
        }

        public Bisector bisectorAlongN
        {
            get { return path.bisectorAlongN; }
        }

        public Bisector bisectorInverseN
        {
            get { return path.bisectorInverseN; }
        }

        public Bisector bisectorAlongO
        {
            get { return path.bisectorAlongO; }
        }

        public Bisector bisectorInverseO
        {
            get { return path.bisectorInverseO; }
        }

        public Vector2 bisectorAlongCenter
        {
            get { return path.bisectorAlong.center; }
        }

        public Vector2 bisectorInverseCenter
        {
            get { return path.bisectorInverse.center; }
        }

        public Node2D fromNode { set; get; }

        public Node2D toNode { set; get; }

        public float height3D { set; get; } = 1;

        public Vector2 Other(Vector2 p, bool same = true)
        {
            return bisector.Other(p, same);
        }

        /// <summary>
        /// 在线的顺时针的点是否在当前线的面里
        /// </summary>
        public bool interiorAlong;

        public Bisector interiorBisectorN
        {
            get
            {
                if (interiorAlong)
                    return bisectorAlongN;
                else
                    return bisectorInverseN;
            }
        }

        public Bisector ExteriorBisector
        {
            get
            {
                if (interiorAlong)
                    return bisectorInverse;
                else
                    return bisectorAlong;
            }
        }

        public Surface2D surface;
        public CollageData surfaceCollageData;

        private Vector2 _alongFrom;
        private Vector2 _inverseFrom;
        private Vector2 _alongTo;
        private Vector2 _inverseTo;
        private Vector2 _alongFromN;
        private Vector2 _inverseFromN;
        private Vector2 _alongToN;
        private Vector2 _inverseToN;
        private Vector2 _alongFromO;
        private Vector2 _inverseFromO;
        private Vector2 _alongToO;
        private Vector2 _inverseToO;

        //private Color lineColor;

        override protected void DrawVertex()
        {
            var vh = new VertexHelper();
            vh.AddVert(new Vector3(_alongFrom.x, _alongFrom.y), Color.red, new Vector2(0f, 0f));
            vh.AddVert(new Vector3(_inverseFrom.x, _inverseFrom.y), Color.red, new Vector2(0f, 1f));
            vh.AddVert(new Vector3(_inverseTo.x, _inverseTo.y), Color.red, new Vector2(1f, 1f));
            vh.AddVert(new Vector3(_alongTo.x, _alongTo.y), Color.red, new Vector2(1f, 0f));

            vh.AddTriangle(2, 1, 0);
            vh.AddTriangle(3, 2, 0);
            vh.FillMesh(_meshFilter.GetComponent<MeshFilter>().mesh);

            _meshCollider.sharedMesh = _meshFilter.mesh;
        }

        protected void DrawColor()
        {
            if (SelectedOver)
            {
                _meshRenderer.material.SetColor("_Color", ColorUtils.HexToColor("939393FF"));
            }
            else
            {
                _meshRenderer.material.SetColor("_Color", ColorUtils.HexToColor("606060FF"));
            }
        }

        #region Scale

        GameObject scaleObject;

        void InitScale()
        {
            scaleObject = new GameObject();
            scaleObject.transform.parent = transform;
            scaleObject.name = "ScaleObject";
            scaleObject.layer = gameObject.layer;

            lineRenderer1 = CreateLineRenderer(new GameObject());
            lineRenderer2 = CreateLineRenderer(new GameObject());
            lineRenderer3 = CreateLineRenderer(new GameObject());
            lineRenderer4 = CreateLineRenderer(new GameObject());

            GameObject obj = GameObject.Instantiate(Resources.Load("UI/Text",
                typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
            obj.name = "ScaleText";
            obj.transform.parent = scaleObject.transform;
            obj.layer = scaleObject.layer;
            obj.GetComponent<Canvas>().worldCamera = SceneManager.Instance.Camera2D;
            scaleText = obj.GetComponentInChildren<Text>();
            scaleText.gameObject.layer = scaleObject.layer;
            scaleTextRect = scaleText.gameObject.GetComponent<RectTransform>();

            scaleObject.transform.position = new Vector3(scaleObject.transform.position.x,
               scaleObject.transform.position.y,
               scaleObject.transform.position.z - 0.01f);
        }

        Text scaleText;
        RectTransform scaleTextRect;
        LineRenderer lineRenderer1;
        LineRenderer lineRenderer2;
        LineRenderer lineRenderer3;
        LineRenderer lineRenderer4;

        LineRenderer CreateLineRenderer(GameObject line)
        {
            line.transform.parent = scaleObject.transform;
            line.name = "LineRenderer";
            line.layer = gameObject.layer;
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.widthMultiplier = 0.05f;
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
            lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
            lineRenderer.material.color = Color.black;
            return lineRenderer;
        }

        public float angle
        {
            get { return PlaneUtils.Angle(_alongFrom, _alongTo); }
        }

        void UpdateScale()
        {
            if (!scaleText) return;

            float distance;
            Vector2 center;
            Vector2 p;
            Vector2 p1;
            Vector2 p2;

            if (interiorAlong)
            {
                distance = Vector2.Distance(_alongFrom, _alongTo);

                Vector2 tp = Vector2.Lerp(_alongFrom, _alongTo, .5f);
                Vector2 tp0 = Vector2.Lerp(_alongFromO, _alongToO, .5f);
                Vector2 tp1 = PlaneUtils.AngleDistanceGetPoint(tp, angle + 90, 1);
                Vector2 tp2 = PlaneUtils.AngleDistanceGetPoint(tp, angle - 90, 1);
                float d1 = Vector2.Distance(tp1, tp0);
                float d2 = Vector2.Distance(tp2, tp0);
                if (d1 < d2) center = tp1; else center = tp2;

                center = PlaneUtils.AngleDistanceGetPoint(center, PlaneUtils.Angle(Vector2.Lerp(_alongFrom, _alongTo, .5f), center), -.5f);
                p = PlaneUtils.AngleDistanceGetPoint(center, angle, distance / 2);
                p1 = PlaneUtils.AngleDistanceGetPoint(center, angle - 180, distance / 2);

                Vector2 tpi = Vector2.Lerp(_inverseFrom, _inverseTo, .5f);
                Vector2 tpi0 = Vector2.Lerp(_inverseFromO, _inverseToO, .5f);
                Vector2 tpi1 = PlaneUtils.AngleDistanceGetPoint(tpi, angle + 90, .8f);
                Vector2 tpi2 = PlaneUtils.AngleDistanceGetPoint(tpi, angle - 90, .8f);
                float di1 = Vector2.Distance(tpi1, tpi0);
                float di2 = Vector2.Distance(tpi2, tpi0);
                if (di1 < di2) p2 = tpi1; else p2 = tpi2;
            }
            else
            {
                distance = Vector2.Distance(_inverseFrom, _inverseTo);

                Vector2 tp = Vector2.Lerp(_inverseFrom, _inverseTo, .5f);
                Vector2 tp0 = Vector2.Lerp(_inverseFromO, _inverseToO, .5f);
                Vector2 tp1 = PlaneUtils.AngleDistanceGetPoint(tp, angle + 90, 1);
                Vector2 tp2 = PlaneUtils.AngleDistanceGetPoint(tp, angle - 90, 1);
                float d1 = Vector2.Distance(tp1, tp0);
                float d2 = Vector2.Distance(tp2, tp0);
                if (d1 < d2) center = tp1; else center = tp2;

                center = PlaneUtils.AngleDistanceGetPoint(center, PlaneUtils.Angle(Vector2.Lerp(_inverseFrom, _inverseTo, .5f), center), -.5f);
                p = PlaneUtils.AngleDistanceGetPoint(center, angle, distance / 2);
                p1 = PlaneUtils.AngleDistanceGetPoint(center, angle - 180, distance / 2);

                Vector2 tpa = Vector2.Lerp(_alongFrom, _alongTo, .5f);
                Vector2 tpa0 = Vector2.Lerp(_alongFromO, _alongToO, .5f);
                Vector2 tpa1 = PlaneUtils.AngleDistanceGetPoint(tpa, angle + 90, .8f);
                Vector2 tpa2 = PlaneUtils.AngleDistanceGetPoint(tpa, angle - 90, .8f);
                float da1 = Vector2.Distance(tpa1, tpa0);
                float da2 = Vector2.Distance(tpa2, tpa0);
                if (da1 < da2) p2 = tpa1; else p2 = tpa2;
            }

            scaleText.transform.position = new Vector3(center.x, center.y, -.1f);
            scaleText.transform.localRotation = Quaternion.Euler(0, 0, angle);
            interiorLength = (float)Math.Round(distance, 2);
            scaleText.text = interiorLength.ToString() + "m";

            float width = scaleTextRect.rect.width == 0 ? 19 : scaleTextRect.rect.width;

            lineRenderer1.SetPosition(0, PlaneUtils.AngleDistanceGetPoint(center, angle, width / 12 / 2));
            lineRenderer1.SetPosition(1, p);

            lineRenderer2.SetPosition(0, PlaneUtils.AngleDistanceGetPoint(center, angle - 180, width / 12 / 2));
            lineRenderer2.SetPosition(1, p1);

            lineRenderer3.SetPosition(0, PlaneUtils.AngleDistanceGetPoint(p, angle + 90, .1f));
            lineRenderer3.SetPosition(1, PlaneUtils.AngleDistanceGetPoint(p, angle - 90, .1f));

            lineRenderer4.SetPosition(0, PlaneUtils.AngleDistanceGetPoint(p1, angle + 90, .1f));
            lineRenderer4.SetPosition(1, PlaneUtils.AngleDistanceGetPoint(p1, angle - 90, .1f));
        }

        private float _interiorLength;

        public float interiorLength
        {
            set { _interiorLength = value; }
            get { return _interiorLength; }
        }

        #endregion

        void Update()
        {
            if (MouseManager.overGameObject == this.gameObject)
            {
                SelectedOver = true;
            }
            else
            {
                SelectedOver = false;
            }
        }

        private bool _selectedOver;

        public virtual bool SelectedOver
        {
            set
            {
                if (_selectedOver == value) return;
                _selectedOver = value;

                DrawColor();
            }
            get { return _selectedOver; }
        }

        private bool _selected;

        public virtual bool Selected
        {
            set
            {
                if (_selected == value) return;
                _selected = value;
            }
            get { return _selected; }
        }

        public void UpdateNow()
        {
            if (_isDispose) return;

            List<Bisector> t = GetLineExtend(GetNearAlongLine("from"), GetNearAlongLine("to"));
            Vector2[] points = CountIntersection(t[0], t[1], t[2], t[3], bisectorAlong, bisectorInverse);

            _alongFrom = points[0];
            _inverseFrom = points[1];
            _alongTo = points[2];
            _inverseTo = points[3];

            t = GetLineExtendN(GetNearAlongLine("from"), GetNearAlongLine("to"));
            points = CountIntersection(t[0], t[1], t[2], t[3], bisectorAlongN, bisectorInverseN);

            _alongFromN = points[0];
            _inverseFromN = points[1];
            _alongToN = points[2];
            _inverseToN = points[3];

            bisectorAlongN.from = points[0];
            bisectorInverseN.from = points[1];
            bisectorAlongN.to = points[2];
            bisectorInverseN.to = points[3];

            DrawVertex();
            DrawColor();

            t = GetLineExtendO(GetNearAlongLine("from"), GetNearAlongLine("to"));
            points = CountIntersection(t[0], t[1], t[2], t[3], bisectorAlongO, bisectorInverseO);

            _alongFromO = points[0];
            _inverseFromO = points[1];
            _alongToO = points[2];
            _inverseToO = points[3];

            UpdateScale();
        }

        private Vector2[] CountIntersection(Bisector fba, Bisector fbi, Bisector tba, Bisector tbi, Bisector ab, Bisector ib)
        {
            if (fbi != null && ab.IncludedAngle(fbi) < 10) fbi = null;
            if (fba != null && ib.IncludedAngle(fba) < 10) fba = null;
            if (tbi != null && ab.IncludedAngle(tbi) < 10) tbi = null;
            if (tba != null && ib.IncludedAngle(tba) < 10) tba = null;

            Vector2 alongFrom = ab.Intersection(fbi);
            Vector2 inverseFrom = ib.Intersection(fba);
            Vector2 alongTo = ab.Intersection(tbi);
            Vector2 inverseTo = ib.Intersection(tba);

            if (float.IsNaN(alongFrom.x) || alongFrom.x == float.PositiveInfinity || float.IsNaN(alongFrom.y) || alongFrom.y == float.PositiveInfinity)
                alongFrom = ab.Intersection(bisector.verticalFrom);
            if (float.IsNaN(inverseFrom.x) || inverseFrom.x == float.PositiveInfinity || float.IsNaN(inverseFrom.y) || inverseFrom.y == float.PositiveInfinity)
                inverseFrom = ib.Intersection(bisector.verticalFrom);
            if (float.IsNaN(alongTo.x) || alongTo.x == float.PositiveInfinity || float.IsNaN(alongTo.y) || alongTo.y == float.PositiveInfinity)
                alongTo = ab.Intersection(bisector.verticalTo);
            if (float.IsNaN(inverseTo.x) || inverseTo.x == float.PositiveInfinity || float.IsNaN(inverseTo.y) || inverseTo.y == float.PositiveInfinity)
                inverseTo = ib.Intersection(bisector.verticalTo);

            ab.from = alongFrom;
            ab.to = alongTo;
            ib.from = inverseFrom;
            ib.to = inverseTo;

            //          trace(fromUp,fromDown,toUp,toDown)
            return new Vector2[4] { alongFrom, inverseFrom, alongTo, inverseTo };
        }

        /// <summary>
        /// 检索线的一个端点的所有的线，找出顺时针和逆时针的最靠近的第一条线 
        /// </summary>
        /// <param name="type">端点(from,to)</param>
        /// <returns>{line:找到的线段,angle:找到的线段角度,same:找到的线段起始点或结束点是否与搜索线段对应}</returns>
        private List<LineObject> GetNearAlongLine(string type)
        {
            Line2D targetLine;
            float angle;
            float targetAngle = 0;
            List<LineObject> list = new List<LineObject>();
            Node2D node;
            bool same = false;

            if (type == "from")
            {
                node = fromNode;
                angle = PlaneUtils.Angle(path.from, path.to, true);
            }
            else
            {
                node = toNode;
                angle = PlaneUtils.Angle(path.to, path.from, true);
            }

            for (int i = 0; i < node.containNodes.Count; i++)
            {
                if (node.containNodes[i].line == this) continue;

                targetLine = node.containNodes[i].line;

                Node2D node2f;
                Node2D node2t;
                Node2D node2f1;
                Vector2 vf;
                Vector2 vt;

                if (type == "from")
                {
                    node2f = this.fromNode;
                    node2t = targetLine.toNode;
                    node2f1 = targetLine.fromNode;

                    vf = targetLine.path.to;
                    vt = targetLine.path.from;
                }
                else
                {
                    node2f = toNode;
                    node2t = targetLine.fromNode;
                    node2f1 = targetLine.toNode;

                    vf = targetLine.path.from;
                    vt = targetLine.path.to;
                }

                if (Has(node2f.containNodes, node2t))
                {
                    targetAngle = PlaneUtils.Angle(vf, vt, true);
                    same = false;
                }
                if (Has(node2f.containNodes, node2f1))
                {
                    targetAngle = PlaneUtils.Angle(vf, vt, true);
                    same = true;
                }

                if (targetAngle < angle) targetAngle += 360;

                if ((targetLine.from.Equals(this.from) && !targetLine.to.Equals(this.to)) ||
                    (targetLine.from.Equals(this.to) && !targetLine.to.Equals(this.from)) ||
                    (targetLine.to.Equals(this.from) && !targetLine.from.Equals(this.to)) ||
                    (targetLine.to.Equals(this.to) && !targetLine.from.Equals(this.from)))
                {
                    LineObject ld = new LineObject();
                    ld.line = targetLine;
                    ld.angle = targetAngle;
                    ld.same = same;
                    list.Add(ld);
                }
            }

            SortOn(list);
            if (type == "from") list.Reverse();
            return list;
        }

        private List<Bisector> GetLineExtend(List<LineObject> fromList, List<LineObject> toList)
        {
            Bisector fba = null, fbi = null, tba = null, tbi = null;
            if (fromList.Count > 0)
            {
                fba = fromList[0].same ? fromList[0].line.bisectorAlong : fromList[0].line.bisectorInverse;
                fbi = fromList[fromList.Count - 1].same ? fromList[fromList.Count - 1].line.bisectorInverse : fromList[fromList.Count - 1].line.bisectorAlong;
            }
            if (toList.Count > 0)
            {
                tba = toList[0].same ? toList[0].line.bisectorAlong : toList[0].line.bisectorInverse;
                tbi = toList[toList.Count - 1].same ? toList[toList.Count - 1].line.bisectorInverse : toList[toList.Count - 1].line.bisectorAlong;
            }
            return new List<Bisector>() { fba, fbi, tba, tbi };
        }

        private List<Bisector> GetLineExtendN(List<LineObject> fromList, List<LineObject> toList)
        {
            Bisector fba = null, fbi = null, tba = null, tbi = null;
            if (fromList.Count > 0)
            {
                fba = fromList[0].same ? fromList[0].line.bisectorAlongN : fromList[0].line.bisectorInverseN;
                fbi = fromList[fromList.Count - 1].same ? fromList[fromList.Count - 1].line.bisectorInverseN : fromList[fromList.Count - 1].line.bisectorAlongN;
            }
            if (toList.Count > 0)
            {
                tba = toList[0].same ? toList[0].line.bisectorAlongN : toList[0].line.bisectorInverseN;
                tbi = toList[toList.Count - 1].same ? toList[toList.Count - 1].line.bisectorInverseN : toList[toList.Count - 1].line.bisectorAlongN;
            }
            return new List<Bisector>() { fba, fbi, tba, tbi };
        }

        private List<Bisector> GetLineExtendO(List<LineObject> fromList, List<LineObject> toList)
        {
            Bisector fba = null, fbi = null, tba = null, tbi = null;
            if (fromList.Count > 0)
            {
                fba = fromList[0].same ? fromList[0].line.bisectorAlongO : fromList[0].line.bisectorInverseO;
                fbi = fromList[fromList.Count - 1].same ? fromList[fromList.Count - 1].line.bisectorInverseO : fromList[fromList.Count - 1].line.bisectorAlongO;
            }
            if (toList.Count > 0)
            {
                tba = toList[0].same ? toList[0].line.bisectorAlongO : toList[0].line.bisectorInverseO;
                tbi = toList[toList.Count - 1].same ? toList[toList.Count - 1].line.bisectorInverseO : toList[toList.Count - 1].line.bisectorAlongO;
            }
            return new List<Bisector>() { fba, fbi, tba, tbi };
        }

        public List<Vector2> alongPointsN
        {
            get { return new List<Vector2>() { _alongFromN, _alongToN }; }
        }

        public List<Vector2> alongPoints
        {
            get { return new List<Vector2>() { _alongFrom, _alongTo }; }
        }

        public List<Vector2> inversePointsN
        {
            get { return new List<Vector2>() { _inverseFromN, _inverseToN }; }
        }

        public List<Vector2> inversePoints
        {
            get { return new List<Vector2>() { _inverseFrom, _inverseTo }; }
        }

        public List<Vector2> alongPointsO
        {
            get { return new List<Vector2>() { _alongFromO, _alongToO }; }
        }

        public List<Vector2> inversePointsO
        {
            get { return new List<Vector2>() { _inverseFromO, _inverseToO }; }
        }

        public bool Equals(Line2D line)
        {
            if (line.from.Equals(from) && line.to.Equals(to))
            {
                return true;
            }
            return false;
        }

        private void SortOn(List<LineObject> arr)
        {
            LineObject ch;
            for (var i = 0; i < arr.Count - 1; i++)
            {
                Type type = arr[i].GetType();
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty("angle");
                float t = (float)propertyInfo.GetValue(arr[i], null);

                Type type1 = arr[i + 1].GetType();
                System.Reflection.PropertyInfo propertyInfo1 = type1.GetProperty("angle");
                float t1 = (float)propertyInfo1.GetValue(arr[i + 1], null);

                if (t > t1)
                {
                    ch = arr[i + 1];
                    arr[i + 1] = arr[i];
                    arr[i] = ch;
                    i = -1;
                }
            }
        }

        public bool Has(List<Node2D> array, Node2D o)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] == o)
                {
                    return true;
                }
            }
            return false;
        }


        #region Hole

        private List<HoleVO> _hds = new List<HoleVO>();

        //      public function set holeDatas(hds:Vector.<HoleVO>):void
        //      {
        //          _hds = hds;
        //      }

        public List<HoleVO> holeDatas
        {
            get { return _hds; }
        }

        public void AddHole(HoleVO holeData)
        {
            _hds.Add(holeData);

            UpdateHoleVO();
        }

        public void RemoveHole(HoleVO holeData)
        {
            for (int i = 0; i < _hds.Count; i++)
            {
                if (_hds[i] == holeData)
                {
                    _hds.RemoveAt(i);

                    UpdateHoleVO();
                    return;
                }
            }
        }

        #endregion

        /**
         * 分割线 
         * @param p
         * @return 
         * 
         */
        //        public List<LineData> interpolate(Vector2 p):
        //        {
        //            LineData lineData = new LineData(from, p, thickness);
        //lineData.alongCollage = lineVO.alongCollage;
        //            lineData.inverseCollage = lineVO.inverseCollage;

        //            LineData lineData1 = new LineData(p, to, thickness);
        //lineData1.alongCollage = lineVO.alongCollage;
        //    lineData1.inverseCollage = lineVO.inverseCollage;

        //    return new List<LineData>(){lineData, lineData1};
        //}

        //public function halve():Array
        //{
        //    return interpolate(bisector.center);
        //}

        //public static function sameLine(line:Line, line1:Line):Boolean
        //{
        //    if((line.from.equals(line1.from) && line.to.equals(line1.to)) ||
        //        (line.to.equals(line1.from) && line.from.equals(line1.to))){
        //        if(line1.surface){
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        override public void UpdateVO()
        {
            UpdateNow();

            _lineVO.interiorAlong = interiorAlong;

            _lineVO.from = from;
            _lineVO.to = to;

            if (interiorAlong)
            {
                _lineVO.afrom = _alongFromN;
                _lineVO.ato = _alongToN;
                _lineVO.ifrom = _inverseFrom;
                _lineVO.ito = _inverseTo;
            }
            else
            {
                _lineVO.afrom = _alongFrom;
                _lineVO.ato = _alongTo;
                _lineVO.ifrom = _inverseFromN;
                _lineVO.ito = _inverseToN;
            }

            UpdateHoleVO();
        }

        private void UpdateHoleVO()
        {
            _lineVO.holes = new List<HoleVO>();

            for (int i = 0; i < holeDatas.Count; i++)
            {
                _lineVO.holes.Add(holeDatas[i].Clone());
            }
        }

        private LineVO _lineVO;

        public override AssetVO VO
        {
            set
            {
                base.VO = value;
                _lineVO = value as LineVO ;

                id = value.id;
                gameObject.name = "Line2 " + _lineVO.id;

                path.setPoint(_lineVO.from, _lineVO.to, _lineVO.thickness);

                height3D = _lineVO.height;
                interiorAlong = _lineVO.interiorAlong;

                fromNode.point = _lineVO.from;
                toNode.point = _lineVO.to;

                _hds = new List<HoleVO>();
                for (int i = 0; i < _lineVO.holes.Count; i++)
                {
                    holeDatas.Add(_lineVO.holes[i].Clone());
                };
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                enabled = value;
            }
        }
    }

    class LineObject
    {
        public Line2D line;
        public float angle;
        public bool same;
    }
}
