using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController singleton;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _tipsPanel;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _LooseMenu;
    [SerializeField] private AudioListener _mainAudioListener;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _HINT;

    private static bool _isFirstStart = true;


    private void Start()
    {
        singleton = this;
        _mainMenu.SetActive(true);
        _gameCanvas.SetActive(false);
        _continueButton.SetActive(false);
        Pause.State = Pause.States.Pause;
        Time.timeScale = 0;

        if (!_isFirstStart)
        {
            _gameCanvas.SetActive(true);
            OnStart();
        }
    }


    public void OnPauseGame()
    {
        _mainMenu.SetActive(true);
        _gameCanvas.SetActive(false);
        _startButton.SetActive(false);
        _continueButton.SetActive(true);
        Pause.State = Pause.States.Pause;
    }
    public void STARTTIPS()
    {
        _tipsPanel.SetActive(false);
        Pause.State = Pause.States.Play;
    }

    public void OnStart()
    {
        
        _mainMenu.SetActive(false);
        _LooseMenu.SetActive(false);
        _gameCanvas.SetActive(true);
        //Pause.State = Pause.States.Play;
        
    }
    public void OnContinue()
    {
        _mainMenu.SetActive(false);
        _gameCanvas.SetActive(true);
        Pause.State = Pause.States.Play;
    }
    public void OnRestart()
    {
        Building.DeathCounter = 0;
        _isFirstStart = false;
        SceneManager.LoadScene(0);
        Pause.State = Pause.States.Play;
        _mainMenu.SetActive(false);
        _gameCanvas.SetActive(true);
    }

    public void OnRestartFromLoose()
    {
        SceneManager.LoadScene(0); 
    }

    public void OnCredits()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(true);
    }
    public void OnBackFromCredits()
    {
        _creditsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
    public void OnBackFromWin()
    {
        _winMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void OnSound()
    {
        if (_mainAudioListener.enabled)
        {
            _mainAudioListener.enabled = false;
        }
        else
        {
            _mainAudioListener.enabled = true;
        }
    }
    public void OnExit()
    {
        Application.Quit();
    }


    public void ShowHint()
    {
        _HINT.SetActive(true);
        Pause.State = Pause.States.Pause;
    }
    public void ResumePause()
    {
        _HINT.SetActive(false);
        Pause.State = Pause.States.Play;
    }
}
