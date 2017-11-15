using System;
using UnityEngine;

public struct TAirState : TICharacterState
{
     
    private TController2D controller;

    private TRaycastOrigins raycastOrigins;

    private int jumpCount;
    private bool wallsticking;

    public TAirState(TController2D controller, bool fell = false)
    {
        if (controller == null)
        {
            throw new ArgumentNullException("controller");
        }

        this.controller = controller;
        raycastOrigins = new TRaycastOrigins();
        jumpCount = fell ? 1 : 0;
        wallsticking = false;
    }

    public void Enter()
    {
        // NOTE: Empty by choice. Here for demonstration purposes.
    }

    public TCharacterStateData Update(Vector2 input, float deltaTime)
    {
        raycastOrigins = controller.GetUpdatedRaycastOrigins();
        
        if (Input.GetKeyDown(controller.JumpKey))
        {
            if (ShouldWallJump(input))
            {
                wallsticking = false;
                WallJump(input);
            }
            if (wallsticking == true && controller.timeToWallUnstick < controller.wallSlideStickTime && controller.timeToWallUnstick > 0)
            {
                wallsticking = false;
                WallLeap(input);
            }
            else if (jumpCount < 2)
            {
                Jump();
            }
        }
        else if (Input.GetKeyUp(controller.JumpKey) && controller.Velocity.y >controller.MinJumpVelocity)
        {
            controller.Velocity.y = controller.MinJumpVelocity;
        }


        var velocity = controller.CalculateVelocity(input, controller.AirAccelerationTime);
        if (ShouldWallJump(input))
        {
            controller.timeToWallUnstick = controller.wallSlideStickTime;
            wallsticking = true;
        }
        else
        {
            if (wallsticking == true)
            {
                if (controller.timeToWallUnstick > 0)
                {
                   //velocity.x = 0;
                   controller.timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    wallsticking = false;
                }
                 
                    
            }

        }
        return HandleMovement(velocity, input, deltaTime);
       //return new TCharacterStateData();
    }

    private void Jump()
    {
        ++jumpCount;
        controller.Jump();
    }
    private bool ShouldWallJump(Vector2 input)
    {
        var shouldWallJump = false;
        if (jumpCount > 0)
        {
            // NOTE: We want a ray length that is just a tiny bit greater than the skin width when checking if being next to a wall.
            // This is very much a tweakable number as in affects "controller feeling". The author decided to hardcode it here instead of
            // exposing it through the editor. A case can easily be made to expose the factor through the editor though, makes perfect sense.
            var horizontalDirection = controller.GetHorizontalDirection(input);
            var horizontalRaycastOrigin = horizontalDirection == THorizontalDirection.Right ? raycastOrigins.BottomRight : raycastOrigins.BottomLeft;
            var rayDirection = Vector2.right * horizontalDirection.ToInt();
            var rayLength = controller.SkinWidth * 1.01f;
            shouldWallJump = Physics2D.Raycast(horizontalRaycastOrigin, rayDirection, rayLength, controller.CollisionMask) == true;
        }
        return shouldWallJump;
    }
    private void WallJump(Vector2 input)
    {
        var horizontalDirection = controller.GetHorizontalDirection(input);
        controller.Velocity.x = -horizontalDirection.ToInt() * controller.MoveSpeed * controller.WallJumpSpeedMultiplier;
        jumpCount = 2;
        controller.Jump();
    }
    private void WallLeap(Vector2 input)
    {
        var horizontalDirection = controller.GetHorizontalDirection(input);
        controller.Velocity.x = horizontalDirection.ToInt() * controller.MoveSpeed * controller.WallJumpSpeedMultiplier;
        jumpCount = 1;
        controller.Jump();
    }


    public void Exit()
    {
        // NOTE: Empty by choice. Here for demonstration purposes.
    }
    private TCharacterStateData HandleMovement(Vector2 velocity, Vector2 input, float deltaTime)
    {
        var characterStateData = new TCharacterStateData();
        var didHitSide = HandleHorizontalMovement(velocity, ref characterStateData);
        HandleVerticalMovement(didHitSide, deltaTime, velocity, ref characterStateData);
        return characterStateData;
    }

    private bool HandleHorizontalMovement(Vector2 velocity, ref TCharacterStateData characterStateData)
    {
        var didHitSide = false;
        var horizontalDirection = controller.GetHorizontalDirection(velocity);
        if (horizontalDirection != THorizontalDirection.None)
        {
            var rayDirection = Vector2.right * horizontalDirection.ToInt();
            var rayOrigin = horizontalDirection == THorizontalDirection.Right ? raycastOrigins.BottomRight : raycastOrigins.BottomLeft;
            var rayLength = Mathf.Abs(velocity.x) + controller.SkinWidth;
            var collisionData = controller.GetHorizontalCollision(rayDirection, rayOrigin, rayLength);
            if (collisionData.Hit)
            {
                didHitSide = true;
                characterStateData.Movement.x += horizontalDirection.ToInt() *
               (collisionData.Distance - controller.SkinWidth);
                controller.ResetVelocityX();
            }
            else
            {
                characterStateData.Movement.x += horizontalDirection.ToInt() *
               (rayLength - controller.SkinWidth);
            }
        }
        return didHitSide;
    }

    private void HandleVerticalMovement(bool didHitSide, float deltaTime, Vector2 velocity, ref TCharacterStateData characterStateData)
    {
        var updatedRayCastOrigins = raycastOrigins.Move(characterStateData.Movement);
        var verticalDirection = controller.GetVerticalDirection(velocity);
        var isWallSliding = verticalDirection == TVerticalDirection.Down && didHitSide;
        var rayLength = (isWallSliding ? controller.WallSlideSpeed * deltaTime : Mathf.Abs(velocity.y)) + controller.SkinWidth;
        var rayDirection = Vector2.up * verticalDirection.ToInt();
        var rayOrigin = verticalDirection == TVerticalDirection.Up ? updatedRayCastOrigins.TopLeft : updatedRayCastOrigins.BottomLeft;
        var collisionData = controller.GetVerticalCollision(rayDirection, rayOrigin, rayLength);
        if (collisionData.Hit)
        {
            characterStateData.Movement.y += verticalDirection.ToInt() *
           (collisionData.Distance - controller.SkinWidth);
            if (velocity.y < 0.0f)
            {
                characterStateData.NewState = new TGroundState(controller);
            }

            controller.Velocity.y = 0.0f;
        }
        else
        {
            var yVelocity = verticalDirection.ToInt() * (rayLength - controller.SkinWidth);
            characterStateData.Movement.y += yVelocity;
           if (isWallSliding)
            {
                controller.Velocity.y = 0.0f;
            }

        }
    }
}
