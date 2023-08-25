using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels = new List<LevelData>();
    public List<bool> isFinished = new List<bool>();
    public List<bool> isUnlocked = new List<bool>();


    public List<PlayerAbility> playerAbilities = new();
    public List<bool> playerAvailableAbilities = new();
    public List<bool> sawAbilities = new();
    LevelData nextLevel = null;
    public static LevelManager get
    {
        get
        {
            return FindObjectOfType<LevelManager>();
        }
    }


    private void Awake()
    {
        isFinished.Clear();
        for (int i = 0; i < levels.Count; i++)
        {
            isUnlocked.Add(false);
        }
        for (int i = 0; i < levels.Count; i++)
        {
            bool LevelFinished = PlayerPrefs.HasKey("LCI|" + i) && PlayerPrefs.GetInt("LCI|" + i) == 1;
            isFinished.Add(LevelFinished);
            if (levels[i].defaultLevel)
            {
                isUnlocked[i] = true;
            }
            if (LevelFinished)
            {
                foreach (LevelData ld in levels[i].unlockOnFinish)
                {
                    isUnlocked[levels.IndexOf(ld)] = true;
                }
            }

        }
        for (int i = 0; i < playerAbilities.Count; i++)
        {
            if (!playerAbilities[i].defaultUnlocked)
            {
                if (playerAbilities[i].unlockAfter != null)
                {
                    playerAvailableAbilities.Add(isFinished[levels.IndexOf(playerAbilities[i].unlockAfter)]);
                }
                else
                {
                    playerAvailableAbilities.Add(false);
                }
                
            }
            else
            {
                playerAvailableAbilities.Add(true);
            }
        }
        for (int i = 0; i < playerAbilities.Count; i++)
        {
            if(playerAvailableAbilities[i] == true)
            {
                bool abilitySaw = PlayerPrefs.HasKey("SPA|" + i) && PlayerPrefs.GetInt("SPA|" + i) == 1;

                sawAbilities.Add(abilitySaw);
            }
            else
            {
                sawAbilities.Add(false);

            }
            

        }
    }
    void Start()
    {
        if(Manager.get != null)
        {
            Manager.get.showNewAvability = PlayerPrefs.GetInt("NAU") == 1;
        }
        PlayerPrefs.SetInt("NAU", 0);
    }

    void Update()
    {

    }
    public bool isAbilitityUnlocked(PlayerAbility pa)
    {
        int paindex = playerAbilities.IndexOf(pa);
        if (paindex == -1) return false;
        else return playerAvailableAbilities[paindex];
    }
    public static void SetThisLevelCompleted()
    {
        get.SetLevelCompleted(get.thisLevelName());
    }
    public void SetLevelCompleted(string sceneName)
    {
        IEnumerable<LevelData> completedLevels = levels.Where(e => e.sceneName == sceneName);
        if (completedLevels.Count() <= 0) return;
        LevelData completedLevel = completedLevels.First();
        int completedLevelIndex = levels.IndexOf(completedLevel);
        PlayerPrefs.SetInt("LCI|" + completedLevelIndex, 1);
        if (completedLevel.unlockOnFinish.Count == 1)
        {
            nextLevel = completedLevel.unlockOnFinish[0];
        }
        foreach(PlayerAbility playerAbility in playerAbilities)
        {
            if(playerAbility.unlockAfter != null &&  playerAbility.unlockAfter.sceneName == sceneName && !isAbilitityUnlocked(playerAbility))
            {
                PlayerPrefs.SetInt("NAU", 1);
            }
        }
    }
    public string thisLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void ReloadScene(float time = 0)
    {
        LoadScene(SceneManager.GetActiveScene().name, time);
    }
    public static void loadMenu(float time = 0)
    {
        LoadScene("Menu", time);
    }
    public static void LoadScene(string name, float time = 0)
    {
        get.StartCoroutine(get.loadScene(name, time));
    }
    IEnumerator loadScene(string name, float time = 0)
    {
        AsyncOperation levelLoading = SceneManager.LoadSceneAsync(name);
        levelLoading.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(time);
        levelLoading.allowSceneActivation = true;

    }
    public static void LoadNextLevel(float time = 0)
    {
        if (get.nextLevel != null)
        {
            LoadScene(get.nextLevel.sceneName, time);
        }
        else
        {
            loadMenu();
        }
    }
    public void SetManuTab(int tab)
    {
        PlayerPrefs.SetInt("MenuTab", tab);
    }
    public void setAvabilityToSaw(PlayerAbility ab)
    {
        int index = playerAbilities.IndexOf(ab);
        if (index == -1) return;

        PlayerPrefs.SetInt("SPA|" + index, 1);
        sawAbilities[index] = true;
    }
}
