using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{

    public StateModel energy = new StateModel(100, false, 100);
    public StateModel social = new StateModel(100 ,false,100);
    public StateModel satiety = new StateModel(100,false, 100);
    public StateModel thirst = new StateModel(100,false, 100);
    public StateModel hygiene = new StateModel(100, false, 0);
    public StateModel fun = new StateModel(100, false, 100);
    public StateModel stress = new StateModel(100, false, 0);
    public StateModel happines = new StateModel(100, false, 100);
    public StateModel weigth = new StateModel(74,true,74);
    public StateModel brakefast = new StateModel(false);
    public StateModel bath = new StateModel(false);
    public StateModel work = new StateModel(false);
    public StateModel clean = new StateModel(false);
    public StateModel eat = new StateModel(false);
    public StateModel dinner = new StateModel(false);
    public StateModel sleep = new StateModel(false);
    public StateModel takignANap = new StateModel(false);
    
    

    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI socialText;
    [SerializeField] private TextMeshProUGUI satietyText;
    [SerializeField] private TextMeshProUGUI thirstText;
    [SerializeField] private TextMeshProUGUI hygineText;
    [SerializeField] private TextMeshProUGUI funText;
    [SerializeField] private TextMeshProUGUI stressText;
    [SerializeField] private TextMeshProUGUI happinessText;
    [SerializeField] private TextMeshProUGUI weightText;

    [SerializeField] private Slider energyBar;
    [SerializeField] private Slider socialBar;
    [SerializeField] private Slider satietyBar;
    [SerializeField] private Slider thirstBar;
    [SerializeField] private Slider hygieneBar;
    [SerializeField] private Slider funBar;
    [SerializeField] private Slider stressBar;
    [SerializeField] private Slider happinessBar;

    [SerializeField] private GameObject brakefatCheck;
    [SerializeField] private GameObject bathCheck;
    [SerializeField] private GameObject workCheck;
    [SerializeField] private GameObject cleanCheck;
    [SerializeField] private GameObject eatCheck;
    [SerializeField] private GameObject dinnerCheck;
    [SerializeField] private GameObject sleepCheck;
    private void Update()
    {
        energyText.text = energy.value.ToString();
        socialText.text = social.value.ToString();
        satietyText.text = satiety.value.ToString();
        thirstText.text = thirst.value.ToString();
        hygineText.text = hygiene.value.ToString();
        funText.text = fun.value.ToString();
        stressText.text = stress.value.ToString();
        happinessText.text = happines.value.ToString();
        weightText.text = weigth.value.ToString();
        energyBar.value = energy.value;
        socialBar.value = social.value;
        satietyBar.value = satiety.value;
        thirstBar.value = thirst.value;
        hygieneBar.value = hygiene.value;
        funBar.value = fun.value;
        stressBar.value = stress.value;
        happinessBar.value = happines.value;
        brakefatCheck.SetActive(brakefast.boolValue);
        bathCheck.SetActive(bath.boolValue);
        workCheck.SetActive(work.boolValue);
        cleanCheck.SetActive(clean.boolValue);
        eatCheck.SetActive(eat.boolValue);
        dinnerCheck.SetActive(dinner.boolValue);
        sleepCheck.SetActive(sleep.boolValue);


    }

}
