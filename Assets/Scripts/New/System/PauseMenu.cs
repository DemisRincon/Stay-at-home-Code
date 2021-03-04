using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public bool GameIsPaused { get;private set; } = false;
    [SerializeField] public bool SeenRecomendations { get; private set; } = false;
    public GameObject PauseMenuUI;
    public GameObject PalyerMenu;
    public GameObject RecomendationsMenu;
    public GameObject GameOverMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!SeenRecomendations)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();

                
            }
        }
        if (Input.GetKeyDown(KeyCode.R)&&!GameIsPaused)
        {
          

            if (SeenRecomendations)
            {
                HideRecomendations();
            }
            else
            {
                ShowRecomendations();
            }
        }
    }

    private void HideRecomendations()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RecomendationsMenu.SetActive(false);
        PalyerMenu.SetActive(true);
        Time.timeScale = 1f;
        SeenRecomendations = false;
    }

    private void ShowRecomendations()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        RecomendationsMenu.SetActive(true);
        PalyerMenu.SetActive(false);
        Time.timeScale = 0f;
        SeenRecomendations = true;
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameOverMenu.SetActive(true);
        PalyerMenu.SetActive(false);
        Time.timeScale = 0f;
       
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        PalyerMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        PalyerMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
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

}
