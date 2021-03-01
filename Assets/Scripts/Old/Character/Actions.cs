using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    [SerializeField] private int hoursSleeping = 8;
    [SerializeField] private int hoursWorking = 6;





    public bool isSleeping { get; private set; }
    public bool isWorking { get; private set; }
    public FullSate currentState;
    public MakeFade transition;
    public ModifiedClock clock;
    private int previousHour;
    private int continousHunger = 0;
    private bool isDead;
    private void Start()
    {
        currentState = gameObject.GetComponent<SatatusController>().GetState();
        previousHour = clock.GetHour();
    }





    private void Update()
    {
        LoseSocialPasive();
        GetStressPassive();
        LoseSatityPassive();
        ChekHunger();
        CheckDeathCondition();





        FitHour();
    }

    private void ApplyHungerPenalizatio()
    {
        if (currentState.GetSatiety().currentValue<50)
        {
            currentState.GetEnergy().ModifyState(-5);
        }
    }





    private void ChekHunger()
    {
        if (currentState.GetSatiety().currentValue <= 20&& (previousHour != clock.GetHour()))
        {
            continousHunger++;
        }
        else
        {
            continousHunger = 0;
        }
    }

    private void CheckDeathCondition()
    {
        if (continousHunger >= 20)
        {
            isDead = true;
        }
        if (isDead)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("shoud game over screen");

    }

    private void LoseSatityPassive()
    {
        if ((previousHour != clock.GetHour()))
        {
            currentState.GetSatiety().ModifyState(-10 * (clock.GetHour() - previousHour));
        }
    }


    private void LoseSatityActive(int cuantity)
    {
        currentState.GetSatiety().ModifyState(-10 * cuantity);
    }
    private void GetStressPassive()
    {
        if ((currentState.GetSocial().currentValue <= 60) && (previousHour != clock.GetHour()))
        {
            currentState.GetStress().ModifyState(10 * (clock.GetHour() - previousHour));
        }
    }
    private void LoseSocialPasive()
    {
        if ((previousHour != clock.GetHour()))
        {
            currentState.GetSocial().ModifyState(-10 * (clock.GetHour() - previousHour));
        }
    }

    private void FitHour()
    {
        previousHour = clock.GetHour();
    }

    public void Sleep()
    {

        isSleeping = true;
        transition.ApplyTransition();
        clock.IncressTime(hoursSleeping * 3600);
        LoseSatityActive(hoursSleeping);
        FitHour();
        if (!currentState.sleep)
        {
            currentState.Sleep();
        }
        currentState.GetEnergy().ModifyState(100);

        isSleeping = false;


        Debug.Log("should apper a leter for some seconds that said that you felt asleep");
    }


    public void Work()
    {

        /*
        
        previousHour = clock.GetHour() + hoursWorking;

        */
        Debug.Log("Hello World working");
        isWorking = true;
        transition.ApplyTransition();
        clock.IncressTime(hoursWorking * 3600);
        FitHour();
        ApplyHungerPenalizatio();
        if (!currentState.work)
        {
            currentState.Work();
        }

        isWorking = false;
    }



}
