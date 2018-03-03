using BuildManager;
using System.Collections.Generic;
using UnityEngine;
using Build2D;

public class MouseManager : EventDispatcher
{
    public static Surface2D SelectedSurface;
    public static Line2D SelectedLine;
    public static Item2D SelectedItem;
    public static Nested2D SelectedNested;
    public static Plane2D SelectedPlane;

    public static Surface2D DownSurface;
    public static Line2D DownLine;
    public static Item2D DownItem;
    public static Nested2D DownNested;
    public static Plane2D DownPlane;

    public static GameObject overGameObject;
    public static GameObject clickGameObject;

    public static float lastMousePointX;
    public static float lastMousePointY;

    public void ClearSelect()
    {
        SelectedSurface = null;
        SelectedLine = null;
        SelectedItem = null;
        SelectedNested = null;
        SelectedPlane = null;

        ClearHighlighter();
    }

    private void ClearHighlighter()
    {
        foreach (ObjectData data in AssetsModel.Instance.itemDatas)
        {
            data.object2.Selected = false;
        }

        foreach (LineData data in BuilderModel.Instance.lineDatas)
        {
            data.line.Selected = false;
        }

        foreach (SurfaceData data in BuilderModel.Instance.surfaceDatas)
        {
            data.surface.Selected = false;
        }
    }

    public static bool CanUpSelect = true;

    public void Update()
    {
        if (SceneManager.Instance.Camera2D == null) return;

        overGameObject = null;

        if (Input.GetMouseButtonUp(0))
        {
            DownItem = null;
            DownNested = null;
            DownSurface = null;
            DownLine = null;
            DownPlane = null;

            ClearHighlighter();
        }

        LayerMask layerGraphic2D = 1 << LayerMask.NameToLayer("Graphic2D");

        RaycastHit2D hit = Physics2D.Raycast(SceneManager.Instance.Camera2D.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 999999, layerGraphic2D);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Graphic2D")
            {
                overGameObject = null;
            }
            else
            {
                overGameObject = hit.collider.gameObject;
            }

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePointX = Input.mousePosition.x;
                lastMousePointY = Input.mousePosition.y;

                if (overGameObject)
                {
                    DownItem = overGameObject.GetComponentInParent<Item2D>();
                    DownNested = overGameObject.GetComponentInParent<Nested2D>();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                clickGameObject = overGameObject;

                if (clickGameObject)
                {
                    SelectedItem = clickGameObject.GetComponentInParent<Item2D>();
                    SelectedNested = clickGameObject.GetComponentInParent<Nested2D>();
                }

                if (SelectedItem)
                {
                    SelectedItem.Selected = true;
                    dispatchEvent(new MouseManagerEvent(MouseManagerEvent.SELECT_OBJECT, SelectedItem));
                }
                else if (SelectedNested)
                {
                    SelectedNested.Selected = true;
                    dispatchEvent(new MouseManagerEvent(MouseManagerEvent.SELECT_OBJECT, SelectedNested));
                }
                else
                {
                    dispatchEvent(new MouseManagerEvent(MouseManagerEvent.RELEASE_OBJECT, null));
                }
            }
        }

        if (!overGameObject)
        {
            Ray ray = SceneManager.Instance.Camera2D.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit3;

            if (Physics.Raycast(ray, out hit3, Mathf.Infinity, layerGraphic2D))
            {
                if (hit3.collider != null && hit3.collider.gameObject.name == "Graphic2D")
                {
                    overGameObject = null;
                }
                else
                {
                    overGameObject = hit3.collider.gameObject;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePointX = Input.mousePosition.x;
                lastMousePointY = Input.mousePosition.y;

                if (overGameObject)
                {
                    DownSurface = overGameObject.GetComponent<Surface2D>();
                    DownLine = overGameObject.GetComponent<Line2D>();
                    DownPlane = overGameObject.GetComponent<Plane2D>();
                }

                if (DownPlane)
                {
                    dispatchEvent(new MouseManagerEvent(MouseManagerEvent.DOWN_OBJECT, DownPlane));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (CanUpSelect)
                {
                    clickGameObject = overGameObject;

                    if (clickGameObject)
                    {
                        SelectedLine = clickGameObject.GetComponent<Line2D>();
                        SelectedSurface = clickGameObject.GetComponent<Surface2D>();
                        SelectedPlane = clickGameObject.GetComponent<Plane2D>();
                    }

                    if (SelectedLine)
                    {
                        SelectedLine.Selected = true;
                        dispatchEvent(new MouseManagerEvent(MouseManagerEvent.SELECT_OBJECT, SelectedLine));
                    }
                    else if (SelectedSurface)
                    {
                        SelectedSurface.Selected = true;
                        dispatchEvent(new MouseManagerEvent(MouseManagerEvent.SELECT_OBJECT, SelectedSurface));
                    }
                    else if (SelectedPlane)
                    {
                        dispatchEvent(new MouseManagerEvent(MouseManagerEvent.SELECT_OBJECT, SelectedPlane));
                    }
                    else
                    {
                        dispatchEvent(new MouseManagerEvent(MouseManagerEvent.RELEASE_OBJECT, null));
                    }
                }
                else
                {
                    CanUpSelect = true;
                }
            }
        }
    }
}
