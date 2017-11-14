using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Controller2D : MonoBehaviour {

    ICharacterState currentState;
    public LayerMask CollisionMask;
    CharacterController controller;
    ICharacterState characterState;
    public KeyCode JumpKey = KeyCode.Space;
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
    private bool jump;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z == startZ)
        {
            previousX = transform.position.x;
        }

        t += Time.fixedDeltaTime;

        if (Physics.SphereCast(transform.position, controller.radius, Vector3.up, out topHit,
          BounceDownOnRoof, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            moveDir.y = -1;
        }
        Vector3 destination = transform.right * charInput.x;
        RaycastHit hit;

        Physics.SphereCast(transform.position, controller.radius, Vector3.down, out hit,
            controller.height / 2, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;
        if (charInput.x == 0 && charInput.y == 0)
        {
            destination = Vector3.zero;
        }
        moveDir.x = destination.x * speed;

       

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

        if (controller.isGrounded)
        {
            jumpTimerDelay = jumpTimer;
        }

        if (jumpTimerDelay > 0 && !controller.isGrounded)
        {
            jumpTimerDelay -= Time.deltaTime;
        }
		
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
}
