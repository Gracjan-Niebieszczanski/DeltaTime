using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager get;
    public bool isTutorialActive = true;
    public int tutorialIndex = 0;
    public List<tutorialStep> tutorialSteps;
    public tutorialStep activeTutorialStep;
    public tutorialStepObject tso;
    // Start is called before the first frame update
    private void Awake()
    {
        get = this;
        tso = FindObjectOfType<tutorialStepObject>();
        if(PlayerPrefs.HasKey("TS"))
        {
            tutorialIndex = PlayerPrefs.GetInt("TS");
        }
        else
        {
            tutorialIndex = 0;
        }
        if(tutorialIndex < tutorialSteps.Count)
        {
            isTutorialActive = true;
            loadTutorialStep();
        }
        else
        {
            isTutorialActive = false;
            tso.gameObject.gameObject.SetActive(false);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeTutorialStep()
    {
        if (tutorialIndex + 1 < tutorialSteps.Count && isTutorialActive)
        {
            tutorialIndex++;
            PlayerPrefs.SetInt("TS", tutorialIndex);
            Invoke("loadTutorialStep", 0.01f);
            if (tutorialIndex + 1 == tutorialSteps.Count)
            {
                Invoke("changeTutorialStep", 5f);
            }
            tso.gameObject.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("TS", tutorialIndex+1);
            isTutorialActive = false;
            tso.gameObject.gameObject.SetActive(false);
        }
    }
    public void loadTutorialStep()
    {
        activeTutorialStep = tutorialSteps[tutorialIndex];
        loadTutorialObject();
        if (tso != null && tso.text != null)
        {
            tso.text.text = activeTutorialStep.text;
        }

    }
    public void loadTutorialObject()
    {
        if (isTutorialActive)
        {
            List<tutorialStepObject> tsos = FindObjectsOfType<tutorialStepObject>(true).ToList();
            foreach (tutorialStepObject tsoa in tsos)
            {
                tsoa.gameObject.SetActive(false);
            }
            tso = tsos.Where(e => e.transform.parent.gameObject.activeSelf == true).OrderByDescending(e => e.priority).First();
            tso.gameObject.SetActive(true);
        }

    }
}
