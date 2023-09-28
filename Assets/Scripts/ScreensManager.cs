using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public enum Screens
{
    MAIN_MENU,
    IN_GAME,
    END_GAME,
    PAUSE_GAME
}
public class ScreensManager : MonoBehaviour
{
    public static ScreensManager Instance;
    public GameObject currentScreen;
    [Header("MainMenuPanel")]
    public GameObject mainMenuPanel;
    public Text hightScore;

    [Header("InGamePanel")]
    public GameObject gamePanel;

    [Header("DiePanel")]
    public GameObject diePanel;
    public Text score;
    public Text resultHightScore;


    [Header("Transition")]
    public GameObject fadeScreen;
    public Animator animatorFade;

    private void Awake()
    {
        Instance = this;
        currentScreen = mainMenuPanel;
        ChangeScreen(Screens.MAIN_MENU);
    }

    public void ChangeScreen(Screens screen)
    {
        currentScreen.SetActive(false);
        switch (screen)
        {
            case Screens.MAIN_MENU:
                animatorFade.SetBool("isFade",true);
                hightScore.text = "Hight score: " + HightScore.Instance.GetHightScore();
                currentScreen = mainMenuPanel;
                
                break;
            case Screens.IN_GAME:
                currentScreen = gamePanel;
                break;
            case Screens.END_GAME:
                currentScreen = diePanel;
                score.text = "Score:" + HightScore.Instance.GetScore();
                resultHightScore.text = "Hight score: " + HightScore.Instance.GetHightScore();


                break;
        }
        currentScreen.SetActive(true);
    }
    public void RestartGame()
    {
        fadeScreen.SetActive(true);
        animatorFade.SetBool("isFade", false);
        StartCoroutine(StartGame());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator StartGame()
    {
        // Play am thanh cua button

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    
}
