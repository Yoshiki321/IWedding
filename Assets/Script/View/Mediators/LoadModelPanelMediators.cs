using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildManager;

public class LoadModelPanelMediators : Mediators
{
    public override void OnRegister()
    {
        AddViewListener(LoadModelPanelEvent.LOAD_MODEL, LoadModelHandle);
        AddViewListener(LoadModelPanelEvent.LOAD_ALBEDO, LoadAlbedoHandle);
        AddViewListener(LoadModelPanelEvent.CREATE, CreateHandle);

        AddContextListener(FileEvent.LOAD_OBJ_COMPLETE, LoadOBJComplete);
        AddContextListener(FileEvent.LOAD_TEXTURE_COMPLETE, LoadTextureComplete);
    }

    public override void OnRemove()
    {
        RemoveViewListener(LoadModelPanelEvent.LOAD_MODEL, LoadModelHandle);
        RemoveViewListener(LoadModelPanelEvent.LOAD_ALBEDO, LoadAlbedoHandle);
        RemoveViewListener(LoadModelPanelEvent.CREATE, CreateHandle);

        RemoveContextListener(FileEvent.LOAD_OBJ_COMPLETE, LoadOBJComplete);
        RemoveContextListener(FileEvent.LOAD_TEXTURE_COMPLETE, LoadTextureComplete);
    }

    private void LoadOBJComplete(EventObject e)
    {
        FileEvent fe = e as FileEvent;
        _model = fe.obj as GameObject;

        (panel as LoadModelPanel).SetModel(_model);
    }

    private void LoadTextureComplete(EventObject e)
    {
        FileEvent fe = e as FileEvent;
        _texture = fe.obj as Texture2D;
    }

    private GameObject _model;
    private Texture2D _texture;

    private void LoadModelHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.LOAD_OBJ));
    }

    private void LoadAlbedoHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.LOAD_TEXTURE));
    }

    private void CreateHandle(EventObject e)
    {
        ItemVO itemvo = AssetsModel.Instance.CreateItemVO("OBJ");
        itemvo.assetId = "OBJ";
        _model.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        itemvo.model = _model;
        itemvo.topImgTexture = (Texture2D)Resources.Load("TopImg/Stage");

        MeshRenderer meshRenderer = _model.GetComponent<MeshRenderer>();
        //meshRenderer.material = new Material(Shader.Find("Standard"));
        //meshRenderer.material.SetTexture("_MainTex", _texture);

        foreach (Material m in meshRenderer.materials)
        {
            m.shader = Shader.Find("Standard");
            m.SetTexture("_MainTex", _texture);
        }

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
            new List<AssetVO>() { null },
            new List<AssetVO>() { itemvo }
            ));
    }
}
