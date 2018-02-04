using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Build3D;
using System.Collections.Generic;

public class TransformComponentUI : BaseComponentUI
{
    private TransformUI pointUI;
    private TransformUI rotateUI;
    private TransformUI scaleUI;

    override public void Init()
    {
        base.Init();

        CreateTitleName("变换");

        pointUI = CreateTransformUI("坐标", TextValueChangedHandle);
        rotateUI = CreateTransformUI("旋转", TextValueChangedHandle);
        scaleUI = CreateTransformUI("缩放", TextValueChangedHandle);

        pointUI.X.text = "0";
        pointUI.Y.text = "0";
        pointUI.Z.text = "0";
        rotateUI.X.text = "0";
        rotateUI.Y.text = "0";
        rotateUI.Z.text = "0";
        scaleUI.X.text = "0";
        scaleUI.Y.text = "0";
        scaleUI.Z.text = "0";

        pointUI.X.onEndEdit.AddListener(TextEndEditHandle);
        pointUI.Y.onEndEdit.AddListener(TextEndEditHandle);
        pointUI.Z.onEndEdit.AddListener(TextEndEditHandle);
        rotateUI.X.onEndEdit.AddListener(TextEndEditHandle);
        rotateUI.Y.onEndEdit.AddListener(TextEndEditHandle);
        rotateUI.Z.onEndEdit.AddListener(TextEndEditHandle);
        scaleUI.X.onEndEdit.AddListener(TextEndEditHandle);
        scaleUI.Y.onEndEdit.AddListener(TextEndEditHandle);
        scaleUI.Z.onEndEdit.AddListener(TextEndEditHandle);
    }

    private void TextValueChangedHandle(string text)
    {
        if (_fillComponent) return;
        foreach (TransformComponent transformComponent in _transforms)
        {
            if (pointUI.X.text != "-") transformComponent.x = float.Parse(pointUI.X.text);
            if (pointUI.Y.text != "-") transformComponent.y = float.Parse(pointUI.Y.text);
            if (pointUI.Z.text != "-") transformComponent.z = float.Parse(pointUI.Z.text);
            if (rotateUI.X.text != "-") transformComponent.rotateX = float.Parse(rotateUI.X.text);
            if (rotateUI.Y.text != "-") transformComponent.rotateY = float.Parse(rotateUI.Y.text);
            if (rotateUI.Z.text != "-") transformComponent.rotateZ = float.Parse(rotateUI.Z.text);
            if (scaleUI.X.text != "-") transformComponent.scaleX = float.Parse(scaleUI.X.text);
            if (scaleUI.Y.text != "-") transformComponent.scaleY = float.Parse(scaleUI.Y.text);
            if (scaleUI.Z.text != "-") transformComponent.scaleZ = float.Parse(scaleUI.Z.text);
        }

        UpdateComponent();

        dispatchEvent(new ComponentPanelEvent(ComponentPanelEvent.TRANSFORM_CHANGE, _items, _oldAssets, _assets));
    }

    private void ShowText()
    {

    }

    override public List<ObjectSprite> items
    {
        set
        {
            base.items = value;

            foreach (ObjectSprite obj in _items)
            {
                _oldAssets.Add(obj.VO.GetComponentVO<TransformVO>().Clone());
                _assets.Add(obj.VO.GetComponentVO<TransformVO>().Clone());
            }

            FillComponent();
        }
    }

    private void TextEndEditHandle(string text)
    {
        if (_fillComponent) return;
        foreach (TransformComponent transformComponent in _transforms)
        {
            if (pointUI.X.text != "-") transformComponent.x = float.Parse(pointUI.X.text);
            if (pointUI.Y.text != "-") transformComponent.y = float.Parse(pointUI.Y.text);
            if (pointUI.Z.text != "-") transformComponent.z = float.Parse(pointUI.Z.text);
            if (rotateUI.X.text != "-") transformComponent.rotateX = float.Parse(rotateUI.X.text);
            if (rotateUI.Y.text != "-") transformComponent.rotateY = float.Parse(rotateUI.Y.text);
            if (rotateUI.Z.text != "-") transformComponent.rotateZ = float.Parse(rotateUI.Z.text);
            if (scaleUI.X.text != "-") transformComponent.scaleX = float.Parse(scaleUI.X.text);
            if (scaleUI.Y.text != "-") transformComponent.scaleY = float.Parse(scaleUI.Y.text);
            if (scaleUI.Z.text != "-") transformComponent.scaleZ = float.Parse(scaleUI.Z.text);
        }

        UpdateComponent();
    }

