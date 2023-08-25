using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AbilitityTile : MonoBehaviour , IPointerEnterHandler
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
            UpdateUI();
        }
    }
    public Image icon;
    public TextMeshProUGUI text;
    public Button btn;
    public GameObject newObj;

    public Color unlockColor;
    public Color lockColor;
    public void UpdateUI()
    {
        icon.sprite = ability.icon;
        text.text = ability.Name;
        
        if (LevelManager.get.isAbilitityUnlocked(ability))
        {
            text.color = unlockColor;
            btn.interactable = true;
            newObj.SetActive(!LevelManager.get.sawAbilities[LevelManager.get.playerAbilities.IndexOf(ability)]);
        }
        else
        {
            text.color = lockColor;
            btn.interactable = false;
            newObj.SetActive(false);
        }
        
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        AbilitityUI.instance.SetDescription(ability);
        newObj.SetActive(false);
        if(LevelManager.get.isAbilitityUnlocked(ability))
            LevelManager.get.setAvabilityToSaw(ability);
    }
    public void setAbility()
    {
        AbilitityUI.instance.SelectAbility(ability);
    }
}
