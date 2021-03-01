using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SatatusController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI socialText;
    [SerializeField] private TextMeshProUGUI satietyText;
    [SerializeField] private TextMeshProUGUI thristText;
    [SerializeField] private TextMeshProUGUI hygineText;
    [SerializeField] private TextMeshProUGUI funText;
    [SerializeField] private TextMeshProUGUI stressText;
    [SerializeField] private TextMeshProUGUI happinessText;
    [SerializeField] private TextMeshProUGUI weightText;

    [SerializeField] private Slider energyBar;
    [SerializeField] private Slider socialBar;
    [SerializeField] private Slider satietyBar;
    [SerializeField] private Slider thristBar;
    [SerializeField] private Slider hygineBar;
    [SerializeField] private Slider funBar;
    [SerializeField] private Slider stressBar;
    [SerializeField] private Slider happinessBar;


    [SerializeField] private GameObject BrakefatCheck;
    [SerializeField] private GameObject BathCheck;
    [SerializeField] private GameObject WorkCheck;
    [SerializeField] private GameObject CleanCheck;
    [SerializeField] private GameObject EatCheck;
    [SerializeField] private GameObject DinnerCheck;
    [SerializeField] private GameObject SleepChake;

    [SerializeField] private ModifiedClock clock;
    FullSate state = new FullSate();


    void FixedUpdate()
    {

        BrakefatCheck.SetActive(state.brakefast);
        BathCheck.SetActive(state.bath);
        WorkCheck.SetActive(state.work);
        CleanCheck.SetActive(state.clean);
        EatCheck.SetActive(state.eat);
        DinnerCheck.SetActive(state.dinner);
        SleepChake.SetActive(state.sleep);


        energyText.text = state.GetEnergy().currentValue.ToString() + "%";
        socialText.text = state.GetSocial().currentValue.ToString() + "%";
        satietyText.text = state.GetSatiety().currentValue.ToString() + "%";
        thristText.text = state.GetThirst().currentValue.ToString() + "%";
        hygineText.text = state.GetHygiene().currentValue.ToString() + "%";
        funText.text = state.GetFun().currentValue.ToString() + "%";
        stressText.text = state.GetStress().currentValue.ToString() + "%";
        happinessText.text = state.GetHappiness().currentValue.ToString() + "%";
        weightText.text = state.GetWeight().currentValue.ToString() + "Kg";

        energyBar.value = state.GetEnergy().currentValue;
        socialBar.value = state.GetSocial().currentValue;
        satietyBar.value = state.GetSatiety().currentValue;
        thristBar.value = state.GetThirst().currentValue;
        hygineBar.value = state.GetHygiene().currentValue;
        funBar.value = state.GetFun().currentValue;
        stressBar.value = state.GetStress().currentValue;
        happinessBar.value = state.GetHappiness().currentValue;

    }



    public FullSate GetState()
    {
        return state;
    }

    public ModifiedClock GetClock()
    {
        return clock;
    }
}
