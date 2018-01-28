using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public SpriteRenderer scoreSprite;
    int score;
    public AudioClip lowScore;
    public AudioClip medScore;
    public AudioClip highScore;
    public AudioClip pitchUp;
    public AudioSource audioSource;
    public Text scoreText;

    // Use this for initialization
    void Start ()
    {
        scoreText.text = "";
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
        audioSource.PlayOneShot(pitchUp);
        yield return new WaitForSeconds(0.3f);

        scoreSprite.size = new Vector2(0f, scoreSprite.size.y);

       

        for (int i = 0; i < score; i++)
        {
            yield return new WaitForSeconds(0.03f);
            scoreSprite.size = new Vector2((float)i / 10f, scoreSprite.size.y);
            scoreSprite.color = new Color(1f-(float)i/100f,(float)i/100f,0.3f);
            
            scoreText.text = " " + i + "%";
        }

        if (score >= 45 && score < 75)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(medScore);
        }
        else if (score < 45)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(lowScore);
        }
        else
        {
            audioSource.PlayOneShot(highScore);
        }

        scoreText.text = " " + score.ToString() + "%";

    }
}
