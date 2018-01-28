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
    public struct generatedGodAspect
    {
        public partTypeLabel category;
        public string aspectName;
        public Sprite godAspectSprite;
        public bool isAspectPositive;
    }

    public struct generatedGodPart
    {
        public string name;
        public GameObject relatedGameObject;
        public generatedGodAspect generatedAspect;
    }

    public List<totemPart> allTotemPartList;
    List<generatedGodPart> generatedGodPartList = new List<generatedGodPart>();

    public List<generatedGodPart> GetGodGeneratedAspects() { return generatedGodPartList; }

    // Use this for initialization
    void Start ()
    {
        GenerateTotem();
        if (SceneManager.instance != null)
        {
            SceneManager.instance.totemManager = this;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
            GenerateTotem();*/
	}
    
    void GenerateTotem()
    {
        // For each part
        foreach(totemPart part in allTotemPartList)
        {
            generatedGodPart gtp;
            gtp.name = part.name;
            gtp.relatedGameObject = part.partGameObject;

            generatedGodAspect gp;
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

            generatedGodPartList.Add(gtp);
        }

        // THEN for each generated part
        
        foreach (generatedGodPart g in generatedGodPartList)
        {
            //if (!g.relatedGameObject.GetComponent<SpriteRenderer>())
                //g.relatedGameObject.AddComponent<SpriteRenderer>();
            
            //g.relatedGameObject.GetComponent<SpriteRenderer>().sprite = g.generatedAspect.godAspectSprite;
            
            Debug.Log(g.name + " , " + g.generatedAspect.aspectName +" , " +  g.generatedAspect.isAspectPositive);
        }
        
        // Save Generated God (to display in gameover screen)
        //SceneManager.instance.savedTotem = generatedGodPartList;

    // Try to Debug.Log() that shit to see if it works
    // ... And apparently, it does !
}


    //Get every sprite for each god part 
    public Sprite GetHeadGodAspect() { return SearchPartInGeneratedTotem("Head"); }
    public Sprite GetUpperbodyGodAspect() { return SearchPartInGeneratedTotem("UpperBody");}
    public Sprite GetLowerbodyGodAspect() { return SearchPartInGeneratedTotem("LowerBody");}
    public Sprite GetAccessoryGodAspect() { return SearchPartInGeneratedTotem("Accessory");}

    Sprite SearchPartInGeneratedTotem(string partName)
    {
        foreach (generatedGodPart g in generatedGodPartList)
        {
            if (g.name == partName)
            {
                return g.generatedAspect.godAspectSprite;
            }
        }

        return null;
    }

    //Get every possibility for each totem part 
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

    //Get each generated part from current God 
    public generatedGodAspect GetGodHeadGeneratedAspect() { return RetrieveGodGeneratedPart("Head"); }
    public generatedGodAspect GetGodUpperbodyGeneratedAspect() { return RetrieveGodGeneratedPart("UpperBody"); }
    public generatedGodAspect GetGodLowerbodyGeneratedAspect() { return RetrieveGodGeneratedPart("LowerBody"); }
    public generatedGodAspect GetGodAccessoryGeneratedAspect() { return RetrieveGodGeneratedPart("Accessory"); }

    generatedGodAspect RetrieveGodGeneratedPart(string godPartName)
    {
        foreach (generatedGodPart g in generatedGodPartList)
        {
            if (g.name == godPartName)
            {
                return g.generatedAspect;
            }
        }

        return default(generatedGodAspect);
    }
}
