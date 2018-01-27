using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoremanager : MonoBehaviour
{
    int score; //4 parties*3
    public OfferingGenerator offeringGenerator;

    // Use this for initialization
    void Awake()
    {
        score = 0;
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
        //
    }

    void AddPointsFromOffering()
    {
        // Get accepted totem part
        TotemManager.partType acceptedPart = offeringGenerator.GetProposedPart();

        // Compare part with ideal


        // Update Score
        UpdateScore();
    }


}
