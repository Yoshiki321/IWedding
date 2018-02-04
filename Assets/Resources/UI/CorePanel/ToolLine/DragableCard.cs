using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DragableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public float xDis;
	public float yDis;

    // begin dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
		SetDraggedPosition(eventData);
    }

    // during dragging
    public void OnDrag(PointerEventData eventData)
    {
		SetDraggedPositionIn (eventData);
    }

    // end dragging
    public void OnEndDrag(PointerEventData eventData)
    {
		SetDraggedPosition(eventData);
    }

    /// <summary>
    /// set position of the dragged game object
    /// </summary>
    /// <param name="eventData"></param>

    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        Vector3 globalMousePos;
		xDis = rt.anchoredPosition3D.x - eventData.position.x;
		yDis = rt.anchoredPosition3D.y - eventData.position.y;
    }
	private void SetDraggedPositionIn(PointerEventData eventData)
	{
		gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(eventData.position.x + xDis,eventData.position.y + yDis);
	}
}