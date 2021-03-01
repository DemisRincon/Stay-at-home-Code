using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModifyerByInteraction : MonoBehaviour
{
    private StateController stateController;
    [SerializeField] private int energyModification;
    [SerializeField] private int socialModification;
    [SerializeField] private int satietyModification;
    [SerializeField] private int thirstModification;
    [SerializeField] private int hygieneModification;
    [SerializeField] private int funModification;
    [SerializeField] private int stressModification;
    [SerializeField] private int happinessModification;
    [SerializeField] private int weightModification;
   
    // Start is called before the first frame update
    void Start()
    {
        stateController = GameObject.Find("Player").GetComponent<StateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
