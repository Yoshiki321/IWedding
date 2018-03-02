using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Build2D;
using Build3D;
using BuildManager;
using System.Xml;
using RTEditor;

public class CoreEditorMediator : Mediators
{
    public override void OnRegister()
    {
        SceneManager.Instance.control3Manager.addEventListener(Control3ManagerEvent.TRANSFORM, TransformItem3Handle);
        SceneManager.Instance.control3Manager.addEventListener(Control3ManagerEvent.COMBINATION, CombinationHandle);
        SceneManager.Instance.control3Manager.addEventListener(Control3ManagerEvent.RESOLVE, CancelCombinationHandle);

        SceneManager.Instance.controlManager.addEventListener(ControlManagerEvent.TRANSFORM_MOVE, TransformItemMove2Handle);
        SceneManager.Instance.controlManager.addEventListener(ControlManagerEvent.TRANSFORM_RELEASE, TransformItem2Handle);

        SceneManager.Instance.controlManager.addEventListener(ControlManagerEvent.CHANGE_NESTED, ChangeNestedHandle);
        SceneManager.Instance.controlManager.addEventListener(ControlManagerEvent.CHANGE_LINE, ChangeLine2Handle);
        SceneManager.Instance.controlManager.addEventListener(ControlManagerEvent.CHANGE_SURFACE, ChangeSurface2Handle);

        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_ITEM, SelectItem3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_LINE, SelectLine3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.SELECT_SURFACE, SelectSurface3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_ITEM, ReleaseItem3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_LINE, ReleaseLine3Handle);
        SceneManager.Instance.mouse3Manager.addEventListener(Mouse3ManagerEvent.RELEASE_SURFACE, ReleaseSurface3Handle);

