using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPanel : BasePanel
{
    protected Transform _content;
    protected GameObject _view;

    private void Awake()
    {
        _view = transform.Find("View").gameObject;
        _content = _view.transform.Find("Viewport").Find("Content");

        _view.SetActive(true);
    }

    public void ClearComponent()
    {
        //_view.gameObject.SetActive(false);

        for (int i = 0; i < _content.childCount; i++)
        {
            _content.GetChild(i).gameObject.SetActive(false);
        }
    }
}
