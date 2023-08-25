using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilititiTile : MonoBehaviour
{
    
    public int maxCount;
    public AbilityBar bar;
    public bool progresAll = false;
    public Image icon;
    int count;
    float progres;
    public int Count
    {
        get { return count; }
        set { count = value; updateUI();}
    }
    public float Progres
    {
        get { return progres; }
        set { progres = value; updateUI(); }
    }
    private void Start()
    {
        
    }
    void updateUI()
    {
        bar.Count = maxCount;
        bar.Used = count;
        bar.Progres = progres;
        bar.progresAll = progresAll;
    }
}
