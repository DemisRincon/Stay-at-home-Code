using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    //Variable
    //Start with min at 2040 to make the sun appear at 7am
    //Set the timeSpeed at 1 to make a day = 48mins if the conditions asks for min >= 2880
    public float min = 2040;
    public float degrees;
    public float timeSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        //1 day = 48 min
        min += timeSpeed * Time.deltaTime;//2 sec = 1 min
        if (min >= 2880)//2880min = 1 dia
        {
            min = 0;
        }
        //360° / 2880  --> 1° = 0.125min
        degrees = min / 8;
        this.transform.localEulerAngles = new Vector3(degrees, -90f, 0f);

    }
}
