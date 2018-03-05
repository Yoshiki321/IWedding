using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSprite : MonoBehaviour
{
    public virtual void UpdateVO()
    {

    }

    protected AssetVO _assetVO;

    public virtual AssetVO VO
    {
        set
        {
            _assetVO = value;
        }
        get { return _assetVO; }
    }

    protected string _id;

    public virtual string id
    {
        set { _id = value; }
        get { return _id; }
    }

    protected bool _isDispose;

    public virtual void Dispose()
    {
        _isDispose = true;

        Destroy(this.gameObject);
    }

    private HighlighterController highlighterController;

    public virtual void Start()
    {
        highlighterController = gameObject.AddComponent<HighlighterController>();
    }

    private bool _selected;

    public virtual bool Selected
    {
        set
        {
            if (_selected == value) return;
            _selected = value;
            UpdateHighlighter();
        }
        get { return _selected; }
    }

    protected void UpdateHighlighter()
    {
        if (Selected)
        {
            highlighterController.highlighter.ConstantOnImmediate();
        }
        else
        {
            highlighterController.highlighter.ConstantOffImmediate();
        }
    }
}
