using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AirState : ICharacterState
{

    private Controller2D controller;
    private int jumpCount;
    private int MaxJumpCount;
    public AirState(Controller2D controller, bool fell = false, int maxJumpCount = 1)
    {
        if (controller == null)
        {

        }
        MaxJumpCount = maxJumpCount;
        this.controller = controller;
        //fell = controller.canJump;
        jumpCount = fell ? 1 : 0;
    }

    public void Enter()
    {

    }

    public CharacterStateData Update(Vector2 input, float deltaTime)
    {


        if (Input.GetKeyDown(controller.JumpKey) && controller.canCMove())
        {
            if (jumpCount < MaxJumpCount)
            {
                Jump();
            }
        }


        else if (controller.getCharController().isGrounded)
        {

            return new CharacterStateData(Vector2.zero, new GroundState(controller), true);

        }

        var velocity = controller.getVelocity();
        return HandleVerticalMovement(velocity, input, deltaTime);
    }

    private CharacterStateData HandleVerticalMovement(Vector2 velocity, Vector2 input, float deltaTime)
    {
        CharacterStateData cs = new CharacterStateData();
        if (controller.getCharController().isGrounded)
        {
            cs.NewState = new GroundState(controller);
        }
        return cs;
    }

    public void Jump()
    {
        ++jumpCount;
        controller.Jump();
    }

    public void Exit()
    {

    }

}
