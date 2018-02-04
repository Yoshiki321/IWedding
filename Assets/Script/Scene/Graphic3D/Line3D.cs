using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using BuildManager;

namespace Build3D
{
    public class Line3D : ObjectSprite
    {
        public void Instantiate(LineVO linevo)
        {
            _ag = new GameObject();
            _ig = new GameObject();
            _fpg = new GameObject();
            _tpg = new GameObject();
            _fpg1 = new GameObject();
            _tpg1 = new GameObject();
            _topp = new GameObject();
            _mask = new GameObject();
            _mask1 = new GameObject();

            _ag.transform.parent = gameObject.transform;
            _ig.transform.parent = gameObject.transform;
            _fpg.transform.parent = gameObject.transform;
            _tpg.transform.parent = gameObject.transform;
            _fpg1.transform.parent = gameObject.transform;
            _tpg1.transform.parent = gameObject.transform;
            _topp.transform.parent = gameObject.transform;
            _mask.transform.parent = gameObject.transform;
            _mask1.transform.parent = gameObject.transform;

            _ag.name = "along";
            _ig.name = "inverse";
            _fpg.name = "from";
            _tpg.name = "to";
            _fpg1.name = "from1";
            _tpg1.name = "to1";
            _topp.name = "top";
            _mask.name = "mask";
            _mask1.name = "mask1";

            _ag.AddComponent<WallController>();
            _ig.AddComponent<WallController>();

            _alongPlane = _ag.AddComponent<WallPlane3D>();
            _inversePlane = _ig.AddComponent<WallPlane3D>();
            _fromPlane = _fpg.AddComponent<WallPlane3D>();
            _fromPlane1 = _fpg1.AddComponent<WallPlane3D>();
            _toPlane = _tpg.AddComponent<WallPlane3D>();
            _toPlane1 = _tpg1.AddComponent<WallPlane3D>();
            _topPlane = _topp.AddComponent<SurfacePlane3D>();
            _maskPlane = _mask.AddComponent<WallPlane3D>();
            _maskPlane1 = _mask1.AddComponent<WallPlane3D>();

            _ag.AddComponent<TubeLightShadowPlane>();
            _ig.AddComponent<TubeLightShadowPlane>();

            this.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _ag.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _ig.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _fpg.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _tpg.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _fpg1.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _tpg1.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _topp.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _mask.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _mask1.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");

            _collageVO = linevo.GetComponentVO<CollageVO>();
            if (_collageVO == null)
            {
                _collageVO = linevo.AddComponentVO<CollageVO>();
                _collageStruct = _collageVO.SetCollage("墙面", "Collage0", "0001");
            }
            else
            {
                _collageStruct = _collageVO.collages[0];
            }

            //CeilingLine3D cl = new CeilingLine3D();
            //ceilingLine3D = cl;
            //cl.Init(linevo);

            transparent = true;

            VO = linevo;
        }

        private CollageStruct _collageStruct;

        private CollageVO _collageVO;

        public GameObject AObject
        {
            get { return _ag; }
        }

        public GameObject IObject
        {
            get { return _ig; }
        }

        public WallPlane3D AlongPlane
        {
            get { return _alongPlane; }
        }

        public WallPlane3D InversePlane
        {
            get { return _inversePlane; }
        }

        //public CeilingLine3D ceilingLine3D;

        private List<Penetrate> _aholes;
        private List<Penetrate> _iholes;

        private float _height;
        private float _aw, _iw;
        private float _fw1, _tw1, _fw2, _tw2;
        private float _rotation;
        private float _fr1, _fr2;
        private float _tr1, _tr2;

        private Vector2 _afp, _atp, _ifp, _itp;
        private Vector2 _fp, _tp, _ap, _ip;
        private Vector2 _fp1, _fp2;
        private Vector2 _tp1, _tp2;

        private string _inside;

        private MeshData _meshData;

        private LineVO _lineVO;

