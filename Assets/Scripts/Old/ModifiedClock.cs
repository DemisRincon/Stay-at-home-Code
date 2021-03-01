using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifiedClock : MonoBehaviour
{
    //seconds in a day 86400
    public int timeSpeed = 1;
    private float totalSecondsIngame = 0;
    public TextMeshProUGUI secondShow;
    public TextMeshProUGUI dayText;
    private int dayCounter;
    private int hourCounter;
    private int minutesCounter;
    private int secondsCounter;
    private float degrees;
    public GameObject Sun;
    private string[] dayName = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        int totalsec;

        totalSecondsIngame += Time.deltaTime * timeSpeed;
        dayCounter = (int)totalSecondsIngame / 84600;
        hourCounter = (int)(totalSecondsIngame - (dayCounter * 86400)) / 3600;
        minutesCounter = (int)(totalSecondsIngame - (hourCounter * 3600)) / 60;
        secondsCounter = (int)totalSecondsIngame % 60;
        totalsec = (int)totalSecondsIngame;
        degrees = (totalSecondsIngame / 240) - 120f;
        Sun.transform.localEulerAngles = new Vector3(degrees, -90f, 0f);
        secondShow.text = ("Hora ") + hourCounter.ToString() + (" : ") + minutesCounter.ToString();
        dayText.text = "Día: " + dayName[dayCounter].ToString();





    }


    public float GetSeconds()
    {
        return totalSecondsIngame*Time.deltaTime;
    }

    public int GetDay (){
        return  dayCounter;
    }

    public int GetHour()
    {
        return hourCounter;
    }

    public int GetMinute()
    {
        return minutesCounter;
    }





    public void IncressTime(float timeAdded)
    {
        totalSecondsIngame += Time.deltaTime * timeAdded;
    }
}
