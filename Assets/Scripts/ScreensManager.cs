using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public GameObject mainMenuPanel;
    public GameObject gamePanel;
    public GameObject diePanel;
    public GameObject pausePanel;
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
                currentScreen = mainMenuPanel;
                
                break;
            case Screens.IN_GAME:
                currentScreen = gamePanel;
                break;
            case Screens.PAUSE_GAME:
                currentScreen = pausePanel;
                break;
            case Screens.END_GAME:
                currentScreen = diePanel;
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
