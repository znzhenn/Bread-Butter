using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot originalSlot = originalParent.GetComponent<Slot>();

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();

        if(dropSlot == null && eventData.pointerEnter != null)
        {
            dropSlot = eventData.pointerEnter.GetComponentInParent<Slot>();
        }

        // invalid drop → return
        if(dropSlot == null)
        {
            transform.SetParent(originalParent);

            RectTransform rt = GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.anchoredPosition = Vector2.zero;

            return;
        }

        // SAME SLOT
        if(dropSlot == originalSlot)
        {
            transform.SetParent(originalSlot.transform);

            RectTransform rt = GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.anchoredPosition = Vector2.zero;

            return;
        }

        GameObject draggedItem = gameObject;
        GameObject targetItem = dropSlot.currentItem;

        // swap target item back into original slot
        if(targetItem != null)
        {
            targetItem.transform.SetParent(originalSlot.transform);

            RectTransform targetRT = targetItem.GetComponent<RectTransform>();
            targetRT.localPosition = Vector3.zero;
            targetRT.anchoredPosition = Vector2.zero;

            originalSlot.currentItem = targetItem;
        }
        else
        {
            originalSlot.currentItem = null;
        }

        // move dragged item into new slot
        transform.SetParent(dropSlot.transform);

        RectTransform draggedRT = GetComponent<RectTransform>();
        draggedRT.localPosition = Vector3.zero;
        draggedRT.anchoredPosition = Vector2.zero;

        dropSlot.currentItem = draggedItem;
    }
}