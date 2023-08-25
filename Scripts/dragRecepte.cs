using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class dragRecepte: MonoBehaviour //, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler ,IPointerClickHandler
{
    /*
    public static dragRecepte active;
    public Vector2 position;
    public bool doubleClickActive;
    ItemsSelector Selector;
    public Component value;

    public UnityAction onPointer;
    public UnityAction onClick;
    public UnityAction OnEndDrag;

    public void Start()
    {
        if (TryGetComponent(out Canvas canvas))
        {
            canvas.overrideSorting = false;
        }
        Selector = GetComponentInParent<ItemsSelector>();
    }
    void OnDestroy()
    {
        //if (CraftingConteiner.Instance != null)
        //{
        //    CraftingConteiner.Instance.DelateItemOnDestroy(IM.titem);
        //}
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onPointer?.Invoke();
        if (!doubleClickActive)
        {
            Invoke("closeDoubleClick",0.2f);
            doubleClickActive = true;
        }
        else
        {
            if (Items.actualItems.IndexOf(IM.titem) != -1)
            {
                receptes.actualItems[receptes.actualItems.IndexOf(IM.titem)] = null;
            }
            receptes.loadItems();
            Destroy(gameObject);
        }    

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointer?.Invoke();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(Selector.transform);
        active = this;
        if(TryGetComponent(out Canvas canvas))
        {
            canvas.overrideSorting = true;
        }
        position = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = position;
        Selector.itemDropped();
        if(transform.parent == Selector)
        {
            transform.SetParent(receptes.availableItemsPlace.transform);
        }
        active = null;
        if (TryGetComponent(out Canvas canvas))
        {
            canvas.overrideSorting = false;
        }
    }
   void closeDoubleClick()
    {
        doubleClickActive = false;
    }
    */
}