        SceneManager.Instance.mouseManager.addEventListener(MouseManagerEvent.SELECT_OBJECT, SelectObject2Handle);
        SceneManager.Instance.mouseManager.addEventListener(MouseManagerEvent.RELEASE_OBJECT, ReleaseObject2Handle);
        SceneManager.Instance.mouseManager.addEventListener(MouseManagerEvent.DOWN_OBJECT, DownObject2Handle);

        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.UNDO, Keyboard_UndoHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.REDO, Keyboard_RedoHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.SAVE_PROJECT, Keyboard_SaveHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.LOAD_PROJECT, Keyboard_LoadHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.DELETE, Keyboard_DeleteHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.ESC, Keyboard_EscHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.COPY, Keyboard_CopyHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.PASTE, Keyboard_PasteHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CUT, Keyboard_CutHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.COMBINATION, Keyboard_CombinationHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CANCEL_COMBINATION, Keyboard_CancelCombinationHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.FOCUSON_SELECTION, Keyboard_FocusOnSelectionHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.OPEN_DRAWLINEPANEL, Keyboard_OpenDrawLinePanelHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.LOAD_COMBINATION, Keyboard_LoadCombinationHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.OPEN_FILTERPANEL, Keyboard_OpenFilterPanelHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CHANGEVIEW_ONE, Keyboard_ChangeViewOneHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CHANGEVIEW_TWO, Keyboard_ChangeViewTwoHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CHANGEVIEW_THREE, Keyboard_ChangeViewThreeHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.OPEN_BRUSH, Keyboard_OpenBrushHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CLOSE_BRUSH, Keyboard_CloseBrushHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CHANGEVIEW_3D, Keyboard_ChangeView3DHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CHANGEVIEW_2D, Keyboard_ChangeView2DHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.OPEN_LIGHT, Keyboard_OpenLightHandle);
        SceneManager.Instance.keyboardManager.addEventListener(KeyboardManagerEvent.CLOSE_LIGHT, Keyboard_CloseLightHandle);

        AddViewListener(ItemToolEvent.ITEMTOOL_LEFT, ItemToolLeftRotateHandle);
        AddViewListener(ItemToolEvent.ITEMTOOL_RIGHT, ItemToolRightRotateHandle);
        AddViewListener(ItemToolEvent.ITEMTOOL_RESET, ItemToolResetRotateHandle);
        AddViewListener(ItemToolEvent.ITEMTOOL_COMBINATION, ItemToolCombinationHandle);
        AddViewListener(ItemToolEvent.ITEMTOOL_RESOLVE, ItemToolResolveHandle);
        AddViewListener(ItemToolEvent.ITEMTOOL_REMOVE, ItemToolRemoveHandle);

        AddViewListener(LineToolEvent.LineTool_From, LineToolFromHandle);
        AddViewListener(LineToolEvent.LineTool_To, LineToolToHandle);
        AddViewListener(LineToolEvent.LineTool_Line, LineToolLineHandle);
        AddViewListener(LineToolEvent.LineTool_lengthText, LineToolLengthTextHandle);
        AddViewListener(LineToolEvent.LineTool_EndEdit_lengthText, LineToolEndEditLengthTextHandle);
        //上方文件工具条
        AddContextListener(TopToolPanelEvent.SAVE, SceneToolbarSaveHandle);
        AddContextListener(TopToolPanelEvent.LOAD, SceneToolbarLoadHandle);

        AddContextListener(TopToolPanelEvent.UNDO, SceneToolbarUndoHandle);
        AddContextListener(TopToolPanelEvent.REDO, SceneToolbarRedoHandle);
        AddContextListener(TopToolPanelEvent.COPY, Keyboard_CopyHandle);
        AddContextListener(TopToolPanelEvent.PASTE, Keyboard_PasteHandle);
        AddContextListener(TopToolPanelEvent.DELETE, Keyboard_DeleteHandle);
        AddContextListener(TopToolPanelEvent.GROUP, Keyboard_CombinationHandle);
        AddContextListener(TopToolPanelEvent.REGROUP, Keyboard_CancelCombinationHandle);
        AddContextListener(TopToolPanelEvent.FILTER, SceneToolbarFilterHandle);
        AddContextListener(TopToolPanelEvent.ADDITEM, SceneToolbarAddItemHandle);
        AddContextListener(TopToolPanelEvent.ADDHOME, SceneToolbarAddHomeHandle);

        AddContextListener(TopToolPanelEvent.OPENBRUSH, Keyboard_OpenBrushHandle);
        AddContextListener(TopToolPanelEvent.CLOSEBRUSH, Keyboard_CloseBrushHandle);
        AddContextListener(TopToolPanelEvent.ALIGN, SceneToolbarAlignHandle);
        AddContextListener(TopToolPanelEvent.PHOTO, SceneToolbarTakePhotoHandle);
        AddContextListener(TopToolPanelEvent.VIEWONE, Keyboard_ChangeViewOneHandle);
        AddContextListener(TopToolPanelEvent.VIEWTWO, Keyboard_ChangeViewTwoHandle);
        AddContextListener(TopToolPanelEvent.VIEWTHREE, Keyboard_ChangeViewThreeHandle);
        AddContextListener(TopToolPanelEvent.VIEWFOUR, Keyboard_ChangeView3DHandle);
        AddContextListener(TopToolPanelEvent.VIEWFIVE, Keyboard_ChangeView2DHandle);
        AddContextListener(TopToolPanelEvent.OPENLIGHT, Keyboard_OpenLightHandle);
        AddContextListener(TopToolPanelEvent.CLOSELIGHT, Keyboard_CloseLightHandle);
        AddContextListener(TopToolPanelEvent.OPENDRAWPANEL, openDrawLinePanelHandle);
        AddContextListener(TopToolPanelEvent.OPENMODELPANEL, SceneLoadModelHandle);
        //顶部工具条
        AddViewListener(SceneToolbarEvent.UNDO, SceneToolbarUndoHandle);
        AddViewListener(SceneToolbarEvent.REDO, SceneToolbarRedoHandle);
        AddViewListener(SceneToolbarEvent.COPY, Keyboard_CopyHandle);
        AddViewListener(SceneToolbarEvent.PASTE, Keyboard_PasteHandle);
        AddViewListener(SceneToolbarEvent.DELETE, Keyboard_DeleteHandle);
        AddViewListener(SceneToolbarEvent.BRUSH, SceneToolbarBrushHandle);
        AddViewListener(SceneToolbarEvent.VR, SceneToolbarVRHandle);

        AddViewListener(SceneToolbarEvent.GROUP, Keyboard_CombinationHandle);
        AddViewListener(SceneToolbarEvent.REGROUP, Keyboard_CancelCombinationHandle);
        AddViewListener(SceneToolbarEvent.PHOTO, SceneToolbarFilterHandle);

        AddViewListener(SceneToolbarEvent.TO3D, SceneToolbarTo3DHandle);
        AddViewListener(SceneToolbarEvent.TO2D, SceneToolbarTo2DHandle);

        AddViewListener(SceneToolbarEvent.POSTION, SceneToolbarPostionHandle);
        AddViewListener(SceneToolbarEvent.ROTATION, SceneToolbarRotationHandle);
        AddViewListener(SceneToolbarEvent.SCALE, SceneToolbarScaleHandle);

        AddViewListener(SceneToolbarEvent.ALIGN, SceneToolbarAlignHandle);
        AddViewListener(SceneToolbarEvent.FILTER, openDrawLinePanelHandle);
        AddViewListener(SceneToolbarEvent.RENDER, SceneToolbarTakePhotoHandle);
        AddViewListener(SceneToolbarEvent.ADDHOME, SceneToolbarAddHomeHandle);
        AddViewListener(SceneToolbarEvent.ADDITEM, SceneToolbarAddItemHandle);

        AddViewListener(SceneToolbarEvent.LIGHT, SceneToolbarLightHandle);
        AddViewListener(SceneToolbarEvent.SAVE, SceneToolbarSaveHandle);
        AddViewListener(SceneToolbarEvent.LOAD, SceneToolbarLoadHandle);
        AddViewListener(SceneToolbarEvent.CAMERA, CameraChangeHandle);

        AddViewListener(RankToolPanelEvent.CLOSE, RankTool_Close_Handle);
        AddViewListener(RankToolPanelEvent.POSITION_X, RankTool_PositionXHandle);
        AddViewListener(RankToolPanelEvent.POSITION_Y, RankTool_PositionYHandle);
        AddViewListener(RankToolPanelEvent.POSITION_Z, RankTool_PositionZHandle);
        AddViewListener(RankToolPanelEvent.SCALE_X, RankTool_ScaleXHandle);
        AddViewListener(RankToolPanelEvent.SCALE_Y, RankTool_ScaleYHandle);
        AddViewListener(RankToolPanelEvent.SCALE_Z, RankTool_ScaleZHandle);
        AddViewListener(RankToolPanelEvent.ROTATION_X, RankTool_RotationXHandle);
        AddViewListener(RankToolPanelEvent.ROTATION_Y, RankTool_RotationYHandle);
        AddViewListener(RankToolPanelEvent.ROTATION_Z, RankTool_RotationZHandle);

        AddViewListener(SceneToolPanelEvent.Space, OpenCreateSurfaceHandle);
        AddViewListener(SceneToolPanelEvent.Nested, OpenCreateNestedHandle);
        AddViewListener(SceneToolPanelEvent.Item, OpenCreateItemHandle);

        AddContextListener(SceneEvent.TRANSFORM, EventDispatcher_ChangeItemTransformHandler);

        AddContextListener(SceneEvent.CHANGE_GROUP_ITEM, EventDispatcher_ChangeItemHandler);

        AddContextListener(SceneEvent.CHANGE_NESTED, EventDispatcher_ChangeNestedHandler);
        AddContextListener(SceneEvent.CHANGE_ITEM, EventDispatcher_ChangeItemHandler);
        AddContextListener(SceneEvent.CHANGE_BUILD, EventDispatcher_ChangeBuildHandler);
        AddContextListener(SceneEvent.CHANGE_COLLAGE, EventDispatcher_ChangeCollageHandler);
        AddContextListener(SceneEvent.CHANGE_SURFACE, EventDispatcher_ChangeSurfaceHandler);

        AddContextListener(SceneEvent.ADD_ITEM, EventDispatcher_AddObjectHandler);
        AddContextListener(SceneEvent.ADD_SURFACE, EventDispatcher_AddSurfaceHandler);
        AddContextListener(SceneEvent.DELETE, EventDispatcher_DeleteHandler);

        AddContextListener(CameraCommandEvent.UPDATE, UpdateCameraState);

        AddContextListener(UndoRedoEvent.UNDO_LIST_CHANGE, UndoListChangeHandle);
        AddContextListener(FileEvent.LOAD_COMBINATION_COMPLETE, LoadCombinationCompleteHandle);
    }

    public override void OnRemove()
    {
        SceneManager.Instance.control3Manager.removeEventListener(Control3ManagerEvent.TRANSFORM, TransformItem3Handle);
        SceneManager.Instance.control3Manager.removeEventListener(Control3ManagerEvent.COMBINATION, CombinationHandle);
        SceneManager.Instance.control3Manager.removeEventListener(Control3ManagerEvent.RESOLVE, CancelCombinationHandle);

        SceneManager.Instance.controlManager.removeEventListener(ControlManagerEvent.CHANGE_NESTED, ChangeNestedHandle);
        SceneManager.Instance.controlManager.removeEventListener(ControlManagerEvent.CHANGE_LINE, ChangeLine2Handle);
        SceneManager.Instance.controlManager.removeEventListener(ControlManagerEvent.CHANGE_SURFACE, ChangeSurface2Handle);

        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_ITEM, SelectItem3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_LINE, SelectLine3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.SELECT_SURFACE, SelectSurface3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_ITEM, ReleaseItem3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_LINE, ReleaseLine3Handle);
        SceneManager.Instance.mouse3Manager.removeEventListener(Mouse3ManagerEvent.RELEASE_SURFACE, ReleaseSurface3Handle);

        SceneManager.Instance.mouseManager.removeEventListener(MouseManagerEvent.SELECT_OBJECT, SelectObject2Handle);
        SceneManager.Instance.mouseManager.removeEventListener(MouseManagerEvent.RELEASE_OBJECT, ReleaseObject2Handle);

        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.UNDO, Keyboard_UndoHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.REDO, Keyboard_RedoHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.SAVE_PROJECT, Keyboard_SaveHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.LOAD_PROJECT, Keyboard_LoadHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.DELETE, Keyboard_DeleteHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.ESC, Keyboard_EscHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.COPY, Keyboard_CopyHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.PASTE, Keyboard_PasteHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CUT, Keyboard_CutHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.COMBINATION, Keyboard_CombinationHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CANCEL_COMBINATION, Keyboard_CancelCombinationHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.FOCUSON_SELECTION, Keyboard_FocusOnSelectionHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.OPEN_DRAWLINEPANEL, Keyboard_OpenDrawLinePanelHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.OPEN_FILTERPANEL, Keyboard_OpenFilterPanelHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CHANGEVIEW_ONE, Keyboard_ChangeViewOneHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CHANGEVIEW_TWO, Keyboard_ChangeViewTwoHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CHANGEVIEW_THREE, Keyboard_ChangeViewThreeHandle);

        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.OPEN_BRUSH, Keyboard_OpenBrushHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CLOSE_BRUSH, Keyboard_CloseBrushHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CHANGEVIEW_3D, Keyboard_ChangeView3DHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CHANGEVIEW_2D, Keyboard_ChangeView2DHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.OPEN_LIGHT, Keyboard_OpenLightHandle);
        SceneManager.Instance.keyboardManager.removeEventListener(KeyboardManagerEvent.CLOSE_LIGHT, Keyboard_CloseLightHandle);


        RemoveViewListener(LineToolEvent.LineTool_From, LineToolFromHandle);
        RemoveViewListener(LineToolEvent.LineTool_To, LineToolToHandle);
        RemoveViewListener(LineToolEvent.LineTool_Line, LineToolLineHandle);
        RemoveViewListener(LineToolEvent.LineTool_lengthText, LineToolLengthTextHandle);
        RemoveViewListener(LineToolEvent.LineTool_EndEdit_lengthText, LineToolEndEditLengthTextHandle);
        //上方文件工具条
        RemoveContextListener(TopToolPanelEvent.SAVE, SceneToolbarSaveHandle);
        RemoveContextListener(TopToolPanelEvent.LOAD, SceneToolbarLoadHandle);
        RemoveContextListener(TopToolPanelEvent.UNDO, SceneToolbarUndoHandle);
        RemoveContextListener(TopToolPanelEvent.REDO, SceneToolbarRedoHandle);
        RemoveContextListener(TopToolPanelEvent.COPY, Keyboard_CopyHandle);
        RemoveContextListener(TopToolPanelEvent.PASTE, Keyboard_PasteHandle);
        RemoveContextListener(TopToolPanelEvent.DELETE, Keyboard_DeleteHandle);
        RemoveContextListener(SceneToolbarEvent.BRUSH, SceneToolbarBrushHandle);
        RemoveContextListener(TopToolPanelEvent.GROUP, Keyboard_CombinationHandle);
        RemoveContextListener(TopToolPanelEvent.REGROUP, Keyboard_CancelCombinationHandle);
        RemoveContextListener(TopToolPanelEvent.FILTER, SceneToolbarFilterHandle);
        RemoveContextListener(TopToolPanelEvent.ADDITEM, SceneToolbarAddItemHandle);
        RemoveContextListener(TopToolPanelEvent.ADDHOME, SceneToolbarAddHomeHandle);

        RemoveContextListener(TopToolPanelEvent.OPENBRUSH, Keyboard_OpenBrushHandle);
        RemoveContextListener(TopToolPanelEvent.CLOSEBRUSH, Keyboard_CloseBrushHandle);
        RemoveContextListener(TopToolPanelEvent.ALIGN, SceneToolbarAlignHandle);
        RemoveContextListener(TopToolPanelEvent.PHOTO, SceneToolbarTakePhotoHandle);
        RemoveContextListener(TopToolPanelEvent.VIEWONE, Keyboard_ChangeViewOneHandle);
        RemoveContextListener(TopToolPanelEvent.VIEWTWO, Keyboard_ChangeViewTwoHandle);
        RemoveContextListener(TopToolPanelEvent.VIEWTHREE, Keyboard_ChangeViewThreeHandle);
        RemoveContextListener(TopToolPanelEvent.VIEWFOUR, SceneToolbarTo3DHandle);
        RemoveContextListener(TopToolPanelEvent.VIEWFIVE, SceneToolbarTo2DHandle);
        RemoveContextListener(TopToolPanelEvent.OPENLIGHT, Keyboard_OpenLightHandle);
        RemoveContextListener(TopToolPanelEvent.CLOSELIGHT, Keyboard_CloseLightHandle);
        RemoveContextListener(TopToolPanelEvent.OPENDRAWPANEL, openDrawLinePanelHandle);
        RemoveContextListener(TopToolPanelEvent.OPENMODELPANEL, SceneLoadModelHandle);

        //顶部工具条
        RemoveViewListener(SceneToolbarEvent.UNDO, SceneToolbarUndoHandle);
        RemoveViewListener(SceneToolbarEvent.REDO, SceneToolbarRedoHandle);
        RemoveViewListener(SceneToolbarEvent.COPY, Keyboard_CopyHandle);
        RemoveViewListener(SceneToolbarEvent.PASTE, Keyboard_PasteHandle);
        RemoveViewListener(SceneToolbarEvent.DELETE, Keyboard_DeleteHandle);
        RemoveViewListener(SceneToolbarEvent.VR, SceneToolbarVRHandle);
        RemoveViewListener(SceneToolbarEvent.GROUP, Keyboard_CombinationHandle);
        RemoveViewListener(SceneToolbarEvent.REGROUP, Keyboard_CancelCombinationHandle);
        RemoveViewListener(SceneToolbarEvent.PHOTO, SceneToolbarFilterHandle);

        RemoveViewListener(SceneToolbarEvent.TO3D, SceneToolbarTo3DHandle);
        RemoveViewListener(SceneToolbarEvent.TO2D, SceneToolbarTo2DHandle);

        RemoveViewListener(SceneToolbarEvent.POSTION, SceneToolbarPostionHandle);
        RemoveViewListener(SceneToolbarEvent.ROTATION, SceneToolbarRotationHandle);
        RemoveViewListener(SceneToolbarEvent.SCALE, SceneToolbarScaleHandle);

        RemoveViewListener(SceneToolbarEvent.ALIGN, SceneToolbarAlignHandle);
        RemoveViewListener(SceneToolbarEvent.FILTER, openDrawLinePanelHandle);
        RemoveViewListener(SceneToolbarEvent.RENDER, SceneToolbarTakePhotoHandle);
        RemoveViewListener(SceneToolbarEvent.ADDHOME, SceneToolbarAddHomeHandle);
        RemoveViewListener(SceneToolbarEvent.ADDITEM, SceneToolbarAddItemHandle);

        RemoveViewListener(SceneToolbarEvent.LIGHT, SceneToolbarLightHandle);
        RemoveViewListener(SceneToolbarEvent.SAVE, SceneToolbarSaveHandle);
        RemoveViewListener(SceneToolbarEvent.LOAD, SceneToolbarLoadHandle);
        RemoveViewListener(SceneToolbarEvent.CAMERA, CameraChangeHandle);

        RemoveViewListener(SceneToolPanelEvent.Space, OpenCreateSurfaceHandle);
        RemoveViewListener(SceneToolPanelEvent.Nested, OpenCreateNestedHandle);
        RemoveViewListener(SceneToolPanelEvent.Item, OpenCreateItemHandle);

        RemoveContextListener(SceneEvent.ADD_ITEM, EventDispatcher_AddObjectHandler);

        RemoveContextListener(SceneEvent.TRANSFORM, EventDispatcher_ChangeItemHandler);

        RemoveContextListener(SceneEvent.CHANGE_NESTED, EventDispatcher_ChangeNestedHandler);
        RemoveContextListener(SceneEvent.CHANGE_ITEM, EventDispatcher_ChangeItemHandler);
        RemoveContextListener(SceneEvent.CHANGE_BUILD, EventDispatcher_ChangeBuildHandler);
        RemoveContextListener(SceneEvent.CHANGE_COLLAGE, EventDispatcher_ChangeCollageHandler);
        RemoveContextListener(SceneEvent.CHANGE_SURFACE, EventDispatcher_ChangeSurfaceHandler);

        RemoveContextListener(CameraCommandEvent.UPDATE, UpdateCameraState);

        RemoveContextListener(UndoRedoEvent.UNDO_LIST_CHANGE, UndoListChangeHandle);
    }

    private void UndoListChangeHandle(EventObject e)
    {

    }

    private void LoadCombinationCompleteHandle(EventObject e)
    {
        FileEvent fe = e as FileEvent;
        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM, new List<AssetVO>() { null }, CodeManager.LoadCombination(fe.obj as XmlNode)));
    }

    #region Rank
    private void RankTool_Close_Handle(EventObject e)
    {
        (panel as CorePanel).SetActiveRankTool(false);
    }

    private void RankTool_PositionXHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "x");
    }

    private void RankTool_PositionYHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "y");
    }

    private void RankTool_PositionZHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "z");
    }

    private void RankTool_ScaleXHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "sx");
    }

    private void RankTool_ScaleYHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "sy");
    }

    private void RankTool_ScaleZHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "sz");
    }

    private void RankTool_RotationXHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "rx");
    }

    private void RankTool_RotationYHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "ry");
    }

    private void RankTool_RotationZHandle(EventObject e)
    {
        RankTransform(Mouse3Manager.selectionItem, "rz");
    }

    private void RankTransform(List<Item3D> list, string value)
    {
        List<AssetVO> newTranslationAssetVOs = new List<AssetVO>();
        List<AssetVO> oldTranslationAssetVOs = new List<AssetVO>();

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            oldTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }

        if (value == "x") RankManager.RankPositionX(list);
        if (value == "y") RankManager.RankPositionY(list);
        if (value == "z") RankManager.RankPositionZ(list);
        if (value == "sx") RankManager.RankScaleX(list);
        if (value == "sy") RankManager.RankScaleY(list);
        if (value == "sz") RankManager.RankScaleZ(list);
        if (value == "rx") RankManager.RankRotationX(list);
        if (value == "ry") RankManager.RankRotationY(list);
        if (value == "rz") RankManager.RankRotationZ(list);

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            newTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }

        DispatcherEvent(new SceneEvent(SceneEvent.TRANSFORM, oldTranslationAssetVOs, newTranslationAssetVOs));
    }

    #endregion

    #region Keyboard

    private List<AssetVO> _copyItemList;

    private void Keyboard_CopyHandle(EventObject e)
    {
        _copyItemList = new List<AssetVO>();

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            _copyItemList.Add(item.VO);
        }
    }

    private void Keyboard_PasteHandle(EventObject e)
    {
        if (_copyItemList == null)
        {
            return;
        }

        List<AssetVO> list = new List<AssetVO>();

        Hashtable h = new Hashtable();

        foreach (ItemVO vo in _copyItemList)
        {
            ItemVO v = vo.Clone() as ItemVO;
            v.id = NumberUtils.GetGuid();

            if (v.groupId != "")
            {
                if (h[v.groupId] == null)
                {
                    h.Add(v.groupId, NumberUtils.GetGuid());
                }
                v.groupId = h[v.groupId].ToString();
            }

            list.Add(v);
        }

        DispatcherEvent(new SceneEvent(SceneEvent.ADD_ITEM,
           new List<AssetVO>() { null },
           list
           ));
    }

    private void Keyboard_CutHandle(EventObject e)
    {

    }

    private void Keyboard_UndoHandle(EventObject e)
    {
        DispatcherEvent(new UndoRedoEvent(UndoRedoEvent.UNDO));
    }

    private void Keyboard_GroupHandle(EventObject e)
    {
        DispatcherEvent(new KeyboardManagerEvent(KeyboardManagerEvent.COMBINATION));
    }

    private void Keyboard_RedoHandle(EventObject e)
    {
        DispatcherEvent(new UndoRedoEvent(UndoRedoEvent.REDO));
    }

    private void Keyboard_SaveHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.SAVE));
    }

    private void Keyboard_LoadHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.LOAD));
    }

    private void Keyboard_DeleteHandle(EventObject e)
    {
        if (MouseManager.SelectedSurface)
        {
            SceneManager.Instance.mouse3Manager.ClearSelect();

            DispatcherEvent(new SceneEvent(SceneEvent.DELETE,
               new List<AssetVO>() { MouseManager.SelectedSurface.VO },
               new List<AssetVO>() { null }
               ));

            return;
        }

        if (Mouse3Manager.selectionItem != null && Mouse3Manager.selectionItem.Count > 0)
        {
            List<AssetVO> list = new List<AssetVO>();

            foreach (Item3D item in Mouse3Manager.selectionItem)
            {
                list.Add(item.VO);
            }

            DispatcherEvent(new SceneEvent(SceneEvent.DELETE,
               list,
               new List<AssetVO>() { null }
               ));
        }
    }

    private void Keyboard_EscHandle(EventObject e)
    {
        if (CameraManager.visual != CameraFlags.Two)
        {
            SceneManager.Instance.mouse3Manager.ClearSelect();
        }
        if (CameraManager.visual == CameraFlags.Roam)
        {
            CameraManager.ChangeCamera(CameraFlags.Fly);
        }
    }

    private void Keyboard_CombinationHandle(EventObject e)
    {
        SceneManager.Instance.control3Manager.CombinationGroupItem(Mouse3Manager.selectionItem);

        //DispatcherEvent(new FileEvent(FileEvent.SAVE_COMBINATION, "", "", CodeManager.GetCombinationCode(Mouse3Manager.selectionItem)));
    }

    private void Keyboard_CancelCombinationHandle(EventObject e)
    {
        SceneManager.Instance.control3Manager.ResolveGroupItem(Mouse3Manager.selectionItem);
    }

    private void Keyboard_FocusOnSelectionHandle(EventObject e)
    {
        SceneManager.Instance.control3Manager.FocusOnSelection();
    }

    private void Keyboard_OpenDrawLinePanelHandle(EventObject e)
    {
        UIManager.OpenUI(UI.DrawLinePanel);
    }

    private void Keyboard_OpenFilterPanelHandle(EventObject e)
    {
        SceneManager.Instance.mouse3Manager.SelectCamera();
    }

    private void Keyboard_LoadCombinationHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.LOAD_COMBINATION));
    }

    private void Keyboard_ChangeViewOneHandle(EventObject e) {
        CameraManager.ChangeCamera(CameraFlags.Fly);
    }

    private void Keyboard_ChangeViewTwoHandle(EventObject e)
    {
        CameraManager.ChangeCamera(CameraFlags.Roam);
    }

    private void Keyboard_ChangeViewThreeHandle(EventObject e)
    {
        CameraManager.ChangeCamera(CameraFlags.VR);
    }

    private void Keyboard_OpenBrushHandle(EventObject e)
    {
        SceneManager.Instance.OpenBrushHandle();
        SceneToolbarPanel.Instance.IFBrushIsOpenedHandle();
    }

    private void Keyboard_CloseBrushHandle(EventObject e)
    {
        SceneToolbarPanel.Instance.IFBrushIsClosedHandle();
        SceneManager.Instance.CloseBrushHandle();
    }

    private void Keyboard_ChangeView3DHandle(EventObject e)
    {
        SceneToolbarPanel.Instance.IFViewIs3DHandle();
        DispatcherEvent(new CameraCommandEvent(CameraCommandEvent.CHANGE, CameraFlags.Fly));

        SceneManager.Instance.mouseManager.ClearSelect();
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }

    private void Keyboard_ChangeView2DHandle(EventObject e)
    {
        SceneToolbarPanel.Instance.IFViewIs2DHandle();
        DispatcherEvent(new CameraCommandEvent(CameraCommandEvent.CHANGE, CameraFlags.Two));

        SceneManager.Instance.mouseManager.ClearSelect();
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }

    private void Keyboard_OpenLightHandle(EventObject e)
    {
        SceneToolbarPanel.Instance.IFLightIsOpenedHandle();
        SceneManager.Instance.OpenLightHandle();
    }

    private void Keyboard_CloseLightHandle(EventObject e)
    {
        SceneToolbarPanel.Instance.IFLightIsClosedHandle();
        SceneManager.Instance.CloseLightHandle();
    }

    #endregion

    #region CameraState

    private void UpdateCameraState(EventObject e)
    {
        if (CameraManager.visual == CameraFlags.Two)
        {
            (panel as CorePanel).SetActiveSceneToolPanel(true);
        }
        else
        {
            (panel as CorePanel).SetActiveSceneToolPanel(false);
            (panel as CorePanel).SetActiveLineTool(false);
        }
    }

    #endregion

    #region ControlManagerEvent

    private void CombinationHandle(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.CHANGE_GROUP_ITEM,
         (e as Control3ManagerEvent).oldAssetVOs,
         (e as Control3ManagerEvent).newAssetVOs
       ));
    }

    private void CancelCombinationHandle(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.CHANGE_GROUP_ITEM,
         (e as Control3ManagerEvent).oldAssetVOs,
         (e as Control3ManagerEvent).newAssetVOs
       ));
    }

    private void ChangeLine2Handle(EventObject e)
    {
    }

    private void ChangeSurface2Handle(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.CHANGE_SURFACE,
          (e as ControlManagerEvent).oldAssetVOs,
          (e as ControlManagerEvent).newAssetVOs
        ));
    }

    private void ChangeNestedHandle(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.CHANGE_NESTED,
          (e as ControlManagerEvent).oldAssetVOs,
          (e as ControlManagerEvent).newAssetVOs
        ));
    }

    private void TransformItemMove2Handle(EventObject e)
    {

    }

    private void TransformItem2Handle(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.TRANSFORM,
          (e as ControlManagerEvent).oldAssetVOs,
          (e as ControlManagerEvent).newAssetVOs
        ));
    }

    private void TransformItem3Handle(EventObject e)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.TRANSFORM,
          (e as Control3ManagerEvent).oldAssetVOs,
          (e as Control3ManagerEvent).newAssetVOs
        ));
    }

    #endregion

    #region LineTool

    private void ItemToolRemoveHandle(EventObject e)
    {
        if (Mouse3Manager.selectionItem != null && Mouse3Manager.selectionItem.Count > 0)
        {
            List<AssetVO> list = new List<AssetVO>();

            foreach (Item3D item in Mouse3Manager.selectionItem)
            {
                list.Add(item.VO);
            }

            DispatcherEvent(new SceneEvent(SceneEvent.DELETE,
               list,
               new List<AssetVO>() { null }
               ));
        }
    }

    private void ItemToolCombinationHandle(EventObject e)
    {
        SceneManager.Instance.control3Manager.CombinationGroupItem(Mouse3Manager.selectionItem);
    }

    private void ItemToolResolveHandle(EventObject e)
    {
        SceneManager.Instance.control3Manager.ResolveGroupItem(Mouse3Manager.selectionItem);
    }

    private void ItemToolLeftRotateHandle(EventObject e)
    {
        RotateItemY(-30);
    }

    private void ItemToolRightRotateHandle(EventObject e)
    {
        RotateItemY(30);
    }

    private void ItemToolResetRotateHandle(EventObject e)
    {
        RotateItemY(0);
    }

    private void RotateItemY(float value)
    {
        List<AssetVO> newTranslationAssetVOs = new List<AssetVO>();
        List<AssetVO> oldTranslationAssetVOs = new List<AssetVO>();

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            oldTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            if (value != 0)
            {
                item.gameObject.Rotate(new Vector3(0, 1, 0), value, SceneManager.Instance.editorGizmoSystem.TranslationGizmo.transform.position);
            }
            else
            {
                item.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

        foreach (Item3D item in Mouse3Manager.selectionItem)
        {
            newTranslationAssetVOs.Add(AssetsModel.Instance.GetTransformVO(item));
        }

        DispatcherEvent(new SceneEvent(SceneEvent.TRANSFORM, oldTranslationAssetVOs, newTranslationAssetVOs));
    }

    private void LineToolFromHandle(EventObject e)
    {
        SceneManager.Instance.controlManager.SetOperateNode((panel as CorePanel).lineTool.line.fromNode);
    }

    private void LineToolToHandle(EventObject e)
    {
        SceneManager.Instance.controlManager.SetOperateNode((panel as CorePanel).lineTool.line.toNode);
    }

    private void LineToolLineHandle(EventObject e)
    {
        SceneManager.Instance.controlManager.SetOperateLine((panel as CorePanel).lineTool.line);
    }

    private void LineToolLengthTextHandle(EventObject e)
    {
        MouseManager.CanUpSelect = false;
    }

    private void LineToolEndEditLengthTextHandle(EventObject e)
    {
        LineToolEvent le = e as LineToolEvent;
        Debug.Log(le.data);

        SceneManager.Instance.controlManager.ChangeLineLength((panel as CorePanel).lineTool.line, le.data);
    }

    #endregion

    #region CorePanel;
    private bool rankFlag = true;
    private void SceneToolbarHandle(EventObject e)
    {
        //SceneManager.Instance.editorObjectSelection.ClearSelection(false);
        //DispatcherEvent(new SceneEvent(SceneEvent.DELETE, itemsList, null));
    }

    private void SceneToolbarLightHandle(EventObject e)
    {
        SceneManager.Instance.ToggleLightHandle();
    }

    private void SceneToolbarSaveHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.SAVE));
    }

    private void SceneToolbarUndoHandle(EventObject e)
    {
        DispatcherEvent(new UndoRedoEvent(UndoRedoEvent.UNDO));
    }

    private void SceneToolbarRedoHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(UndoRedoEvent.REDO));
    }

    private void SceneToolbarBrushHandle(EventObject e)
    {
        SceneManager.Instance.ToggleBrushMode();
    }

    private void SceneToolbarVRHandle(EventObject e)
    {
        SceneToolbarEvent ee = e as SceneToolbarEvent;
        if (ee.ViewId == 0)
        {
            CameraManager.ChangeCamera(CameraFlags.Fly);
        }
        else if (ee.ViewId == 1)
        {
            CameraManager.ChangeCamera(CameraFlags.Roam);
        }
        else if (ee.ViewId == 2)
        {
            CameraManager.ChangeCamera(CameraFlags.VR);
        }
    }

    private void SceneToolbarTo3DHandle(EventObject e)
    {
        DispatcherEvent(new CameraCommandEvent(CameraCommandEvent.CHANGE, CameraFlags.Fly));

        SceneManager.Instance.mouseManager.ClearSelect();
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }

    private void SceneToolbarTo2DHandle(EventObject e)
    {
        DispatcherEvent(new CameraCommandEvent(CameraCommandEvent.CHANGE, CameraFlags.Two));

        SceneManager.Instance.mouseManager.ClearSelect();
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }

    private void SceneToolbarPostionHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.ActiveGizmoType = RTEditor.GizmoType.Translation;
    }

    private void SceneToolbarRotationHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.ActiveGizmoType = RTEditor.GizmoType.Rotation;
    }

    private void SceneToolbarScaleHandle(EventObject e)
    {
        SceneManager.Instance.editorGizmoSystem.ActiveGizmoType = RTEditor.GizmoType.Scale;
    }

    private void SceneToolbarAlignHandle(EventObject e)
    {
        if (rankFlag)
        {
            rankFlag = false;
            (panel as CorePanel).SetActiveRankTool(true);
        }
        else
        {
            rankFlag = true;
            (panel as CorePanel).SetActiveRankTool(false);
        }
        //SetActiveSceneToolPanel(true);
    }

    private void SceneToolbarFilterHandle(EventObject e)
    {
        SceneManager.Instance.mouse3Manager.SelectCamera();
    }

    private void openDrawLinePanelHandle(EventObject e)
    {
        UIManager.OpenUI(UI.DrawLinePanel);
    }

    private void SceneToolbarAddHomeHandle(EventObject e)
    {
        UIManager.CloseUI(UI.ItemToolPanel);
        UIManager.OpenUI(UI.ChooseSurfacePanel);
    }

    private void SceneToolbarAddItemHandle(EventObject e)
    {
        UIManager.OpenUI(UI.ItemToolPanel);
        UIManager.CloseUI(UI.ChooseSurfacePanel);
    }

    private void SceneToolbarTakePhotoHandle(EventObject e) {
        SceneManager.Instance.TakePhotoHandle();
    }

    private void SceneToolbarLoadHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.OPEN_PROJECT));
    }

    private void SceneLoadModelHandle(EventObject e)
    {
        UIManager.OpenUI(UI.LoadModelPanel);
    }

    private void SceneOpenDrawHandle(EventObject e)
    {
        DispatcherEvent(new FileEvent(FileEvent.OPEN_PROJECT));
    }

    private void CameraChangeHandle(EventObject e)
    {
        //CAMERA CHANGE
    }
    #endregion

    #region SceneToolPanel

    private void OpenCreateSurfaceHandle(EventObject e)
    {
        UIManager.OpenUI(UI.ChooseSurfacePanel);
    }

    private void OpenCreateNestedHandle(EventObject e)
    {
        AssetsModel.Instance.CreateNestedVO("1001", LoadNestedvoComplete);
    }

    private void LoadNestedvoComplete(NestedVO nestedvo)
    {
        DispatcherEvent(new SceneEvent(SceneEvent.ADD_NESTED,
            new List<AssetVO>() { null },
            new List<AssetVO>() { nestedvo }
            ));
    }

    private void OpenCreateItemHandle(EventObject e)
    {
        UIManager.OpenUI(UI.ItemToolPanel);
    }

    public void CompleteNested(ObjectData nestedData)
    {

    }

    #endregion

    #region SceneEvent

    private void EventDispatcher_ChangeBuildHandler(EventObject e)
    {
        Debug.Log(e);
    }

    private void EventDispatcher_ChangeCollageHandler(EventObject e)
    {
        List<AssetVO> assets = (e as SceneEvent).newAssets;

        foreach (AssetVO vo in assets)
        {
            if (vo is SurfaceVO)
            {
                UpdateSurfaceHandle(vo as SurfaceVO);
            }
            else if (vo is LineVO)
            {
                UpdateLineHandle(vo as LineVO);
            }
        }
    }

    private void EventDispatcher_ChangeSurfaceHandler(EventObject e)
    {
        SurfaceVO vo = (e as HistoryEvent).newAssets[0] as SurfaceVO;
        ApplySurface(vo.Clone() as SurfaceVO);

    }

    private void EventDispatcher_DeleteHandler(EventObject e)
    {
        SceneManager.Instance.mouse3Manager.ClearSelect();
    }

    private List<Item3D> itemsList;

    private void SelectItem3Handle(EventObject value)
    {
        itemsList = new List<Item3D>();

        Mouse3ManagerEvent e = value as Mouse3ManagerEvent;
        foreach (object o in e.objects)
        {
            if (o is Item3D)
            {
                Item3D item3 = o as Item3D;
                itemsList.Add(item3);

                if (e.objects.Count == 1)
                {
                    SceneManager.Instance.EditorCamera.GetComponent<RulerManager>().item = item3;
                }
            }
        }

        if (itemsList.Count > 0)
        {
            CorePanel corePanel = UIManager.GetUI(UI.CorePanel) as CorePanel;
            corePanel.SetActiveItemTool(true);
            corePanel.itemTool.items = itemsList;
        }
    }

    private void SelectLine3Handle(EventObject e)
    {

    }
    private void SelectSurface3Handle(EventObject e)
    {
    }

    private void ReleaseItem3Handle(EventObject e)
    {
        CorePanel corePanel = UIManager.GetUI(UI.CorePanel) as CorePanel;
        corePanel.SetActiveItemTool(false);
    }
    private void ReleaseLine3Handle(EventObject e)
    {
    }
    private void ReleaseSurface3Handle(EventObject e)
    {
    }

    private void DownObject2Handle(EventObject e)
    {
        MouseManagerEvent me = e as MouseManagerEvent;

        if (me.objectSprite is Plane2D)
        {

        }
    }

    private void SelectObject2Handle(EventObject e)
    {
        MouseManagerEvent me = e as MouseManagerEvent;

        if (me.objectSprite is Line2D)
        {
        }
        else if (me.objectSprite is Surface2D)
        {

        }
        else if (me.objectSprite is Plane2D)
        {

        }
        else if (me.objectSprite is Item2D)
        {
            SpotlightVO spotlightVO = (me.objectSprite as Item2D).VO.GetComponentVO<SpotlightVO>();
            if (spotlightVO != null)
            {
                //(UIManager.OpenPanel(UI.SpotlightComponentUI) as SpotlightComponentUI).spotlightVO = spotlightVO;
            }
        }
        else if (me.objectSprite is Nested2D)
        {

        }
    }

    private void ReleaseObject2Handle(EventObject e)
    {
    }

    private void EventDispatcher_AddSurfaceHandler(EventObject e)
    {
        foreach (ObjectData obj in AssetsModel.Instance.itemDatas)
        {
            TubeLightComponent light = obj.object3.GetComponentInChildren<TubeLightComponent>();
            if (light)
            {
                light.UpdateShadowPlanes();
            }
        }
    }

    private void EventDispatcher_AddObjectHandler(EventObject e)
    {
        SceneEvent se = e as SceneEvent;

        if (se.isUndoAction)
        {
            SceneManager.Instance.mouse3Manager.ClearSelect();
            return;
        }

        List<GameObject> lists = new List<GameObject>();
        foreach (AssetVO vo in se.newAssets)
        {
            lists.Add(AssetsModel.Instance.GetObjectData(vo.id).object3.gameObject);
        }

        if (CameraManager.visual != CameraFlags.Two && SceneManager.Instance.brushManager.brushMode == BrushManager.BrushMode.Place)
        {
            SceneManager.Instance.mouse3Manager.ClearSelect();
            SceneManager.Instance.editorObjectSelection.SetSelectedObjects(lists, false);
        }
    }

    private void EventDispatcher_ChangeNestedHandler(EventObject e)
    {
        List<AssetVO> assets = (e as SceneEvent).newAssets;

        foreach (AssetVO vo in assets)
        {
            if (vo is ObjectVO)
            {
                UpdateObjectHandle(vo as ObjectVO);
            }
            if (vo is LineVO)
            {
                UpdateLineHandle(vo as LineVO);
            }
        }
    }

    private void EventDispatcher_ChangeItemTransformHandler(EventObject e)
    {
        List<AssetVO> assets = (e as SceneEvent).newAssets;

        for (int i = 0; i < assets.Count; i++)
        {
            UpdateObjectHandle(AssetsModel.Instance.GetObjectData(assets[i].id).object3.VO as ObjectVO);
        }

        if ((e as SceneEvent).isRedoAction == true || (e as SceneEvent).isUndoAction == true)
        {
            SceneManager.Instance.editorGizmoSystem.EstablishActiveGizmoPosition();
        }
    }

    private void EventDispatcher_ChangeItemHandler(EventObject e)
    {
        List<AssetVO> assets = (e as SceneEvent).newAssets;

        foreach (AssetVO vo in assets)
        {
            if (vo is ObjectVO)
            {
                UpdateObjectHandle(vo as ObjectVO);
            }
        }

        if ((e as SceneEvent).isRedoAction == true || (e as SceneEvent).isUndoAction == true)
        {
            SceneManager.Instance.editorGizmoSystem.EstablishActiveGizmoPosition();
        }
    }

    #endregion

    #region Update

    private void UpdateLineHandle(LineVO vo)
    {
        ApplyLine(vo as LineVO);
    }

    private void UpdateSurfaceHandle(SurfaceVO vo)
    {
        ApplySurface(vo as SurfaceVO);
    }

    private void UpdateBuildHandle(BuildVO vo)
    {

    }

    private void UpdateObjectHandle(ObjectVO vo)
    {
        if (vo is ItemVO)
        {
            ApplyItem(vo as ItemVO);
        }
        if (vo is NestedVO)
        {
            ApplyNested(vo as NestedVO);
        }
    }

    #endregion

    #region Apply

    private void ApplyItem(ItemVO vo)
    {
        ObjectData data = AssetsModel.Instance.GetObjectData(vo.id);
        (data.object2 as Item2D).VO = vo;
        (data.object3 as Item3D).VO = vo;
        data.vo = vo;
    }

    private void ApplyNested(NestedVO vo)
    {
        ObjectData data = AssetsModel.Instance.GetObjectData(vo.id);
        (data.object2 as Nested2D).VO = vo;
        (data.object3 as Nested3D).VO = vo;
        data.vo = vo;

        SceneManager.Instance.controlManager.UpdateNestedOnLine(data.object2 as Nested2D);
    }

    public void ApplyLine(LineVO vo)
    {
        LineData data = BuilderModel.Instance.GetLineData(vo.id);
        data.line.VO = vo;
        data.line3.VO = vo;
        data.vo = vo;
    }

    public void ApplySurface(SurfaceVO vo)
    {
        SurfaceData surfaceData = BuilderModel.Instance.GetSurfaceData(vo.id);

        foreach (LineVO linevo in vo.linesVO)
        {
            ApplyLine(linevo);
        }
        foreach (LineVO linevo in vo.linesVO)
        {
            LineData data = BuilderModel.Instance.GetLineData(linevo.id);
            data.line.UpdateNow();
        }

        surfaceData.surface.VO = vo;
        surfaceData.surface3.VO = vo;
        surfaceData.vo = vo;
    }

    #endregion
}
