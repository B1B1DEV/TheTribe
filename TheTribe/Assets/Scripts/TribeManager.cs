using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeManager : MonoBehaviour {

    // private properties
    int age;
    int step; // 0 = mine resource
    int faith;

    // public Get Methods
    public int GetAge()
    {
        return age;
    }

    public int GetStep()
    {
        return step;
    }

    public int GetFaith()
    {
        return faith;
    }

    [SerializeField]
    float baseRefusalChance;

    [SerializeField]
    int lastStepIndex;

    // Events
    public delegate void NextStepEvent();
    public static event NextStepEvent OnNextStepLaunched;

    public delegate void NextAgeEvent();
    public static event NextAgeEvent OnNewAge;

    //TribeManager.OnNextStepLaunched += MyMethod();
    //TribeManager.OnNewAge += MyMethod();

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
        step = 0;
        faith = 3;
	}

    // Next Step
    public void NextStep()
    {
        if (step < lastStepIndex)
        {
            step += 1;
            // + event next step
            OnNextStepLaunched();
        }
        else
        {
            age += 1;
            step = 0;
            // + event new age
            OnNewAge();
        }
    }


    // Test Function
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            OnNextStepLaunched();
        }
    }



}
