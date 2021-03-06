using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class PassiveController : MonoBehaviour
{
    private StateController stateController;
    [SerializeField] private GameController gameController;
    private int hungerPenalization = 0;
    private int hungerTime = 0;
    private int thirstTime = 0;
    ObjectInteractable currentFocus;
    private TimeController timeController;
    private int noHealtyFoodDay = 0;
    private int foodDay = 0;
    private VideoPlayer video;
    private AudioSource spotify;
    private int stresCounter;
   private GameOverAnalisis gameOverAnalisis;
    // Start is called before the first frame update
    void Start()
    {
        stateController = gameObject.GetComponent<StateController>();
        gameController = GameObject.Find("EventSystem").GetComponent<GameController>();
        timeController = GameObject.Find("EventSystem").GetComponent<TimeController>();
        video = GameObject.Find("TVCanvas").GetComponent<VideoPlayer>();
        spotify = GameObject.Find("Spotify").GetComponent<AudioSource>();
        gameOverAnalisis = gameObject.GetComponent<GameOverAnalisis>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckForInstnatPassive());
    }

    public void PerfromModification(ObjectInteractable focus)
    {

        currentFocus = focus;

        
        
        if (focus.doBath)
        {
            Bath();
        }
        else if (focus.doEat)
        {
            Eat();
        }
        else if (focus.doClean)
        {
            Clean();
        }
        else if (focus.doSleep)
        {
            Sleep();
        }
        else if (focus.doWork)
        {
            Work();
        }
        else if (focus.doACall)
        {
            Call();
        }
        else if (focus.doExercise)
        {
            Exercise();
        }
        else if (focus.turnOnTV)
        {
            VideoClip videoClip = video.GetComponent<AudioRandom>().GetVideoSource();
            if (video.isPlaying)
            {
                video.Stop();
            }
            else
            {
                video.clip = videoClip;
                video.Play();
                GeneralMod();
            }
        }
        else if (focus.turnOnStereo)
        {
            AudioClip audioClip = spotify.GetComponent<AudioRandom>().GetAudioSource();
            if (spotify.isPlaying)
            {
                spotify.Stop();
            }
            else
            {
                spotify.clip = audioClip;
                spotify.Play();
                GeneralMod();
            }
        }
        else
        {
            GeneralMod();
        }


        currentFocus = null;


    }

    private void Exercise()
    {
        stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
        stateController.stress.ModifyValue(currentFocus.stressModification);
        if (stateController.weigth.value + currentFocus.weightModification >= 74)
        {
            stateController.weigth.ModifyValue(currentFocus.weightModification);
        }
        timeController.AddHours(currentFocus.hoursSpent);
        gameController.RunFade();
    }

    private void Call()
    {
        if (timeController.hourCounter >= 16 && timeController.hourCounter <= 23)
        {
            stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
            stateController.social.ModifyValue(currentFocus.socialModification);
            timeController.AddHours(currentFocus.hoursSpent);
            gameController.RunFade();
        }
    }
    private void GeneralMod()
    {

        stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
        stateController.social.ModifyValue(currentFocus.socialModification);
        AddWeigthForExtraSatitey();
        stateController.satiety.ModifyValue(currentFocus.satietyModification);
        stateController.thirst.ModifyValue(currentFocus.thirstModification);
        stateController.hygiene.ModifyValue(currentFocus.hygieneModification);
        stateController.fun.ModifyValue(currentFocus.funModification);
        stateController.stress.ModifyValue(currentFocus.stressModification);
        stateController.happines.ModifyValue(currentFocus.happinessModification);
        if (stateController.weigth.value+currentFocus.weightModification>=74)
        {

        stateController.weigth.ModifyValue(currentFocus.weightModification);
        }
        
        if (currentFocus.isFood)
        {
            foodDay++;
            if (currentFocus.notHealty)
            {


                noHealtyFoodDay++;
            }
            CheckWeightIncressing();
        }
        timeController.AddHours(currentFocus.hoursSpent);
    }

    private void Work()
    {

        if ((timeController.hourCounter >= 9 && timeController.hourCounter < 15) && !stateController.work.boolValue)
        {
            int late = timeController.hourCounter - 9;
            int workingHours = 15 - timeController.hourCounter;
            int stressAdded = (workingHours / 4);
            int prevsocial = stateController.social.value;
            stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
            stateController.social.ModifyValue(prevsocial + currentFocus.socialModification);
            stateController.stress.ModifyValue(10 * late + 10 * stressAdded);
            stateController.fun.ModifyValue(currentFocus.funModification);
            stateController.work.ModifyValue(true);
            timeController.AddHours(workingHours);
            gameController.RunFade();
        }
        else
        {
            stateController.energy.ModifyValue(-10 - hungerPenalization);
            stateController.fun.ModifyValue(40);
            stateController.social.ModifyValue(30);
            stateController.stress.ModifyValue(-20);
            timeController.AddHours(3);

        }
    }

    private void Clean()
    {
        stateController.fun.ModifyValue(currentFocus.funModification);
        stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
        stateController.hygiene.ModifyValue(currentFocus.hygieneModification);
        stateController.clean.ModifyValue(true);
        timeController.AddHours(currentFocus.hoursSpent);
        gameController.RunFade();
    }

    private void Eat()
    {

        if ((timeController.hourCounter >= 8 && timeController.hourCounter <= 11) && !stateController.brakefast.boolValue)
        {
            stateController.brakefast.ModifyValue(true);
        }
        else if ((timeController.hourCounter > 13 && timeController.hourCounter <= 16) && !stateController.eat.boolValue) 
        {
            stateController.eat.ModifyValue(true);
        }
        else if ((timeController.hourCounter > 19 && timeController.hourCounter <= 22) && !stateController.dinner.boolValue)
        {
            stateController.dinner.ModifyValue(true);
        }
        else
        {

        AddWeigthForExtraSatitey();
        }
        foodDay++;
        CheckWeightIncressing();
        stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
        stateController.satiety.ModifyValue(currentFocus.satietyModification);
        timeController.AddHalfHour();
        gameController.RunFade();

    }

    private void AddWeigthForExtraSatitey()
    {
        if (stateController.satiety.value + currentFocus.satietyModification > stateController.satiety.maxValue)
        {
            Debug.Log("enter to satiety");

            stateController.weigth.ModifyValue(((stateController.satiety.value + currentFocus.satietyModification) - stateController.satiety.maxValue) / 20);
        };
    }

    private void CheckWeightIncressing()
    {
        if (foodDay == 5)
        {
            stateController.weigth.ModifyValue(1);
            foodDay = 0;
        }
        if (noHealtyFoodDay == 3)
        {
            stateController.weigth.ModifyValue(1);
            noHealtyFoodDay = 0;
        }
        
    }

    private void Bath()
    {
        if (!stateController.bath.boolValue)
        {
            stateController.bath.ModifyValue(true);
            stateController.energy.ModifyValue(currentFocus.energyModification - hungerPenalization);
            stateController.hygiene.ModifyValue(currentFocus.hygieneModification);
            timeController.AddHours(currentFocus.hoursSpent);
            gameController.RunFade();
        }

    }

    private void Sleep()
    {
        if (stateController.energy.value <= 40)
        {
            int prevsocial = stateController.social.value;
            int prevFun = stateController.social.value;
            stateController.takignANap.ModifyValue(true);
            stateController.sleep.ModifyValue(true);
            
            if (stateController.weigth.value >= 80)
            {
                stateController.energy.ModifyMaxValue(80);
            }
            else
            {
                stateController.energy.ModifyMaxValue(100);
            }
            if (stateController.stress.value >= 80)
            {
                GameOver("stress");
            }
            stateController.energy.ModifyValue(100);
            stateController.stress.ModifyValue(-20);
            stateController.hygiene.ModifyValue(-100);
            gameController.RunFade();
            timeController.AddHours(8);
            stateController.takignANap.ModifyValue(false);
        }
    }
    IEnumerator CheckForInstnatPassive()
    {
        if (stateController.satiety.value <= 50)
        {
            hungerPenalization = 5;
        }
        else
        {
            hungerPenalization = 0;
        }
        if (stateController.energy.value == 0)
        {
            Sleep();
            yield return new WaitForSeconds(2);
        }
        yield return null;
    }



    public void GameOver(string reason)
    {
        gameOverAnalisis.GameOverReason(reason);

    }

    public void PerformActionByHour(int hour)
    {
        Debug.Log("TIME DAY"+timeController.dayCounter);

        if (hour==23)
        {
            CleanStatus();            
        }

        if (hour == 15&& stateController.work.boolValue==false)
        {
            GameOver("work");
        }
        if (stateController.weigth.value > 95)
        {
            GameOver("fat");
        }
        if (stateController.stress.value>=90)
        {
            stresCounter++;
        }
        else
        {
            stresCounter = 0;
        }
        if (stresCounter>=5)
        {
            GameOver("stress");
        }
        if (stateController.social.value <= 60 && !stateController.takignANap.boolValue)
        {
            stateController.stress.ModifyValue(10);
        }
        if (stateController.fun.value <= 40 && !stateController.takignANap.boolValue)
        {
            stateController.stress.ModifyValue(10);
        }
        if (!stateController.takignANap.boolValue)
        {
            stateController.fun.ModifyValue(-10);
            stateController.energy.ModifyValue(-5);
        }
        if (hour == 23 && stateController.hygiene.value <= 50 && (!stateController.takignANap.boolValue))
        {
            stateController.stress.ModifyValue(10);
        }
        if (stateController.satiety.value <= 20)
        {
            hungerTime++;
        }
        else
        {
            hungerTime = 0;
        }
        if (stateController.thirst.value <= 40)
        {
            thirstTime++;
        }
        else
        {
            thirstTime = 0;
        }

        if (thirstTime >= 12 )
        {
            GameOver("hid");
        }
        if ( hungerTime >= 12)
        {
            GameOver("hunger");
        }
        stateController.social.ModifyValue(-5);
        stateController.satiety.ModifyValue(-10);
        stateController.thirst.ModifyValue(-10);
        stateController.happines.ModifyMaxValue((stateController.fun.value+stateController.social.value)/ 2);

        if (hour==23&&timeController.dayCounter==6)
        {
            GameOver("win");
        }
    }

    public void CleanStatus()
    {
        stateController.brakefast.ModifyValue(false);
        stateController.bath.ModifyValue(false);
        stateController.work.ModifyValue(false);
        stateController.clean.ModifyValue(false);
        stateController.eat.ModifyValue(false);
        stateController.dinner.ModifyValue(false);
        stateController.sleep.ModifyValue(false);
        noHealtyFoodDay = 0;
        foodDay = 0;
    }
}
