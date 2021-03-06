﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AirState : ICharacterState
{

    private Controller2D controller;
    private int jumpCount;
    private int MaxJumpCount;
    bool fell;
    bool airDash;
    int MaxAirDashCount;
    public AirState(Controller2D controller, bool fell = false, int maxJumpCount = 1, int MaxAirDashCount = 1)
    {
        if (controller == null)
        {

        }
        MaxJumpCount = maxJumpCount;
        this.controller = controller;
        //fell = controller.canJump;
        this.fell = fell;
        this.MaxAirDashCount = MaxAirDashCount;
        jumpCount = fell ? 1 : 0;
        airDash = controller.airDashCount > 0; 
    }

    public void Enter()
    {

    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {
        if (!controller.getAlive())
        {
            return new CharacterStateData(Vector2.zero, new GhostState(controller), true);
        }

        if (Input.GetKeyDown(controller.JumpKey))
        {
            if (controller.canCMove())
            {
                if (jumpCount < MaxJumpCount)
                {
                    Jump();
                }

                else
                {
                    controller.savedJumpInput = true;
                }
            }
        }
        else if (Input.GetKeyUp(controller.JumpKey) && controller.moveDir.y > controller.minJumpSpeed)
        {
            controller.moveDir.y = controller.minJumpSpeed;
        }

        if (controller.triggerInput > 0 && controller.canCMove() && controller.canDash && controller.airDashCount<MaxAirDashCount)
        {
            return new CharacterStateData(Vector2.zero, new DashState(controller, controller.dashSpeed), true);
        }


        /*  if (controller.getCharController().isGrounded)
          {
              //return new CharacterStateData(Vector2.zero,new GroundState(controller), true);
          }*/




        var velocity = controller.getVelocity();
        return HandleVerticalMovement(velocity, input, deltaTime);
    }

    private CharacterStateData HandleVerticalMovement(Vector2 velocity, Vector2 input, float deltaTime)
    {
        CharacterStateData cs = new CharacterStateData();
        if (controller.Grounded)
        {
            cs.NewState = new GroundState(controller);
        }
        return cs;
    }

    public void Jump()
    {
        ++jumpCount;
        controller.Jump(jumpCount, fell);
  
    }

    public void Exit()
    {

    }

}
