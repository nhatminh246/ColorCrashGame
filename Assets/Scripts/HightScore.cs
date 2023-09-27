using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HightScore : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Text scoreText;
    [SerializeField] private Text hightScoreText;
    public Text hightScoreDefaulScreenText;
    public static HightScore Instance;


    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public  void ShowScoreAndHightScoreDieScreen()
    {
        int score = (PlayerPrefs.GetInt("Score"));
        scoreText.text = "Score: " + score ;
        int hightScore = (PlayerPrefs.GetInt("HightScore"));
        if (score > hightScore) { 
            PlayerPrefs.SetInt("HightScore", score);
            hightScore = score;
        }
        hightScoreText.text = "HightScore: " + hightScore;
        //PlayerPrefs.SetInt("Score", 0);
        //PlayerPrefs.SetInt("HightScore", 0);

    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
    public void ResetHightScore()
    {
        PlayerPrefs.SetInt("HightScore", 0);
        ShowHightScore(hightScoreDefaulScreenText);
    }
    public void ShowScore(Text scoreText)
    {
        int score = (PlayerPrefs.GetInt("Score"));
        scoreText.text = "Score: " + score;
    }
    public void ShowHightScore(Text hightScoreText)
    {
        int hightScore = (PlayerPrefs.GetInt("HightScore"));
        hightScoreText.text = "HightScore: " + hightScore;


    }
}
