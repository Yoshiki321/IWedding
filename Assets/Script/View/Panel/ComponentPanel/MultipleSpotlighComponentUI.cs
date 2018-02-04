using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleSpotlighComponentUI : BaseComponentUI
{
    public InputField upSpotCount;
    public InputField downSpotCount;
    public InputField upSpacing;
    public InputField downSpacing;
    public InputField verticalSpacing;
    public InputField upFromY;
    public InputField upToY;
    public InputField upFromX;
    public InputField upToX;
    public InputField upTimeY;
    public InputField upTimeX;
    public InputField downFromY;
    public InputField downToY;
    public InputField downFromX;
    public InputField downToX;
    public InputField downTimeY;
    public InputField downTimeX;
    public Toggle startRotate;
    public Button confirm;

    public override void Init()
    {
        base.Init();
        upSpotCount = GetUI("UpSpotCount").GetComponentInChildren<InputField>();
        downSpotCount = GetUI("DownSpotCount").GetComponentInChildren<InputField>();
        upSpacing = GetUI("UpSpacing").GetComponentInChildren<InputField>();
        downSpacing = GetUI("DownSpacing").GetComponentInChildren<InputField>();
        verticalSpacing = GetUI("VerticalSpacing").GetComponentInChildren<InputField>();
        upFromY = GetUI("UpFromY").GetComponentInChildren<InputField>();
        upToY = GetUI("UpToY").GetComponentInChildren<InputField>();
        upFromX = GetUI("UpFromX").GetComponentInChildren<InputField>();
        upToX = GetUI("UpToX").GetComponentInChildren<InputField>();
        upTimeY = GetUI("UpTimeY").GetComponentInChildren<InputField>();
        upTimeX = GetUI("UpTimeX").GetComponentInChildren<InputField>();
        downFromY = GetUI("DownFromY").GetComponentInChildren<InputField>();
        downToY = GetUI("DownToY").GetComponentInChildren<InputField>();
        downFromX = GetUI("DownFromX").GetComponentInChildren<InputField>();
        downToX = GetUI("DownToX").GetComponentInChildren<InputField>();
        downTimeY = GetUI("DownTimeY").GetComponentInChildren<InputField>();
        downTimeX = GetUI("DownTimeX").GetComponentInChildren<InputField>();

        confirm = GetUI("Confirm").GetComponentInChildren<Button>();
        startRotate = GetUI("StartRotate").GetComponentInChildren<Toggle>();

        startRotate.onValueChanged.AddListener(StartRotateHandle);
        confirm.onClick.AddListener(ComfirmHandle);
    }

    private void ComfirmHandle()
    {
        UpdateComponent();
    }

    private void StartRotateHandle(bool value)
    {
        UpdateComponent();
    }

    public override void UpdateComponent()
    {

        foreach (AssetVO avo in _assets)
        {
            MultipleSpotlightVO vo = avo as MultipleSpotlightVO;

            vo.upSpotCount = int.Parse(upSpotCount.text);
            vo.downSpotCount = int.Parse(downSpotCount.text);
            vo.upSpacing = int.Parse(upSpacing.text);
            vo.downSpacing = int.Parse(downSpacing.text);
            vo.verticalSpacing = int.Parse(verticalSpacing.text);

            vo.upFromX = int.Parse(upFromX.text);
            vo.upFromY = int.Parse(upFromY.text);
            vo.upToX = int.Parse(upToX.text);
            vo.upToY = int.Parse(upToY.text);
            vo.upTimeX = int.Parse(upTimeX.text);
            vo.upTimeY = int.Parse(upTimeY.text);

            vo.downFromX = int.Parse(downFromX.text);
            vo.downFromY = int.Parse(downFromY.text);
            vo.downToX = int.Parse(downToX.text);
            vo.downToY = int.Parse(downToY.text);
            vo.downTimeX = int.Parse(downTimeX.text);
            vo.downTimeY = int.Parse(downTimeY.text);

            vo.startRotate = startRotate.isOn;

        }
    }

    protected override void FillComponent()
    {
        base.FillComponent();

        foreach (AssetVO avo in _assets)
        {
            MultipleSpotlightVO vo = avo as MultipleSpotlightVO;

            upSpotCount.text = vo.upSpotCount.ToString();
            downSpotCount.text = vo.downSpotCount.ToString();
            upSpacing.text = vo.upSpacing.ToString();
            downSpacing.text = vo.downSpacing.ToString();
            verticalSpacing.text = vo.verticalSpacing.ToString();

            upFromX.text = vo.upFromX.ToString();
            upFromY.text = vo.upFromX.ToString();
            upToX.text = vo.upToX.ToString();
            upToY.text = vo.upToY.ToString();
            upTimeX.text = vo.upTimeX.ToString();
            upTimeY.text = vo.upTimeY.ToString();

            downFromX.text = vo.downFromX.ToString();
            downFromY.text = vo.downFromX.ToString();
            downToX.text = vo.downToX.ToString();
            downToY.text = vo.downToY.ToString();
            downTimeX.text = vo.downTimeX.ToString();
            downTimeY.text = vo.downTimeY.ToString();

            startRotate.isOn = vo.startRotate;
        }
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<MultipleSpotlightVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<MultipleSpotlightVO>().Clone());
            }

            FillComponent();
        }
    }
}
