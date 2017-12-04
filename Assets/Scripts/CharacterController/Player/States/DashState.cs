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

    public DashState(Controller2D controller, float dashSpeed)
    {
        this.controller = controller;
        this.dashSpeed = dashSpeed;
    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {

      

        if (controller.triggerInput > 0 && controller.canCMove() && controller.canDash)
        {
            Dash();
            DashCollision();
        }

        if (!controller.getAlive())
        {
            return new CharacterStateData(Vector2.zero, new GhostState(controller), true);
        }

        if (Input.GetKeyDown(controller.JumpKey) && controller.canCMove())
        {
            controller.dash = false;
            return new CharacterStateData(Vector2.zero, new AirState(controller), true);
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

        if (airborne)
        {
            characterStateData.NewState = new AirState(controller, true);
        }

        return characterStateData;
    }

    void DashCollision()
    {
        RaycastHit[] rayhit;
        Ray right = new Ray(controller.transform.position, Vector3.right);
        Ray left = new Ray(controller.transform.position, Vector3.left);

        if (controller.dashInput.x > 0)
        {
            rayhit = Physics.RaycastAll(right, 0.5f);
            foreach (RaycastHit hit in rayhit)
            {
                    if (hit.collider.tag == "Player" && hit.distance < 0.4f && hit.collider is CapsuleCollider && hit.collider.GetComponent<Controller2D>().canCMove() && hit.collider.GetComponent<Controller2D>() != controller)
                    {
                        hit.collider.GetComponent<Controller2D>().stopMove(1.0f);
                        hit.collider.GetComponent<Controller2D>().forceDrop(controller.moveDir);
                    }
            }
        }

        else
        {
            rayhit = Physics.RaycastAll(left, 0.5f);
            foreach (RaycastHit hit in rayhit)
                    if (hit.collider.tag == "Player" && hit.distance < 0.4f && hit.collider is CapsuleCollider && hit.collider.GetComponent<Controller2D>().canCMove() && hit.collider.GetComponent<Controller2D>() != controller)
                    {
                        {
                            hit.collider.GetComponent<Controller2D>().stopMove(1.0f);
                            hit.collider.GetComponent<Controller2D>().forceDrop(controller.moveDir);
                        }
                    }
        }
    }

    void Dash()
    {
        if (controller.isGhost)
        {
            controller.gameObject.GetComponent<Ghost>().Dash();
        }

        else
        {
            controller.Dash();
        }
    }

    public void Exit()
    {

    }
}

