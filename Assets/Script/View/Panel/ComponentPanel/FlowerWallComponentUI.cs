using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;
using Build3D;
using BuildManager;

public class FlowerWallComponentUI : BaseComponentUI
{
    private GameObject _menu;
    private DrawPlane _drawPanel;
    private DrawLine _rline;

    private Button _addBtn;
    private Button _removeBtn;
    private Button _resetCBtn;

    private ButtonImageUI _colorUI;
    private Color _color;

    TitleButtonUI _hideButton;

    override public void Init()
    {
        base.Init();

        CreateTitleName("花墙设置");

        CreateTitleButtonUI("编辑", "快捷键 R", value => { Editor(); });
        CreateTitleButtonUI("填充", "快捷键 E", RelationClickHandle);
        CreateTitleButtonUI("清空", "清空花墙", value => { Clear(); });
        _hideButton = CreateTitleButtonUI("隐藏", "隐藏背板", value => { Hide(); });
        _colorUI = CreateButtonImageUI("背板颜色", ColorClickHandle);

        _menu = Instantiate(Resources.Load("UI/DrawLinePanel/menu") as GameObject);
        _menu.transform.parent = UIManager.GetUI(UI.ComponentPanel).transform;
        _menu.SetActive(false);

        _addBtn = _menu.transform.Find("AddBtn").gameObject.GetComponent<Button>();
        _removeBtn = _menu.transform.Find("RemoveBtn").gameObject.GetComponent<Button>();
        _resetCBtn = _menu.transform.Find("ResetBtn").gameObject.GetComponent<Button>();
        _addBtn.onClick.AddListener(AddBtnClickHandle);
        _removeBtn.onClick.AddListener(RemoveBtnClickHandle);
        _resetCBtn.onClick.AddListener(ResetCBtnClickHandle);

        UpdateHeight();
    }

    private void DrawPanelEventHandle(EventObject e)
    {
        UpdateComponent();
    }

    private void ColorClickHandle(ButtonImageUI ui)
    {
        SelectColorPanel sp = UIManager.OpenPanel(Panel.SelectColorPanel, _color,
         _colorUI.button.transform.position) as SelectColorPanel;
        sp.onPicker.AddListener(UpdateColor);
    }

    private void UpdateColor(Color color)
    {
        _color = color;
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.color = _color;
        UpdateComponent();
        _colorUI.image.color = _color;
    }

    private void AddBtnClickHandle()
    {
        _drawPanel.AddLine(_rline);
        _menu.SetActive(false);
    }

    private void RemoveBtnClickHandle()
    {
        _drawPanel.RemoveLine(_rline);
        _menu.SetActive(false);
    }

    private void ResetCBtnClickHandle()
    {
        _drawPanel.ResetCurve(_rline);
        _menu.SetActive(false);
    }

    public override void Close()
    {
        if (gameObject.activeSelf == false) return;
        SceneManager.EnabledEditorObjectSelection(true);
        if (_drawPanel != null) _drawPanel.isOperate = false;
    }

    private void LineRightClick(DrawLine line)
    {
        _rline = line;
        _menu.SetActive(true);
        _menu.transform.SetSiblingIndex(9999);
        _menu.transform.position = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) - new Vector3(0, 0, 10);
    }

    private void HideMenu()
    {
        _menu.SetActive(false);
    }

    private void Hide()
    {
        VisiblePanel(!_flowerWall[0].visible);
    }

    private void VisiblePanel(bool value)
    {
        if (!value)
        {
            _hideButton.buttonText.text = "显示";
            _hideButton.titleText.text = "显示背板";
        }
        else
        {
            _hideButton.buttonText.text = "隐藏";
            _hideButton.titleText.text = "隐藏背板";
        }
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.visible = value;
    }

    private void Editor()
    {
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.Editor();
    }

    private void Clear()
    {
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.assetId = "";
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            FlowerWallVO vo = avo as FlowerWallVO;
            vo.assetId = _flowerWall[0].assetId;
            vo.color = _color;
            vo.visible = _flowerWall[0].visible;
            vo.panelCode = _drawPanel.Code;
        }
    }

    private string itemId;

    private void RelationClickHandle(TitleButtonUI ui)
    {
        Hashtable h = new Hashtable();
        h.Add("10030003", "UI/ItemTool/itemImg/道具/img2");
        h.Add("10030004", "UI/ItemTool/itemImg/道具/img3");

        SelectTexturePanel sp = UIManager.OpenPanel(Panel.SelectTexturePanel, h,
           ui.button.transform.position - new Vector3(30, 0)) as SelectTexturePanel;
        sp.getTextue += UpdateTexture;
        sp.selectItem = itemId;
    }

    private void UpdateTexture(string id)
    {
        foreach (AssetVO avo in _assets)
        {
            itemId = id;
            foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.assetId = id;
            UpdateComponent();
        }
    }

    private void Fill()
    {
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.assetId = "1001";
        UpdateComponent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Fill();
            DispatchUpdate();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Editor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUpHandle();
            Invoke("HideMenu", 0.1f);
        }
    }

    private List<FlowerWallComponent> _flowerWall;

    protected override void FillComponent()
    {
        base.FillComponent();

        _flowerWall = new List<FlowerWallComponent>();

        foreach (Item3D item in _items)
        {
            _flowerWall.Add(item.GetComponentInChildren<FlowerWallComponent>());
        }

        foreach (AssetVO avo in _assets)
        {
            FlowerWallVO vo = avo as FlowerWallVO;
            VisiblePanel(vo.visible);

            _color = vo.color;
            _colorUI.image.color = _color;
        }

        _fillComponent = false;
    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            _drawPanel = items[0].GetComponentInChildren<DrawPlane>();
            _drawPanel.AddLineRightClick(LineRightClick);
            _drawPanel.addEventListener(DrawPlaneEvent.CHANGE, DrawPanelEventHandle);

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<FlowerWallVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<FlowerWallVO>().Clone());
            }

            //FlowerWallVO vo = _assets[0] as FlowerWallVO;
            //vo.panelCode = _drawPanel.Code;

            FillComponent();
        }
    }
}
