using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsModifier : MonoBehaviour
{
    [SerializeField] private bool doBath;
    [SerializeField] private bool doWokr;
    [SerializeField] private bool doClean;
    [SerializeField] private bool doEat;
    [SerializeField] private bool doSleep;
    [SerializeField] private int energyModification;
    [SerializeField] private int socialModification;
    [SerializeField] private int satietyModification;
    [SerializeField] private int thirstModification;
    [SerializeField] private int hygieneModification;
    [SerializeField] private int funModification;
    [SerializeField] private int stressModification;
    [SerializeField] private int happinessModification;
    [SerializeField] private int weightModification;
    [SerializeField] private int hoursSpent;
    [SerializeField] private bool itIsFun;
    [SerializeField] private bool notHealty;
    private int debuffStress = 0;
    private StateController stateController;

    [SerializeField] private GameController gameController;

    [SerializeField] private Passives passives;
    private TimeController timeController;

    public int passiveDebuffToEnergy { get; private set; } = 0;

    public void SetDebuffToEnergy(int debuff)
    {

        passiveDebuffToEnergy = debuff;
    }
    void Start()
    {

       
        if (gameController == null)
        {

            gameController = GameObject.Find("EventSystem").GetComponent<GameController>();
        }
        
        stateController = GameObject.Find("Player").GetComponent<StateController>();
        timeController = GameObject.Find("EventSystem").GetComponent<TimeController>();
        passives = GameObject.Find("EventSystem").GetComponent<Passives>();

    }


    public void PerformStateModifications()
    {
        if (itIsFun)
        {
            debuffStress = 10;
        }
        else
        {
            debuffStress = 0;
        }
   

        stateController.energy.ModifyValue(energyModification+passiveDebuffToEnergy);
        stateController.social.ModifyValue(socialModification);
        stateController.satiety.ModifyValue(satietyModification);
        stateController.thirst.ModifyValue(thirstModification);
        stateController.hygiene.ModifyValue(hygieneModification);
        stateController.fun.ModifyValue(funModification);
        stateController.stress.ModifyValue(stressModification-debuffStress);
        stateController.happines.ModifyValue(happinessModification);
        stateController.weigth.ModifyValue(weightModification);
        timeController.AddHours(hoursSpent);

    }


    public void ActivateAction()
    {
        if (doBath)
        {
            if (!stateController.bath.boolValue)
            {

                Bath();
            }
        }
        else if (doWokr)
        {
            if (stateController.energy.value >= 30)
            {
                Work();
            }
        }
        else if (doClean)
        {
            Clean();
        }
        else if (doEat)
        {

            if (timeController.hourCounter >= 8 && timeController.hourCounter <= 11)
            {
                Eat();
                stateController.brakefast.ModifyValue(true);
            }
            else if (timeController.hourCounter > 13 && timeController.hourCounter <= 16)
            {
                Eat();
                stateController.eat.ModifyValue(true);
            }
            else if (timeController.hourCounter > 19 && timeController.hourCounter <= 22)
            {
                Eat();
                stateController.dinner.ModifyValue(true);

            }
            else
            {
                Eat();
            }
        }
        else if (doSleep)
        {
            // Solo si el jugador tiene 40 o menos puntos de energía podrá dormir voluntariamente sin importar la hora.
            if (stateController.energy.value <= 40)
            {
                Sleep();
            }
        }
        else
        {
            PerformStateModifications();
            return;
        }
    }

    public void Bath()
    {
        //Bañarse genera 50 puntos de higiene, solo se puede hacer 1 vez cada 24hrs/juego (significativa 1 hora) consume 10 de energía y resta un punto de estrés. -interactuar en regadera-
        hygieneModification = 50;
        energyModification = -10;
        stressModification = -10;
        hoursSpent = 1;
        gameController.RunFade();
        stateController.bath.ModifyValue(true);
        PerformStateModifications();
    }
    public void Eat()
    {
        //Desayunar recargara 60 puntos de saciedad, recarga 10 de energía, consume 30 minutos/juego (significativa) -Interactuar con refrigerador-
        satietyModification = 60;
        energyModification = 10;
        hoursSpent = 1;

        passives.MealTook(notHealty);
        if (stateController.satiety.value+satietyModification>stateController.satiety.maxValue)
        {
            int exesiveEat = (stateController.satiety.value + satietyModification) - stateController.satiety.maxValue;
            for (int i = exesiveEat; i >0;)
            {
                weightModification += 1;
                i -= 5;
            }
        }


        gameController.RunFade();



        Debug.Log("state eat "+stateController.weigth.value);

        PerformStateModifications();
        Debug.Log("state eat after " + stateController.weigth.value);
    }
    public void Clean()
    {
        //Limpiar sumara 50 puntos al estatus de higiene, restara 30 de energía y restara 10 de diversión (significativa hora y media) – interactuar con objetos de limpieza en cuarto del lavado-
        hygieneModification = 50;
        energyModification = -30;
        funModification = -10;
        hoursSpent = 2;
        gameController.RunFade();
        stateController.clean.ModifyValue(true);
        PerformStateModifications();
    }
    public void Work()
    {
        if (timeController.hourCounter > 9)
        {
            int lateWorkTime = timeController.hourCounter - 9;
            stressModification = lateWorkTime * 10;
        }
        //Trabajar restara 20 puntos al estatus de diversión, restara 30 de energía y otorgara 10 de social.
        funModification = -20;
        energyModification = -30;
        socialModification = 10;
        //Trabajar restara 20 puntos al estatus de diversión, restara 30 de energía y otorgara 10 de social.


        //El trabajo inicia a las 9:00 am y termina a las 3:00 pm de lunes a viernes, se puede llegar tarde al trabajo, por cada hora de retraso se añadirá un punto de estrés adicional, sin importar si llega tarde la hora de salida siempre sera a las 2:00 pm(significativa) - interactuar con computadora-
        hoursSpent = 14 - timeController.hourCounter;
        //El trabajo inicia a las 9:00 am y termina a las 3:00 pm de lunes a viernes, se puede llegar tarde al trabajo, por cada hora de retraso se añadirá un punto de estrés adicional, sin importar si llega tarde la hora de salida siempre sera a las 2:00 pm (significativa) -interactuar con computadora-

        //Por cada 3 horas de trabajo se sumara un punto de estres
        stressModification = hoursSpent / 3;
        ///Por cada 3 horas de trabajo se sumara un punto de estres
        gameController.RunFade();
        stateController.work.ModifyValue(true);
        PerformStateModifications();
        //Mientras se realiza una actividad social, se duerme o se trabaja no se perderán puntos del estatus social
        passives.SetLastHour(passives.lastHour + hoursSpent);
    }
    public void Sleep()
    {
        //Dormir siempre consumirá 8hrs / juego del día y recargara el máximo de energía
       
        stateController.takignANap.ModifyValue(true);
        stateController.hygiene.ModifyValue(-100);
        energyModification = 100;
        hoursSpent = 8;
        funModification = (stateController.fun.value + stateController.social.value) / 2;
        gameController.RunFade();
        stateController.sleep.ModifyValue(true);
        PerformStateModifications();
        //Mientras se realiza una actividad social, se duerme o se trabaja no se perderán puntos del estatus social
        passives.SetLastHour(passives.lastHour + hoursSpent);
        if (stateController.weigth.value>80)
        {
            stateController.energy.ModifyMaxValue(80);
        }
        else
        {
            stateController.energy.ModifyMaxValue(100);
        }
        stateController.takignANap.ModifyValue(false);

    }



}
