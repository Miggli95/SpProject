using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostState : ICharacterState
{
    private Controller2D controller;

    bool isGhost;
    public GhostState(Controller2D controller)
    {
        if (controller == null)
        {

        }

        isGhost = true;
        this.controller = controller;
        controller.isGhost = true;
    }

    public void Enter()
    {
        controller.gameObject.GetComponent<Ghost>().InitializeGhost();
    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {
        //if (Input.GetKeyDown(controller.DashKey) && controller.canCMove() && controller.canDash)
        if (controller.triggerInput > 0 && controller.canCMove() && controller.canDash)
        {
            return new CharacterStateData(Vector2.zero, new DashState(controller, controller.dashSpeed), true);
        }

        var velocity = controller.getVelocity();
        var movement = Vector2.zero;
        var airborne = !controller.Grounded;// && !controller.canJump;   
        var isGhost = !controller.getAlive();  
        var characterStateData = GetCharacterStateData(movement, isGhost, airborne);

        return characterStateData;
    }

    /*rivate bool HandleMovement()
     {
         var airborne = !controller.getCharController().isGrounded;
         return airborne;
     }*/

    private CharacterStateData GetCharacterStateData(Vector2 movement, bool isGhost, bool airborne)
    {
        var characterStateData = new CharacterStateData();
        characterStateData.Movement = movement;

        if (!isGhost)
        {
            /*if (controller.canJump)
            {
                controller.startJumpTimer();
            }*/
            controller.isGhost = isGhost;
            if (airborne)
            {
                characterStateData.NewState = new AirState(controller, true);
            }

            else
            {
                characterStateData.NewState = new GroundState(controller);
            }
        }

        return characterStateData;
    }

    public void Exit()
    {
        controller.gameObject.GetComponent<Ghost>().Exit();
    }

}