        public override AssetVO VO
        {
            set
            {
                //if (_lineVO != null && value.Equals(_lineVO)) return;
                base.VO = value;
                _lineVO = value as LineVO;

                _aholes = new List<Penetrate>();
                _iholes = new List<Penetrate>();
                List<HoleVO> holes = _lineVO.holes;

                for (int i = 0; i < holes.Count; i++)
                {
                    HoleVO holeData = holes[i];

                    Penetrate aPenetrate = new Penetrate();
                    aPenetrate.width = holeData.holeWidth;
                    aPenetrate.height = holeData.holeHeight;
                    aPenetrate.x = holeData.holeAlongX3;
                    aPenetrate.y = holeData.holeY;
                    aPenetrate.type = holeData.holeType;
                    _aholes.Add(aPenetrate);

                    Penetrate iPenetrate = new Penetrate();
                    iPenetrate.width = holeData.holeWidth;
                    iPenetrate.height = holeData.holeHeight;
                    iPenetrate.x = holeData.holeInverseX3;
                    iPenetrate.y = holeData.holeY;
                    iPenetrate.type = holeData.holeType;
                    _iholes.Add(iPenetrate);
                }

                id = _lineVO.id;

                _fp = _lineVO.from;
                _tp = _lineVO.to;
                _afp = _lineVO.afrom;
                _atp = _lineVO.ato;
                _ifp = _lineVO.ifrom;
                _itp = _lineVO.ito;
                _height = _lineVO.height;

                _aw = Vector2.Distance(_afp, _atp);
                _iw = Vector2.Distance(_ifp, _itp);

                _fw1 = Vector2.Distance(_afp, _fp);
                _fw2 = Vector2.Distance(_ifp, _fp);
                _tw1 = Vector2.Distance(_atp, _tp);
                _tw2 = Vector2.Distance(_itp, _tp);

                _ap = Vector2.Lerp(_afp, _atp, .5f);
                _ip = Vector2.Lerp(_ifp, _itp, .5f);
                _fp1 = Vector2.Lerp(_afp, _fp, .5f);
                _fp2 = Vector2.Lerp(_ifp, _fp, .5f);
                _tp1 = Vector2.Lerp(_atp, _tp, .5f);
                _tp2 = Vector2.Lerp(_itp, _tp, .5f);

                _fr1 = PlaneUtils.Angle(_afp, _fp);
                _fr2 = PlaneUtils.Angle(_fp, _ifp);
                _tr1 = PlaneUtils.Angle(_tp, _atp);
                _tr2 = PlaneUtils.Angle(_itp, _tp);

                List<Vector2> p = new List<Vector2>();
                p.Add(_afp);
                p.Add(_atp);
                p.Add(_tp);
                p.Add(_itp);
                p.Add(_ifp);
                p.Add(_fp);
                
                _meshData = Triangulator.GetMeshData(p);

                _rotation = PlaneUtils.Angle(_afp, _atp);

                //          _rotation = Vector3D.angleBetween(new Vector3D(_afp.x,0,_afp.y),new Vector3D(_atp.x,0,_atp.y));

                gameObject.name = "line3 " + _lineVO.id;

                //if (Vector2.distance(_fp, _tp) == 0)
                //{
                //    visible = false;
                //    return;
                //}
                //else
                //{
                //    visible = true;
                //}

                UpdateNow();

                string t = _lineVO.interiorAlong ? "a" : "i";
                if (_inside != t)
                {
                    _inside = t;
                }

                _collageVO = value.GetComponentVO<CollageVO>();
                _collageStruct = _collageVO.GetCollageByUrl("Collage0");
                UpdateCollage();

                //ceilingLine3D.lineVO = _lineVO;
            }

            get { return _lineVO; }
        }

        public WallPlane3D insidePlane
        {
            get
            {
                if (_inside == "a")
                {
                    return _alongPlane;
                }
                else
                {
                    return _inversePlane;
                }
            }
        }

        public object ScenneManager { get; private set; }

        override public void Dispose()
        {
            //ceilingLine3D.Dispose();
            base.Dispose();
        }

        public void UpdateNow()
        {
            Draw();
        }

        private void Update()
        {
            if (transparent)
            {
                float angle = PlaneUtils.Angle(_fp, _tp);
                Vector2 cp = Vector2.Lerp(_fp, _tp, .5f);
                Vector2 cp1 = PlaneUtils.AngleDistanceGetPoint(cp, angle + 90, 5);
                Vector2 cp2 = PlaneUtils.AngleDistanceGetPoint(cp, angle - 90, 5);
                Vector2 cpa = new Vector2();
                Vector2 cpi = new Vector2();
                if (Vector2.Distance(cp1, _ap) < Vector2.Distance(cp2, _ap))
                    cpa = cp1;
                else
                    cpa = cp2;
                if (Vector2.Distance(cp1, _ip) < Vector2.Distance(cp2, _ip))
                    cpi = cp1;
                else
                    cpi = cp2;

                float apd = Vector3.Distance(SceneManager.Instance.EditorCamera.transform.position, new Vector3(cpa.x, _height / 2, cpa.y));
                float ipd = Vector3.Distance(SceneManager.Instance.EditorCamera.transform.position, new Vector3(cpi.x, _height / 2, cpi.y));

                if ((apd < ipd && _inside == "i") || (apd > ipd && _inside == "a"))
                {
                    if (_inside == "i") _alongPlane.gameObject.SetActive(false);
                    if (_inside == "a") _inversePlane.gameObject.SetActive(false);
                    _toPlane.gameObject.SetActive(false);
                    _toPlane1.gameObject.SetActive(false);
                    _fromPlane.gameObject.SetActive(false);
                    _fromPlane1.gameObject.SetActive(false);
                    _topp.gameObject.SetActive(false);
                    //ceilingLine3D.transform.gameObject.SetActive(false);
                }
                else
                {
                    _alongPlane.gameObject.SetActive(true);
                    _inversePlane.gameObject.SetActive(true);
                    _toPlane.gameObject.SetActive(true);
                    _toPlane1.gameObject.SetActive(true);
                    _fromPlane.gameObject.SetActive(true);
                    _fromPlane1.gameObject.SetActive(true);
                    _topp.gameObject.SetActive(true);
                    //ceilingLine3D.transform.gameObject.SetActive(true);
                }
            }
        }

