using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentStatus : MonoBehaviour
{
    [SerializeField] private StateController stateController;
    [SerializeField] private GameObject energyText;
    [SerializeField] private GameObject satietyText;
    [SerializeField] private GameObject thirstText;
    [SerializeField] private GameObject funText;
    [SerializeField] private GameObject stressText;
    [SerializeField] private GameObject happinessText;
    [SerializeField] private GameObject weightText;
    [SerializeField] private GameObject brakefatCheck;
    [SerializeField] private GameObject bathCheck;
    [SerializeField] private GameObject workCheck;
    [SerializeField] private GameObject cleanCheck;
    [SerializeField] private GameObject eatCheck;
    [SerializeField] private GameObject dinnerCheck;
    private TimeController timeController;
    // Start is called before the first frame update
    void Start()
    {
        stateController = GameObject.Find("Player").GetComponent<StateController>();
        timeController = GameObject.Find("EventSystem").GetComponent<TimeController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!stateController.bath.boolValue && stateController.hygiene.value <= 30)
        {
            bathCheck.SetActive(true);
        }
        else
        {
            bathCheck.SetActive(false);
        }



        if ((timeController.hourCounter >= 19 && timeController.hourCounter < 22) && stateController.dinner.boolValue)
        {
            dinnerCheck.SetActive(true);
        }
        else
        {
            dinnerCheck.SetActive(false);
        }



        if ((timeController.hourCounter >= 13 && timeController.hourCounter < 16) && stateController.eat.boolValue)
        {
            eatCheck.SetActive(true);
        }
        else
        {
            eatCheck.SetActive(false);
        }


        if ((timeController.hourCounter >= 8 && timeController.hourCounter < 11) && stateController.brakefast.boolValue)
        {
            brakefatCheck.SetActive(true);
        }
        else
        {
            brakefatCheck.SetActive(false);
        }

        if ((timeController.hourCounter >= 9 && timeController.hourCounter < 15) && stateController.work.boolValue)
        {
            workCheck.SetActive(true);
        }
        else
        {
            workCheck.SetActive(false);
        }



        if (stateController.weigth.value >= 85)
        {
            weightText.SetActive(true);
        }
        else
        {
            weightText.SetActive(false);
        }



        if (stateController.happines.value <= 60)
        {
            happinessText.SetActive(true);
        }
        else
        {
            happinessText.SetActive(false);
        }
        if (stateController.stress.value >= 40)
        {
            stressText.SetActive(true);
        }
        else
        {
            stressText.SetActive(false);
        }

        if (stateController.fun.value <= 60)
        {
            funText.SetActive(true);
        }
        else
        {
            funText.SetActive(false);
        }

        if (stateController.hygiene.value <= 30)
        {
            cleanCheck.SetActive(true);
        }
        else
        {
            cleanCheck.SetActive(false);
        }

        if (stateController.energy.value <= 50)
        {
            energyText.SetActive(true);
        }
        else
        {
            energyText.SetActive(false);
        }

        if (stateController.satiety.value <= 60)
        {
            satietyText.SetActive(true);
        }
        else
        {
            satietyText.SetActive(false);
        }

        if (stateController.thirst.value <= 60)
        {
            thirstText.SetActive(true);
        }
        else
        {
            thirstText.SetActive(false);
        }
    }
}
