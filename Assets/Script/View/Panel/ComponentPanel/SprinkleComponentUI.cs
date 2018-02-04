using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;
using Build3D;
using BuildManager;

public class SprinkleComponentUI : BaseComponentUI
{
    override public void Init()
    {
        base.Init();

        CreateTitleName("撒花设置");

        CreateSliderUI("花瓣数量", 1f, 10f, value => { _count = value; });

        Vector3 cpoint = SceneManager.Instance.editorGizmoSystem.TranslationGizmo.transform.position;
        Vector3 sp = SceneManager.Instance.Camera3D.WorldToScreenPoint(cpoint);

        CreateButtonImageUI("撒花", value => { Sprinkle(); });

        GameObject sprinkleObj = Instantiate(Resources.Load("UI/Button")) as GameObject;
        GameObject clearObj = Instantiate(Resources.Load("UI/Button")) as GameObject;
        Button sprinkleBtn = sprinkleObj.GetComponent<Button>();
        Button clearBtn = clearObj.GetComponent<Button>();
        sprinkleBtn.transform.Find("Text").GetComponent<Text>().text = "撒花";
        clearBtn.transform.Find("Text").GetComponent<Text>().text = "清空";
        sprinkleBtn.onClick.AddListener(Sprinkle);
        clearBtn.onClick.AddListener(Clear);
    }

    private void Clear()
    {
        foreach (SprinkleComponent sprinkle in _sprinkle) sprinkle.Clear();
    }

    private float _count;

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        for (int i = 0; i < _sprinkle.Count; i++)
        {
            SprinkleVO vo = _assets[i] as SprinkleVO;

            foreach (SprinkleData data in _sprinkle[i].vo.dataList)
            {
                vo.dataList.Add(data.Clone());
            }
        }
    }

    private void Sprinkle()
    {
        foreach (SprinkleComponent sprinkle in _sprinkle) sprinkle.Sprinkle(_count);
        UpdateComponent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Sprinkle();
        }
    }

    private List<SprinkleComponent> _sprinkle;

    protected override void FillComponent()
    {
        base.FillComponent();

        _sprinkle = new List<SprinkleComponent>();

        foreach (Item3D item in _items)
        {
            _sprinkle.Add(item.GetComponentInChildren<SprinkleComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            SprinkleVO vo = avo as SprinkleVO;
        }

        _fillComponent = false;
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<SprinkleVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<SprinkleVO>().Clone());
            }

            FillComponent();
        }
    }
}
