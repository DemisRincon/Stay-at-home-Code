using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passives : MonoBehaviour
{
    ActionsModifier actionsModifier;
    private StateController stateController;
    private TimeController timeController;

    private int hoursWithHunger = 0;
    private int hoursWithThirst = 0;
    public int lastHour { get; private set; }

    public int notHealtyMeal { get; private set; } = 0;


    // Start is called before the first frame update
    void Start()
    {
        stateController = GameObject.Find("Player").GetComponent<StateController>();
        actionsModifier = gameObject.GetComponent<ActionsModifier>();
        timeController = gameObject.GetComponent<TimeController>();
        lastHour = timeController.GetHour();
    }

    // Update is called once per frame
    void Update()
    {
        int diference = timeController.hourCounter - lastHour;
        StartCoroutine(CheckForDebuff());
        StartCoroutine(PassivePerHour(diference));
        StartCoroutine(PassiveNoEnegy());
        StartCoroutine(CheckForDeath());
        StartCoroutine(CheckForHygiene(diference));
        SetLastHour(lastHour + diference);
    }

    IEnumerator CheckForHygiene(int diference)
    {

        if (diference != 0&&timeController.hourCounter==0)
        {
            stateController.stress.ModifyValue(10);
        }
        yield return null;
    }

    IEnumerator CheckForDebuff()
    {

        if (stateController.satiety.value < 50 && !stateController.takignANap.boolValue)
        {
            actionsModifier.SetDebuffToEnergy(5);
        }
        else
        {
            actionsModifier.SetDebuffToEnergy(0);
        }
        yield return null;
    }

    IEnumerator CheckForDeath()
    {
        
        if (
            hoursWithHunger>=12||
            hoursWithThirst>12||
            (stateController.stress.value >= 80 && stateController.takignANap.boolValue)
            )
        {
            Debug.Log("YOU ARE DEAD");
        }

      yield return null;
    }

    IEnumerator PassivePerHour(int diference)
    {

        //Cada hora/juego el jugador pierde 10 puntos de social.
        if (diference != 0 && (timeController.dayCounter != 0 || timeController.hourCounter > 7))
        {

            for (int i = 0; i < diference; i++)
            {
                stateController.satiety.ModifyValue(-10);
                stateController.thirst.ModifyValue(-10);
                if (stateController.satiety.value <= 20)
                {
                    hoursWithHunger++;
                }
                else
                {
                    hoursWithHunger = 0;
                }
                if (stateController.thirst.value<40)
                {
                    hoursWithThirst++;
                }
                else
                {
                    hoursWithThirst = 0;
                }
            }
            if (!stateController.takignANap.boolValue)
            {
                for (int i = 0; i < diference; i++)
                {
                    stateController.social.ModifyValue(-10);


                    if (stateController.fun.value<=40)
                    {
                        stateController.stress.ModifyValue(-10);
                    }
                    stateController.fun.ModifyValue(-10);

                    if (stateController.social.value <= 60)
                    {
                        stateController.stress.ModifyValue(10);
                    }
                }
                yield return new WaitForSeconds(2);
            }
        }

        yield return null;
    }
    IEnumerator PassiveNoEnegy()
    {
        //Al llegar la energía a 0 el jugador se quedara dormido 8 horas / juego
        if (stateController.energy.value <= 0 && !stateController.takignANap.boolValue)
        {

            actionsModifier.Sleep();

            yield return new WaitForSeconds(2);
        }
        yield return null;
    }
    public void MealTook(bool noHealty)
    {
        if (noHealty)
        {
            notHealtyMeal += 1;
        }
        return;
    }
    public void SetLastHour(int hour)
    {
        if (hour > 23)
        {
            lastHour = hour - 24;
        }
        else
        {

            lastHour = hour;
        }
    }
}
