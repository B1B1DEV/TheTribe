using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public SpriteRenderer scoreSprite;
    int score;
    public AudioClip lowScore;
    public AudioClip medScore;
    public AudioClip highScore;
    public AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        DisplayScore();
    }

    // Update is called once per frame
    void DisplayScore()
    {
        score = SceneManager.instance.savedScore;
        //score = Random.Range(0, 12);
        Debug.Log(score.ToString());
        StartCoroutine(AnimateScoreDisplay());
    }

    IEnumerator AnimateScoreDisplay()
    {
        audioSource.PlayOneShot(highScore);
        yield return new WaitForSeconds(0.3f);

        scoreSprite.size = new Vector2(0f, scoreSprite.size.y);

        for (int i = 0; i < score; i++)
        {
            yield return new WaitForSeconds(0.03f);
            scoreSprite.size = new Vector2((float)i / 10f, scoreSprite.size.y);
            scoreSprite.color = new Color(1f-(float)i/100f,(float)i/100f,0.3f);
        }
        Debug.Log("j'ai fini");

        if (score >= 45 && score < 75)
        {
            Debug.Log("j'ai med");
            audioSource.Stop();
            audioSource.PlayOneShot(medScore);
        }
        else if (score < 45)
        {
            Debug.Log("j'ai low");
            audioSource.Stop();
            audioSource.PlayOneShot(lowScore);
        }
        
    }
}
