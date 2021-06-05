using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public bool GameIsPaused { get;private set; } = false;
    [SerializeField] public bool SeenRecomendations { get; private set; } = false;
    public GameObject PauseMenuUI;
    public GameObject PalyerMenu;
    public GameObject RecomendationsMenu;
    public GameObject GameOverMenu;
    
    private bool pressR = false;
    private bool pressEscape = false;
    private GameObject joystick;
    private GameObject touchPanel;
    void Start()
    {
        joystick = GameObject.Find("Fixed Joystick");
        touchPanel = GameObject.Find("TouchPanel");
    }
    // Update is called once per frame
    void Update()
    {
        //PressR();
        if ((Input.GetKeyDown(KeyCode.Escape) || (pressEscape)) && !SeenRecomendations)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();                
            }
            pressEscape = false;
        }
        if ((Input.GetKeyDown(KeyCode.R)||(pressR))&&!GameIsPaused)
        {
          

            if (SeenRecomendations)
            {
                HideRecomendations();
            }
            else
            {
                ShowRecomendations();
            }
            pressR = false;
        }
    }

    private void HideRecomendations()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        RecomendationsMenu.SetActive(false);
        PalyerMenu.SetActive(true);
        Time.timeScale = 1f;
        SeenRecomendations = false;
        joystick.SetActive(true);
        touchPanel.SetActive(true);
    }

    private void ShowRecomendations()
    {
        //Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        RecomendationsMenu.SetActive(true);
        PalyerMenu.SetActive(false);
        Time.timeScale = 0f;
        SeenRecomendations = true;
        joystick.SetActive(false);
        touchPanel.SetActive(false);
    }

    public void GameOver()
    {
        //Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        GameOverMenu.SetActive(true);
        PalyerMenu.SetActive(false);
        Time.timeScale = 0f;
        joystick.SetActive(false);
        touchPanel.SetActive(false);
    }

    public void Resume()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        PalyerMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        joystick.SetActive(true);
        touchPanel.SetActive(true);
    }

    void Pause()
    {
        //Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        PalyerMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        joystick.SetActive(false);
        touchPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }

    public void LoadCredits()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(2);

    }

    public void PressR()
    {
        pressR = true;        
    }
    public void PressEscape()
    {
        pressEscape = true;
    }    
}
