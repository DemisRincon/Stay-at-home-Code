using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModel
{
    public int maxValue { get; private set; } = 0;
    public int minValue { get; private set; } = 0;
    public int value { get; private set; }

    public bool boolValue { get; private set; }

   
    public StateModel(int maxValue, bool isMinVale, int value)
    {


        if (isMinVale)
        {
            this.minValue = maxValue;
        }
        else
        {
            this.maxValue = maxValue;
        }
        this.value = value;
    }



    public StateModel(bool value)
    {
        boolValue = value;
    }


    public void ModifyValue(bool newValue)
    {
        boolValue = newValue;
    }

    public void ModifyValue(int newValue)
    {
        if (value + newValue > maxValue && maxValue != 0)
        {
            value = maxValue;
        }
        else if (value + newValue < 0)
        {
            value = 0;
        }
        else
        {
            value += newValue;

        }

    }

    public void ModifyMaxValue(int newValue)
    {
        maxValue = newValue;
        if (value > newValue)
        {
            value = newValue;
        }
    }
}
