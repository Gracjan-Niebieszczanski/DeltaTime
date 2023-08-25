using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> tabs;
    public List<GameObject> tabsButtons;


    public Color NavButtonSelectedColor;
    public Color NavButtonUnSelectedColor;
    public Color NavButtonTextSelectedColor;
    public Color NavButtonTextUnSelectedColor;



    public Button startButton;
    public int SelectedLevel = -1;


    public static MenuManager get
    {
        get
        {
            return FindObjectOfType<MenuManager>();
        }
    }
        
    void Start()
    {
        if(PlayerPrefs.HasKey("MenuTab"))
        {
            setTab(PlayerPrefs.GetInt("MenuTab"));
            PlayerPrefs.DeleteKey("MenuTab");
        }
        else
        {
            setTab(0);
        }
        
        foreach (LevelButton lb in FindObjectsOfType<LevelButton>(true))
        {
            if (lb.level < LevelManager.get.isFinished.Count && lb.TryGetComponent(out Button btn))
            {
                btn.interactable = LevelManager.get.isUnlocked[lb.level];
                if (LevelManager.get.isUnlocked[lb.level] && !LevelManager.get.isFinished[lb.level])
                {
                    btn.GetComponent<Image>().color = Color.yellow;
                }
            }
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            LevelManager.ReloadScene();
        }
    }
    public void setTab(int number)
    {
        for(int i = 0;i < tabs.Count;i++)
        {
            if(i == number)
            {
                tabs[i].SetActive(true);
                if (tabsButtons[i].TryGetComponent(out Image img))
                {
                    img.color = NavButtonSelectedColor;
                }
                if (tabsButtons[i].transform.GetChild(0).gameObject.TryGetComponent(out TextMeshProUGUI text))
                {
                    text.color = NavButtonTextSelectedColor;
                }
            }
            else
            {
                tabs[i].SetActive(false);
                if (tabsButtons[i].TryGetComponent(out Image img))
                {
                    img.color = NavButtonUnSelectedColor;
                }
                if (tabsButtons[i].transform.GetChild(0).TryGetComponent(out TextMeshProUGUI text))
                {
                    text.color = NavButtonTextUnSelectedColor;
                }
            }
            
        }
    }
    public void SelectLevel(int levelIndex)
    {
        SelectedLevel = levelIndex;
        startButton.interactable = true;
    }
    public void LoadSelectedLevel()
    {
        LevelManager.LoadScene(LevelManager.get.levels[SelectedLevel].sceneName);
    }
}
