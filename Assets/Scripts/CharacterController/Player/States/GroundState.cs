using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : ICharacterState
{
    private Controller2D controller;


    public GroundState(Controller2D controller)
    {
        if (controller == null)
        {

        }

        this.controller = controller;
    }

    public void Enter()
    {

    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {
        if (Input.GetKeyDown(controller.JumpKey) && controller.getJumpDir() == (int)JumpDir.JumpUp && controller.canCMove())
        { 
            return new CharacterStateData(Vector2.zero, new AirState(controller), true);
        }

        if (Input.GetKeyDown(controller.DashKey) && controller.canCMove() && controller.canDash)
        {
            return new CharacterStateData(Vector2.zero, new DashState(controller, 5), true);
        }

        var velocity = controller.getVelocity();
        var movement = Vector2.zero;
        var airborne = !controller.getCharController().isGrounded;// && !controller.canJump;     
        var characterStateData = GetCharacterStateData(movement, airborne);

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
            /*if (controller.canJump)
            {
                controller.startJumpTimer();
            }*/
            if (!controller.canJump)
            {
                characterStateData.NewState = new AirState(controller, true);
            }
        }

        return characterStateData;
    }

    public void Exit()
    {

    }

}
