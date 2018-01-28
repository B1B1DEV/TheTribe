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
    bool newAgeReady;
    AudioSource audio;

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

    public int GetLastAgeIndex() { return lastAgeIndex; }

    // Other Managers
    public ScoreManager scoreManager;


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
        audio = GetComponent<AudioSource>();

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
        StartCoroutine(MoveToNextStep(6.5f));
        faith = 3;
        newAgeReady = false;
    }

    // Next Step
    public void NextStep()
    {
        if (currentStep < Step.Epilogue)
        {
            currentStep += 1;
            // + event next step

            if (currentStep == Step.Work)
                StartCoroutine(MoveToNextStep(2.5f));

            OnNextStepLaunched();
        }
        else
        {
            Debug.Log("CurrentStep : " + currentStep.ToString());
            
            if (newAgeReady)
            {
                newAgeReady = false;
                if (age < lastAgeIndex)
                {
                    age += 1;
                    currentStep = 0;
                    faith = 3;
                    // + event new age
                    StartCoroutine(MoveToNextStep(2.5f));
                    OnNewAge();
                }
                else
                {
                    GameOver();
                }
            }
            else
            {
                StartCoroutine(MoveToNextStep(2.5f));
                currentStep = 0;
                OnNextStepLaunched();
            }
            
        }
    }

    public void CallNextStepCoroutine(float t)
    {
        StartCoroutine(MoveToNextStep(t));
    }

    IEnumerator MoveToNextStep(float timeToWait)
    {
        //yield return new WaitUntil(() => !audio.isPlaying);
        Debug.Log("Nextstep coroutine called");

        yield return new WaitForSeconds(timeToWait);
        yield return new WaitForEndOfFrame();

        NextStep();
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
            newAgeReady = true;
            // Go to Epilogue step
            NextStep();
        }
        else
        {
            // Gestion cas faith=0 à ajouter !!!
            // Send Event Refusal
            DivineWrath();
            faith -= 1;
            NextStep();
        }
    }

    // GameOver
    private void GameOver()
    {
        SceneManager.instance.savedScore = scoreManager.GetScore();
        SceneManager.instance.LoadGameOver();
    }


    // Test Function
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }*/
    }

}
