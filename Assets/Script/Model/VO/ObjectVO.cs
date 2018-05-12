using UnityEngine;
using System.Collections.Generic;

public class ObjectVO : AssetVO
{
    public string assetId { set; get; }

    public void UpdateSize(GameObject value)
    {
        Vector3 v = MeshUtils3D.BoundSizeBoxCollider(value);

        if (v.x != 0 && v.y != 0 && v.z != 0)
        {
            sizeX = v.x;
            sizeY = v.y;
            sizeZ = v.z;
        }

        //Vector3 vf = _model.transform.position + Vector3.forward * sizeX / 2;
        //Vector3 vb = _model.transform.position + Vector3.back * sizeX / 2;
        //Vector3 vl = _model.transform.position + Vector3.left * sizeZ / 2;
        //Vector3 vr = _model.transform.position + Vector3.right * sizeZ / 2;
    }

    private GameObject _model;
    private string _modelId;

    public string modelId
    {
        set
        {
            _modelId = value;
            model = Resources.Load(_modelId) as GameObject;
        }
        get { return _modelId; }
    }

    public GameObject model
    {
        set
        {
            _model = value;

            if (_model != null && sizeX == 0 && sizeY == 0 && sizeZ == 0)
            {
                _model.AddMeshCollider(false);

                UpdateSize(_model);
            }
        }
        get { return _model; }
    }

    private string _topImgId;

    public string topImgId
    {
        set
        {
            _topImgId = value;
            topImgTexture = (Texture2D)Resources.Load(_topImgId);
        }
        get { return _topImgId; }
    }

    public Texture2D topImgTexture { set; get; }

    public float sizeX = 0;
    public float sizeY = 0;
    public float sizeZ = 0;

    public void FillFromObject(ObjectVO asset)
    {
        name = asset.name;

        _modelId = asset.modelId;
        _topImgId = asset.topImgId;
        model = asset.model;
        topImgTexture = asset.topImgTexture;

        assetId = asset.assetId;

        sizeX = asset.sizeX;
        sizeY = asset.sizeY;
        sizeZ = asset.sizeZ;

        id = asset.id;

        componentVOList = new List<ComponentVO>();
        for (int i = 0; i < asset.componentVOList.Count; i++)
        {
            componentVOList.Add(asset.componentVOList[i].Clone() as ComponentVO);
        }

        //if(asset.name){
        //    name = asset.name;
        //}
    }

    override public AssetVO Clone()
    {
        ObjectVO vo = new ObjectVO();
        vo.FillFromObject(vo);
        return vo;
    }

    override public bool Equals(AssetVO asset)
    {
        ObjectVO oa = (ObjectVO)asset;
        return (
                this.assetId == oa.assetId &&

                EqualsComponentVO(asset)
               );
    }
}
