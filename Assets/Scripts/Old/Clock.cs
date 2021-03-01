using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [Tooltip("Initial time in seconds")]
    public int initialTime;

    //Set range at 30f so the time goes 1 hr == 2 min in real life
    [Tooltip("Clock time scale")]
    [Range(30f, 20000.0f)]
    public float timeScale = 1f;

    private Text myText;
    private float frameTimeWithTimeScale = 0f;
    private float timeInSecondsToShow = 0f;
    private int days = 1;
    
    // Start is called before the first frame update
    void Start()
    {

        myText = GetComponent<Text>();

        timeInSecondsToShow = initialTime;

        updateClock(initialTime);
    }

    // Update is called once per frame
    void Update()
    {

        //time of each frame related to the time scale
        frameTimeWithTimeScale = Time.deltaTime * timeScale;

        //saves time elapsed to show it in the clock
        timeInSecondsToShow += frameTimeWithTimeScale;
        updateClock(timeInSecondsToShow);
                
    }

    public void updateClock(float timeInSeconds)
    {
        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        string clockText;

        //Make sure time is not a negative
        if (timeInSeconds < 0) timeInSeconds = 0;
        //

        //calculate hours, minutes and seconds
        hours = (int)timeInSeconds / 3600;
        minutes = (int)(timeInSeconds - (hours * 3600)) / 60;
        seconds = (int)timeInSeconds % 60;

        if (hours > 23)
        {
            hours = 0;
            timeInSecondsToShow = 0f;
            days += 1;
        }
        //full clock string (to see minutes and seconds)
        clockText = "Hr:  " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00") + "\n" + "Day: " + days.ToString();
        
        //Print clock string 
        //clockText = "Hr:  " + hours.ToString("00") + "\n" + "Day: " + days.ToString();

        //Update text
        myText.text = clockText;
    }

}
