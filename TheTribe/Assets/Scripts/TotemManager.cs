using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour {

    public enum partTypeLabel { Animal, Vegetal, Emotion};

    [System.Serializable]
    public struct partType
    {
        public partTypeLabel name;

        [HideInInspector]
        public bool isAspectPositive;

        [Header("Respect the same order for god AND totem related aspect sprites!")]
        public List<Sprite> godPositiveAspect;
        public List<Sprite> godNegativeAspect;
        public List<Sprite> totemPositiveAspects;
        public List<Sprite> totemNegativeAspects;
    }

    [System.Serializable]
    public struct totemPart
    {
        public string name;
        public GameObject partGameObject;
        public List<partType> aspect;
        float value;
    }

    public struct generatedPart
    {
        public partTypeLabel name;
        public bool isPositive;
        public Sprite totemAspectSprite;
        public Sprite godAspectSprite;
    }

    public struct generatedTotemPart
    {
        public string name;
        public GameObject relatedGameObject;
        public generatedPart generatedAspect;
    }

    public List<totemPart> allTotemPartList;
    List<generatedTotemPart> generatedTotemPartList = new List<generatedTotemPart>();

    public List<generatedTotemPart> GetTotemGeneratedParts() { return generatedTotemPartList; }

    // Use this for initialization
    void Start ()
    {
        GenerateTotem();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void GenerateTotem()
    {
        // For each part
        foreach(totemPart part in allTotemPartList)
        {
            generatedTotemPart gtp;
            gtp.name = part.name;
            gtp.relatedGameObject = part.partGameObject;

            generatedPart gp;
            partType chosenAspect;

            // Choose a random aspect type 
            int i = Random.Range(0, part.aspect.Count);
            chosenAspect = part.aspect[i];

            gp.name = chosenAspect.name;

            // THEN apply random on aspect boolean value 
            if (Random.value > 0.5f)
                gp.isPositive = true;
            else
                gp.isPositive = false;
            


            // THEN choose randomly a sprite in either positive or negative aspect depending on bool value 
            int k = Random.Range(0, 2);
            if (gp.isPositive)
            {
                gp.totemAspectSprite = chosenAspect.totemPositiveAspects[k];
                gp.godAspectSprite = chosenAspect.godPositiveAspect[k];
            }
            else
            {
                gp.totemAspectSprite = chosenAspect.totemNegativeAspects[k];
                gp.godAspectSprite = chosenAspect.godNegativeAspect[k];
            }

            gtp.generatedAspect = gp;

            // Add the generated part to the list
            generatedTotemPartList.Add(gtp);
        }


        // THEN for each generated part
        foreach (generatedTotemPart g in generatedTotemPartList)
        {
            // Apply the sprite to the game object related to the part

            Debug.Log(g.name + " , " + g.generatedAspect.isPositive);
        }
        
        // Try to Debug.Log() that shit to see if it works
        // ... And apparently, it does !
    }

    public Sprite GetHeadGodAspect()
    {
        return SearchPartInGeneratedTotem("Head");
    }

    public Sprite GetUpperbodyGodAspect()
    {
        return SearchPartInGeneratedTotem("UpperBody");
    }

    public Sprite GetLowerbodyGodAspect()
    {
        return SearchPartInGeneratedTotem("LowerBody");
    }

    public Sprite GetAccessoryGodAspect()
    {
        return SearchPartInGeneratedTotem("Accessory");
    }

    Sprite SearchPartInGeneratedTotem(string partName)
    {
        foreach (generatedTotemPart g in generatedTotemPartList)
        {
            if (g.name == partName)
            {
                return g.generatedAspect.godAspectSprite;
            }
        }

        return null;
    }
}
