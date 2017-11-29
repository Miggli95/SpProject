using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AirState : ICharacterState
{

    private Controller2D controller;
    private int jumpCount;
    private int MaxJumpCount;
    bool fell;
    public AirState(Controller2D controller, bool fell = false, int maxJumpCount = 1)
    {
        if (controller == null)
        {

        }
        MaxJumpCount = maxJumpCount;
        this.controller = controller;
        //fell = controller.canJump;
        this.fell = fell;
        jumpCount = fell ? 1 : 0;
    }

    public void Enter()
    {

    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {


        if (Input.GetKeyDown(controller.JumpKey))
        {
            if (controller.canCMove())
            {
                if (jumpCount < MaxJumpCount)
                {
                    Jump();
                }
            }
        }
        else if (Input.GetKeyUp(controller.JumpKey) && controller.moveDir.y > controller.minJumpSpeed)
        {
            controller.moveDir.y = controller.minJumpSpeed;
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
