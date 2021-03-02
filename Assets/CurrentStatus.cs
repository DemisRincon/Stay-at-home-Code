using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStatus : MonoBehaviour
{
    [SerializeField] private StateController stateController;
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
