using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DashState : ICharacterState
{
    float dashSpeed;
    Controller2D controller;
    // Use this for initialization
    public void Enter()
    {
    }
    bool airDash;
    int airDashCount;
    public DashState(Controller2D controller, float dashSpeed, int airDashCount = 0)
    {
        this.controller = controller;
        this.dashSpeed = dashSpeed;
        this.airDashCount = airDashCount;
        airDash = !controller.Grounded;
    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {

        if (airDash)
        {
            airDashCount++;
        }

        if (controller.triggerInput > 0 && controller.canCMove())
        {
            Dash();
            
        }

        if (controller.canCMove() && controller.dash && !controller.isGhost) //|| controller.isGhost && controller.GetComponent<Ghost>().dash)
        {
            DashCollision();
        }

        if (!controller.getAlive())
        {
            return new CharacterStateData(Vector2.zero, new GhostState(controller), true);
        }

        if (Input.GetKeyDown(controller.JumpKey) && controller.canCMove() && controller.Grounded)
        {
            controller.dash = false;
            return new CharacterStateData(Vector2.zero, new AirState(controller,false,1,1,0), true);
        }
        var velocity = controller.getVelocity();
        var movement = Vector2.zero;
        var airborne = !controller.Grounded;
        var characterStateData = GetCharacterStateData(movement, airborne);

        if (!controller.dash && !airborne)
        {
            return new CharacterStateData(Vector2.zero, new GroundState(controller), true);
        }
       
        return characterStateData;
    }

    /*rivate bool HandleMovement()
     {
         var airborne = !controller.getCharController().isGrounded;
         return airborne;
     }*/

    private CharacterStateData GetCharacterStateData(Vector2 movement, bool airborne)
    {
        var characterStateData = new CharacterStateData();
        characterStateData.Movement = movement;

        if (airborne && !controller.dash)
        {
            characterStateData.NewState = new AirState(controller, true,1,1,airDashCount);
        }

        return characterStateData;
    }

    void DashCollision()
    {
        RaycastHit[] rayhit;
        Ray right = new Ray(controller.transform.position, Vector3.right);
        Ray left = new Ray(controller.transform.position, Vector3.left);
        CharacterController charController = controller.getCharController();
        Vector3 halfExtent = new Vector3(charController.radius, charController.height / 2, charController.radius);
       

        //if (controller.dashInput.x > 0)
        
            rayhit = Physics.BoxCastAll(charController.transform.position + charController.center,halfExtent, controller.dashInput,new Quaternion(),charController.radius);
          
            foreach (RaycastHit hit in rayhit)
            {
                    if (hit.collider.tag == "Player" && hit.collider.GetComponent<Controller2D>().canCMove() && hit.collider.GetComponent<Controller2D>() != controller)
                    {
                        hit.collider.GetComponent<Controller2D>().stopMove(controller.stunTime);
                        hit.collider.GetComponent<Controller2D>().forceDrop(controller.moveDir);
                    }
            }
        

       /* else
        {
            
            rayhit = Physics.BoxCastAll(charController.transform.position + charController.center, halfExtent, Vector3.left, new Quaternion(),charController.radius);
           
            foreach (RaycastHit hit in rayhit)
            {
                if (hit.collider.tag == "Player" && hit.collider.GetComponent<Controller2D>().canCMove() && hit.collider.GetComponent<Controller2D>() != controller)
                {
                    {
                        hit.collider.GetComponent<Controller2D>().stopMove(1.0f);
                        hit.collider.GetComponent<Controller2D>().forceDrop(controller.moveDir);
                    }
                }

               
            }
        }
        */
        
       
    }

    void Dash()
    {
 
        if (controller.isGhost && controller.GetComponent<Ghost>().canDash)
        {
            controller.gameObject.GetComponent<Ghost>().Dash();
        }

        else if(controller.canDash)
        {
            controller.Dash();
        }

    }

    public void Exit()
    {

    }
}

