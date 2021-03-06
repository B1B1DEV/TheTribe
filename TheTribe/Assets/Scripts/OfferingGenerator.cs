﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferingGenerator : MonoBehaviour
{
    public TotemManager totemManager;
    public SpriteRenderer partOfferedRenderer;
    public GameObject EffectOnOffer;
    private List<TotemManager.partType> rejectedList;
    private TotemManager.partType currentProposedpart;

    SpriteRenderer srToUpdate;

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
        TribeManager.DivineFavor += ValidateProposal;
        TribeManager.DivineFizzle += ValidateAnyway;
    }

    // Unsubscribe
    void OnDisable()
    {
        TribeManager.OnNewAge -= ChangeList;
        TribeManager.OnNextStepLaunched -= GenerateTotemPart;
        TribeManager.DivineWrath -= RemoveFromList;
        TribeManager.DivineFavor -= ValidateProposal;
        TribeManager.DivineFizzle -= ValidateAnyway;
    }

    public TotemManager.partType GetProposedPart()
    {
        return currentProposedpart;
    }

    private void ChangeList()
    {
        int age = TribeManager.instance.GetAge();
        switch (age)
        {
            case 0:
                rejectedList = totemManager.GetHeadPossibleAspects();
                srToUpdate = totemManager.gameObject.transform.Find("TotemHead").GetComponent<SpriteRenderer>();
                break;
            case 1:
                rejectedList = totemManager.GetUpperbodyPossibleAspects();
                srToUpdate = totemManager.gameObject.transform.Find("TotemUpperBody").GetComponent<SpriteRenderer>();
                FindObjectOfType<MaterialDisplayManager>().SwitchToWoodAspect();
                break;
            case 2:
                rejectedList = totemManager.GetLowerbodyPossibleAspects();
                srToUpdate = totemManager.gameObject.transform.Find("TotemLowerBody").GetComponent<SpriteRenderer>();
                FindObjectOfType<MaterialDisplayManager>().SwitchToStoneAspect();
                break;
            case 3:
                rejectedList = totemManager.GetAccessoryPossibleAspects();
                srToUpdate = totemManager.gameObject.transform.Find("TotemAccessory").GetComponent<SpriteRenderer>();
                FindObjectOfType<MaterialDisplayManager>().SwitchToGoldAspect();
                break;
        }
    }

    private void RemoveFromList()
    {
        EffectOnOffer.SetActive(false);
        partOfferedRenderer.sprite = null;
        rejectedList.Remove(currentProposedpart);
        TribeManager.instance.CallNextStepCoroutine(2.5f);
    }

    void ValidateAnyway()
    {
        StartCoroutine(WaitBeforeValidate());
    }

    IEnumerator WaitBeforeValidate()
    {
        yield return new WaitForSeconds(2.5f);
        ValidateProposal();
    }

    private void ValidateProposal()
    {
        EffectOnOffer.SetActive(false);
        partOfferedRenderer.sprite = null;

        //Move up the totem as construction the age grows. Flat values for now
        if (TribeManager.instance.GetAge() == 1)
            srToUpdate.transform.parent.position += new Vector3(0, 1.515f, 0);
        else if(TribeManager.instance.GetAge() == 2)
            srToUpdate.transform.parent.position += new Vector3(0, 1.15f, 0);

        srToUpdate.sprite = currentProposedpart.totemAspectSprite;
        TribeManager.instance.CallNextStepCoroutine(2.5f);
    }
    /*     public struct generatedPart
    {
        public partTypeLabel category;
        public string aspectName;
        public Sprite godAspectSprite;
        public bool isAspectPositive;
    }*/
    
    private void GenerateTotemPart()
    {
        if (TribeManager.instance.GetStep() == TribeManager.Step.Offering)
        {
            Debug.Log("GeneratePart");
            currentProposedpart = rejectedList[Random.Range(0, rejectedList.Count)];
            EffectOnOffer.SetActive(true);
            partOfferedRenderer.sprite = currentProposedpart.totemAspectSprite;

            if(TribeManager.instance.GetAge() == TribeManager.instance.GetLastAgeIndex() && partOfferedRenderer.transform.rotation.eulerAngles.z != 90)
            {
                partOfferedRenderer.transform.Rotate(new Vector3(0, 0, 90));
                partOfferedRenderer.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                partOfferedRenderer.transform.localPosition = new Vector3(partOfferedRenderer.transform.localPosition.x, 1.21f, partOfferedRenderer.transform.localPosition.z);
                EffectOnOffer.transform.localPosition = new Vector3(EffectOnOffer.transform.localPosition.x, 1.21f, EffectOnOffer.transform.localPosition.z);
            }
        }

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
