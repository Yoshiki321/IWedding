using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Build2D;

namespace Build2D
{
    public class Surface2D : BuildSprite
    {
        private CollageData _collageDataFloor3D;
        private CollageData _collageDataCeiling3D;

        public CollageData collageDataCeiling3D
        {
            set { _collageDataCeiling3D = value; }
            get { return _collageDataCeiling3D; }
        }

        public CollageData collageDataFloor3D
        {
            set { _collageDataFloor3D = value; }
            get { return _collageDataFloor3D; }
        }

        public float height3D
        {
            get { return lines[0].height3D; }
        }

        public List<Line2D> lines
        {
            set
            {
                _lines = value;

                foreach (Line2D line in value)
                {
                    line.surface = this;
                }

                InitLine();
            }
            get { return _lines; }
        }

        public List<Vector2> points
        {
            get { return _polygon.points; }
        }

        public List<Vector2> nPoints
        {
            get
            {
                List<Vector2> ps = new List<Vector2>();
                foreach (Line2D l in _lines)
                {
                    List<Vector2> list = l.interiorAlong ? l.alongPointsN : l.inversePointsN;
                    if (!ArrayUtils.HasVector2(ps, list[0]))
                    {
                        ps.Add(list[0]);
                    }
                    if (!ArrayUtils.HasVector2(ps, list[1]))
                    {
                        ps.Add(list[1]);
                    }
                }
                return ps;
            }
        }


        private List<Line2D> _lines = new List<Line2D>();

        private Polygon _polygon;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;

        public void Init(SurfaceVO surfacevo)
        {
            InitSurfaceName();

            _polygon = new Polygon();

            _meshFilter = this.gameObject.AddComponent<MeshFilter>();
            _meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
            _meshCollider = this.gameObject.AddComponent<MeshCollider>();

            _meshRenderer.material.SetFloat("_Glossiness", 0f);
            _meshRenderer.material.SetColor("_Color", ColorUtils.HexToColor("a4a7a7FF"));
            RenderingModeUnits.SetMaterialRenderingMode(_meshRenderer.material, RenderingModeUnits.RenderingMode.Transparent);

            this.gameObject.layer = LayerMask.NameToLayer("Graphic2D");

            VO = surfacevo;

            UpdatePolygon();
            InitLine();
        }

        override protected void DrawVertex()
        {
            MeshData meshData = Triangulator.GetMeshData(_polygon.points, 0, true);
            Mesh mesh = new Mesh();
            mesh.vertices = meshData.vertices;
            mesh.triangles = meshData.triangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            _meshFilter.GetComponent<MeshFilter>().mesh = mesh;
            _meshCollider.sharedMesh = _meshFilter.mesh;
        }

        protected void DrawColor()
        {
            //if (Selected)
            //{
            //    _meshRenderer.material.SetColor("_Color", ColorUtils.HexToColor("e8e8e8ff"));
            //}
            //else
            //{
            //    _meshRenderer.material.SetColor("_Color", ColorUtils.HexToColor("a4a7a7FF"));
            //}
        }

        public string surfaceName = "未命名";

        public SurfaceName2D SurfaceName2D
        {
            get { return _surfaceName2D.GetComponent<SurfaceName2D>(); }
        }

        public void SetSurfaceName(string name)
        {
            if (_surfaceName2D)
            {
                _surfaceName2D.GetComponent<SurfaceName2D>().nameText.text = _surfacevo.surfaceName;
                surfaceName = name;
            }
        }

        private GameObject _surfaceName2D;

        public void InitSurfaceName()
        {
            _surfaceName2D = Instantiate(Resources.Load("UI/SurfaceName/SurfaceName2D", typeof(GameObject)), new Vector3(), new Quaternion()) as GameObject;
            _surfaceName2D.transform.parent = this.transform;
        }

        //public function set surfaceNameSize(value:Number):void
        //{
        //    if(_surfaceName2D){
        //        _surfaceName2D.setSize(value);


        //        refreshSurfaceName();
        //    }
        //}

        private void InitLine()
        {
            int i;
            for (i = 0; i < _surfacevo.linesVO.Count; i++)
            {
                Line2D l = BuilderModel.Instance.GetLineData(_surfacevo.linesVO[i].id).line;
                if (l != null)
                {
                    _lines.Add(l);
                }
                else
                {
                    _surfacevo.linesVO.RemoveAt(i);
                    i--;
                }
            }

            _nodes = new List<Node2D>();
            foreach (Line2D l in lines)
            {
                _nodes.Add(l.fromNode);
                _nodes.Add(l.toNode);
            }

            Line2D line;
            for (i = 0; i < lines.Count; i++)
            {
                line = lines[i];
                if (polygon.inside(lines[i].bisectorAlongCenter))
                {
                    line.interiorAlong = true;
                }
                if (polygon.inside(lines[i].bisectorInverseCenter))
                {
                    line.interiorAlong = false;
                }
                line.surface = this;

                for (int j = 0; j < _nodes.Count; j++)
                {
                    if (line.fromNode != _nodes[j])
                    {
                        if (line.fromNode.point.Equals(_nodes[j].point))
                        {
                            line.fromNode.containNodes = new List<Node2D>();
                            line.fromNode.containNodes.Add(_nodes[j]);
                        }
                    }
                    if (line.toNode != _nodes[j])
                    {
                        if (line.toNode.point.Equals(_nodes[j].point))
                        {
                            line.toNode.containNodes = new List<Node2D>();
                            line.toNode.containNodes.Add(_nodes[j]);
                        }
                    }
                }
            }
        }

        private List<Node2D> _nodes = new List<Node2D>();

