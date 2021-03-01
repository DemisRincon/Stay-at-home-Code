using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = 2f;

    public bool allowInteract = false;
    private GameObject player;
    private SatatusController playerStatus;
    private FullSate currentState;
    [SerializeField] private int energy = 0;
    [SerializeField] private int social = 0;
    [SerializeField] private int satiety = 0;
    [SerializeField] private int thirst = 0;
    [SerializeField] private int hygiene = 0;
    [SerializeField] private int fun = 0;
    [SerializeField] private int stress = 0;
    [SerializeField] private int happiness = 0;
    [SerializeField] private int weight = 0;

    private ModifiedClock clock;

    public bool brakefast;
    public bool bath;
    public bool work;
    public bool clean;
    public bool eat;
    public bool dinner;
    public bool sleep;
    public float timeCost;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<SatatusController>();
        currentState = playerStatus.GetState();
        clock = playerStatus.GetClock();
   
    }
    private void Update()
    {

        float distance = Vector3.Distance(player.GetComponent<Transform>().position, transform.position);
        if (distance <= radius)
        {
            allowInteract = true;
        }
        else
        {
            allowInteract = false;
        }

    }


    public void DoInteraction()
    {
        currentState.GetEnergy().ModifyState(energy);
        currentState.GetSocial().ModifyState(social);
        currentState.GetSatiety().ModifyState(satiety);
        currentState.GetThirst().ModifyState(thirst);
        currentState.GetHygiene().ModifyState(hygiene);
        currentState.GetFun().ModifyState(fun);
        currentState.GetStress().ModifyState(stress);
        currentState.GetHappiness().ModifyState(happiness);
        currentState.GetWeight().ModifyState(weight);
   
        clock.IncressTime(timeCost*3600);



    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
