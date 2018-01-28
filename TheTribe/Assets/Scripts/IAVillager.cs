using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IAVillager : IACharacter {

    public bool needToFlip;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void FlipPeonOnX()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
}
