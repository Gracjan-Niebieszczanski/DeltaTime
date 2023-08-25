using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemsSelector : MonoBehaviour
{
    /*
    public List<Component> items;

    public List<GameObject> targetPlaces;

    public UnityAction onChange;

    public List<GameObject> objectsIndide;

    private void Start()
    {
        
    }
    public void OnChange()
    {
        onChange?.Invoke();
    }
    public void itemDropped()
    {

    }
    public void clearIndex(GameObject targetObject)
    {

    }
    public void OnDrawGizmosSelected()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect rect = rectTransform.rect;
        Gizmos.color = Color.blue;
        bool selectedChildren = false;
        for (int i = 0; i < targetPlaces.Count; i++)
        {
            if (targetPlaces[i] != null && targetPlaces[i].TryGetComponent(out RectTransform rt))
            {

                if (inSquere(new Vector2(rt.rect.width, rt.rect.height), Input.mousePosition, rt))
                {
                    Gizmos.color = Color.red;
                    selectedChildren = true;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawWireCube(new Vector2(rt.position.x, rt.position.y - (rt.rect.height / 2)), new Vector2(rt.rect.width, rt.rect.height));
            }

        }
        if (!selectedChildren && inSquere(new Vector2(rect.width, rect.height), Input.mousePosition, rectTransform))
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawWireCube(new Vector2(rectTransform.position.x, rectTransform.position.y - (rectTransform.rect.height / 2)), new Vector2(rectTransform.rect.width, rectTransform.rect.height));
    }

    bool inSquere(Vector2 squere, Vector2 point, RectTransform rt)
    {
        Vector2 halfSize = squere / 2;

        Vector2 position = new Vector2(rt.position.x, rt.position.y - (rt.rect.height / 2));
        Vector4 squerePoints = new Vector4
        (
            position.x - halfSize.x,
            position.x + halfSize.x,
            position.y - halfSize.y,
            position.y + halfSize.y
        );
        return point.x >= squerePoints.x && point.x <= squerePoints.y && point.y >= squerePoints.z && point.y <= squerePoints.w;
    }
    */
}
