using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour {

    public enum partTypeLabel { Animal, Vegetal, Emotion};

    // Structures used for initialisation
    [System.Serializable]
    public struct partType
    {
        public partTypeLabel category;
        public string aspectName;
        public Sprite godAspectSprite;
        public Sprite totemAspectSprite;
        public bool isAspectPositive;
    }

    [System.Serializable]
    public struct totemPart
    {
        public string name;
        public GameObject partGameObject;
        public List<partType> aspect;
    }

    // Structures of the generated god
    public struct generatedPart
    {
        public partTypeLabel category;
        public string aspectName;
        public Sprite godAspectSprite;
        public bool isAspectPositive;
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
        if (Input.GetKeyDown(KeyCode.R))
            GenerateTotem();
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

            // Retrieve necessary informations
            gp.category = chosenAspect.category;
            gp.aspectName = chosenAspect.aspectName;
            gp.isAspectPositive = chosenAspect.isAspectPositive;
            gp.godAspectSprite = chosenAspect.godAspectSprite;

            gtp.generatedAspect = gp;

            generatedTotemPartList.Add(gtp);
        }

        // THEN for each generated part
        foreach (generatedTotemPart g in generatedTotemPartList)
        {
            if (!g.relatedGameObject.GetComponent<SpriteRenderer>())
                g.relatedGameObject.AddComponent<SpriteRenderer>();
            
            g.relatedGameObject.GetComponent<SpriteRenderer>().sprite = g.generatedAspect.godAspectSprite;
            
            //Debug.Log(g.name + " , " + g.generatedAspect.aspectName +" , " +  g.generatedAspect.isAspectPositive);
        }
        
        // Try to Debug.Log() that shit to see if it works
        // ... And apparently, it does !
    }

    public Sprite GetHeadGodAspect() { return SearchPartInGeneratedTotem("Head"); }
    public Sprite GetUpperbodyGodAspect() { return SearchPartInGeneratedTotem("UpperBody");}
    public Sprite GetLowerbodyGodAspect() { return SearchPartInGeneratedTotem("LowerBody");}
    public Sprite GetAccessoryGodAspect() { return SearchPartInGeneratedTotem("Accessory");}

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

    public List<partType> GetHeadPossibleAspects() { return RetrieveAllTotemPartPossibilities("Head"); }
    public List<partType> GetUpperbodyPossibleAspects() { return RetrieveAllTotemPartPossibilities("UpperBody"); }
    public List<partType> GetLowerbodyPossibleAspects() { return RetrieveAllTotemPartPossibilities("LowerBody"); }
    public List<partType> GetAccessoryPossibleAspects() { return RetrieveAllTotemPartPossibilities("Accessory"); }

    List<partType> RetrieveAllTotemPartPossibilities(string totemPartName)
    {
        List<partType> resultList = new List<partType>();

        foreach (totemPart g in allTotemPartList)
        {
            if (g.name == totemPartName)
            {
                foreach (partType part in g.aspect)
                {
                    resultList.Add(part);
                }

                return resultList;
            }
        }

        return null;
    }
}
