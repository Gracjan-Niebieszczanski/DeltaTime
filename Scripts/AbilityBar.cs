using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityBar : MonoBehaviour
{
    public GameObject Handleprefab;
    public Color activeColor;
    public Color disActiveColor;
    public int count;
    public int used;
    public float progres = 0;
    public bool progresAll = false;
    public int Count
    {
        get { return count; }
        set { count = value; UpdateUI(); }
    }    
    public int Used
    {
        get { return used; }
        set { used = value;UpdateUI(); }
    }
    public float Progres
    {
        get { return progres; }
        set { progres = value; UpdateUI(); }
    }
    void Start()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        Manager.ClearChldrens(transform);
        float width = GetComponent<RectTransform>().rect.width;
        float marign = GetComponent<HorizontalLayoutGroup>().spacing + 7 / count;
        for (int i = 0; i < count; i++)
        {
            GameObject CreatedBar = Instantiate(Handleprefab, transform);
            CreatedBar.GetComponent<RectTransform>().sizeDelta = new Vector2((width / count) - marign, CreatedBar.GetComponent<RectTransform>().sizeDelta.y);
            if (i < used)
            {
                CreatedBar.GetComponent<Image>().color = activeColor;

            }
            else
            {
                if ((!progresAll &&  i == used) || progresAll)
                {
                    Transform ProgresChild = CreatedBar.transform.GetChild(0);
                    ProgresChild.gameObject.SetActive(true);
                    ProgresChild.GetComponent<Image>().fillAmount = progres;
                }
                CreatedBar.GetComponent<Image>().color = disActiveColor;
            }
        }
    }
}
