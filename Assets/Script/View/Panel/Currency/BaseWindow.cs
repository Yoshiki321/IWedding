using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseWindow : DispatcherEventPanel, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region drag

    private Vector2 _lastMouse;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastMouse = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        _lastMouse = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = view.gameObject.GetComponent<RectTransform>();

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position -= _lastMouse.ToVector3() - globalMousePos;
        }
    }

    #endregion

    protected Button exitBtn;
    protected Transform view;

    public virtual void Init()
    {
        view = transform.Find("View");
        exitBtn = transform.Find("View").Find("ExitBtn").GetComponent<Button>();

        exitBtn.onClick.AddListener(ExitHandle);
    }

    public virtual void SetContent(object value)
    {

    }

    private void ExitHandle()
    {
        Close();

        UIManager.ClosePanel(name);
    }

    void Update()
    {

    }

    protected virtual void Close()
    {

    }
}
