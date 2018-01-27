using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeManager : MonoBehaviour
{
    // private properties
    int age;
    public enum Step{Work,Offering,Epilogue};
    Step currentStep;
    int faith;

    // public Get Methods
    public int GetAge()
    {
        return age;
    }

    public Step GetStep()
    {
        return currentStep;
    }

    public int GetFaith()
    {
        return faith;
    }

    const int lastAgeIndex = 3;

    // Other Managers
    //public TotemManager totemManager


    // Events
    public delegate void NextStepEvent();
    public static event NextStepEvent OnNextStepLaunched;

    public delegate void NextAgeEvent();
    public static event NextAgeEvent OnNewAge;

    public delegate void Favor();
    public static event Favor DivineFavor;

    public delegate void Wrath();
    public static event Wrath DivineWrath;

    // Singleton
    public static TribeManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start ()
    {
        age = 0;
        currentStep = Step.Work;
        faith = 3;
	}

    // Next Step
    public void NextStep()
    {
        if (currentStep < Step.Epilogue)
        {
            currentStep += 1;
            // + event next step

            OnNextStepLaunched();
        }
        else
        {
            if (age < lastAgeIndex)
            {
                age += 1;
                currentStep = 0;
                faith = 3;
                // + event new age
                OnNewAge();
            }
            else
            {
                GameOver();
            }
        }
    }

    // Gaze Function
    public void CastGaze(bool favorable)
    {
        // check if right step
        if (currentStep != Step.Offering)
        {
            Debug.Log("Wrong step ?! Cannot cast gaze :-(");
            return;
        }

        if (favorable)
        {
            // Send Event Acceptance
            DivineFavor();
            // Go to Epilogue step
            NextStep();
        }
        else
        {
            // Gestion cas faith=0 à ajouter !!!
            // Send Event Refusal
            DivineWrath();
            faith -= 1;
            currentStep = Step.Work;
            OnNextStepLaunched();
        }
    }

    // GameOver
    private void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScreen");
    }


    // Test Function
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
    }

}
