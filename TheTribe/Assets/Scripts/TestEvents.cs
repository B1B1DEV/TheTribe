using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour {

    public GameObject canvasChoice;
    public AudioSource stepAudio;

	// Subscribe
	void OnEnable ()
    {
        canvasChoice.SetActive(false);

        TribeManager.OnNextStepLaunched += DoTheThing;
        TribeManager.OnNewAge += WriteSomethingInConsole;

        TribeManager.OnNextStepLaunched += UpdateVillagersAnimation;
        TribeManager.OnNewAge += VillagersBackToWork;
        TribeManager.DivineFavor += VillagerRewardedAnimation;
        TribeManager.DivineWrath += VillagerPunishAnimation;

        VillagersBackToWork();
    }
	
	// Unsubscribe
	void OnDisable ()
    {
        TribeManager.OnNextStepLaunched -= DoTheThing;
        TribeManager.OnNewAge -= WriteSomethingInConsole;

        TribeManager.OnNextStepLaunched -= UpdateVillagersAnimation;
        TribeManager.OnNewAge -= VillagersBackToWork;
        TribeManager.DivineFavor -= VillagerRewardedAnimation;
        TribeManager.DivineWrath -= VillagerPunishAnimation;
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

        foreach(IACharacter villager in FindObjectsOfType<IACharacter>())
        {
            villager.GetComponent<Animator>().SetTrigger("Reset");

            foreach (AnimatorControllerParameter g in villager.GetComponent<Animator>().parameters)
                villager.GetComponent<Animator>().ResetTrigger(g.ToString());
        }
        
        // Manage God eye feedback
        Animator godAnim = GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>();

        godAnim.SetBool("IsLookingConfirmButton", false);
        godAnim.SetBool("IsLookingRefuseButton", false);
        godAnim.SetTrigger("Reset");
        foreach (AnimatorControllerParameter g in godAnim.parameters)
            godAnim.ResetTrigger(g.ToString());

        StartCoroutine(WaitBeforePlayingStepSound("PeonWorking", true));
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

                foreach (IACharacter villager in FindObjectsOfType<IACharacter>())
                {
                    Debug.Log(villager.name);
                    villager.GetComponent<Animator>().SetTrigger("JobDone");
                }

                canvasChoice.SetActive(true);
                GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>().SetTrigger("JobDone");

                StartCoroutine(WaitBeforePlayingStepSound("PriestPropose", false));

                break;

            default:
                break;
        }
    }

    // Attitude depending on God's decision
    void VillagerPunishAnimation()
    {
        canvasChoice.SetActive(false);

        foreach (IACharacter villager in FindObjectsOfType<IACharacter>())
        {
            villager.GetComponent<Animator>().SetTrigger("Reject");
        }

        GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>().SetTrigger("Reject");

        StartCoroutine(WaitBeforePlayingStepSound("thunder", false));
    }

    void VillagerRewardedAnimation()
    {
        canvasChoice.SetActive(false);

        foreach (IACharacter villager in FindObjectsOfType<IACharacter>())
        {
            villager.GetComponent<Animator>().SetTrigger("Accept");
        }

        GameObject.FindGameObjectWithTag("Eye").GetComponent<Animator>().SetTrigger("Accept");

        StartCoroutine(WaitBeforePlayingStepSound("Yeah", false));
    }

    IEnumerator WaitBeforePlayingStepSound(string audioToLoad, bool isLooping)
    {
        stepAudio.Stop();

        yield return new WaitForSeconds(0.1f);

        stepAudio.clip = Resources.Load(audioToLoad) as AudioClip;
        stepAudio.loop = isLooping;
        stepAudio.Play();
    }
}
