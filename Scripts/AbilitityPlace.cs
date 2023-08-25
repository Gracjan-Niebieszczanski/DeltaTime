using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitityPlace : MonoBehaviour
{
    [SerializeField]PlayerAbility ability;
    public PlayerAbility Ability
    {
        get
        { 
            return ability;
        }
        set
        {
            ability = value;
            UpdateObj();
        }

    }
    public Button selectButton;
    public Image icon;
    public TextMeshProUGUI Name;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    void UpdateObj()
    {
        if(ability != null)
        {
            icon.sprite = ability.icon;
            Name.text = ability.Name;
        }
        else
        {
            Name.text = "<#777><i>Brak</i></color>";
            icon.sprite = null;
        }
       
    }
    public void ShowButton()
    {
        selectButton?.gameObject.SetActive(true);
    }
    public void HideButton()
    {
        selectButton?.gameObject.SetActive(false);
    }
}
