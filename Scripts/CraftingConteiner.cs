using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingConteiner : MonoBehaviour
{
    /*
    public static CraftingConteiner Instance;
    public List<dragRecepte> dragReceptes = new List<dragRecepte>();
    public Button craftButton;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
      
    }

    void Update()
    {

    }
    public void itemDropped()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect rect = rectTransform.rect;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) != null && transform.GetChild(i).TryGetComponent(out RectTransform rt))
            {

                if (inSquere(new Vector2(rt.rect.width, rt.rect.height), Input.mousePosition, transform.GetChild(i).position))
                {
                    if (dragRecepte.active != null && dragRecepte.active.TryGetComponent(out ItemMenu IM) && receppers.actualItems.Count > i)char 
                    {
                        if(dragReceptes[i] != null)
                        {
                            Destroy(dragReceptes[i].gameObject);
                        }
                        receppers.actualItems[i] = IM.titem;
                        dragRecepte.active.transform.SetParent(transform.GetChild(i));
                        dragRecepte.active.transform.localPosition = Vector2.zero;
                        dragReceptes[i] = dragRecepte.active;
                        receppers.loadItems();
                        setButtonState();
                        return;
                    }
                =}
            }

        }
        if (inSquere(new Vector2(rect.width, rect.height), Input.mousePosition, transform.position))
        {
            if (dragRecepte.active != null && dragRecepte.active.TryGetComponent(out ItemMenu IM) && IM.titem != lastHanginItem)
            {
                for (int i = 0; i < receppers.actualItems.Count; i++)
                {
                    if (receppers.actualItems[i] == null )
                    {
                        receppers.actualItems[i] = IM.titem;
                        dragRecepte.active.transform.SetParent(transform.GetChild(i));
                        dragRecepte.active.transform.localPosition = Vector2.zero;
                        receppers.loadItems();
                        setButtonState();
                        return;
                    }
                }

            }
        }
        pickUpItem();
        setButtonState();
        
    }
    void setButtonState()
    {
        for (int i = 0; i < receppers.actualItems.Count; i++)
        {
            if(receppers.actualItems[i] == null)
            {
                craftButton.interactable = false;
                return;
            }
        }
        craftButton.interactable = true;
    }
    
    public void pickUpItem()
    {
        craftButton.interactable = false;
        if (dragRecepte.active == null) return;
        
        dragRecepte.active.transform.SetParent(receppers.availableItemsPlace.transform);

        if (dragRecepte.active.TryGetComponent(out ItemMenu IM))
        {
            if (receppers.actualItems.IndexOf(IM.titem) != -1)
            {
                receppers.actualItems[receppers.actualItems.IndexOf(IM.titem)] = null;
                
            }
            
        }
        setButtonState();
        Destroy(dragRecepte.active.gameObject);
        receppers.loadItems();
    }
    public void unlinkObject()
    {
        
        if (dragRecepte.active != null && dragRecepte.active.TryGetComponent(out ItemMenu IM))
        {
            if (receppers.actualItems.IndexOf(IM.titem) != -1)
            {
                lastHanginItem = IM.titem;
                receppers.actualItems[receppers.actualItems.IndexOf(IM.titem)] = null;
            }
        }
        setButtonState();
    }
    public void DelateItemOnDestroy(Item it)
    {
        if (receppers.actualItems.IndexOf(it) != -1)
        {
            receppers.actualItems[receppers.actualItems.IndexOf(it)] = null;

        }
        setButtonState();
    }
    
    */
}
