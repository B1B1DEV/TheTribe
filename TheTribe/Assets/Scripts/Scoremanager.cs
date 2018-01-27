using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    int score; //4 parties*
    int maxScore;
    int percentageScore;
    public OfferingGenerator offeringGenerator;
    public TotemManager totemManager;


    [SerializeField]
    int pointsPerCategory;
    [SerializeField]
    int pointsPerAlignment;
    [SerializeField]
    int pointsPerAspect;

    // Use this for initialization
    void Awake()
    {
        score = 0;
        maxScore = 4 * (pointsPerCategory + pointsPerAlignment + pointsPerAspect);
    }

    private void Start()
    {
        totemManager = offeringGenerator.totemManager;
    }

    // Subscribe
    void OnEnable()
    {
        TribeManager.DivineFavor += AddPointsFromOffering;
    }

    // Unsubscribe
    void OnDisable()
    {
        TribeManager.DivineFavor -= AddPointsFromOffering;
    }

    void UpdateScore()
    {
        percentageScore = (int)((float)score /maxScore);
    }

    void AddPointsFromOffering()
    {
        // Get accepted totem part
        TotemManager.partType acceptedPart = offeringGenerator.GetProposedPart();
        //acceptedPart.category

        // Compare part with ideal
        TotemManager.generatedGodAspect godAspect = totemManager.GetGodHeadGeneratedAspect();

        score += godAspect.category == acceptedPart.category ? 1 : 0;
        score += godAspect.isAspectPositive == acceptedPart.isAspectPositive ? 1 : 0;
        score += godAspect.aspectName == acceptedPart.aspectName ? 1 : 0;

        // Update Score
        UpdateScore();
    }


}
