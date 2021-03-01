using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicsController : MonoBehaviour
{
    Actions actionsThePlayer ;
    FullSate currentState ;


    // Start is called before the first frame update
    void Start()
    {
        actionsThePlayer =gameObject.GetComponent<Actions>();
        currentState = GetComponent<SatatusController>().GetState();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState.GetEnergy().currentValue<=0&&!actionsThePlayer.isSleeping)
        {
            Debug.Log("the player should sleep");
            actionsThePlayer.Sleep();
      
      
        }
    }
}
