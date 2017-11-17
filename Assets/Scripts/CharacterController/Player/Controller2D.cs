﻿using interactable;
using PickUp;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller2D : MonoBehaviour
{

    ICharacterState currentState;
    public LayerMask CollisionMask;
    CharacterController controller;
    ICharacterState characterState;
    public bool consoleControlls = true;
    [HideInInspector]
    public KeyCode JumpKey;
    [HideInInspector]
    public KeyCode DashKey;
    [HideInInspector]
    public KeyCode InteractKey;
    [HideInInspector]
    public KeyCode PickUpKey;
    [HideInInspector]
    public KeyCode UseKey;
    Vector2 charInput;
    public float jumpTimerDelay;
    public float jumpTimer = 0.3f;
    private float startZ;
    private float previousX;
    private float t;
    public float speed = 2;
    public Vector3 moveDir;
    private RaycastHit topHit;
    public float stickToGrouncForce = 1;
    public float BounceDownOnRoof = 0.1f;
    public float gravityMultiplier = 1;
    private CollisionFlags colFlags;
    public float jumpSpeed = 10;
    public float dashSpeed = 10;
    private bool jump;
    public bool dash;
    public float DashTimer = 0.5f;
    private float dashTimer;
    public float accelerationTime = 0.1f;
    public float deaccelrationTime = 0.8f;
    public float airAccelerationTime = 0.2f;
    public float airDeaccelrationTime = 1.6f;
    public Vector3 dashDestination;
    public float bottomRayLength = 0.08f;
    RaycastHit bottom;
    public bool canJump = true;
    private IInteractable InteractFocus;
    private IPickUp PickUpFocus;
    private IPickUp PickUpCarry;
    private List<IPickUp> PickUpFocusList;
    private int PickUpFocusSelected;
    float onewayPlatformIndex;

    public GameObject Trail; 

    
    private ICharacterState GetInitialCharacterState()
    {

        ICharacterState cS = null;

        if (controller.isGrounded)
        {
            cS = new GroundState(this);
        }

        else
        {
            cS = new AirState(this, true);
        }

        return cS;
    }





    // Use this for initialization
    void Start()
    {
        if (!consoleControlls)
        {
            JumpKey = KeyCode.Space;
            DashKey = KeyCode.LeftShift;
            UseKey = KeyCode.E;
            InteractKey = KeyCode.Q;
            PickUpKey = KeyCode.C;
            onewayPlatformIndex = 0;
        }

        else
        {
            JumpKey = KeyCode.JoystickButton0;

            DashKey = KeyCode.Joystick1Button5;

            InteractKey = KeyCode.JoystickButton3;

            PickUpKey = KeyCode.JoystickButton2;
            UseKey = KeyCode.JoystickButton1;
            onewayPlatformIndex = -0.7f;
        }
        controller = GetComponent<CharacterController>();
        startZ = transform.position.z;
        characterState = GetInitialCharacterState();
        characterState.Enter();

        PickUpFocusList = new List<IPickUp>();
        PickUpFocusSelected = 0;
    }

    public float Smooth(float target, ref float currentValue, float accelerationTime, float deaccelrationTime)
    {
        float value = 0;
        var smoothTime = accelerationTime;
        if (Mathf.Abs(charInput.x) < TMathHelper.FloatEpsilon)
        {
            smoothTime *= deaccelrationTime;
        }

        value = Mathf.SmoothDamp(currentValue, target, ref currentValue, smoothTime);
        return value;
    }

    void Rays()
    {

        if (Physics.SphereCast(transform.position, controller.radius, Vector3.up, out topHit,
          BounceDownOnRoof, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (!topHit.collider.CompareTag("One Way"))
            {
                moveDir.y = -1;
            }

            else
            {
                Physics.IgnoreCollision(controller, topHit.collider, true);
            }
        }

        if (Physics.SphereCast(transform.position, controller.radius, Vector3.down, out bottom,
     controller.height, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (bottom.collider.CompareTag("One Way"))
            {
                Physics.IgnoreCollision(controller, bottom.collider, false);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z == startZ)
        {
            previousX = transform.position.x;
        }

        t += Time.fixedDeltaTime;



        Vector3 destination = transform.right * charInput.x;
        RaycastHit hit;

        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hit,
            controller.height / 2, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;
        if (charInput.x == 0 && charInput.y == 0)
        {
            destination = Vector3.zero;
        }

        //new Code

        Vector3 targetDir = Vector3.zero;
        if (dash)
        {


            if (dashTimer >= 0)
            {
                dash = dashDestination.x != 0;
                targetDir.x = dashDestination.x * dashSpeed;
                dashTimer -= Time.fixedDeltaTime;
            }

            else
            {
                dash = false;
            }


        }

        else
        {
            targetDir.x = destination.x * speed;//Mathf.SmoothDamp(moveDir.x,destination.x * speed,ref moveDir.x,smoothTime);

        }




        if (controller.isGrounded)
        {
            moveDir.x = Smooth(targetDir.x, ref moveDir.x, accelerationTime, deaccelrationTime);
            moveDir.y = -stickToGrouncForce;
        }

        else
        {
            moveDir.x = Smooth(targetDir.x, ref moveDir.x, airAccelerationTime, airDeaccelrationTime);
            moveDir += Physics.gravity * gravityMultiplier * Time.deltaTime;

        }



        if (jump)
        {
            moveDir.y = jumpSpeed;
            jump = false;
        }



        colFlags = controller.Move(moveDir * Time.fixedDeltaTime);

        if (controller.velocity.x == 0)
        {
            moveDir.x = 0;
            dashDestination.x = 0;
        }
        //print("velocity" + controller.velocity); 
        if (transform.position.z != startZ)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = previousX;
            newPosition.z = startZ;
            transform.position = newPosition;
        }
    }

    void Update()
    {
        if(Mathf.Abs(controller.velocity.x) > 0)
        {
            Trail.SetActive(true);

        }else
        {
            Trail.SetActive(false);
        }

        //temp restart code
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //print("jumpKEy" + JumpKey);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        charInput = new Vector2(horizontal, vertical);

        if (charInput.sqrMagnitude > 1)
        {
            charInput.Normalize();
        }

        var characterStateData = characterState.Update(charInput, Time.deltaTime);
        if (characterStateData.NewState != null)
        {
            ChangeCharacterState(charInput, characterStateData);
        }

        Rays();

        if (charInput.y < onewayPlatformIndex)
        {
            if (bottom.collider != null)
            {
                if (bottom.collider.CompareTag("One Way"))
                {
                    Physics.IgnoreCollision(controller, bottom.collider);
                }
            }

        }
        /*if (controller.isGrounded)
        {
            jumpTimerDelay = jumpTimer;
        }
        */
        if (jumpTimerDelay > 0 && !controller.isGrounded)
        {
            canJump = true;
            jumpTimerDelay -= Time.deltaTime;
        }

        if (jumpTimerDelay <= 0 && !controller.isGrounded)
        {
            canJump = false;
        }

        else
        {
            canJump = true;
        }
        checkAction();
    }

    private void ChangeCharacterState(Vector2 input, CharacterStateData characterStateData)
    {
        PrintStateSwitching(characterStateData);
        characterState.Exit();
        characterState = characterStateData.NewState;
        characterState.Enter();

        if (characterStateData.RunNewStateSameUpdate)
        {
            characterState.Update(charInput, Time.deltaTime);
        }
    }

    public CharacterController getCharController()
    {

        return controller;
    }



    public Vector2 getVelocity()
    {
        return controller.velocity;
    }

    [Conditional("UNITY_EDITOR")]
    private void PrintStateSwitching(CharacterStateData characterStateData)
    {
        print("Switching from" + characterState.ToString() + " to " + characterStateData.NewState.ToString());
    }

    public void Jump()
    {
        //startJumpTimer();
        jump = true;
    }

    public void startJumpTimer()
    {
        canJump = true;
        jumpTimerDelay = jumpTimer;
    }

    public void Dash()
    {
        if (!dash)
        {
            dashDestination = moveDir;
            dashTimer = DashTimer;
            dash = true;
        }
    }

    public string getCarryId()
    {
        if (PickUpCarry == null)
        {
            return "noobject";
        }
        return PickUpCarry.getID();
    }
    public void setPickUpFocus(IPickUp pickup)
    {
        PickUpFocus = pickup;
    }
    public void addPickUpFocus(IPickUp pickup)
    {
        bool highlight = PickUpFocusList.Count == 0;
        PickUpFocusList.Add(pickup);
        if (highlight)
        {
            PickUpFocusSelected = 0;
            PickUpFocusList[PickUpFocusSelected].Outline();
        }

    }

    public void removePickUpFocus(IPickUp pickup)
    {
        var preserve = PickUpFocusList[PickUpFocusSelected];
        var index = PickUpFocusList.IndexOf(pickup);
        var removed = PickUpFocusList.Remove(pickup);
        PickUpFocusList.TrimExcess();

        if (index != PickUpFocusSelected && removed)
            PickUpFocusSelected = PickUpFocusList.IndexOf(preserve);
        if (index == PickUpFocusSelected && removed)
        {
            pickup.removeOutline();
            if (PickUpFocusSelected <= (PickUpFocusList.Count))
                PickUpFocusSelected = PickUpFocusList.Count - 1;

        }

    }

    public IPickUp getPickUpFocus()
    {
        return PickUpFocus;
    }

    public void setInteractableFocus(IInteractable interactable)
    {
        InteractFocus = interactable;
    }

    private void checkAction()
    {
        if (Input.GetKeyDown(InteractKey) && InteractFocus != null)
        {
            InteractFocus.Interact(this);
        }
        if (Input.GetKeyDown(UseKey) && PickUpCarry != null)
        {
            PickUpCarry.Use();
        }
        if (Input.GetKeyDown(PickUpKey) && PickUpFocus != null && PickUpCarry == null)
        {
            PickUpCarry = PickUpFocus;
        }
        if (Input.GetKeyDown(PickUpKey) && PickUpCarry != null)
        {
            PickUpCarry.Drop();
            PickUpCarry = null;
        }
    }
    private void cyclePickUpSelected(int i) // takes in -1 or +1 
    {
        if (i != 1 || i != -1)
        {
            return;
        }
        if (PickUpFocusList.Count == 0)
        {
            return;
        }
        PickUpFocusList[PickUpFocusSelected].removeOutline();
        if (PickUpFocusSelected + i >= PickUpFocusList.Count)
        {
            PickUpFocusSelected = 0;
        }
        else if (PickUpFocusSelected + i < 0)
        {
            PickUpFocusSelected = PickUpFocusList.Count - 1;
        }
        else
        {
            PickUpFocusSelected += i;
        }

        PickUpFocusList[PickUpFocusSelected].Outline();


    }
}
