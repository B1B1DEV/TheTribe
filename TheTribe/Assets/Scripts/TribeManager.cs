using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeManager : MonoBehaviour {

    // private properties
    int age;
    public enum Step{Work,Offering,Decision,Epilogue};
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
    //public TotemManager totemManager;



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
        if (currentStep != Step.Decision)
        {
            Debug.Log("Wrong step ?! Cannot cast gaze :-(");
            return;
        }

        if (favorable)
        {
            // Send Event Acceptance
            DivineFavor();
        }
        else
        {
            DivineWrath();
            faith -= 1;
        }

        // Go to Epilogue step
        NextStep();
    }

    // GameOver
    private void GameOver()
    {
        Debug.Log("Game Ovah mah bruddha :-(");
    }


    // Test Function
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            NextStep();
        }
    }

}
