using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Controller2D controller2D;
    CharacterController charController;
    public LayerMask CollisionMask;
    private CollisionFlags colFlags;
    public Vector3 moveDir;
    Vector3 targetDir;
    float startZ;
    float previousX;
    Vector2 charInput;
    float triggerInput;
    float gravityMultiplier = 0;
    public float speed = 5;
    public float dashSpeed = 10;
    public float accelerationTime = 0.1f;
    public float deaccelerationTime = 0.2f;
    public bool initialized = false;
    // Use this for initialization
    public void InitializeGhost ()
    {
        controller2D = GetComponent<Controller2D>();
        charController = GetComponent<CharacterController>();
        startZ = transform.position.z;
        initialized = true;
	}
	
	// Update is called once per frame
	public void GhostUpdate (Vector2 charInput, float triggerInput)
    {
        this.charInput = charInput;
        this.triggerInput = triggerInput;
	}

    public void FixedUpdate()
    {
        if (controller2D == null)
            return;

        if (transform.position.z == startZ)
        {
            previousX = transform.position.x;
        }

        Vector3 destination;
        Vector3 targetDir = Vector3.zero;
        RaycastHit hit;

            //controller.gameObject.layer = Ghost;
            destination = transform.up * charInput.y + transform.right * charInput.x;
            gravityMultiplier = 0;
            Physics.SphereCast(transform.position, charController.radius, Vector3.down, out hit,
               charController.height / 2, CollisionMask, QueryTriggerInteraction.Ignore);
        

       
            if (controller2D.dash)
            {
                destination = transform.right * controller2D.dashInput.x;
            }
            

            destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;
        

        if (charInput.x == 0 && charInput.y == 0)
        {
            destination = Vector3.zero;
        }

        //new Code


        if (controller2D.dash)
        {


            if (controller2D.dashTimer >= 0 && controller2D.canDash)
            {
                controller2D.dash = destination.x != 0;
                targetDir.x = destination.x *  dashSpeed;
                targetDir.y = destination.y * dashSpeed;
                controller2D.dashTimer -= Time.fixedDeltaTime;
            }

            else
            {
                controller2D.dashCooldownTimer = controller2D.dashCooldown;
                controller2D.canDash = false;
                controller2D.dash = false;
            }


        }

        else
        {
            targetDir.x = destination.x * speed;//Mathf.SmoothDamp(moveDir.x,destination.x * speed,ref moveDir.x,smoothTime);
            targetDir.y = destination.y * speed;

        }


      

        //Rays();

        //controller2D.Grounded = charController.isGrounded;
        

        
       
            moveDir.y = controller2D.Smooth(targetDir.y, ref moveDir.y, accelerationTime, deaccelerationTime);
            //controller.GetComponent<Collider>().isTrigger = true;
       
            moveDir.x = controller2D.Smooth(targetDir.x, ref moveDir.x, accelerationTime, deaccelerationTime);
            
        

       

        if (moveDir.x != targetDir.x)
        {
            moveDir.x = targetDir.x;
        }


        if (moveDir.y != targetDir.y)
        {
            moveDir.y = targetDir.y;
        }


        if (controller2D.boost)
        {
            moveDir.y = controller2D.BoostSpeed;
            controller2D.boost = false;
        }

        colFlags = charController.Move(moveDir * Time.fixedDeltaTime);

        if (charController.velocity.x == 0)
        {
            moveDir.x = 0;
            //dashDestination.x = 0;
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

    public void Exit()
    {
        controller2D = null;
        charController = null;
        initialized = false;
    }
}
