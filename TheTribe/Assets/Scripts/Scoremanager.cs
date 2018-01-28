using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;
    int maxScore;
    int percentageScore;
    public OfferingGenerator offeringGenerator;
    public TotemManager totemManager;

    private List<TotemManager.generatedGodPart> savedTotem;

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
        //totemManager = offeringGenerator.totemManager;
        savedTotem = new List<TotemManager.generatedGodPart>();
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
        percentageScore = Mathf.RoundToInt(((float)score / (float)maxScore) * 100f);
        SceneManager.instance.savedScore = percentageScore;
    }

    public int GetScore()
    {
        return percentageScore;
    }

    void AddPointsFromOffering()
    {
        // Get accepted totem part
        TotemManager.partType acceptedPart = offeringGenerator.GetProposedPart();

        // Save
        TotemManager.generatedGodPart gp = new TotemManager.generatedGodPart();
        gp.generatedAspect.category = acceptedPart.category;
        gp.generatedAspect.isAspectPositive = acceptedPart.isAspectPositive;
        gp.generatedAspect.aspectName = acceptedPart.aspectName;
        gp.generatedAspect.godAspectSprite = acceptedPart.godAspectSprite;
        gp.name = "savedTotem";
        gp.relatedGameObject = null;
        savedTotem.Add(gp);
        SceneManager.instance.savedTotem = savedTotem;

        // Compare part with ideal
        TotemManager.generatedGodAspect godAspect = totemManager.GetGodHeadGeneratedAspect();

        score += godAspect.category == acceptedPart.category ? 1 : 0;
        score += godAspect.isAspectPositive == acceptedPart.isAspectPositive ? 1 : 0;
        score += godAspect.aspectName == acceptedPart.aspectName ? 1 : 0;

        // Update Score
        UpdateScore();
    }


}
