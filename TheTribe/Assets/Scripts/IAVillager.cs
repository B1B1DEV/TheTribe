using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IAVillager : IACharacter {

    public Sprite offeringSprite;
    public Sprite punishedSprite;
    public Sprite rewardedSprite;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SwitchToOfferingSprite() { sr.sprite = offeringSprite; }
    public void SwitchToPunishedSprite() { sr.sprite = punishedSprite; }
    public void SwitchToRewardedSprite() { sr.sprite = rewardedSprite; }
}
