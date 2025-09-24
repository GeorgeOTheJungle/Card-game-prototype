using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragableObject : MonoBehaviour
{
    Vector3 m_originalPosition;
    private Vector3 m_offset;

    private Vector3 m_screenPoint;

    public UnityEvent OnCardDrag;
    public UnityEvent OnCardDrop;

    private void Start()
    {
        m_originalPosition = transform.position;
    }

    public void InitializeCard()
    {
        m_originalPosition = transform.position; 
    }

    private void OnMouseDown()
    {
        m_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        m_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenPoint.z));
    }
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + m_offset;
        transform.position = curPosition;

        OnCardDrag?.Invoke();
    }

    private void OnMouseUp()
    {
        OnCardDrop?.Invoke();
        if (transform.position != m_originalPosition)
        {
            transform.position = m_originalPosition;
        }
    }
}
