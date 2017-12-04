﻿using interactable;
using PickUp;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum JumpDir
{
    JumpDown = -1,
    JumpUp = 1
}

public class Controller2D : MonoBehaviour
{

    ICharacterState currentState;
    public LayerMask CollisionMask;
    public LayerMask Ghost;
    CharacterController controller;
    ControllerKeyManager keyManager;
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
    [HideInInspector]
    public KeyCode CycleLeftKey;
    public KeyCode CycleRightKey;
    public Vector2 charInput;
    public float triggerInput;
    [HideInInspector]
    public KeyCode DieKey; //temp key remove in future
    public float jumpTimerDelay = 0.1f;
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
    public bool jump;
    public bool dash;
    public bool alive = true;
    private GameObject[] players;
    public float DashTimer = 0.5f;
    public float dashTimer;
    public float accelerationTime = 0.1f;
    public float deaccelrationTime = 0.8f;
    public float airAccelerationTime = 0.2f;
    public float airDeaccelrationTime = 1.6f;
    // public Vector3 dashDestination;
    public float bottomRayLength = 0.08f;
    RaycastHit bottom;
    public bool canJump = true;
    private IInteractable InteractFocus;
    private IPickUp PickUpFocus;
    private IPickUp PickUpCarry;
    private List<IPickUp> PickUpFocusList;
    private int PickUpFocusSelected;
    private bool cycleButtonUp;
    float onewayPlatformIndex;
    int jumpDir = 1;
    public GameObject Trail;
    public float BoostSpeed = 40;
    public bool boost = false;
    private bool canMove = true;
    private float canMoveTimer = 0f;
    public bool Grounded;
    public Vector3 velocity;
    public float dashCooldown = 0.1f;
    public float dashCooldownTimer = 0;
    public bool canDash = true;
    public bool canInteract = false;
    public float minJumpSpeed;
    public bool onPlayerHead = false;
    public bool savedJumpInput = false;
    public float savedJumpInputValue= 0.3f;
    public float savedJumpInputTimer = 0;
    public bool isGhost = false;
    public bool XBOX = false;
    public Vector2 dashInput;
    public Ghost ghost;
    public bool jumping = false;
    bool onOneWay = false;
    private float selectedSpeed;
    List<GameObject> oneways = new List<GameObject>();

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



    public int getJumpDir()
    {
        return jumpDir;
    }

    public void Spawn(Vector3 position)
    {
        Vector3 target = position;
        target.z = startZ;
        controller.transform.position = target;
    }

