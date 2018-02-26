using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Build3D;

public class SmokeComponentUI : BaseComponentUI
{
    private Slider size;
    private Slider speed;

    private Button startColorBtn;
    private Image startColorImage;
    private Button endColorBtn;
    private Image endColorImage;

    override public void Init()
    {
        base.Init();

        size = GetUI("Size").GetComponentInChildren<Slider>();
        speed = GetUI("Speed").GetComponentInChildren<Slider>();

        startColorBtn = GetUI("StartColor").GetComponent<Button>();
        startColorImage = startColorBtn.transform.Find("ColorImage").GetComponent<Image>();

        endColorBtn = GetUI("EndColor").GetComponent<Button>();
        endColorImage = endColorBtn.transform.Find("ColorImage").GetComponent<Image>();

        size.onValueChanged.AddListener(SizeSliderHandle);
        speed.onValueChanged.AddListener(SpeedSliderHandle);

        startColorBtn.onClick.AddListener(StartColorClickHandle);
        endColorBtn.onClick.AddListener(EndColorClickHandle);
    }

    private Color _startColor;
    private Color _endColor;

    private void StartColorClickHandle()
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _startColor,
         startColorBtn.transform.position) as SelectColorPanel;
        sp.onPicker.AddListener(UpdateStartColor);
    }

    private void UpdateStartColor(Color color)
    {
        _startColor = color;
        UpdateComponent();
        startColorImage.color = _startColor;
    }

    private void EndColorClickHandle()
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _endColor,
         endColorBtn.transform.position) as SelectColorPanel;
        sp.onPicker.AddListener(UpdateEndColor);
    }

    private void UpdateEndColor(Color color)
    {
        _endColor = color;
        UpdateComponent();
        endColorImage.color = _endColor;
    }

    private void SizeSliderHandle(float value)
    {
        foreach (SmokeComponent smoke in _smokes) smoke.size = value;
        UpdateComponent();
    }

    private void SpeedSliderHandle(float value)
    {
        foreach (SmokeComponent smoke in _smokes) smoke.speed = value;
        UpdateComponent();
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            SmokeVO vo = avo as SmokeVO;
            vo.size = size.value;
            vo.speed = speed.value;
            vo.startColor = _startColor;
            vo.endColor = _endColor;
        }
    }

    private List<SmokeComponent> _smokes;

    protected override void FillComponent()
    {
        base.FillComponent();

        _smokes = new List<SmokeComponent>();

        foreach (Item3D item in _items)
        {
            _smokes.Add(item.GetComponentInChildren<SmokeComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            SmokeVO vo = avo as SmokeVO;
            size.value = vo.size;
            speed.value = vo.speed;

            _startColor = vo.startColor;
            startColorImage.color = _startColor;

            _endColor = vo.endColor;
            endColorImage.color = _endColor;
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
                _oldAssets.Add(obj.VO.GetComponentVO<SmokeVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<SmokeVO>().Clone());
            }

            FillComponent();
        }
    }
}
