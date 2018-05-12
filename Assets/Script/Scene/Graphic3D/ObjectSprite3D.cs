using System.Collections.Generic;
using UnityEngine;

public class ObjectSprite3D : AssetSprite
{
    public Vector3 mousePoint { set; get; }

    override public void Instantiate(ObjectVO vo)
    {
        layer = "ObjectSprite3D";

        base.Instantiate(vo);
    }

    protected string _assetId = "ObjectSprite3D";

    public string assetId
    {
        set
        {
            if (_assetId == value)
            {
                return;
            }

            _assetId = value;
        }
        get { return _assetId; }
    }

    private SceneComponent[] components;

    private GameObject _model;

    public virtual GameObject model
    {
        set
        {
            if (_model == value || _model == _vo.model)
            {
                return;
            }

            if (_model != null)
            {
                Destroy(_model);
            }

            if (_vo.assetId == "ThickIrregularPlane3D" || _vo.assetId == "OBJ")
            {
                if (_vo.model == null)
                {
                    _model = new GameObject();
                }
                else
                {
                    //if (_vo.model.GetComponent<ThickIrregularPlane3D>())
                    //{
                    //    GameObject drawPanelObject = new GameObject("DrawPanel");
                    //    ThickIrregularPlane3D thickIrregularPlane3D = drawPanelObject.AddComponent<ThickIrregularPlane3D>();
                    //    drawPanelObject.AddComponent<ThickIrregularComponent>();
                    //    thickIrregularPlane3D.Code = _vo.model.GetComponent<ThickIrregularPlane3D>().Code;
                    //    Destroy(_vo.model);
                    //    _model = drawPanelObject;
                    //    _vo.model = drawPanelObject;
                    //}
                    //else
                    //{
                        if (_vo.model.transform.parent != null)
                        {
                            _model = Instantiate(_vo.model, new Vector3(), new Quaternion());
                        }
                        else
                        {
                            _model = _vo.model;
                        }
                    //}
                }
            }
            else
            {
                if (_vo.model == null)
                {
                    Debug.Log(_vo.assetId + "---------------------" + "null");
                    _model = new GameObject();
                }
                else
                {
                    _model = Instantiate(_vo.model, new Vector3(), new Quaternion());
                }
            }

            _model.transform.parent = transform;
            _model.transform.localPosition = new Vector3();
            _model.transform.localRotation = Quaternion.Euler(new Vector3());

            if (_vo.assetId == "OBJ")
            {
                _model.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                _model.name = "OBJ";

                gameObject.AddBoxCollider(false);
            }
            else if (_vo.assetId == "ThickIrregularPlane3D")
            {
                _model.transform.localScale = new Vector3(1, 1, 1);

                if (_model.GetComponent<ThickIrregularComponent>() == null)
                {
                    _model.AddComponent<ThickIrregularComponent>();
                    //_model.AddComponent<CurvyColumnComponent>();
                }
            }
            else
            {
                _model.transform.localScale = new Vector3(1, 1, 1);
            }

            if (_model.GetComponent<TransformComponent>() == null)
            {
                _model.AddComponent<TransformComponent>();
            }

            if (_model.GetComponent<CollageComponent>() == null)
            {
                _model.AddComponent<CollageComponent>();
            }

            //初始化Component
            components = gameObject.GetComponentsInChildren<SceneComponent>();
            foreach (SceneComponent sc in components)
            {
                sc.Init(this);
            }

            Vector3 v = MeshUtils3D.BoundSizeBoxCollider(_model);
            (VO as ObjectVO).sizeX = v.x;
            (VO as ObjectVO).sizeY = v.y;
            (VO as ObjectVO).sizeZ = v.z;

            _model = value;

            layer = "ObjectSprite3D";
        }
        get { return _model; }
    }

    public float sizeX
    {
        get { return _vo.sizeX * scaleX; }
    }

    public float sizeY
    {
        get { return _vo.sizeY * scaleY; }
    }

    public float sizeZ
    {
        get { return _vo.sizeZ * scaleZ; }
    }

    public override AssetVO VO
    {
        set
        {
            base.VO = value;

            assetId = _vo.assetId;
            model = _vo.model;

            UpdateComponent();
        }
    }

    public void UpdateComponent()
    {
        SceneComponent[] list = GetComponentsInChildren<SceneComponent>();

        foreach (SceneComponent sc in list)
        {
            sc.VO = VO;
        }
    }
}
