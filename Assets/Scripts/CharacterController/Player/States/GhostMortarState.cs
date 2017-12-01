using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMortarState : ICharacterState
{

    private Controller2D controller;

    bool isGhost;
    bool canSwitchState;
    public GhostMortarState(Controller2D controller, bool canSwitchState)
    {
        if (controller == null)
        {

        }

        isGhost = true;
        this.controller = controller;
        controller.isGhost = true;
        this.canSwitchState = canSwitchState;
    }

    public void Enter()
    {
        //controller.gameObject.GetComponent<Ghost>().InitializeGhost();
        InitializeMortar();
    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {
        //if (Input.GetKeyDown(controller.DashKey) && controller.canCMove() && controller.canDash)
        if (Input.GetKeyUp(controller.InteractKey))
        {
            canSwitchState = true;
        }

        if (Input.GetKeyDown(controller.InteractKey) && canSwitchState)
        {
            controller.GetComponent<Ghost>().stationary = false;
            // return new CharacterStateData(Vector2.zero, new GhostState(controller), true);
        }

        

        var velocity = controller.getVelocity();
        var movement = Vector2.zero;
        var airborne = !controller.Grounded;// && !controller.canJump;   
        var isGhost = !controller.getAlive();
        var stationary = controller.GetComponent<Ghost>().stationary;
        var characterStateData = GetCharacterStateData(movement, isGhost, airborne, stationary);

        return characterStateData;
    }

    /*rivate bool HandleMovement()
     {
         var airborne = !controller.getCharController().isGrounded;
         return airborne;
     }*/

    private CharacterStateData GetCharacterStateData(Vector2 movement, bool isGhost, bool airborne, bool stationary)
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

        else if (!stationary)
        {
            characterStateData.NewState = new GhostState(controller);
        }

        return characterStateData;
    }

    public void InitializeMortar()
    {
        controller.gameObject.GetComponent<Ghost>().MortarState();
    }

    public void Exit()
    {
    }

}

