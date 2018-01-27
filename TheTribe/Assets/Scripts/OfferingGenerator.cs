using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferingGenerator : MonoBehaviour
{

    public TotemManager totemManager;
    private List<TotemManager.partType> rejectedList;
    private TotemManager.partType currentProposedpart;

    // Use this for initialization
    void Start ()
    {
        ChangeList();
    }

    // Subscribe
    void OnEnable()
    {
        TribeManager.OnNewAge += ChangeList;
        TribeManager.OnNextStepLaunched += GenerateTotemPart;
        TribeManager.DivineWrath += RemoveFromList;
    }

    // Unsubscribe
    void OnDisable()
    {
        TribeManager.OnNewAge -= ChangeList;
        TribeManager.OnNextStepLaunched -= GenerateTotemPart;
        TribeManager.DivineWrath -= RemoveFromList;
    }

    private void ChangeList()
    {
        int age = TribeManager.instance.GetAge();
        switch (age)
        {
            case 0:
                rejectedList = totemManager.GetHeadPossibleAspects();
                break;
            case 1:
                rejectedList = totemManager.GetUpperbodyPossibleAspects();
                break;
            case 2:
                rejectedList = totemManager.GetLowerbodyPossibleAspects();
                break;
            case 3:
                rejectedList = totemManager.GetAccessoryPossibleAspects();
                break;
        }
    }

    private void RemoveFromList()
    {
        rejectedList.Remove(currentProposedpart);
    }



    /*     public struct generatedPart
    {
        public partTypeLabel category;
        public string aspectName;
        public Sprite godAspectSprite;
        public bool isAspectPositive;
    }*/

    //
    private void GenerateTotemPart()
    {
        currentProposedpart = rejectedList[Random.Range(0,rejectedList.Count)];
        /*
        TotemManager.generatedPart gp;
        gp.category = TotemManager.partTypeLabel.Animal;
        gp.isAspectPositive = false;
        gp.aspectName = "";

        // Choose a random category
        gp.category = (TotemManager.partTypeLabel)Random.Range(0, 3);

        // Choose a random alignment
        gp.isAspectPositive = Random.Range(0, 2) == 0;

        // Choose a random aspect
        int i = Random.Range(0, part.aspect.Count);
        chosenAspect = part.aspect[i];*/
    }
}
