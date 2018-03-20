using Build3D;
using System.Collections.Generic;

public class ThickIrregularComponentUI : BaseComponentUI
{
    private ButtonImageUI buttonUI;

    override public void Init()
    {
        base.Init();

        CreateTitleName("自定义模型设置");

        buttonUI = CreateButtonImageUI("设置", CookieClickHandle);
    }

    private void CookieClickHandle(ButtonImageUI ui)
    {
        UIManager.OpenUI(UI.DrawLinePanel, _items[0].VO);
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            ThickIrregularVO vo = avo as ThickIrregularVO;
        }
    }

    private List<ThickIrregularComponent> _thickIrregulars;

    protected override void FillComponent()
    {
        base.FillComponent();

        _thickIrregulars = new List<ThickIrregularComponent>();

        foreach (Item3D item in _items)
        {
            _thickIrregulars.Add(item.GetComponentInChildren<ThickIrregularComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            ThickIrregularVO vo = avo as ThickIrregularVO;
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
                _oldAssets.Add(obj.VO.GetComponentVO<ThickIrregularVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<ThickIrregularVO>().Clone());
            }

            FillComponent();
        }
    }
}


