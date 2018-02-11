using BuildManager;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class DrawLinePanel : BasePanel
{
    private GameObject exitBtn;
    private GameObject createBtn;
    private GameObject resetBtn;
    private GameObject addBtn;
    private GameObject removeBtn;
    private GameObject resetCBtn;
    private GameObject reviseBtn;
    private Dropdown direction;
    private InputField heightText;
    private GameObject menu;
    private GameObject panel;
    private GameObject panel1;
    private GameObject shapeDraw;
    private GameObject _openTextureBtn;
    private GameObject _saveModelBtn;
    private GameObject _loadModelBtn;
    private Slider _tilingXSlider;
    private Slider _tilingYSlider;
    private Slider _offestXSlider;
    private Slider _offestYSlider;
    private GameObject _helpBtn;
    private GameObject _helpPlane;
    private GameObject _closeHelpPanelBtn;

    private DrawPlane _drawPanel;

    public DrawPlane drawPanel
    {
        get { return _drawPanel; }
    }

    void Awake()
    {
        _helpBtn = GetUI("Help");
        _helpPlane = GetUI("HelpPlane");
        _closeHelpPanelBtn = GetUI("HelpPlane").transform.Find("Button").gameObject;

        panel = GetUI("Panel");
        panel1 = GetUI("Panel (1)");
        exitBtn = GetUI("ExitBtn");
        createBtn = GetUI("CreateBtn");
        resetBtn = GetUI("ResetBtn");
        reviseBtn = GetUI("ReviseBtn");
        menu = GetUI("Menu");
        shapeDraw = GetUI("ShapeDraw");
        addBtn = GetUI("Menu").transform.Find("AddBtn").gameObject;
        removeBtn = GetUI("Menu").transform.Find("RemoveBtn").gameObject;
        resetCBtn = GetUI("Menu").transform.Find("ResetBtn").gameObject;
        direction = GetUI("Direction").GetComponent<Dropdown>();
        heightText = GetUI("HeightText").GetComponent<InputField>();
        _openTextureBtn = GetUI("OpenTextureBtn");
        _saveModelBtn = GetUI("SaveModelBtn");
        _loadModelBtn = GetUI("LoadModelBtn");
        _tilingXSlider = GetUI("TilingXSlider").GetComponent<Slider>();
        _tilingYSlider = GetUI("TilingYSlider").GetComponent<Slider>();
        _offestXSlider = GetUI("OffestXSlider").GetComponent<Slider>();
        _offestYSlider = GetUI("OffestYSlider").GetComponent<Slider>();

        menu.SetActive(false);

        _tilingXSlider.onValueChanged.AddListener(TilingXChangeHandle);
        _tilingYSlider.onValueChanged.AddListener(TilingYChangeHandle);
        _offestXSlider.onValueChanged.AddListener(OffestXChangeHandle);
        _offestYSlider.onValueChanged.AddListener(OffestYChangeHandle);

        AddEventClick(panel);
        AddEventClick(panel1);
        AddEventClick(exitBtn);
        AddEventClick(resetBtn);
        AddEventClick(reviseBtn);
        AddEventClick(createBtn);
        AddEventClick(resetCBtn);
        AddEventClick(addBtn);
        AddEventClick(removeBtn);
        AddEventClick(_openTextureBtn);
        AddEventClick(_saveModelBtn);
        AddEventClick(_loadModelBtn);
        AddEventClick(_closeHelpPanelBtn);
        AddEventClick(_helpBtn);

        _drawPanel = GetComponentInChildren<DrawPlane>();

        heightText.text = "50";

        heightText.onEndEdit.AddListener(HeightTextEditHandle);
        direction.onValueChanged.AddListener(SelectDirectionHandle);

        _drawPanel.AddLineRightClick(LineRightClick);
        _drawPanel.AddLineClick(LineClick);
        _drawPanel.AddNodeClick(NodeClick);
        _drawPanel.isOperate = true;

        var _dataNum = 0;
        foreach (ShapeDrawData data in DrawShapeManager.ShapeDrawDataList)
        {
            _dataNum++;
            GameObject obj = new GameObject();
            obj.transform.parent = shapeDraw.transform.Find("DrawTypePanel").transform;
            Image img = obj.AddComponent<Image>();
            hash.Add(obj, data);
            img.overrideSprite = Resources.Load("UI/DrawLinePanel/" + _dataNum.ToString(), typeof(Sprite)) as Sprite;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
            img.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(482f, 180f - (_dataNum - 1) * 60, 0);
            AddEventClick(obj);
        }
    }

    private void TilingXChangeHandle(float value)
    {
        _drawPanel.TilingX = value;
    }

    private void TilingYChangeHandle(float value)
    {
        _drawPanel.TilingY = value;
    }

    private void OffestXChangeHandle(float value)
    {
        _drawPanel.OffestX = value;
    }

    private void OffestYChangeHandle(float value)
    {
        _drawPanel.OffestY = value;
    }

    private void OpenTextureHandle()
    {
        SelectTexturePanel sp = UIManager.OpenPanel(Panel.SelectTexturePanel, TexturesManager.CollageImageList,
        new Vector3(30, 0)) as SelectTexturePanel;
        sp.getTextue += UpdateTexture;
    }

    public void CloseTexturePanel()
    {
        UIManager.ClosePanel(Panel.SelectTexturePanel);
    }

    private void UpdateTexture(string id)
    {
        _drawPanel.SetMaterial(id);
    }

    private Hashtable hash = new Hashtable();

    private DrawLine _rline;

    private void LineRightClick(DrawLine line)
    {
        _rline = line;
        menu.SetActive(true);
        menu.transform.SetSiblingIndex(9999);
        menu.transform.position = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) - new Vector3(0, 0, 10);
    }

    private void LineClick(DrawLine line)
    {
        menu.SetActive(false);
    }

    private void NodeClick(DrawNode node)
    {
        menu.SetActive(false);
    }

    private void HeightTextEditHandle(string value)
    {
        _drawPanel.thickness = float.Parse(value);
    }

    private void SelectDirectionHandle(int value)
    {
        if (value == 0)
        {
            _drawPanel.direction = DrawPlane.HORIZONTAL;
        }
        else
        {
            _drawPanel.direction = DrawPlane.VERTICAL;
        }
    }

    private void DrawDefault()
    {
        if (content != null)
        {
            _drawPanel.ClearAll();
            _drawPanel.Code = content as XmlNode;
        }
        else
        {
            heightText.text = DrawShapeManager.ShapeDrawDataList[0].thickness.ToString();
            _drawPanel.Draw(DrawShapeManager.ShapeDrawDataList[0].nodes, float.Parse(heightText.text));
            _drawPanel.id = "";

            _drawPanel.TilingX = 0.01f;
            _drawPanel.TilingY = 0.01f;
            _drawPanel.OffestX = 0;
            _drawPanel.OffestY = 0;
        }

        _tilingXSlider.value = _drawPanel.TilingX;
        _tilingYSlider.value = _drawPanel.TilingY;
        _offestXSlider.value = _drawPanel.OffestX;
        _offestYSlider.value = _drawPanel.OffestY;
    }

    protected override void OnClick(GameObject obj)
    {
        if (obj == _helpBtn) {
            _helpPlane.SetActive(true);
            foreach (Transform child in this.transform)
            {
                if (child.name == "drawFillPanel") {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "DrawLineObject")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        if (obj == _closeHelpPanelBtn)
        {
            _helpPlane.SetActive(false);
            foreach (Transform child in this.transform)
            {
                if (child.name == "drawFillPanel")
                {
                    child.gameObject.SetActive(true);
                }
                if (child.name == "DrawLineObject")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        if (obj == exitBtn)
        {
            CloseSelf();
        }
        if (obj == createBtn)
        {
            ItemVO itemvo = _drawPanel.CreateMesh();
            dispatchEvent(new DrawLinePanelEvent(DrawLinePanelEvent.ADD_ITEM, itemvo, _drawPanel.Code));
        }
        if (obj == resetBtn)
        {
            content = null;
            _drawPanel.ClearAll();
            DrawDefault();
        }
        if (obj == reviseBtn)
        {
            ItemVO itemvo = _drawPanel.CreateMesh();
            dispatchEvent(new DrawLinePanelEvent(DrawLinePanelEvent.REVISE_ITEM, itemvo, _drawPanel.Code));
        }
        if (obj == addBtn)
        {
            _drawPanel.AddLine(_rline);
            menu.SetActive(false);
        }
        if (obj == removeBtn)
        {
            _drawPanel.RemoveLine(_rline);
            menu.SetActive(false);
        }
        if (obj == resetCBtn)
        {
            _drawPanel.ResetCurve(_rline);
            menu.SetActive(false);
        }
        if (hash[obj] != null)
        {
            _drawPanel.Draw((hash[obj] as ShapeDrawData).nodes, float.Parse(heightText.text));
        }
        if (obj == _openTextureBtn)
        {
            OpenTextureHandle();
        }
        if (obj == _saveModelBtn)
        {
            dispatchEvent(new DrawLinePanelEvent(DrawLinePanelEvent.SAVE, null, _drawPanel.Code));
        }
        if (obj == _loadModelBtn)
        {
            dispatchEvent(new DrawLinePanelEvent(DrawLinePanelEvent.LOAD));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            menu.SetActive(false);
        }
    }

    public override void Close()
    {
        base.Close();

        SceneManager.Instance.controlManager.loop = true;
        SceneManager.Instance.control3Manager.loop = true;
    }

    public override void Open()
    {
        base.Open();

        SceneManager.Instance.controlManager.loop = false;
        SceneManager.Instance.control3Manager.loop = false;

        Invoke("DrawDefault", .01f);
    }
}
