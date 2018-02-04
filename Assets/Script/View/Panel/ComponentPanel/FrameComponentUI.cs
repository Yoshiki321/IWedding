using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;

public class FrameComponentUI : BaseComponentUI
{
    private ButtonImageUI loadBtnUI;

    private string _url;
    private Texture2D texture2;

    override public void Init()
    {
        base.Init();

        CreateTitleName("相框设置");

        loadBtnUI = CreateButtonImageUI("上传图片", loadBtnClickHandle);
    }

    FileControllor f = new FileControllor();

    private void loadBtnClickHandle(ButtonImageUI ui)
    {
        OpenFileDlg dlg = f.OpenProject(FileType.TEXTURE);
        if (dlg == null) return;
        _url = dlg.file;

        UpdateImage();
        UpdateComponent();
        DispatchUpdate();
    }

    private void UpdateImage()
    {
        if (_url == "")
        {
            loadBtnUI.image.sprite = null;
        }
        else
        {
            StartCoroutine(LoadTexture(_url, delegate (Texture2D tex)
            {
                texture2 = tex;
                loadBtnUI.image.sprite = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
            }));
        }
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            FrameVO vo = avo as FrameVO;
            vo.url = _url;
        }
    }

    protected override void FillComponent()
    {
        base.FillComponent();

        foreach (AssetVO avo in _assets)
        {
            FrameVO vo = avo as FrameVO;
            _url = vo.url;
        }

        UpdateImage();

        _fillComponent = false;
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<FrameVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<FrameVO>().Clone());
            }

            FillComponent();
        }
    }
}
