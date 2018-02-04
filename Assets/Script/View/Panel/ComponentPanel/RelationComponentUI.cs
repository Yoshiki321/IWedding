using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelationComponentUI : BaseComponentUI
{
    private ButtonImageUI buttonUI;
    private string itemId;

    override public void Init()
    {
        base.Init();

        CreateTitleName("关联道具设置");

        buttonUI = CreateButtonImageUI("样式", RelationClickHandle);
    }

    private void RelationClickHandle(ButtonImageUI ui)
    {
        Hashtable h = new Hashtable();
        h.Add("10030003", "UI/ItemTool/itemImg/道具/img2");
        h.Add("10030004", "UI/ItemTool/itemImg/道具/img3");

        SelectTexturePanel sp = UIManager.OpenPanel(Panel.SelectTexturePanel, h,
           buttonUI.button.transform.position - new Vector3(30, 0)) as SelectTexturePanel;
        sp.getTextue += UpdateTexture;
        sp.selectItem = itemId;
    }

    private void UpdateTexture(string id)
    {
        foreach (AssetVO avo in _assets)
        {
            itemId = id;
            UpdateComponent();
        }
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            RelationVO vo = avo as RelationVO;
            vo.itemId = itemId;
        }
    }

    private List<RelationComponent> _relations;

    protected override void FillComponent()
    {
        base.FillComponent();

        _relations = new List<RelationComponent>();

        foreach (Item3D item in _items)
        {
            _relations.Add(item.GetComponentInChildren<RelationComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            RelationVO vo = avo as RelationVO;
            itemId = vo.itemId;
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
                _oldAssets.Add(obj.VO.GetComponentVO<RelationVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<RelationVO>().Clone());
            }

            FillComponent();
        }
    }
}


