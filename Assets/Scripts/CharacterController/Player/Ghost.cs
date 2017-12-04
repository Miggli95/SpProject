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
    Vector3 aimDir;
    Vector3 targetDir;
    float startZ;
    float previousX;
    Vector2 charInput;
    float triggerInput;
    float gravityMultiplier = 0;
    public float speed = 5;
    public float aimSpeed = 5;
    public float projectileSpeed = 5;
    public float dashSpeed = 10;
    public float accelerationTime = 0.1f;
    public float deaccelerationTime = 0.2f;
    public bool initialized = false;
    public float dashTimer;
    public float dashDuration;
    public bool canDash = true;
    public Vector2 dashInput;
    public float dashCooldown = 0.1f;
    public float dashCooldownTimer;
    public bool dash;
    public bool stationary = false;
    public GameObject mortarAim;
    public GameObject projectile;
    public GameObject mortarTarget;
    public float shootAngle;
    bool shoot = false;
    public float minDistance = 0.7f;
    public float maxDistance = 10f;
    public Vector3 previousAimPosition;
    Transform preview;
    void Start()
    {
        controller2D = GetComponent<Controller2D>();
        dashDuration = controller2D.DashTimer;
        dashCooldown = controller2D.dashCooldown;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == "MortarAim")
            {
                mortarAim = transform.GetChild(i).gameObject;
                preview = Instantiate(mortarAim.transform);
            }

            else if (transform.GetChild(i).gameObject.name == "MortarProjectile")
            {
                projectile = transform.GetChild(i).gameObject;
            }

            else if (transform.GetChild(i).gameObject.name == "MortarTarget")
            {
                mortarTarget = transform.GetChild(i).gameObject;
            }
        }

        
    }

    // Use this for initialization
    public void InitializeGhost ()
    {
        if (!controller2D.getAlive())
        {
            mortarAim.SetActive(stationary);
            charController = GetComponent<CharacterController>();
            startZ = transform.position.z;
            initialized = true;
        }
        

	}
	
	// Update is called once per frame
	public void GhostUpdate (Vector2 charInput, float triggerInput)
    {
        this.charInput = charInput;
        this.triggerInput = triggerInput;

        if (dashCooldownTimer >= 0)
        {

            dashCooldownTimer -= Time.deltaTime;
        }

        else
        {
            canDash = true;
        }

        if (stationary)
        {
            MortarAim();
            return;
        }

    }

    public float distance;

    public void MortarAim()
    {
        preview.position = mortarAim.transform.position;
        
        Vector3 aim ,targetDir;
         aim = transform.up * charInput.y + transform.right * charInput.x;
        /*if (distance >= maxDistance)
        {
            mortarAim.transform.position = previousAimPosition;
        }*/

        targetDir.x = aim.x * speed;
        targetDir.y = aim.y * speed;

        aimDir.y = controller2D.Smooth(targetDir.y, ref moveDir.y, accelerationTime, deaccelerationTime);

        aimDir.x = controller2D.Smooth(targetDir.x, ref moveDir.x, accelerationTime, deaccelerationTime);
            

        if (aimDir.x != targetDir.x)
        {
            aimDir.x = targetDir.x;
        }


        if (aimDir.y != targetDir.y)
        {
            aimDir.y = targetDir.y;
        }


       preview.Translate(aimDir*Time.deltaTime);
        distance = Vector3.Distance(transform.position, preview.transform.position);

        if (distance <= maxDistance)
        {
            mortarAim.transform.Translate(aimDir * Time.deltaTime);
            preview.position = mortarAim.transform.position;
        }
        if (triggerInput > 0)
        {
            //float distance = Vector3.Distance(transform.position, mortarAim.transform.position);
            if (distance>minDistance && distance<maxDistance)
            {
                MortarShoot();
                stationary = false;
            }
        }

        if (distance < maxDistance)
        {
            previousAimPosition = mortarAim.transform.position;
        }

    }

    public void MortarShoot(float Angle = 0, float Speed = 0)
    {
       float angle;
        float speed;

        if (Angle == 0)
        {

            angle = transform.GetComponent<AimAssist>().angle;

        }

        else
        {
            angle = Angle;
        }

        if (Speed == 0)
        {
            speed = projectileSpeed;
        }

        else
        {
            speed = Speed;
        }

        //shootAngle = angle;
        GameObject target = Instantiate(mortarTarget, mortarAim.transform.position, new Quaternion());
        target.SetActive(true);
        GameObject bullet = Instantiate(projectile,transform.position,new Quaternion());
        Quaternion rotation = transform.GetComponent<AimAssist>().rotation;
        bullet.GetComponent<Projectile>().Shoot(angle, projectileSpeed, rotation, target.GetComponent<Target>());
        bullet.SetActive(true);
        target.GetComponent<Target>().setProjectile(bullet.GetComponent<Projectile>());
}

    public void FixedUpdate()
    {
        if (!controller2D.canCMove())
            return;

        if (charController == null)
            return;

        /*if (shoot)
        {
            MortarShoot();
        }*/

        if (stationary)
        {
            return;
        }

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
        

       
            if (dash)
            {
                destination = transform.up * dashInput.y + transform.right * dashInput.x;
            }
            

            destination = Vector3.ProjectOnPlane(destination, hit.normal).normalized;
        

        if (charInput.x == 0 && charInput.y == 0)
        {
            destination = Vector3.zero;
        }

        //new Code


        if (dash)
        {


            if (dashTimer >= 0 && canDash)
            {
                dash = destination.x != 0;
                targetDir.x = destination.x *  dashSpeed;
                targetDir.y = destination.y * dashSpeed;
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

    public void Dash()
    {
        if (!dash)
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            dashInput = charInput;
            //dashDestination = moveDir;
            dashTimer = dashDuration;
            dash = true;
        }
    }

    public void MortarState()
    {
        stationary = true;
        mortarAim.transform.localPosition = Vector3.zero;
        mortarAim.SetActive(stationary);
        previousAimPosition = mortarAim.transform.position;
    }

    public void Exit()
    {
        if (controller2D.getAlive())
        {
            charController = null;
            initialized = false;
        }
    }
}
