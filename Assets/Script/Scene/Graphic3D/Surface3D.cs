using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

namespace Build3D
{
    public class Surface3D : ObjectSprite
    {
        private List<Triangle3D> _drawMeshList;

        public void Init(SurfaceVO surfacevo)
        {
            _fp = new GameObject();
            _cp = new GameObject();

            _fp.name = "floor";
            _cp.name = "ceiling";

            _fp.transform.parent = gameObject.transform;
            _cp.transform.parent = gameObject.transform;

            //_reflectionProbeLight = new GameObject();
            //_reflectionProbeLight.AddComponent<ReflectionProbe>();
            //_reflectionProbeLight.transform.parent = gameObject.transform;
            //_reflectionProbeLight.name = "reflectionProbe";

            //_reflectionProbe = _reflectionProbeLight.GetComponent<ReflectionProbe>();
            //_reflectionProbe.mode = UnityEngine.Rendering.ReflectionProbeMode.Realtime;
            //_reflectionProbe.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.EveryFrame;
            //_reflectionProbe.resolution = 512;

            _floorPlane = _fp.AddComponent<SurfacePlane3D>();
            _ceilingPlane = _cp.AddComponent<SurfacePlane3D>();

            //FloorController fc = _fp.AddComponent<FloorController>();
            //fc.irregularPlane3D = _floorPlane;
            //fc = _cp.AddComponent<FloorController>();
            //fc.irregularPlane3D = _ceilingPlane;

            this.gameObject.layer = LayerMask.NameToLayer("Build3D");
            _fp.transform.gameObject.layer = LayerMask.NameToLayer("Floor");
            _cp.transform.gameObject.layer = LayerMask.NameToLayer("Build3D");

            _collageVO = surfacevo.GetComponentVO<CollageVO>();
            if (_collageVO == null)
            {
                _collageVO = surfacevo.AddComponentVO<CollageVO>();
                _collageVO.SetCollage("地板", "Collage0", "F0001");
                _collageVO.SetCollage("屋顶", "Collage1", "F0002");
            }
            else
            {
            }

            VO = surfacevo;
        }

        private CollageVO _collageVO;

        private GameObject _reflectionProbeLight;
        private ReflectionProbe _reflectionProbe;

        private SurfacePlane3D _ceilingPlane;
        private SurfacePlane3D _floorPlane;
        private GameObject _fp;
        private GameObject _cp;

        private void Draw()
        {
            _floorPlane.BuildIrregularGeometry(_drawFloorData);

            _ceilingPlane.InversionTexture = true;
            _ceilingPlane.BuildIrregularGeometry(_drawCeilingData);

            _fp.transform.position = new Vector3(0, .01f, 0);
            _cp.transform.position = new Vector3(0, _height - .05f, 0);

            Destroy(amc);
            Destroy(imc);

            amc = _fp.AddComponent<MeshCollider>();
            imc = _cp.AddComponent<MeshCollider>();
        }

        private MeshCollider amc;
        private MeshCollider imc;

        public List<string> lines = new List<string>();

        public string SkirtingLineData;

        private float _height;
        private MeshData _drawFloorData;
        private MeshData _drawCeilingData;

        private SurfaceVO _surfaceVO;

        public override AssetVO VO
        {
            set
            {
                //if (_surfaceVO != null && value.Equals(_surfaceVO)) return;
                base.VO = value;
                _surfaceVO = value as SurfaceVO;
                _collageVO = value.GetComponentVO<CollageVO>();

                id = value.id;

                gameObject.name = "surface3 " + id;

                _height = _surfaceVO.height;

                MeshData meshData = Triangulator.GetMeshData(_surfaceVO.points);
                _drawFloorData = meshData;
                _drawCeilingData = meshData;

                //if (!_floorPlane3D.collageData ||
                //	(_floorPlane3D.collageData && value.floorCollage && !_floorPlane3D.collageData.Equals(value.floorCollage))) _floorPlane3D.setCollage(value.floorCollage);
                //if (!_ceilingPlane3D.collageData ||
                //(_ceilingPlane3D.collageData && value.ceilingCollage && !_ceilingPlane3D.collageData.Equals(value.ceilingCollage))) _ceilingPlane3D.setCollage(value.ceilingCollage);

                Draw();

                //_reflectionProbeLight.transform.position = CenterPoint();

                UpdateCollage();
            }
        }

        public void UpdateCollage()
        {
            _floorPlane.SetCollage(_collageVO.collages[0]);
            _ceilingPlane.SetCollage(_collageVO.collages[1]);
        }

        public Vector3 CenterPoint()
        {
            List<float> xl = new List<float>();
            List<float> yl = new List<float>();

            for (int i = 0; i < _surfaceVO.points.Count - 1; i++)
            {
                xl.Add(_surfaceVO.points[i].x);
                yl.Add(_surfaceVO.points[i].y);
            }

            float ch;
            for (int i = 0; i < xl.Count - 1; i++)
            {
                float t = xl[i];
                float t1 = xl[i + 1];
                if (t > t1)
                {
                    ch = xl[i + 1];
                    xl[i + 1] = xl[i];
                    xl[i] = ch;
                    i = -1;
                }
            }
            for (int i = 0; i < yl.Count - 1; i++)
            {
                float t = yl[i];
                float t1 = yl[i + 1];
                if (t > t1)
                {
                    ch = yl[i + 1];
                    yl[i + 1] = yl[i];
                    yl[i] = ch;
                    i = -1;
                }
            }

            float xx = xl[0] + (xl[xl.Count - 1] - xl[0]) / 2;
            float yy = yl[0] + (yl[yl.Count - 1] - yl[0]) / 2;

            return new Vector3(xx, _height / 2, yy);
        }
    }
}
