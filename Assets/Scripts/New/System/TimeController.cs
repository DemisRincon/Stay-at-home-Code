using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private int timeSpeed;
    [SerializeField] private float totalSeconds = 85400;
    [SerializeField] private TextMeshProUGUI hourText;
    [SerializeField] private TextMeshProUGUI dayText;
    private PassiveController passive;
    public int dayCounter { get; private set; }
    public int hourCounter { get; private set; }
    private string[] days = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
    private int minutesCounter;
    private readonly int secondsDay = 84600;
    private readonly int secondsHour = 3600;
    public
        GameObject Sun;
    private float degrees;
    private int hourPrev;
    void Start()
    {
        hourPrev = hourCounter;
        passive = GameObject.Find("Player").GetComponent<PassiveController>();
    }

    // Update is called once per frame
    void Update()
    {
        CountAndModifyTime();
        CalculateDegrees();
        MoveSun();
        ChangeText();
        if (totalSeconds>=590000)
        {
            passive.GameOver("win");
        }
    }


    public int GetHour()
    {
        return hourCounter;
    }
    public void AddHours(int hours)
    {
        totalSeconds += hours * secondsHour;
    }
    public void AddHalfHour()
    {
        totalSeconds += secondsHour / 2;
    }
    private void ChangeText()
    {

        if (hourText != null)
            hourText.text = ("Hora ") + hourCounter.ToString() + (" : ") + minutesCounter.ToString();
        if (dayText != null)
            dayText.text = "Día: " + days[dayCounter];
    }

    private void MoveSun()
    {
        if (Sun != null)
        {
            Sun.transform.localEulerAngles = new Vector3(degrees, -90f, 0f);
        }
    }

    private void CalculateDegrees()
    {
        degrees = (totalSeconds / 240) - 120f;
    }

    private void CountAndModifyTime()
    {
        totalSeconds += Time.deltaTime * timeSpeed;
        dayCounter = (int)(totalSeconds / secondsDay);
        hourCounter = (int)(Mathf.Floor((totalSeconds - (dayCounter * secondsDay)) / secondsHour));
        int diferenceBetweenHours;

        if (hourCounter != hourPrev)
        {
            diferenceBetweenHours = DiferenceHours(hourCounter);
            if (dayCounter == 0 && hourCounter <= 7)
            {
                hourPrev = hourCounter;
                return;
            }
            passive.PerformActionByHour(hourCounter);
            hourPrev = hourCounter;
        }
        minutesCounter = (int)(Mathf.Floor(totalSeconds - (dayCounter * secondsDay) - (hourCounter * secondsHour)) / 60);
    }

    public int DiferenceHours(int hourCounter)
    {

        if (hourCounter > hourPrev)
        {            
            return (hourCounter - hourPrev);
        }
        else
        {
            passive.CleanStatus();
            return (hourCounter - hourPrev) + 24;
        }
    }
}
