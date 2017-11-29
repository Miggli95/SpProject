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

        if (Input.GetKeyDown(controller.DashKey) && controller.canCMove())
        {
            Dash();
        }

        if (Input.GetKeyDown(controller.JumpKey) && controller.canCMove())
        {
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
        RaycastHit rayhit;
        if (input.x > 0)
        {
            if (Physics.Raycast(controller.transform.position, Vector3.right, out rayhit))
            {
                if (rayhit.collider.tag == "Player" && rayhit.distance < 0.4f && rayhit.collider is CapsuleCollider && rayhit.collider.GetComponent<Controller2D>().canCMove() && rayhit.collider.GetComponent<Controller2D>() != controller)
                {
                    rayhit.collider.GetComponent<Controller2D>().stopMove(1.0f);
                    rayhit.collider.GetComponent<Controller2D>().forceDrop(controller.moveDir);
                }
            }
        }
            if (Physics.Raycast(controller.transform.position, Vector3.left, out rayhit))
            {
                if (rayhit.collider.tag == "Player" && rayhit.distance < 0.4f && rayhit.collider is CapsuleCollider && rayhit.collider.GetComponent<Controller2D>().canCMove() && rayhit.collider.GetComponent<Controller2D>() != controller)
                {
                    {
                        rayhit.collider.GetComponent<Controller2D>().stopMove(1.0f);
                        rayhit.collider.GetComponent<Controller2D>().forceDrop(controller.moveDir);
                    }
                }
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

    void Dash()
    {
        controller.Dash();
    }

    public void Exit()
    {

    }
}

