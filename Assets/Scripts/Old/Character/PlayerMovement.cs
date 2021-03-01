using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float InteractionRange = 1.8f;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI interactionText;

    public Interactable focus;
    private CharacterController controller;
    private Transform playerBody;
    private Camera mainCamera;
    private float xRotation = 0f;
    private int rayLayerMask;
    private SatatusController currentStatus;
    private Actions playerActions;
    public List<Transform> interactablesData = new List<Transform>();
    private List<IntractableObject> interctablesObjects = new List<IntractableObject>();
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        rayLayerMask = 1 << iRayLM.value;
        playerActions = gameObject.GetComponent<Actions>();
        currentStatus = gameObject.GetComponent<SatatusController>();
        foreach (var item in interactablesData)
        {

            interctablesObjects.Add(new IntractableObject(item.name));

        }





    }
    void Update()
    {
        MouseLook();
        MoventControl();
        InteractionControl();

    }
    private void FixedUpdate()
    {

    }
    private void InteractionControl()
    {
        RaycastHit hit;
        Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        int currentEnergy = currentStatus.GetState().GetEnergy().currentValue;



        if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit, InteractionRange, rayLayerMask))
        {
            focus = hit.collider.GetComponent<Interactable>();
            if (focus != null)
            {

                foreach (var item in interctablesObjects)
                {
                    if ((item.getName() == focus.name) && focus.allowInteract)
                    {
                        messagePanel.SetActive(true);
                        interactionText.text = item.getAction() + " (E)";

                    }
                }
                if (Input.GetKeyDown(KeyCode.E) && focus.allowInteract)
                {


                    switch (focus.name)
                    {
                        case "Bed":

                            if (currentEnergy <= 40)
                            {
                                playerActions.Sleep();

                            }
                
                            break;


                        case "Computer":

                            if (!currentStatus.GetState().work)
                            {
                                playerActions.Work();
                            }



                            break;
                        default:

                            focus.DoInteraction();
                            break;
                    }


                    if (focus.name == "Bed")
                    {
                    
                    }
                    else
                    {

                    }

                }
            }
        }
        else
        {
            messagePanel.SetActive(false);
            interactionText.text = "";
        }


    }


    private void MoventControl()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        controller.Move(move * movementSpeed * Time.deltaTime);

    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

}



