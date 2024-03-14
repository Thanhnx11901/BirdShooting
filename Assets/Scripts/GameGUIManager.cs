using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;

    public Dialog gameDialog;
    public Dialog pauseDialog;

    public Image fireRateFilled;
    public Text timeText;
    public Text killedCountingText;

    public Dialog m_curDialog { get; set; }
    public override void Awake()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            // Yêu cầu người chơi quay ngang màn hình
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        MakeSingleton(false);
    }
    public void ShowGameGui(bool isShow)
    {
        if (gameGui)
        {
            gameGui.SetActive(isShow);
        }
        if (homeGui)
        {
            homeGui.SetActive(!isShow);
        }
    }
    public void UpdateTimer(string time)
    {
        if (timeText)
        {
            timeText.text = time;
        }
    }
    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
        {
            killedCountingText.text = "x" + killed.ToString();//x1000
        }
    }
    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
        {
            fireRateFilled.fillAmount = rate;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseDialog)
        {
            pauseDialog.Show(true);
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED: " + Prefs.bestScore);
            m_curDialog = pauseDialog;
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (m_curDialog) 
        {
            m_curDialog.Show(false);
        }
    }
    public void BackToHome()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    public void Replay()
    {
        if (m_curDialog)
        {
            m_curDialog.Show(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void ExitGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }

}
