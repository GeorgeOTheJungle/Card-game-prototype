using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 m_originalPosition;
    private Vector3 offset;
    private RectTransform rect;

    private void Start()
    {
        m_originalPosition = transform.position;
    }

    public void InitializeCard()
    {
        m_originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MouseWorldPosition(eventData);
    }

    // If card moves detects the board, then transform to a preview unit
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = MouseWorldPosition(eventData) + offset;

        //Vector2 mousePosition = eventData.position;
        //var worldPosition = Camera.main.ScreenPointToRay(mousePosition);
        //RaycastHit2D hit;
        //if (Physics2D.Raycast(worldPosition, Vector2.up, Mathf.Infinity, 0)
        //{

        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Validate if the card is over the board, if not, return to original position

    }

    private Vector3 MouseWorldPosition(PointerEventData eventData)
    {
        var mouseScreenPos = eventData.position;
        mouseScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