    public override void UpdateComponent()
    {
        if (_fillComponent) return;

        foreach (AssetVO vo in _assets)
        {
            TransformVO tvo = vo as TransformVO;

            if (rotateUI.X.text != "-") tvo.rotateX = float.Parse(rotateUI.X.text);
            if (rotateUI.Y.text != "-") tvo.rotateY = float.Parse(rotateUI.Y.text);
            if (rotateUI.Z.text != "-") tvo.rotateZ = float.Parse(rotateUI.Z.text);
            if (pointUI.X.text != "-") tvo.x = float.Parse(pointUI.X.text);
            if (pointUI.Y.text != "-") tvo.y = float.Parse(pointUI.Y.text);
            if (pointUI.Z.text != "-") tvo.z = float.Parse(pointUI.Z.text);
            if (scaleUI.X.text != "-") tvo.scaleX = float.Parse(scaleUI.X.text);
            if (scaleUI.Y.text != "-") tvo.scaleY = float.Parse(scaleUI.Y.text);
            if (scaleUI.Z.text != "-") tvo.scaleZ = float.Parse(scaleUI.Z.text);
        }

        DispatchUpdate();
    }

    private List<TransformComponent> _transforms;

    protected override void FillComponent()
    {
        base.FillComponent();

        _transforms = new List<TransformComponent>();

        foreach (ObjectSprite3D item in _items)
        {
            _transforms.Add(item.GetComponentInChildren<TransformComponent>());
        }

        rotateUI.X.text = "";
        rotateUI.Y.text = "";
        rotateUI.Z.text = "";
        pointUI.X.text = "";
        pointUI.Y.text = "";
        pointUI.Z.text = "";
        scaleUI.X.text = "";
        scaleUI.Y.text = "";
        scaleUI.Z.text = "";

        for (int i = 0; i < _items.Count; i++)
        {
            TransformVO tvo = AssetsModel.Instance.GetTransformVO(_items[i] as ObjectSprite3D);

            if (rotateUI.X.text != "" && (i != 0 && rotateUI.X.text != tvo.x.ToString())) rotateUI.X.text = "-"; else rotateUI.X.text = tvo.rotateX.ToString();
            if (rotateUI.Y.text != "" && (i != 0 && rotateUI.Y.text != tvo.y.ToString())) rotateUI.Y.text = "-"; else rotateUI.Y.text = tvo.rotateY.ToString();
            if (rotateUI.Z.text != "" && (i != 0 && rotateUI.Z.text != tvo.z.ToString())) rotateUI.Z.text = "-"; else rotateUI.Z.text = tvo.rotateZ.ToString();
            if (pointUI.X.text != "" && (i != 0 && pointUI.X.text != tvo.x.ToString())) pointUI.X.text = "-"; else pointUI.X.text = tvo.x.ToString();
            if (pointUI.Y.text != "" && (i != 0 && pointUI.Y.text != tvo.y.ToString())) pointUI.Y.text = "-"; else pointUI.Y.text = tvo.y.ToString();
            if (pointUI.Z.text != "" && (i != 0 && pointUI.Z.text != tvo.z.ToString())) pointUI.Z.text = "-"; else pointUI.Z.text = tvo.z.ToString();
            if (scaleUI.X.text != "" && (i != 0 && scaleUI.X.text != tvo.x.ToString())) scaleUI.X.text = "-"; else scaleUI.X.text = tvo.scaleX.ToString();
            if (scaleUI.Y.text != "" && (i != 0 && scaleUI.Y.text != tvo.y.ToString())) scaleUI.Y.text = "-"; else scaleUI.Y.text = tvo.scaleY.ToString();
            if (scaleUI.Z.text != "" && (i != 0 && scaleUI.Z.text != tvo.z.ToString())) scaleUI.Z.text = "-"; else scaleUI.Z.text = tvo.scaleZ.ToString();
        }

        _fillComponent = false;
    }
}
