using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour {

	// Subscribe
	void OnEnable ()
    {
        TribeManager.OnNextStepLaunched += DoTheThing;
        TribeManager.OnNewAge += WriteSomethingInConsole;
    }
	
	// Unsubscribe
	void OnDisable ()
    {
        TribeManager.OnNextStepLaunched -= DoTheThing;
        TribeManager.OnNewAge -= WriteSomethingInConsole;
    }

    // Do the thing
    void DoTheThing()
    {
        Debug.Log("New phase : " + TribeManager.instance.GetStep().ToString());
    }

    void WriteSomethingInConsole()
    {
        Debug.Log("New Age, bitches !");
        Debug.Log("New phase : " + TribeManager.instance.GetStep().ToString());
    }

}