        public List<Node2D> nodes
        {
            get { return _nodes; }
        }

        //        private var _color:uint;

        //        private function setColor(color:uint):void
        //        {
        //            _bitmapData = null;
        //            _color = color;



        //            update();
        //        }

        //        private var _triangulate:List<Triangle3D>;
        //        private var _bitmapData:BitmapData;

        //        private function setBitmapData(b:BitmapData, r:Number = 0):void
        //        {
        //            _bitmapData = b;



        //            update();
        //        }

        private List<Vector2> _points;

        public Polygon polygon
        {
            get { return _polygon; }
        }

        public void UpdatePolygon()
        {
            _polygon.points = _surfacevo.points;
            _polygon.triangulate();
        }

        public void UpdateNow()
        {
            _surfacevo.Update();

            UpdatePolygon();

            //-------------------

            DrawVertex();
            DrawColor();

            //if(_surfaceName2D) _surfaceName2D.areaLabel.text = unescape(NumberUtils.getNumFun(PlaneUtils.countAreaForPoint(points, GraphicSprite2D.stage),1)+"m%B2");
            for (int i = 0; i < lines.Count; i++)
            {
                Line2D line = lines[i];
                if (polygon.inside(lines[i].bisectorAlongCenter))
                {
                    line.interiorAlong = true;
                }
                if (polygon.inside(lines[i].bisectorInverseCenter))
                {
                    line.interiorAlong = false;
                }

                //line.transform.parent = transform;
            }

            SetSurfaceName(_surfacevo.surfaceName);
            _surfaceName2D.GetComponent<SurfaceName2D>().areaText.text = "123456";

            RefreshSurfaceName();
        }

        /**
         * 刷新面里的名字 
         * 
         */
        public void RefreshSurfaceName()
        {
            if (_surfaceName2D)
            {
                Vector2 p = CenterPoint;
                _surfaceName2D.GetComponent<RectTransform>().position = new Vector3(p.x, p.y, 0);
                _surfaceName2D.transform.localPosition = new Vector3(p.x, p.y, 0);
            }
        }

        /**
         * 面的中心点 
         * @return 
         * 
         */
        public Vector2 CenterPoint
        {
            get
            {
                if (nodes.Count == 0) return new Vector2();

                List<float> xl = new List<float>();
                List<float> yl = new List<float>();

                for (int i = 0; i < nodes.Count; i++)
                {
                    xl.Add(nodes[i].x);
                    yl.Add(nodes[i].y);
                }

                xl.Sort(delegate (float x, float y)
                {
                    return x.CompareTo(y);
                });
                yl.Sort(delegate (float x, float y)
                {
                    return x.CompareTo(y);
                });

                float xx = xl[0] + (xl[xl.Count - 1] - xl[0]) / 2;
                float yy = yl[0] + (yl[yl.Count - 1] - yl[0]) / 2;

                return new Vector2(xx, yy);
            }
        }

        //        public function updateScale(s:Number):void
        //        {
        //            if(_surfaceName2D){
        //                _surfaceName2D.scaleX = 2-s;
        //                if(_surfaceName2D.scaleX< .7)_surfaceName2D.scaleX = .7;
        //                _surfaceName2D.scaleY = _surfaceName2D.scaleX;


        //                refreshSurfaceName();
        //            }

        //            for each(var l:Line in lines)
        //{
        //    l.updateScale(s)


        //            }
        //        }

        //        public function getRelationLine(line:Line):Array
        //        {
        //            var arr:Array = [];
        //            for each(var l:Line in lines)
        //{
        //    if (l != line)
        //    {
        //        if (l.from.equals(line.from) || l.to.equals(line.from) || l.from.equals(line.to) || l.to.equals(line.from))
        //        {
        //            arr.push(l);
        //        }
        //    }
        //}
        //            return arr;
        //        }

        //        public function getRelationLineToPoint(line:Line, point:Point):Line
        //        {
        //            for each(var l:Line in lines)
        //{
        //    if (l != line)
        //    {
        //        if (l.from.equals(point) || l.to.equals(point))
        //        {
        //            return line;
        //        }
        //    }
        //}
        //            return null;
        //        }

        //        public function getRelationLineForPoint(point:Point):Array
        //        {
        //            var arr:Array = [];
        //            for each(var l:Line in lines)
        //{
        //    if (l.from.equals(point) || l.to.equals(point))
        //    {
        //        arr.push(l);
        //    }
        //}
        //            return arr;
        //        }

        public bool EqualsLine(List<Line2D> line)
        {
            if (line.Count != lines.Count)
            {
                return false;
            }
            int c = 0;
            for (int i = 0; i < line.Count; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (!line[i].Equals(lines[j]))
                    {
                        c++;
                    }
                }
                if (c >= line.Count)
                {
                    return false;
                }
            }
            return true;
        }

        override public void Dispose()
        {
            foreach (Line2D line in lines)
            {
                line.surface = null;
            }

            Destroy(_surfaceName2D);
            Destroy(this.gameObject);
        }


        private SurfaceVO _surfacevo;

        public override AssetVO VO
        {
            set
            {
                base.VO = value;
                _surfacevo = value as SurfaceVO;

                id = _surfacevo.id;

                this.gameObject.name = "Surface2 " + _surfacevo.id;

                UpdateNow();
            }
        }

        override public void UpdateVO()
        {
            //surfaceVO.surfaceName = _surfaceName2D.GetComponent<SurfaceName2D>().nameText.text;

            foreach (Line2D line in lines)
            {
                line.UpdateVO();
            }

            UpdateNow();
        }
    }
}
