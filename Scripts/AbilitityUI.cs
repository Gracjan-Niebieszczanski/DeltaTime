using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AbilitityUI : MonoBehaviour
{
    public static AbilitityUI instance;
    public GameObject descriptionTitle;
    public Image abilititIcon; 
    public TextMeshProUGUI abilititTitle; 
    public TextMeshProUGUI abilititDescription;

    public List<AbilitityPlace> places;
    public GameObject AbilityTileObject;
    public GameObject AbilityTilePlace;

    public List<PlayerAbility> selectedAbilities = new();

    public PlayerAbility selectedAbilitity;

    float abilititiesCount = 3;
    private void Awake()
    {
        instance = this;
        for(int i = 0; i < abilititiesCount; i++)
        {
            if(PlayerPrefs.HasKey("P|" + i))
            {
                int AbilitityIndex = PlayerPrefs.GetInt("P|" + i);
                if(AbilitityIndex != -1)
                {
                    selectedAbilities.Add(LevelManager.get.playerAbilities[AbilitityIndex]);
                }
                else
                {
                    selectedAbilities.Add(null);
                }
            }
            else
            {
                selectedAbilities.Add(null);
            }
            places[i].Ability = selectedAbilities[i];
        }
            
    }
    void Start()
    {
        LoadAvabilityList();
    }
    void Update()
    {
        
    }
    public void SetDescription(PlayerAbility pa)
    {
        descriptionTitle.SetActive(true);
        abilititIcon.sprite = pa.icon;
        abilititTitle.text = pa.Name;
        abilititDescription.text = pa.description;

    }
    public void LoadAvabilityList()
    {
        List<PlayerAbility> playerAbilities = LevelManager.get.playerAbilities ;
        playerAbilities.OrderBy(e => LevelManager.get.isAbilitityUnlocked(e));
        foreach(PlayerAbility ability in playerAbilities)
        {
            GameObject abtO = Instantiate(AbilityTileObject, AbilityTilePlace.transform);
            if(abtO.TryGetComponent(out AbilitityTile aT))
            { 
                aT.Ability= ability;
            }
        }

    }
    public bool replaceEmpty(PlayerAbility toAdd)
    {
        if(selectedAbilities.Count < abilititiesCount)
        {
            selectedAbilities.Add(toAdd);
            return true;
        }
        else
        {
            for(int i = 0; i < selectedAbilities.Count;i++)
            {
                if (selectedAbilities[i] == null)
                {
                    selectedAbilities[i] = toAdd;
                    return true;
                }
                    
            }
        }
        return false;
    }
    public void SelectAbility(PlayerAbility pa)
    {
        selectedAbilitity = pa;
        foreach (AbilitityPlace ap in places)
            ap.ShowButton();
    }
    public void UseAbility(int index)
    {
        
        foreach (AbilitityPlace ap in places)
            ap.HideButton();
        for(int i = 0; i < selectedAbilities.Count;i++)
        {
            if (selectedAbilities[i] != null && selectedAbilities[i]== selectedAbilitity)
            {
                selectedAbilities[i] = null;
                places[i].Ability = null;
            }
                
        }
        places[index].Ability = selectedAbilitity;
        selectedAbilities[index] = selectedAbilitity;
        SaveAvabilitities();
    }
    
    void SaveAvabilitities()
    {
        for(int i = 0; i < abilititiesCount;i++)
        {
            Debug.Log(LevelManager.get.playerAbilities.IndexOf(selectedAbilities[i]));
            PlayerPrefs.SetInt("P|" + i, LevelManager.get.playerAbilities.IndexOf(selectedAbilities[i]));
        }
    }    
}
