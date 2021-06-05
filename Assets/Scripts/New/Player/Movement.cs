using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = .5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float InteractionRange = 1.8f;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Joystick joystick;
    [SerializeField] float joystickSpeed = .3f;
    [SerializeField] float joystickRange = .2f;
    [SerializeField] private bool isAndroid;

        
    [SerializeField] private FixedTouchField touchField;
    private Vector2 lookAxis;

    public ObjectInteractable focus;
    public MultipleInteracionConroller focusVariable;
    private int rayLayerMask;
    private float xRotation = 0f;
    [SerializeField] private MoveableObject moveableObject;
    private Animator animator;
    private const string animBoolName = "isOpen_Obj_";
    private PauseMenu pauseMenu;
    private bool pressE = false;



    void Start()
    {
        if (!isAndroid)
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseSensitivity = 100f;
        }
        
        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        rayLayerMask = 1 << iRayLM.value;
        pauseMenu = GameObject.Find("EventSystem").GetComponent<PauseMenu>();
    }

    void Update()
    {
        MouseLook();
        MoventControl();
        RaycastInteraction();
        AndroidControl();
        LookAxis();
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
                    if ((Input.GetKeyDown(KeyCode.E) || (pressE)) && !pauseMenu.GameIsPaused && !pauseMenu.SeenRecomendations)
                    {
                        focus.PerformActions();
                        if (moveableObject != null)
                        {
                            animator.enabled = true;
                            animator.SetBool(animBoolNameNum, !isOpen);
                        }
                        pressE = false;
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
    private void AndroidControl()
    {
        //the range is the start position where the joystick is triggered
        float moveX = 0f;
        float moveY = 0f;
        joystickSpeed = .3f;
        joystickRange = .2f;
        //moveX = joystick.Horizontal;
        //moveY = joystick.Vertical;

        if (joystick.Horizontal >= joystickRange)
        {
            moveX = (joystickSpeed * movementSpeed) * joystick.Horizontal;
        }else if (joystick.Horizontal <= -joystickRange)
        {
            moveX = (joystickSpeed * movementSpeed) * joystick.Horizontal;
        }
        else
        {
            moveX = 0f;
        }

        if (joystick.Vertical >= joystickRange)
        {
            moveY = (joystickSpeed * movementSpeed) * joystick.Vertical;
        }
        else if (joystick.Vertical <= -joystickRange)
        {
            moveY = (joystickSpeed * movementSpeed) * joystick.Vertical;
        }
        else
        {
            moveY = 0f;
        }
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
    private void LookAxis()
    {
        if (!pauseMenu.GameIsPaused)
        {
            //This is the look control for touch pad
            var fps = GetComponent<Movement>();
            float lookX = lookAxis.x * mouseSensitivity;
            float lookY = lookAxis.y * mouseSensitivity;


            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * lookX);
            fps.lookAxis = touchField.TouchDist;
        }
    }
    public void PressE()
    {
        pressE = true;
    }
}
