using UnityEngine;
using System.Collections;

public class SurfacePlane3D : IrregularPlane3D
{
    //private HighlighterController highlighterController;

    //private void Start()
    //{
    //    highlighterController = gameObject.AddComponent<HighlighterController>();
    //}

    public void SetCollage(string id)
    {
        SetMaterial(TexturesManager.CreateMaterials(id));
    }

    //private bool _selected;

    //public virtual bool Selected
    //{
    //    set
    //    {
    //        if (_selected == value) return;
    //        _selected = value;
    //        UpdateHighlighter();
    //    }
    //    get { return _selected; }
    //}

    //protected void UpdateHighlighter()
    //{
    //    if (Selected)
    //    {
    //        highlighterController.highlighter.ConstantSwitch();
    //    }
    //    else
    //    {
    //        highlighterController.highlighter.ConstantSwitchImmediate();
    //    }
    //}
}
