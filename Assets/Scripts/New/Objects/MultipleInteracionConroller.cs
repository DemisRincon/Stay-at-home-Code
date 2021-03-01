using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultipleInteracionConroller : MonoBehaviour
{
    [SerializeField] private float radius = 2f;
    [SerializeField] private string defaultAction;
    [SerializeField] private int[] initialHours;
    [SerializeField] private int[] finalalHours;
    [SerializeField] private string[] actionsName;
    public bool allowInteract { get; private set; }

    private string nameObject;
    private GameObject player;
    private TextMeshProUGUI messageText;
    private GameObject messagePanel;
    private ActionsModifier actionsModifier;
    private TimeController timeController;
    void Start()
    {
        timeController = GameObject.Find("EventSystem").GetComponent<TimeController>();
        nameObject = gameObject.name;
        player = GameObject.Find("Player");
        actionsModifier = gameObject.GetComponent<ActionsModifier>();
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
        if (actionsModifier != null)
        {
            Debug.Log("performing action");

            actionsModifier.ActivateAction();
        }
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
                else
                {
                    return defaultAction;
                }
            }
        }
        return defaultAction;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
