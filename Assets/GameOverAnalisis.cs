using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnalisis : MonoBehaviour
{
    [SerializeField] private GameObject hungerText;
    [SerializeField] private GameObject hidratationText;
    [SerializeField] private GameObject stressText;
    [SerializeField] private GameObject fatText;
    [SerializeField] private GameObject workText;
    [SerializeField] private PauseMenu pauseMenu;
    // Start is called before the first frame update
    private string reason="";


    void Start()
    {
       
    }


    public void GameOverReason(string reason)
    {
        this.reason = reason;
        pauseMenu.GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (reason=="work")
        {
            workText.SetActive(true);
        }
        else
        {
            workText.SetActive(false);
        }
        if (reason == "fat")
        {
            fatText.SetActive(true);
        }
        else
        {
            fatText.SetActive(false);
        }
        if (reason == "stress")
        {
            stressText.SetActive(true);
        }
        else
        {
            stressText.SetActive(false);
        }
        if (reason == "hid")
        {
            hidratationText.SetActive(true);
        }
        else
        {
            hungerText.SetActive(false);
        }
        if (reason == "hunger")
        {
            hungerText.SetActive(true);
        }
        else
        {
            hungerText.SetActive(false);
        }
    }
}
