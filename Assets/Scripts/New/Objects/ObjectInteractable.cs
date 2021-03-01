using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    /// <summary>
    /// ATRIBUTOS GENERALES
    /// </summary>
    [SerializeField] private float radius = 2f;
    [SerializeField] private string actionName;
    [SerializeField] private PassiveController passiveController;
    private GameObject player;
    private TimeController timeController;
    public bool allowInteract { get; private set; }
    public string nameObject { get; private set; }

    /// <summary>
    /// ATRIBUTOS MULTIPLES
    /// </summary>

    [SerializeField] private int[] initialHours;
    [SerializeField] private int[] finalalHours;
    [SerializeField] private string[] actionsName;

    /// <summary>
    /// ACCIONES
    /// </summary>
    [SerializeField] public bool doBath;
    [SerializeField] public bool doWork;
    [SerializeField] public bool doClean;
    [SerializeField] public bool doEat;
    [SerializeField] public bool doSleep;
    [SerializeField] public bool doACall;
    [SerializeField] public bool doExercise;
    [SerializeField] public int energyModification;
    [SerializeField] public int socialModification;
    [SerializeField] public int satietyModification;
    [SerializeField] public int thirstModification;
    [SerializeField] public int hygieneModification;
    [SerializeField] public int funModification;
    [SerializeField] public int stressModification;
    [SerializeField] public int happinessModification;
    [SerializeField] public int weightModification;
    [SerializeField] public int hoursSpent;
    [SerializeField] public bool isFood;
    [SerializeField] public bool notHealty;
    [SerializeField] public bool turnOnTV;
    [SerializeField] public bool turnOnStereo;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        nameObject = gameObject.name;
        passiveController = player.GetComponent <PassiveController>();
        timeController = GameObject.Find("EventSystem").GetComponent<TimeController>();

    }

    // Update is called once per frame
    void Update()
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

    public void PerformActions()
    {
        passiveController.PerfromModification(gameObject.GetComponent<ObjectInteractable>());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public string GetActionName()
    {
        if (actionsName.Length > 0)
        {
            for (int i = 0; i < actionsName.Length; i++)
            {
                if (timeController.hourCounter >= initialHours[i] && timeController.hourCounter <= finalalHours[i])
                {
                    return actionsName[i];
                }
           
                    return actionName;
              
            }
        }
        return actionName;
    }
}
