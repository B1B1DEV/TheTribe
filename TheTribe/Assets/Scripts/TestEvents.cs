using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour {

	// Subscribe
	void OnEnable ()
    {
        TribeManager.OnNextStepLaunched += DoTheThing;
        TribeManager.OnNewAge += WriteSomethingInConsole;

        TribeManager.OnNextStepLaunched += UpdateVillagersAnimation;
        TribeManager.OnNewAge += VillagersBackToWork;
        TribeManager.DivineFavor += VillagerRewardedAnimation;
        TribeManager.DivineWrath += VillagerPunishAnimation;
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

    // Put villagers back to work
    void VillagersBackToWork()
    {
        //Debug.Log("VillagersBackToWork called");

        foreach(IAVillager villager in FindObjectsOfType<IAVillager>())
        {
            villager.GetComponent<Animator>().SetTrigger("Reset");

            foreach (AnimatorControllerParameter g in villager.GetComponent<Animator>().parameters)
                villager.GetComponent<Animator>().ResetTrigger(g.ToString());
        }
    }

    // Working or waiting depending on state
    void UpdateVillagersAnimation()
    {
        switch (TribeManager.instance.GetStep())
        {
            case TribeManager.Step.Work:
                VillagersBackToWork();
                break;

            case TribeManager.Step.Offering:

                foreach (IAVillager villager in FindObjectsOfType<IAVillager>())
                {
                    GetComponent<Animator>().SetTrigger("JobDone");
                }
                break;

            default:
                break;
        }
    }

    // Attitude depending on God's decision
    void VillagerPunishAnimation()
    {
        foreach (IAVillager villager in FindObjectsOfType<IAVillager>())
        {
            GetComponent<Animator>().SetTrigger("Reject");
        }
    }

    void VillagerRewardedAnimation()
    {
        foreach (IAVillager villager in FindObjectsOfType<IAVillager>())
        {
            GetComponent<Animator>().SetTrigger("Accept");
        }
    }
}
