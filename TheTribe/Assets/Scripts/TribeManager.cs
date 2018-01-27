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
        }
        else
        {
            age += 1;
            step = 0;
            // + event new age
        }
    }


    // Next step event



}
