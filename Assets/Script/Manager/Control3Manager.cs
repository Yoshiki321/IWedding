using UnityEngine;
using RTEditor;
using System.Collections.Generic;
using BuildManager;
using Build2D;
using Build3D;

public class Control3Manager : EventDispatcher
{
    #region Translation

    private TranslationGizmo translationGizmo;
    private RotationGizmo rotationGizmo;
    private ScaleGizmo scaleGizmo;

    public Control3Manager()
    {
        translationGizmo = SceneManager.Instance.editorGizmoSystem.TranslationGizmo;
        rotationGizmo = SceneManager.Instance.editorGizmoSystem.RotationGizmo;
        scaleGizmo = SceneManager.Instance.editorGizmoSystem.ScaleGizmo;

        translationGizmo.GizmoDragStart += new Gizmo.GizmoDragStartHandler(TranslationGizmoDragStartHandler);
        translationGizmo.GizmoDragUpdate += new Gizmo.GizmoDragUpdateHandler(TranslationGizmoDragUpdateHandler);
        translationGizmo.GizmoDragEnd += new Gizmo.GizmoDragEndHandler(TranslationGizmoDragEndHandler);

        rotationGizmo.GizmoDragStart += new Gizmo.GizmoDragStartHandler(RotationGizmoDragStartHandler);
        rotationGizmo.GizmoDragUpdate += new Gizmo.GizmoDragUpdateHandler(RotationGizmoDragUpdateHandler);
        rotationGizmo.GizmoDragEnd += new Gizmo.GizmoDragEndHandler(RotationGizmoDragEndHandler);

        scaleGizmo.GizmoDragStart += new Gizmo.GizmoDragStartHandler(ScaleGizmoDragStartHandler);
        scaleGizmo.GizmoDragUpdate += new Gizmo.GizmoDragUpdateHandler(ScaleGizmoDragUpdateHandler);
        scaleGizmo.GizmoDragEnd += new Gizmo.GizmoDragEndHandler(ScaleGizmoDragEndHandler);
    }

    private bool _loop;

    public bool loop
    {
        set
        {
            _loop = value;
            translationGizmo.isEnabled = value;
            rotationGizmo.isEnabled = value;
            scaleGizmo.isEnabled = value;
        }
        get { return _loop; }
    }

    public List<AssetVO> oldTranslationAssetVOs;
    public List<AssetVO> newTranslationAssetVOs;
    public List<ObjectSprite> translationItems;

    private void TranslationGizmoDragStartHandler(Gizmo gizmo)
    {
        SaveOldHandle();
    }

    private void TranslationGizmoDragUpdateHandler(Gizmo gizmo)
    {
        UpdateHandle();
    }

    private void TranslationGizmoDragEndHandler(Gizmo gizmo)
    {
        UpdateNewHandle();
    }

    private void RotationGizmoDragStartHandler(Gizmo gizmo)
    {
        SaveOldHandle();
    }

    private void RotationGizmoDragUpdateHandler(Gizmo gizmo)
    {
        UpdateHandle();
    }

    private void RotationGizmoDragEndHandler(Gizmo gizmo)
    {
        UpdateNewHandle();
    }

    private void ScaleGizmoDragStartHandler(Gizmo gizmo)
    {
        SaveOldHandle();
    }

    private void ScaleGizmoDragUpdateHandler(Gizmo gizmo)
    {
        UpdateHandle();
    }

    private void ScaleGizmoDragEndHandler(Gizmo gizmo)
    {
        UpdateNewHandle();
    }

    private void SaveOldHandle()
    {
        newTranslationAssetVOs = new List<AssetVO>();
        oldTranslationAssetVOs = new List<AssetVO>();

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            oldTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }
    }

    private void UpdateHandle()
    {
        newTranslationAssetVOs = new List<AssetVO>();

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            newTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }

        dispatchEvent(new Control3ManagerEvent(Control3ManagerEvent.TRANSFORM_UPDATE, oldTranslationAssetVOs, newTranslationAssetVOs));
    }

    private void UpdateNewHandle()
    {
        if (oldTranslationAssetVOs == null || oldTranslationAssetVOs.Count == 0) return;

        newTranslationAssetVOs = new List<AssetVO>();

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            newTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }

        dispatchEvent(new Control3ManagerEvent(Control3ManagerEvent.TRANSFORM, oldTranslationAssetVOs, newTranslationAssetVOs));
    }

    public void FocusOnSelection()
    {
        if (CameraManager.visual == CameraFlags.Two)
        {
            return;
        }
        SceneManager.Instance.EditorCamera.GetComponent<EditorCamera>().FocusOnSelection();
    }

    #endregion

    #region Group

    public List<AssetVO> oldGroupAssetVOs;
    public List<AssetVO> newGroupAssetVOs;

    public void CombinationGroupItem(List<Item3D> list)
    {
        if (list.Count > 1)
        {
            oldGroupAssetVOs = new List<AssetVO>();
            newGroupAssetVOs = new List<AssetVO>();

            foreach (Item3D os in list)
            {
                oldGroupAssetVOs.Add(os.VO.Clone());
            }

            string r = NumberUtils.GetGuid();

            foreach (Item3D os in list)
            {
                ItemVO nvo = os.VO.Clone() as ItemVO;
                nvo.groupId = r;
                newGroupAssetVOs.Add(nvo);
            }

            dispatchEvent(new Control3ManagerEvent(Control3ManagerEvent.COMBINATION, oldGroupAssetVOs, newGroupAssetVOs));
        }
    }

    public void ResolveGroupItem(List<Item3D> list)
    {
        if (list.Count > 1)
        {
            oldGroupAssetVOs = new List<AssetVO>();
            newGroupAssetVOs = new List<AssetVO>();

            foreach (Item3D os in list)
            {
                oldGroupAssetVOs.Add(os.VO.Clone());
            }

            foreach (Item3D os in list)
            {
                ItemVO nvo = os.VO.Clone() as ItemVO;
                nvo.groupId = "";
                newGroupAssetVOs.Add(nvo);
            }

            dispatchEvent(new Control3ManagerEvent(Control3ManagerEvent.RESOLVE, oldGroupAssetVOs, newGroupAssetVOs));
        }
    }

    #endregion
}
