using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    public TotemManager totemManager;
    public List<TotemManager.generatedGodPart> savedTotem;
    public List<TotemManager.generatedGodPart> trueGod;
    public int savedScore;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void LoadMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void LoadGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScreen");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    // Public method to call display
    public void DisplayLastTotemInMenu(Transform parent)
    {
        DisplayTotem(savedTotem, parent);
        Debug.Log("Call display");
    }

    // Display Totem
    private void DisplayTotem(List<TotemManager.generatedGodPart> listOfParts, Transform parent)
    {
        foreach (TotemManager.generatedGodPart g in listOfParts)
        {
            Debug.Log(g.name);
            if (g.name.Contains("Head"))
            {
                SpriteRenderer sp = parent.gameObject.transform.Find("TotemHead").GetComponent<SpriteRenderer>();
                sp.sprite = g.generatedAspect.godAspectSprite;
                Debug.Log("Head");
            }
            else if(g.name.Contains("UpperBody"))
            {
                SpriteRenderer sp = parent.gameObject.transform.Find("TotemUpperBody").GetComponent<SpriteRenderer>();
                sp.sprite = g.generatedAspect.godAspectSprite;
                Debug.Log("Upper Body");
            }
            else if (g.name.Contains("LowerBody"))
            {
                SpriteRenderer sp = parent.gameObject.transform.Find("TotemLowerBody").GetComponent<SpriteRenderer>();
                sp.sprite = g.generatedAspect.godAspectSprite;
                Debug.Log("Lower Body");
            }
            else if (g.name.Contains("Accessory"))
            {
                SpriteRenderer sp = parent.gameObject.transform.Find("TotemAccessory").GetComponent<SpriteRenderer>();
                sp.sprite = g.generatedAspect.godAspectSprite;
                Debug.Log("Accessory");
            }

            //if (!g.relatedGameObject.GetComponent<SpriteRenderer>())
            //g.relatedGameObject.AddComponent<SpriteRenderer>();

            //g.relatedGameObject.GetComponent<SpriteRenderer>().sprite = g.generatedAspect.godAspectSprite;

            Debug.Log(g.name + " , " + g.generatedAspect.aspectName + " , " + g.generatedAspect.isAspectPositive);
        }
    }

}
