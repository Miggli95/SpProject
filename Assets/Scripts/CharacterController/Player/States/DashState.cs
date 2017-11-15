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

        if (Input.GetKeyDown(controller.DashKey))
        {
            Dash();
        }

        if (Input.GetKeyDown(controller.JumpKey))
        {
            return new CharacterStateData(Vector2.zero, new AirState(controller), true);
        }
        var velocity = controller.getVelocity();
        var movement = Vector2.zero;
        var airborne = !controller.getCharController().isGrounded;
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

    void Dash()
    {
        controller.Dash();
    }

    public void Exit()
    {

    }
}

