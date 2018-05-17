using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindBellLineComponentUI : BaseComponentUI
{
    private SliderUI _count;
    private SliderUI _length;
    private SliderUI _radius;
    private SliderUI _itemRadius;
    private Color _color;
    private ButtonImageUI colorUI;

    override public void Init()
    {
        base.Init();

        CreateTitleName("风铃设置");

        _count = CreateSliderUI("数量", 3, 50, value => { foreach (WindBellLineComponent line in _windBellLines) line.Count = (int)value; });
        _length = CreateSliderUI("长度", 0.1f, 2, value => { foreach (WindBellLineComponent line in _windBellLines) line.Length = value; });
        _radius = CreateSliderUI("范围", 0.1f, 1f, value => { foreach (WindBellLineComponent line in _windBellLines) line.Radius = value; });
        _itemRadius = CreateSliderUI("半径", 0.01f, 0.2f, value => { foreach (WindBellLineComponent line in _windBellLines) line.ItemRadius = value; });

        colorUI = CreateButtonImageUI("颜色", ColorClickHandle);
    }

    private void ColorClickHandle(ButtonImageUI ui)
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _color,
       colorUI.button.transform.position) as SelectColorPanel;
        sp.onPicker.AddListener(UpdateColor);
    }

    private void UpdateColor(Color color)
    {
        _color = color;
        foreach (WindBellLineComponent line in _windBellLines) line.Color = _color;
        UpdateComponent();
        colorUI.image.color = _color;
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            WindBellLineVO vo = avo as WindBellLineVO;
            vo.count = (int)_count.value;
            vo.length = _length.value;
            vo.radius = _radius.value;
            vo.itemRadius = _itemRadius.value;
            vo.color = _color;
        }
    }

    private List<WindBellLineComponent> _windBellLines;

    protected override void FillComponent()
    {
        base.FillComponent();

        _windBellLines = new List<WindBellLineComponent>();

        foreach (Item3D item in _items)
        {
            _windBellLines.Add(item.GetComponentInChildren<WindBellLineComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            WindBellLineVO vo = avo as WindBellLineVO;
            _count.value = vo.count;
            _length.value = vo.length;
            _radius.value = vo.radius;
            _itemRadius.value = vo.itemRadius;
            _color = vo.color;
            colorUI.image.color = _color;
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
                _oldAssets.Add(obj.VO.GetComponentVO<WindBellLineVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<WindBellLineVO>().Clone());
            }

            FillComponent();
        }
    }
}


