using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float InteractionRange = 1.8f;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject messagePanel;
    public ObjectInteractable focus;
    public MultipleInteracionConroller focusVariable;
    private int rayLayerMask;
    private float xRotation = 0f;
    [SerializeField] private MoveableObject moveableObject;
    private Animator animator;
    private const string animBoolName = "isOpen_Obj_";
    private PauseMenu pauseMenu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        rayLayerMask = 1 << iRayLM.value;
        pauseMenu = GameObject.Find("EventSystem").GetComponent<PauseMenu>();
    }

    void Update()
    {
        MouseLook();
        MoventControl();
        RaycastInteraction();
    }

    private void RaycastInteraction()
    {
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, InteractionRange, rayLayerMask))
        {
            focus = hit.collider.GetComponent<ObjectInteractable>();
            focusVariable = hit.collider.GetComponent<MultipleInteracionConroller>();
            moveableObject = hit.collider.GetComponent<MoveableObject>();
            if (moveableObject != null)
            {
                animator = hit.collider.GetComponent<Animator>();
            }
            if (focus != null)
            {
                if (focus.allowInteract)
                {
                    messagePanel.SetActive(true);
                    bool isOpen = false;
                    string animBoolNameNum = "true";
                    if (moveableObject != null)
                    {
                        animBoolNameNum = animBoolName + moveableObject.objectNumber.ToString();
                        isOpen = animator.GetBool(animBoolNameNum);
                        if (isOpen)
                        {
                            messageText.text = "(E) Cerrar";
                        }
                        else
                        {
                            messageText.text = "(E) Abrir";
                        }
                    }
                    else
                    {
                        messageText.text = focus.GetActionName() + " (E)";
                    }
                    if ((Input.GetKeyDown(KeyCode.E)) && !pauseMenu.GameIsPaused && !pauseMenu.SeenRecomendations)
                    {
                        focus.PerformActions();
                        if (moveableObject != null)
                        {
                            animator.enabled = true;
                            animator.SetBool(animBoolNameNum, !isOpen);
                        }
                    }
                }
            }
            else if (focusVariable != null)
            {
                if (focusVariable.allowInteract)
                {
                    messagePanel.SetActive(true);

                    messageText.text = focusVariable.GetActionName() + " (E)";

                    if ((Input.GetKeyDown(KeyCode.E)) && !pauseMenu.GameIsPaused && !pauseMenu.SeenRecomendations)
                    {

                        focusVariable.PerformActions();
                    }
                }
            }
        }
        else
        {
            messagePanel.SetActive(false);
            messageText.text = "";
        }
    }

    private void MoventControl()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveY + transform.up * 0;
        characterController.Move(move * movementSpeed * Time.deltaTime);

    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
