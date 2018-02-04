using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using BuildManager;
using System.Collections.Generic;
using Build3D;

public class Mouse3Manager : EventDispatcher
{
    private static ArrayList selectionObject;
    private static List<ItemVO> selectionItemVO = new List<ItemVO>();
    public static List<Item3D> selectionItem = new List<Item3D>();

    public Mouse3Manager()
    {
        InitEditorEvent();
    }

    #region Editor

    private void InitEditorEvent()
    {
        SceneManager.Instance.editorObjectSelection.SelectionChanged += new RTEditor.EditorObjectSelection.SelectionChangedHandler(SelectionChanged);
    }

    private bool isSelect = true;

    private void SelectionChanged(RTEditor.ObjectSelectionChangedEventArgs selectionChangedEventArgs)
    {
        if (SceneManager.Instance.editorObjectSelection.SelectedGameObjects.Count == 0)
        {
            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_ITEM, selectionObject));
            SceneManager.Instance.EditorCamera.GetComponent<RulerManager>().item = null;
            return;
        }

        if (nowDouble)
        {
            nowDouble = false;
            return;
        }

        if (selectionChangedEventArgs.SelectedObjects.Count == 1)
        {
            GameObject obj = selectionChangedEventArgs.SelectedObjects[0];
            selectedSurface = obj.GetComponentInParent<Surface3D>();
            selectedLine = obj.GetComponentInParent<Line3D>();

            if (selectedSurface)
            {
                ClearSelect();
                selectionItemVO = new List<ItemVO>();
                selectedSurface.Selected = true;
                SceneManager.Instance.EditorCamera.GetComponent<RulerManager>().item = null;
                dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_ITEM, selectionObject));
                dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_SURFACE, new ArrayList { selectedSurface }));
                return;
            }
            else if (selectedLine)
            {
                ClearSelect();
                selectionItemVO = new List<ItemVO>();
                selectedLine.Selected = true;
                SceneManager.Instance.EditorCamera.GetComponent<RulerManager>().item = null;
                dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_ITEM, selectionObject));
                dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_LINE, new ArrayList { selectedLine }));
                return;
            }
        }

        if (selectedSurface)
        {
            ClearBuildSelect();
            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_SURFACE, null));
            selectedSurface = null;
        }

        if (selectedLine)
        {
            ClearBuildSelect();
            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_LINE, null));
            selectedLine = null;
        }

        selectionObject = new ArrayList();
        selectionItemVO = new List<ItemVO>();
        selectionItem = new List<Item3D>();
        List<GameObject> objSelectionItem = new List<GameObject>();

        foreach (GameObject obj in SceneManager.Instance.editorObjectSelection.SelectedGameObjects)
        {
            Item3D item3 = obj.GetComponentInParent<Item3D>();
            if (item3)
            {
                if (!ArrayUtils.Has(selectionObject, item3))
                {
                    selectionItemVO.Add(item3.GetComponent<Item3D>().VO as ItemVO);
                }
            }
        }

        List<ItemVO> vos = GetGroupItem(selectionItemVO);

        selectionItemVO = new List<ItemVO>();
        foreach (ItemVO itemvo in vos)
        {
            ObjectData data = AssetsModel.Instance.GetItemData(itemvo.id);
            if (data != null)
            {
                Item3D item3 = data.object3 as Item3D;
                if (item3.SelectEnabled)
                {
                    objSelectionItem.Add(item3.gameObject);
                    selectionItem.Add(item3);
                    selectionItemVO.Add(itemvo);
                    selectionObject.Add(item3);

                    if (vos.Count == 1)
                    {
                        lastMousePointX = Input.mousePosition.x;
                        lastMousePointY = Input.mousePosition.y;
                        count = 0;
                    }
                }
            }
        }

        if (isSelect)
        {
            isSelect = false;
            ClearSelect();
            SceneManager.Instance.editorObjectSelection.SetSelectedObjects(objSelectionItem, false);
        }
        else
        {
            isSelect = true;
        }

        dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_ITEM, selectionObject));
    }

    private void DoubleClick()
    {
        if (selectionItemVO.Count == 0) return;
        List<GameObject> objSelectionItem = new List<GameObject>();
        selectionObject = new ArrayList();
        selectionItem = new List<Item3D>();
        foreach (ObjectData d in AssetsModel.Instance.itemDatas)
        {
            if ((d.object3.VO as ItemVO).assetId == selectionItemVO[0].assetId)
            {
                objSelectionItem.Add(d.object3.gameObject);
                selectionObject.Add(d.object3);
                selectionItem.Add(d.object3 as Item3D);
            }
        }

        isSelect = false;
        nowDouble = true;
        SceneManager.Instance.editorObjectSelection.SetSelectedObjects(objSelectionItem, false);
        dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_ITEM, selectionObject));
    }

    public List<ItemVO> GetGroupItem(List<ItemVO> assets)
    {
        List<string> ids = new List<string>();
        List<ItemVO> vos = new List<ItemVO>();
        foreach (ItemVO vo in assets)
        {
            if (vo.groupId == "")
            {
                vos.Add(vo);
            }
            else
            {
                if (!ArrayUtils.Has(ids, vo.groupId))
                {
                    ids.Add(vo.groupId);
                    vos.AddRange(GetGroupItem(vo.groupId));
                }
            }
        }

        return vos;
    }

    public List<ItemVO> GetGroupItem(string id)
    {
        List<ItemVO> lists = new List<ItemVO>();

        foreach (ObjectData ob in AssetsModel.Instance.itemDatas)
        {
            if ((ob.vo as ItemVO).groupId == id)
            {
                lists.Add(ob.vo as ItemVO);
            }
        }

        return lists;
    }

    public void SelectCamera()
    {
        dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_ITEM, new ArrayList() { SceneManager.Instance.EditorCamera.GetComponent<SceneCamera3D>() }));
    }

    #endregion

    #region Clear

    public void ClearSelect()
    {
        ClearItemSelect();
        ClearBuildSelect();
    }

    public void ClearItemSelect()
    {
        SceneManager.Instance.editorObjectSelection.ClearSelection(false);
        SceneManager.Instance.EditorCamera.GetComponent<RulerManager>().item = null;
    }

    public void ClearBuildSelect()
    {
        foreach (Line3D l in BuilderModel.Instance.line3s)
        {
            l.Selected = false;
        }

        foreach (SurfaceData sData in BuilderModel.Instance.surfaceDatas)
        {
            sData.surface3.Selected = false;
        }
    }

    #endregion;

    private float time;
    private bool isTime;
    private bool nowDouble;
    private bool canDouble;
    public bool canClick;
    private float count = 0;

    public static Surface3D selectedSurface;
    public static Line3D selectedLine;

    public static Surface3D downSurface;
    public static Line3D downLine;

    public static float lastMousePointX;
    public static float lastMousePointY;

    public static GameObject overGameObject;

    public void Update()
    {
        if (canDouble) count++;
        if (isTime) time++;

        if (Input.GetMouseButtonDown(0) && count < 20 && count > 5 && canClick)
        {
            if (lastMousePointX == Input.mousePosition.x && lastMousePointY == Input.mousePosition.y)
            {
                canDouble = false;
                isTime = true;
                SceneManager.Instance.editorObjectSelection.isCanOperate = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && selectionItemVO.Count == 1 &&
            (Input.mousePosition.x > 350 && Input.mousePosition.x < 1520) &&
             (Input.mousePosition.y < 1000))
        {
            count = 0;
            canDouble = true;
            canClick = true;
            lastMousePointX = Input.mousePosition.x;
            lastMousePointY = Input.mousePosition.y;
        }

        if (time > 1)
        {
            time = 0;
            isTime = false;
            DoubleClick();
            SceneManager.Instance.editorObjectSelection.isCanOperate = true;
        }

        #region Build

        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        //else
        //{
        //}

        //overGameObject = null;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    lastMousePointX = Input.mousePosition.x;
        //    lastMousePointY = Input.mousePosition.y;

        //    downSurface = null;
        //    downLine = null;
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (Mathf.Abs(lastMousePointX - Input.mousePosition.x) < 1 &&
        //        Mathf.Abs(lastMousePointY - Input.mousePosition.y) < 1)
        //    {
        //        selectionObject = new ArrayList();
        //        ClearBuildSelect();
        //    }
        //}

        //Ray ray = SceneManager.Instance.Camera3D.ScreenPointToRay(Input.mousePosition);
        //LayerMask layer = 1 << LayerMask.NameToLayer("Build3D");
        //RaycastHit hit;

        //bool p = Physics.Raycast(ray, out hit, 9999999);

        //if (p) overGameObject = hit.collider.gameObject;

        //if (Input.GetMouseButtonDown(0) && p)
        //{
        //    downSurface = hit.collider.gameObject.GetComponentInParent<Surface3D>();
        //    downLine = hit.collider.gameObject.GetComponentInParent<Line3D>();
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (p)
        //    {
        //        if (Mathf.Abs(lastMousePointX - Input.mousePosition.x) < 1 &&
        //        Mathf.Abs(lastMousePointY - Input.mousePosition.y) < 1)
        //        {
        //            selectedSurface = hit.collider.gameObject.GetComponentInParent<Surface3D>();
        //            selectedLine = hit.collider.gameObject.GetComponentInParent<Line3D>();
        //        }

        //        if (selectedSurface)
        //        {
        //            selectionItem = new List<ItemVO>();
        //            selectedSurface.Selected = true;
        //            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_SURFACE, new ArrayList { selectedSurface }));
        //        }
        //        else if (selectedLine)
        //        {
        //            selectionItem = new List<ItemVO>();
        //            selectedLine.Selected = true;
        //            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.SELECT_LINE, new ArrayList { selectedLine }));
        //        }
        //        else
        //        {
        //            ClearBuildSelect();

        //            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_SURFACE, null));
        //            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_LINE, null));
        //        }
        //    }
        //    else
        //    {
        //        if (downSurface == null && downLine == null)
        //        {
        //            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_SURFACE, null));
        //            dispatchEvent(new Mouse3ManagerEvent(Mouse3ManagerEvent.RELEASE_LINE, null));
        //        }
        //    }
        //}

        #endregion
    }
}
