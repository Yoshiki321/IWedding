using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTexturePanel : BaseWindow
{
    public delegate void RetureTextue(string id);
    public event RetureTextue getTextue;

    private Transform _content;

    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTransform;
    private Scrollbar _scrollbar;

    private void Awake()
    {
        _content = GetUI("View/ScrollImg").transform.Find("Content");
        _scrollbar = GetUI("View/ScrollImg").transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        _gridLayoutGroup = _content.GetComponent<GridLayoutGroup>();
        _rectTransform = _content.GetComponent<RectTransform>();
    }

    private string _selectItem;

    public string selectItem
    {
        set
        {
            _selectItem = value;

            foreach (Transform t in _content)
            {
                if (t.gameObject.name == value)
                {
                    t.gameObject.GetComponent<Image>().Select(true);
                }
            }
        }

        get { return _selectItem; }
    }

    override public void SetContent(object value)
    {
        AddGrid(value as Hashtable);
    }

    public void AddGrid(Hashtable hashtable)
    {
        for (int i = 0; i < _content.transform.childCount; i++)
        {
            Destroy(_content.transform.GetChild(i).gameObject);
        }

        float height = 0;

        height += _gridLayoutGroup.spacing.y;

        foreach (string id in hashtable.Keys)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = _content;
            Image img = obj.AddComponent<Image>();
            img.overrideSprite = Resources.Load(hashtable[id] as string, typeof(Sprite)) as Sprite;
            obj.name = id.ToString();
            AddEventClick(obj);

            height += _gridLayoutGroup.cellSize.y;
            height += _gridLayoutGroup.spacing.y;
        }

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, height);
        _scrollbar.value = 1;
    }

    protected override void OnClick(GameObject obj)
    {
        foreach (Transform t in _content)
        {
            t.gameObject.GetComponent<Image>().Select(false);
        }
        obj.GetComponent<Image>().Select(true);

        getTextue?.Invoke(obj.name);
    }

    override protected void Close()
    {
        getTextue = null;
    }
}