    // Use this for initialization
    void Start()
    {
        minJumpSpeed = jumpSpeed / 2;
        keyManager = GetComponent<ControllerKeyManager>();
        keyManager.getKeyCode(this.name, this, ref XBOX);

        if (!consoleControlls)
        {
            onewayPlatformIndex = 0;
        }

        else
        {
            onewayPlatformIndex = -0.7f;
        }
        controller = GetComponent<CharacterController>();
        startZ = transform.position.z;
        characterState = GetInitialCharacterState();
        characterState.Enter();

        PickUpFocusList = new List<IPickUp>();
        PickUpFocusSelected = 0;
        players = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        players = GameObject.FindGameObjectsWithTag("Player");
        ghost = GetComponent<Ghost>();
        foreach (GameObject p in players)
        {
            Physics.IgnoreCollision(this.GetComponent<CapsuleCollider>(), p.GetComponent<CapsuleCollider>(), true);
            Physics.IgnoreCollision(this.GetComponent<CharacterController>(), p.GetComponent<CharacterController>(), true);
            Physics.IgnoreCollision(this.GetComponent<CapsuleCollider>(), p.GetComponent<CharacterController>(), true);
        }
        selectedSpeed = speed;
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
            if (!topHit.collider.CompareTag("One Way") && !topHit.collider.CompareTag("Player"))
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
                oneways.Add(bottom.collider.gameObject);
                onOneWay = true;
                if (jumpDir == (int)JumpDir.JumpUp)
                    Physics.IgnoreCollision(controller, bottom.collider, false);
            }
        }
    }

    public bool OnPlayerHead()
    {
        bool onPlayerHead = false;
        if (bottom.collider != null)
        {

                if (bottom.collider.CompareTag("Player"))
                {
                    if (bottom.collider.gameObject.name != gameObject.name)
                    {
                        float distance = Mathf.Abs(controller.transform.position.y - bottom.collider.transform.position.y);
                        if (distance <= controller.height * 0.6f)
                        {
                            onPlayerHead = true;
                            moveDir.y = 0;
                        }

                    }


                }

        }

        return onPlayerHead;
    }
    float getTriggerInput()
    {
        float triggerInput = 0;
        if (!consoleControlls && Input.GetKeyDown(DashKey) || consoleControlls && !XBOX && Input.GetKeyDown(DashKey))
        {
            triggerInput = 1;
        }

        else
        {
            triggerInput = 0;
        }

        if (consoleControlls && XBOX)
        {
            triggerInput =  keyManager.getTriggerInput(this.name, consoleControlls);
        }
 
        return triggerInput;
    }
    bool updateGrounded = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (isGhost)
        {
            ghost.FixedUpdate();
            moveDir = Vector3.zero;
            return;
        }

        if (transform.position.z == startZ)
        {
            previousX = transform.position.x;
        }

        t += Time.fixedDeltaTime;

        Vector3 destination;
        Vector3 targetDir = Vector3.zero;
        RaycastHit hit;

        if (dash)
        {
            destination = transform.right * dashInput.x;
        }
        else
        {
            destination = transform.right * charInput.x;
            if (charInput.x == 0 && charInput.y == 0)
            {
                destination = Vector3.zero;
            }

        }


        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hit,
            controller.height / 2, CollisionMask, QueryTriggerInteraction.Ignore);

        destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;


       
        //new Code


        if (dash)
        {


            if (dashTimer >= 0 && canDash)
            {
                dash = destination.x != 0;
                targetDir.x = destination.x * (speed + dashSpeed);
                dashTimer -= Time.fixedDeltaTime;
            }

            else
            {
                dashCooldownTimer = dashCooldown;
                canDash = false;
                dash = false;
                this.transform.GetChild(3).gameObject.SetActive(false);
            }


        }

        else
        {
            targetDir.x = destination.x * speed;//Mathf.SmoothDamp(moveDir.x,destination.x * speed,ref moveDir.x,smoothTime);

        }


        //Rays();
        if (!jumping)
        {
            onPlayerHead = OnPlayerHead();
        }

        else
        {
            onPlayerHead = false;
        }
        if (onPlayerHead)
        {
            if (!jumpDown)
            {
                Grounded = onPlayerHead;
            }
            else
            {
                Grounded = controller.isGrounded;
                //onPlayerHead = false;
                jumpDown = !controller.isGrounded;
            }
        }

        else
        {
            //onPlayerHead = false;
            Grounded = controller.isGrounded;
        }

        if (jumpDown && Grounded)
        {
            jumpDown = false;
        }

        if (jumping)
        {
            jumping = moveDir.y>0;
        }

        if (Grounded)
        {
           
            jumpTimerDelay = jumpTimer;
            moveDir.x = Smooth(targetDir.x, ref moveDir.x, accelerationTime, deaccelrationTime);
            if (!onPlayerHead)
            {
                moveDir.y = -stickToGrouncForce;
            }
        }

        else
        {
            if (!dash || jumping)
            {
                moveDir.x = Smooth(targetDir.x, ref moveDir.x, airAccelerationTime, airDeaccelrationTime);
                moveDir += Physics.gravity * gravityMultiplier * Time.deltaTime;
            }

        }


        if (moveDir.x != targetDir.x)
        {
            moveDir.x = targetDir.x;
        }

        if (savedJumpInput)
        {
            savedJumpInputTimer = savedJumpInputValue;
            savedJumpInput = false;
        }

        if (savedJumpInputTimer >= 0)
        {
            savedJumpInputTimer -= Time.fixedDeltaTime;
        }


        if (jump || Grounded && savedJumpInputTimer >= 0)
        {
            jumpTimerDelay = 0;
            moveDir.y = jumpSpeed;
            onPlayerHead = false;
            jumping = true;
            jump = false;
            savedJumpInputTimer = 0;

        }


        if (boost)
        {
            moveDir.y = BoostSpeed;
            boost = false;
        }

        colFlags = controller.Move(moveDir * Time.fixedDeltaTime);

        if (controller.velocity.x == 0)
        {
            moveDir.x = 0;
            //dashDestination.x = 0;
        }
        if (canMove)
        {
            speed = selectedSpeed;

        }
        else
        {
            speed = 0;
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

    public bool jumpDown = false;

    void Update()
    {

        velocity = controller.velocity;
        //Grounded = controller.isGrounded;
        if (Mathf.Abs(controller.velocity.x) > 0)
        {
            Trail.SetActive(true);

        }
        else
        {
            Trail.SetActive(false);
        }

        //temp restart code
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        charInput = keyManager.getcharInput(this.name, consoleControlls, canMove);
        triggerInput = getTriggerInput();

        if (charInput.sqrMagnitude > 1)
        {
            charInput.Normalize();
        }




        var characterStateData = characterState.Update(charInput, Time.deltaTime);
        if (characterStateData.NewState != null)
        {
            ChangeCharacterState(charInput, characterStateData);
        }

        if (isGhost)
        {
            ghost.GhostUpdate(charInput, triggerInput);
            return;
        }

        Rays();


        /*if (controller.isGrounded)
        {
            jumpTimerDelay = jumpTimer;
        }
        */

        if (charInput.y < onewayPlatformIndex)
        {
            if (bottom.collider != null)
            {
                if (bottom.collider.CompareTag("One Way") || onPlayerHead)
                {
                    jumpDir = (int)JumpDir.JumpDown;
                }

                else
                {
                    jumpDir = (int)JumpDir.JumpUp;
                }


            }

        }
        else
        {
            jumpDir = (int)JumpDir.JumpUp;
        }


        if (Input.GetKeyDown(JumpKey))
        {
            if (jumpDir == (int)JumpDir.JumpDown)
            {
                jumpDown = true;
                Physics.IgnoreCollision(controller, bottom.collider);
            }
        }
        if (jumpTimerDelay > 0 && !Grounded)
        {
            canJump = true;
            jumpTimerDelay -= Time.deltaTime;
        }

        if (jumpTimerDelay <= 0 && !Grounded)
        {
            canJump = false;
        }

        else
        {
            canJump = true;
        }

        if (dashCooldownTimer >= 0)
        {

            dashCooldownTimer -= Time.deltaTime;
        }

        else
        {
            canDash = true;
        }

        checkAction();
        cyclePickUpSelected(CheckBumper());
        updateCarryPos(this.transform.position);

        if (canMoveTimer > 0)
        {
            canMoveTimer = canMoveTimer - Time.deltaTime;
            if (canMoveTimer <= 0)
            {
                canMove = true;
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void ChangeCharacterState(Vector2 input, CharacterStateData characterStateData)
    {
        PrintStateSwitching(characterStateData);
        characterState.Exit();
        characterState = characterStateData.NewState;
        characterState.Enter();
        print("newstate" + characterState);
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
       // print("Switching from" + characterState.ToString() + " to " + characterStateData.NewState.ToString());
    }

    public void Jump(int jumpCount, bool fell)
    {

        //startJumpTimer();
        jump = true;
        dash = false;
        dashTimer = 0;
        this.transform.GetChild(3).gameObject.SetActive(false);
        //print("jumpcount " + jumpCount + "Fell " + fell);
    }

    public void startJumpTimer()
    {
        canJump = true;

    }

    public void Dash()
    {
        if (!dash)
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            dashInput = charInput;
            //dashDestination = moveDir;
            dashTimer = DashTimer;
            dash = true;
        }
    }

    public void Boost(float BoostSpeed)
    {
        this.BoostSpeed = BoostSpeed;
        boost = true;
    }

    public string getCarryId()
    {
        if (PickUpCarry == null)
        {
            return "noobject"; //Heh Noob.
        }
        //print("current pickup" + PickUpCarry.getID());
        return PickUpCarry.getID();
    }

    public void addPickUpFocus(IPickUp pickup)
    {
        if (PickUpFocusList.Contains(pickup)) //temp solution current issues with ontriggerenter being called twice when a player enters a potion because of players having double colliders
            return;
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
        if (PickUpFocusList.Count == 0)
            return;
        var preserve = PickUpFocusList[PickUpFocusSelected];
        var index = PickUpFocusList.IndexOf(pickup);
        var removed = PickUpFocusList.Remove(pickup);
        UnityEngine.Debug.Log("Pickup removed " + PickUpFocusList.Count);
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
            var UseResult = PickUpCarry.Use(this);
            if (UseResult && InteractFocus != null) //PickUpCarry.Use() should only return true if the object is a key object. Currently pick up key objects are not planned to have a unique use method.
            {
                UnityEngine.Debug.Log("Interacted by trying to use a keyobject");
                InteractFocus.Interact(this);
            }
            else if (!UseResult)
            {
                removePickUpFocus(PickUpCarry);
                PickUpCarry = null;
            }
        }
        if (PickUpFocusList.Count != 0)
        {
            if (Input.GetKeyDown(PickUpKey) && PickUpFocusList[PickUpFocusSelected] != null && PickUpCarry == null)
            {
                PickUpCarry = PickUpFocusList[PickUpFocusSelected].PickUp(this);
                return;
            }
        }
        if (Input.GetKeyDown(PickUpKey) && PickUpCarry != null)
        {
            PickUpCarry.Drop();
            PickUpCarry = null;
        }
        if (Input.GetKeyDown(DieKey))
        {
            //killSelf();
        }
    }
    private void cyclePickUpSelected(int i) // takes in -1 or +1 
    {
        if (i == 0)
        {
            //cycleButtonUp = true;
            return;
        }
        /*if (!cycleButtonUp)
            return;
        cycleButtonUp = false;*/
        if (PickUpFocusList.Count == 0)
        {
            return;
        }
        PickUpFocusList[PickUpFocusSelected].removeOutline();
        if (PickUpFocusSelected + i >= PickUpFocusList.Count)
        {
            UnityEngine.Debug.Log(PickUpFocusSelected + " " + PickUpFocusList.Count);
            PickUpFocusSelected = 0;
            UnityEngine.Debug.Log("Moved to front");
        }
        else if (PickUpFocusSelected + i < 0)
        {
            PickUpFocusSelected = PickUpFocusList.Count - 1;
            UnityEngine.Debug.Log("Moved to back " + PickUpFocusSelected + ":index " + PickUpFocusList.Count + ":list size");
        }
        else
        {
            PickUpFocusSelected += i;
        }

        PickUpFocusList[PickUpFocusSelected].Outline();


    }
    public bool getAlive()
    {
        return alive;
    }
    private void killSelf()
    {
        print("tried to kill self");
        alive = !alive;
    }

    private void updateCarryPos(Vector3 pos)
    {
        if (PickUpCarry == null)
            return;
        PickUpCarry.updatePos(pos);
    }

    public void consumeCarry(string s)
    {
        if (PickUpCarry != null)
        {
            if (s == PickUpCarry.getID())
            {
                removePickUpFocus(PickUpCarry);
                PickUpCarry.Consume();
                PickUpCarry = null;
            }
        }
    }
    public void stopMove(float t)
    {
        canMove = false;
        velocity.x = 0f;
        moveDir.x = 0f;
        this.transform.GetChild(0).gameObject.SetActive(true);
        canMoveTimer = t;
    }
    public bool canCMove()
    {
        return canMove;
    }
    public void doDeath()
    {
        alive = false;
        //do interesting death mechanics
        //this.GetComponent<SpriteRenderer>().enabled = false;
        //canMove = false;
    }

    public void forceDrop(Vector3 dir)
    {
        var c = PickUpCarry;
        if (PickUpCarry == null)
            return;
        PickUpCarry.Drop();
        PickUpCarry = null;
        c.KnockAway(dir);
    }
    public void forceDrop()
    {
        if (PickUpCarry == null)
            return;
        PickUpCarry.Drop();
        PickUpCarry = null;
    }

    public IPickUp GetPickUp()
    {
        if (PickUpCarry != null)
            return PickUpCarry;
        return null;
    }
    public void sizeUp()
    {
        transform.localScale += new Vector3(0.01F, 0.01F, 0.0F);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (isGhost)
        {
            Physics.IgnoreCollision(controller, hit.collider,true);
            print("Collision");
        }
    }

    public IInteractable GetInteractable()
    {
        return InteractFocus;
    }

    private int CheckBumper()
    {
        if (Input.GetKeyDown(CycleLeftKey))
            return -1;
        if (Input.GetKeyDown(CycleRightKey))
            return 1;

        return 0;
    }

}
