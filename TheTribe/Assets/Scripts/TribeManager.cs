using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeManager : MonoBehaviour {

    // private properties
    int age;
    int step;
    int faith;

    [SerializeField]
    float baseRefusalChance;

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


	// Use this for initialization
	void Start ()
    {
        age = 0;
        step = 0;
        faith = 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
