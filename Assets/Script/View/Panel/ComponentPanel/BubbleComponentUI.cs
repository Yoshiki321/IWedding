using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Build3D;

public class BubbleComponentUI : BaseComponentUI
{
    private Slider size;
    private Slider speed;
    private Slider density;
    private Slider directionX;

    override public void Init()
    {
        base.Init();

        size = GetUI("Size").GetComponentInChildren<Slider>();
        speed = GetUI("Speed").GetComponentInChildren<Slider>();
        density = GetUI("Density").GetComponentInChildren<Slider>();
        directionX = GetUI("DirectionX").GetComponentInChildren<Slider>();

        size.onValueChanged.AddListener(SizeSliderHandle);
        speed.onValueChanged.AddListener(SpeedSliderHandle);
        density.onValueChanged.AddListener(DensitySliderHandle);
        directionX.onValueChanged.AddListener(DirectionXSliderHandle);
    }

    private void DensitySliderHandle(float value)
    {
        foreach (BubbleComponent bubble in _bubbles) bubble.density = 1.1f - value;
        UpdateComponent();
    }

    private void DirectionXSliderHandle(float value)
    {
        foreach (BubbleComponent bubble in _bubbles) bubble.directionX = value;
        UpdateComponent();
    }

    private void SizeSliderHandle(float value)
    {
        foreach (BubbleComponent bubble in _bubbles) bubble.size = value;
        UpdateComponent();
    }

    private void SpeedSliderHandle(float value)
    {
        foreach (BubbleComponent bubble in _bubbles) bubble.speed = value;
        UpdateComponent();
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            BubbleVO vo = avo as BubbleVO;
            vo.size = size.value;
            vo.speed = speed.value;
        }
    }

    private List<BubbleComponent> _bubbles;

    protected override void FillComponent()
    {
        base.FillComponent();

        _bubbles = new List<BubbleComponent>();

        foreach (Item3D item in _items)
        {
            _bubbles.Add(item.GetComponentInChildren<BubbleComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            BubbleVO vo = avo as BubbleVO;
            size.value = vo.size;
            speed.value = vo.speed;
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
                _oldAssets.Add(obj.VO.GetComponentVO<BubbleVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<BubbleVO>().Clone());
            }

            FillComponent();
        }
    }
}
