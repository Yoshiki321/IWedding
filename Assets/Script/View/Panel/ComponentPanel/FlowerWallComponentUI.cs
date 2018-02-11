using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Common;
using Build3D;
using BuildManager;

public class FlowerWallComponentUI : BaseComponentUI
{
    private SliderUI _countSliderUI;
    private GameObject _menu;
    private DrawPlane _drawPanel;
    private DrawLine _rline;

    private Button addBtn;
    private Button removeBtn;
    private Button resetCBtn;

    override public void Init()
    {
        base.Init();

        CreateTitleName("花墙设置");

        //_countSliderUI = CreateSliderUI("花瓣数量", 1f, 30f, value => { _count = value; UpdateComponent(); }, true);

        CreateTitleButtonUI("编辑", "快捷键 R", value => { Editor(); });
        CreateTitleButtonUI("填充", "快捷键 E", value => { Fill(); });
        CreateTitleButtonUI("清空", "清空花墙", value => { Clear(); });

        _menu = GameObject.Instantiate(Resources.Load("UI/DrawLinePanel/menu") as GameObject);
        _menu.transform.parent = UIManager.GetUI(UI.ComponentPanel).transform;
        _menu.SetActive(false);

        addBtn = _menu.transform.Find("AddBtn").gameObject.GetComponent<Button>();
        removeBtn = _menu.transform.Find("RemoveBtn").gameObject.GetComponent<Button>();
        resetCBtn = _menu.transform.Find("ResetBtn").gameObject.GetComponent<Button>();
        addBtn.onClick.AddListener(AddBtnClickHandle);
        removeBtn.onClick.AddListener(RemoveBtnClickHandle);
        resetCBtn.onClick.AddListener(ResetCBtnClickHandle);

        UpdateHeight();
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
        if(_drawPanel != null) _drawPanel.isOperate = false;
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

    private void Editor()
    {
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.Editor();
    }

    private void Clear()
    {
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.Clear();
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO avo in _assets)
        {
            FlowerWallVO vo = avo as FlowerWallVO;
            //vo.assetId = rotateTypeUI.dropdown.value;
        }
    }

    private void Fill()
    {
        foreach (FlowerWallComponent flowerWall in _flowerWall) flowerWall.Fill();
        UpdateComponent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Fill();
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

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<FlowerWallVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<FlowerWallVO>().Clone());
            }

            FillComponent();
        }
    }
}
