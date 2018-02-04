using Build3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallLampComponentUI : BaseComponentUI
{
    private SliderUI range;
    private SliderUI rotate;
    private ButtonImageUI buttonUI;
    private string cookieId;

    override public void Init()
    {
        base.Init();

        CreateTitleName("氛围灯设置");

        range = CreateSliderUI("范围", 1, 20, value => { foreach (BallLampComponent ballLamp in _ballLamps) ballLamp.range = value; });
        rotate = CreateSliderUI("旋转速度", 0, 3, value => {
            foreach (BallLampComponent ballLamp in _ballLamps)
            {
                ballLamp.rotateX = value;
                ballLamp.rotateY = value;
                ballLamp.rotateZ = value;
            }
        });
        buttonUI = CreateButtonImageUI("样式", CookieClickHandle);
    }

    private void CookieClickHandle(ButtonImageUI ui)
    {
        SelectTexturePanel sp = UIManager.OpenPanel(Panel.SelectTexturePanel, CookieManager.CookieImageList,
           buttonUI.button.transform.position - new Vector3(30, 0)) as SelectTexturePanel;
        sp.getTextue += UpdateTexture;
        sp.selectItem = cookieId;
    }

    private void UpdateTexture(string id)
    {
        foreach (AssetVO avo in _assets)
        {
            cookieId = id;
            UpdateComponent();
        }
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            BallLampVO vo = avo as BallLampVO;
            vo.rotateX = rotate.value;
            vo.rotateY = rotate.value;
            vo.rotateZ = rotate.value;
            vo.range = range.value;
            vo.cookieId = cookieId;
        }
    }

    private List<BallLampComponent> _ballLamps;

    protected override void FillComponent()
    {
        base.FillComponent();

        _ballLamps = new List<BallLampComponent>();

        foreach (Item3D item in _items)
        {
            _ballLamps.Add(item.GetComponentInChildren<BallLampComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            BallLampVO vo = avo as BallLampVO;
            range.value = vo.range;
            rotate.value = vo.rotateX;
            cookieId = vo.cookieId;
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
                _oldAssets.Add(obj.VO.GetComponentVO<BallLampVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<BallLampVO>().Clone());
            }

            FillComponent();
        }
    }
}


