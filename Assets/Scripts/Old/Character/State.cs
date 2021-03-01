using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class State {

    public int maxValue { get; private set; }
    public int currentValue { get; private set; }

    public State(int maxValue)
    {
        this.maxValue = maxValue;
        currentValue = maxValue;
      
    }
    public State(int maxValue,bool inverse)
    {
        this.maxValue = maxValue;
        if (inverse)
        {
        currentValue = 0;

        }

    }
    public void ModifyState(int modification)
    {
       int newValue = currentValue + modification;

        if (newValue>maxValue)
        {
            currentValue = maxValue;
        }else if (newValue < 0)
        {
            currentValue = 0;
        }
        else
        {
            currentValue = newValue;
        }


    }



}
