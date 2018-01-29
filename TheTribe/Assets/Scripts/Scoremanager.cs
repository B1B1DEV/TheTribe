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
        TribeManager.DivineFizzle += AddPointsFromOffering;
    }

    // Unsubscribe
    void OnDisable()
    {
        TribeManager.DivineFavor -= AddPointsFromOffering;
        TribeManager.DivineFizzle -= AddPointsFromOffering;
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
        gp.generatedAspect.godAspectSprite = acceptedPart.totemAspectSprite;
        int age = TribeManager.instance.GetAge();

        TotemManager.generatedGodAspect godAspect;
        godAspect = totemManager.GetGodHeadGeneratedAspect();

        switch (age)
        {
            case 0:
                gp.name = "Head";
                // Compare part with ideal
                godAspect = totemManager.GetGodHeadGeneratedAspect();
                break;
            case 1:
                gp.name = "UpperBody";
                godAspect = totemManager.GetGodUpperbodyGeneratedAspect();
                break;
            case 2:
                gp.name = "LowerBody";
                godAspect = totemManager.GetGodLowerbodyGeneratedAspect();
                break;
            case 3:
                gp.name = "Accessory";
                godAspect = totemManager.GetGodAccessoryGeneratedAspect();
                break;
        }
        
        gp.relatedGameObject = null;
        savedTotem.Add(gp);
        SceneManager.instance.savedTotem = savedTotem;

        Debug.Log(score.ToString() + "avant modif");

        score += godAspect.category == acceptedPart.category ? pointsPerCategory : 0;
        Debug.Log(score.ToString() + "category god="+ godAspect.category.ToString() + ", totem="+ acceptedPart.category.ToString());

        score += godAspect.isAspectPositive == acceptedPart.isAspectPositive ? pointsPerAlignment : 0;
        Debug.Log(score.ToString() + "alignment god=" + godAspect.isAspectPositive.ToString() + ", totem=" + acceptedPart.isAspectPositive.ToString());

        score += godAspect.aspectName == acceptedPart.aspectName ? pointsPerAspect : 0;
        Debug.Log(score.ToString() + "aspect god=" + godAspect.aspectName.ToString() + ", totem=" + acceptedPart.aspectName.ToString());

        //Debug.Log(score.ToString());

        // Update Score
        UpdateScore();
    }


}
