using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Controller2D : MonoBehaviour {

    ICharacterState currentState;
    public LayerMask CollisionMask;
    CharacterController controller;
    ICharacterState characterState;
    public readonly KeyCode JumpKey = KeyCode.JoystickButton0;
    public readonly KeyCode DashKey = KeyCode.Joystick1Button5; 
    Vector2 charInput;
    float jumpTimerDelay;
    float jumpTimer;
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
    public Vector3 dashDestination;
    public float bottomRayLength = 0.08f;
    RaycastHit bottom;
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
	void Start ()
    {
        controller = GetComponent<CharacterController>();
        startZ = transform.position.z;
        characterState = GetInitialCharacterState();
        characterState.Enter();
	}

    public float Smooth(float target,ref float currentValue)
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

        moveDir.x = Smooth(targetDir.x,ref moveDir.x);

        if (controller.isGrounded)
        {

            moveDir.y = -stickToGrouncForce;
        }

        else
        {
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

    void Update ()
    {
        
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

        if (charInput.y == -1)
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

        if (jumpTimerDelay > 0 && !controller.isGrounded)
        {
            jumpTimerDelay -= Time.deltaTime;
        }*/

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
        jump = true;
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
}