        private WallPlaneVO _avo;
        private WallPlaneVO _ivo;

        private void Draw()
        {
            _alongPlane.InversionTexture = true;
            _alongPlane.height = _height;
            _alongPlane.width = _aw;
            _ag.transform.position = new Vector3(_ap.x, _height / 2, _ap.y);
            _ag.transform.rotation = Quaternion.Euler(90f, 360 - _rotation, _ag.transform.eulerAngles.z);
            _alongPlane.SetPenetrate(_aholes);
            _alongPlane.BuildGeometry();

            WallController wc = _ag.GetComponent<WallController>();
            wc.rect3D = _alongPlane;
            wc.wallPlaneVO = _avo;

            //_inversePlane.InversionTexture = true;
            _inversePlane.height = _height;
            _inversePlane.width = _iw;
            _ig.transform.position = new Vector3(_ip.x, _height / 2, _ip.y);
            _ig.transform.rotation = Quaternion.Euler(90f, 360 - _rotation, _ig.transform.eulerAngles.z);
            _inversePlane.SetPenetrate(_iholes);
            _inversePlane.BuildGeometry();

            wc = _ig.GetComponent<WallController>();
            wc.rect3D = _inversePlane;
            wc.wallPlaneVO = _ivo;

            _fromPlane.height = _height;
            _fromPlane.width = _fw1;
            _fpg.transform.position = new Vector3(_fp1.x, _height / 2, _fp1.y);
            _fpg.transform.rotation = Quaternion.Euler(-90f, 180 - _fr1, _fpg.transform.eulerAngles.z);
            _fromPlane.BuildGeometry();

            _fromPlane1.height = _height;
            _fromPlane1.width = _fw2;
            _fpg1.transform.position = new Vector3(_fp2.x, _height / 2, _fp2.y);
            _fpg1.transform.rotation = Quaternion.Euler(-90f, 180 - _fr2, _fpg1.transform.eulerAngles.z);
            _fromPlane1.BuildGeometry();

            _toPlane.height = _height;
            _toPlane.width = _tw1;
            _tpg.transform.position = new Vector3(_tp1.x, _height / 2, _tp1.y);
            _tpg.transform.rotation = Quaternion.Euler(-90f, 180 - _tr1, _tpg.transform.eulerAngles.z);
            _toPlane.BuildGeometry();

            _toPlane1.height = _height;
            _toPlane1.width = _tw2;
            _tpg1.transform.position = new Vector3(_tp2.x, _height / 2, _tp2.y);
            _tpg1.transform.rotation = Quaternion.Euler(-90f, 180 - _tr2, _tpg1.transform.eulerAngles.z);
            _toPlane1.BuildGeometry();

            _topp.transform.position = new Vector3(0, _height - 0.005f, 0);
            _topPlane.BuildIrregularGeometry(_meshData);

            _maskPlane.height = _height;
            _maskPlane.width = _fw1;
            _mask.transform.position = new Vector3(_fp1.x, _height / 2, _fp1.y);
            _mask.transform.rotation = Quaternion.Euler(-90f, 360 - _fr1 - 90, _fpg.transform.eulerAngles.z);
            _maskPlane.BuildGeometry();

            _maskPlane1.height = _height;
            _maskPlane1.width = _fw1;
            _mask1.transform.position = new Vector3(_tp1.x, _height / 2, _tp1.y);
            _mask1.transform.rotation = Quaternion.Euler(-90f, 360 - _tr1 - 90, _tpg.transform.eulerAngles.z);
            _maskPlane1.BuildGeometry();

            _mask.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            _mask1.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

            Destroy(amc);
            Destroy(imc);

            amc = _ag.AddComponent<MeshCollider>();
            imc = _ig.AddComponent<MeshCollider>();
        }

        public bool transparent;

        private MeshCollider amc;
        private MeshCollider imc;

        public void UpdateCollage()
        {
            _alongPlane.SetCollage(_collageStruct.id as string);
            _inversePlane.SetCollage(_collageStruct.id as string);

            _fromPlane.SetCollage("1002");
            _fromPlane1.SetCollage("1002");
            _toPlane.SetCollage("1002");
            _toPlane1.SetCollage("1002");
            _topPlane.SetCollage("1002");
            _maskPlane.SetCollage("1002");
            _maskPlane1.SetCollage("1002");
        }

        private List<Vector3> drawList = new List<Vector3>();

        private SurfacePlane3D _topPlane;

        private GameObject _ag;
        private GameObject _ig;
        private GameObject _fpg;
        private GameObject _tpg;
        private GameObject _fpg1;
        private GameObject _tpg1;
        private GameObject _topp;
        private GameObject _mask;
        private GameObject _mask1;

        private WallPlane3D _alongPlane;
        private WallPlane3D _inversePlane;
        private WallPlane3D _fromPlane;
        private WallPlane3D _fromPlane1;
        private WallPlane3D _toPlane;
        private WallPlane3D _toPlane1;
        private WallPlane3D _maskPlane;
        private WallPlane3D _maskPlane1;
    }
}
