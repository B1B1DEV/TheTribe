using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPriest : IACharacter {

    Vector3 startingPosition;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        startingPosition = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PutPriestOnAltar()
    {
        transform.localPosition = new Vector3(-0.08f, -2.51f, 0f);
    }

    public void PutPriestOnGround()
    {
        transform.localPosition = new Vector3(1.6f, -3.56f, 0f);//startingPosition;
    }
}
