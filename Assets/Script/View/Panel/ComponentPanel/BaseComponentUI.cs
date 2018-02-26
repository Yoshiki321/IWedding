using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class BaseComponentUI : DispatcherEventPanel
{
    protected Transform content;
    private VerticalLayoutGroup verticalLayoutGroup;
    private RectTransform rectTransform;

    public virtual void Close()
    {

    }

    public virtual void Init()
    {
        content = transform.Find("View").Find("Content");
        transform.Find("View").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        verticalLayoutGroup = content.GetComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.padding.left = 20;
        verticalLayoutGroup.padding.top = 20;
        verticalLayoutGroup.padding.bottom = 20;
        verticalLayoutGroup.spacing = 20;

        rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, rectTransform.sizeDelta.y);

        UpdateHeight();
    }

    protected SliderUI CreateSliderUI(string name, float min, float max, Action<float> action = null, bool wholeNumbers = false)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/SliderUI")) as GameObject;
        obj.transform.parent = content;
        Text text = obj.transform.Find("Content").Find("Text").GetComponent<Text>();
        Slider slider = obj.transform.Find("Content").Find("Slider").GetComponent<Slider>();
        InputField sliderText = obj.transform.Find("Content").Find("SliderText").GetComponent<InputField>();

        slider.wholeNumbers = wholeNumbers;

        text.text = name;
        slider.minValue = min;
        slider.maxValue = max;

        SliderUI ui = obj.AddComponent<SliderUI>();
        ui.ui = this;
        ui.nameText = text;
        ui.slider = slider;
        ui.text = sliderText;

        UpdateHeight();

        if (action != null) ui.SliderValueChanged(action);
        return ui;
    }

    protected ButtonImageUI CreateButtonImageUI(string name, Action<ButtonImageUI> action = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/ButtonImageUI")) as GameObject;
        obj.transform.parent = content;
        ButtonImageUI ui = obj.AddComponent<ButtonImageUI>();
        ui.ui = this;
        ui.button = obj.transform.Find("Content").Find("Button").GetComponent<Button>();
        ui.buttonText = ui.button.transform.Find("Text").GetComponent<Text>();
        ui.image = obj.transform.Find("Content").Find("Image").GetComponent<Image>();
        ui.buttonText.text = name;
        ui.name = name;
        UpdateHeight();

        if (action != null) ui.OnClickButtom(action);
        return ui;
    }

    protected TransformUI CreateTransformUI(string name, Action<string> action = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/TransformUI")) as GameObject;
        obj.transform.parent = content;
        TransformUI ui = obj.AddComponent<TransformUI>();
        ui.ui = this;
        ui.nameText = obj.transform.Find("Content").Find("Text").GetComponent<Text>();
        ui.X = obj.transform.Find("Content").Find("X").GetComponent<InputField>();
        ui.Y = obj.transform.Find("Content").Find("Y").GetComponent<InputField>();
        ui.Z = obj.transform.Find("Content").Find("Z").GetComponent<InputField>();
        ui.nameText.text = name;
        UpdateHeight();

        if (action != null) ui.OnEndEdit(action, action, action);
        return ui;
    }

    protected DropdownUI CreateDropdownUI(string name, List<string> dataList, Action<int> action = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/DropdownUI")) as GameObject;
        obj.transform.parent = content;
        DropdownUI ui = obj.AddComponent<DropdownUI>();
        ui.ui = this;
        ui.dropdown = obj.transform.Find("Content").Find("Dropdown").GetComponent<Dropdown>();
        ui.nameText = obj.transform.Find("Content").Find("Text").GetComponent<Text>();
        ui.nameText.text = name;

        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        foreach (string s in dataList)
        {
            list.Add(new Dropdown.OptionData(s));
        }
        ui.dropdown.options = list;

        if (action != null) ui.OnValueChanged(action);
        return ui;
    }

    protected Toggle CreateToggle(string name, UnityAction<bool> action)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/ToggleUI")) as GameObject;
        obj.transform.parent = content;
        Toggle toggle = obj.transform.Find("Content").Find("Toggle").GetComponent<Toggle>();
        Text text = obj.transform.Find("Content").Find("Text").GetComponent<Text>();
        text.text = name;
        toggle.onValueChanged.AddListener(action);
        return toggle;
    }

    protected Text CreateTitleName(string name)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/TitleNameUI")) as GameObject;
        obj.transform.parent = content;
        Text text = obj.transform.GetComponent<Text>();
        text.text = name;
        return text;
    }

    protected TitleButtonUI CreateTitleButtonUI(string name, string title, Action<TitleButtonUI> action = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/TitleButtonUI")) as GameObject;
        obj.transform.parent = content;
        TitleButtonUI ui = obj.AddComponent<TitleButtonUI>();
        ui.ui = this;
        ui.button = obj.transform.Find("Content").Find("Button").GetComponent<Button>();
        ui.buttonText = ui.button.transform.Find("Text").GetComponent<Text>();
        ui.titleText = obj.transform.Find("Content").Find("Text").GetComponent<Text>();
        ui.buttonText.text = name;
        ui.titleText.text = title;
        UpdateHeight();

        if (action != null) ui.OnClickButtom(action);
        return ui;
    }

    protected TextureUI CreateTextureUI(string name, Action<TextureUI> action = null, Action<TextureUI> actionColor = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Component/TextureUI")) as GameObject;
        obj.transform.parent = content;
        TextureUI ui = obj.AddComponent<TextureUI>();
        ui.ui = this;
        ui.text = obj.transform.Find("Content").Find("Text").GetComponent<Text>();
        ui.texture = obj.transform.Find("Content").Find("Texture").GetComponent<Button>();
        ui.color = obj.transform.Find("Content").Find("Image").GetComponent<Button>();
        ui.text.text = name;
        ui.name = name;
        UpdateHeight();

        if (action != null) ui.OnClickTexture(action);
        if (actionColor != null) ui.OnClickColor(actionColor);
        return ui;
    }

    protected float _height;

    public void UpdateHeight()
    {
        _height = 0;

        foreach (Transform t in content)
        {
            if (t.gameObject.activeSelf)
            {
                _height += t.gameObject.GetComponent<RectTransform>().sizeDelta.y;
                _height += 30;
            }
        }

        Vector2 v = rectTransform.sizeDelta;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(v.x, _height);
        transform.Find("View").GetComponent<RectTransform>().sizeDelta = new Vector2(v.x, _height);
        transform.Find("View").Find("Content").GetComponent<RectTransform>().sizeDelta = new Vector2(v.x, _height);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    protected void AddUI(GameObject ui)
    {
        ui.transform.parent = transform.Find("View").Find("Content").transform;
        UpdateHeight();
    }

    protected void RemoveUI(GameObject ui)
    {
        Destroy(ui);
        UpdateHeight();
    }

    override protected GameObject GetUI(string name)
    {
        return transform.Find("View").Find("Content").Find(name).gameObject;
    }

    protected List<AssetVO> _assets;
    protected List<AssetVO> _oldAssets;
    protected List<ObjectSprite> _items;

    public virtual List<ObjectSprite> items
    {
        set
        {
            _assets = new List<AssetVO>();
            _oldAssets = new List<AssetVO>();
            _items = value;

            UpdateUI();
        }
        get { return _items; }
    }

    protected bool _fillComponent;

    protected virtual void FillComponent()
    {
        _fillComponent = true;
    }

    override public void dispatchEvent(EventObject e)
    {
        GameObject.Find(UI.ComponentPanel).GetComponent<BasePanel>().dispatchEvent(e);
    }

    public virtual void UpdateComponent()
    {

    }

    protected virtual void UpdateUI()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseUpHandle();
        }
    }

    protected virtual void MouseUpHandle()
    {
        DispatchUpdate();
    }

    protected void DispatchUpdate()
    {
        if (Equals(_oldAssets, _assets) == false)
        {
            dispatchEvent(new ComponentPanelEvent(ComponentPanelEvent.UPDATE, _items, _oldAssets, _assets));
        }
    }

    private bool Equals(List<AssetVO> list, List<AssetVO> list1)
    {
        if (list.Count != list1.Count)
        {
            return false;
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].Equals(list1[i]))
            {
                return false;
            }
        }
        return true;
    }

    protected IEnumerator LoadTexture(string url, Action<Texture2D> cb)
    {
        WWW www = new WWW(url);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            cb.Invoke(www.texture);
            www.Dispose();
        }
    }
}
