using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Build3D;

public class CollageComponent : SceneComponent
{
    private Renderer[] list;

    private string[] identifying = new string[10] { "Collage", "Collage1", "Collage2", "Collage3", "Collage4",
                                                    "Collage5", "Collage6", "Collage7", "Collage8", "Collage9"};

    private bool _isInit = false;

    private ObjectSprite3D _item;

    public override void Init(AssetSprite item)
    {
        if (_isInit) return;
        _isInit = true;

        _item = item as ObjectSprite3D;

        if (_item == null) return;

        CollageVO vo = _item.VO.GetComponentVO<CollageVO>();
        if (vo == null)
        {
            vo = _item.VO.AddComponentVO<CollageVO>();
        }

        _renderers = new List<Renderer>();
        list = _item.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in list)
        {
            if (r.gameObject.tag.Contains("Collage"))
            {
                _renderers.Add(r);
            }
        }

        if (vo.collages.Count == 0)
        {
            vo.ResetCollage();
            foreach (Renderer r in _renderers)
            {
                vo.SetCollage(r.gameObject.tag, r.gameObject.tag, "", r.material.color);
            }
        }

        VO = _item.VO;
    }

    private List<Renderer> _renderers = new List<Renderer>();

    private AssetVO _vo;

    override public AssetVO VO
    {
        set
        {
            _renderers = new List<Renderer>();
            list = _item.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in list)
            {
                if (r.gameObject.tag.Contains("Collage"))
                {
                    _renderers.Add(r);
                }
            }

            _vo = value;
            CollageVO vo = value.GetComponentVO<CollageVO>();

            for (int i = 0; i < vo.collages.Count; i++)
            {
                if (vo.collages[i].id is Color)
                {
                    //list1[i].material.color = (Color)vo.collages[i].id;
                }
                if (vo.collages[i].id is uint)
                {
                    //list1[i].material.color = ColorUtils.HexToColor
                }
                if (vo.collages[i].id is string)
                {
                    foreach (Renderer r in _renderers)
                    {
                        if (r.gameObject.tag == vo.collages[i].tag)
                        {
                            string id = vo.collages[i].id as string;

                            if (vo.collages[i].id as string == "")
                            {
                                r.material.color = vo.collages[i].color;
                            }
                            else
                            {
                                Material m = TexturesManager.CreateMaterials(vo.collages[i].id as string);
                                m.color = vo.collages[i].color;

                                SurfacePlane3D surfacePlane = r.gameObject.GetComponent<SurfacePlane3D>();
                                if (surfacePlane)
                                {
                                    surfacePlane.SetMaterial(m);
                                }
                                else
                                {
                                    r.material = m;
                                }
                            }
                        }
                    }
                    //list1[i].material = TexturesManager.CreateMaterials(vo.collages[i].id as string);
                }
            }
        }
        get { return _vo; }
    }
}
