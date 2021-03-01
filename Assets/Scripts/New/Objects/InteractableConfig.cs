using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableConfig : MonoBehaviour
{
    private string nameObject;
    [SerializeField] private string actionName;
    [SerializeField] private float radius = 2f;

    public bool allowInteract { get; private set; }
    private GameObject player;
    private TextMeshProUGUI messageText;
    private GameObject messagePanel;
    private ActionsModifier actionsModifier;
    void Start()
    {
        nameObject = gameObject.name;
        player = GameObject.Find("Player");
        actionsModifier = gameObject.GetComponent<ActionsModifier>();
    }
    // Update is called once per frame
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

    public void PerformActions()
    {
        if (actionsModifier!=null)
        {
            actionsModifier.ActivateAction();
        }
    }
    public string GetName()
    {
        return nameObject;
    }

    public string GetActionName()
    {
        return actionName;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
