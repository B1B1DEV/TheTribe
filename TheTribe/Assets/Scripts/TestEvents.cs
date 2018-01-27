using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour {

	// Subscribe
	void OnEnable ()
    {
        TribeManager.OnNextStepLaunched += DoTheThing;
    }
	
	// Unsubscribe
	void OnDisable ()
    {
        TribeManager.OnNextStepLaunched -= DoTheThing;
    }

    // Do the thing
    void DoTheThing()
    {
        Debug.Log("Event launched !");
    }
}
