using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
   // private AudioSource[] sources;
  /*  private AudioSource source1;
    private AudioSource source2;
    public AudioClip[] jump_sounds;
    private int lastrandom = -1;
    private float volLowRange = 0.2f;
    private float volHighRange = 1.0f;
    private float lowPitchRange = 0.9f;
    private float highPitchRange = 1.05f; */
    public float MinJumpHeight = 1.0f;
    [HideInInspector]
    public float MinJumpVelocity;

    public float MoveSpeed = 6.0f;

    public float GroundAccelerationTime = 0.1f;

    public float DeaccelerationScale = 0.8f;

    public float MaxSlopeAngle = 80.0f;

    [Tooltip("The maximum distance between the bottom of the box collider and the ground beneath it, in terms of box collider scale, for the collider to clamp to the ground.")]

    public float VerticalClampFactor = 0.25f;

    [Tooltip("Multiplier applied to Move Speed when jumping off a wall.")]
    public float WallJumpSpeedMultiplier = 2.0f;

    [Tooltip("Wall slide speed in units per sec.")]
    public float WallSlideSpeed = 1.5f;

    public float wallSlideStickTime = 2.25f;
    public float timeToWallUnstick = 0f;
    public LayerMask CollisionMask;

    public float MaxDistBetweenRays = 0.25f;

    public float AirAccelerationTime = 0.2f;
    public float MaxJumpHeight = 4.0f;
    public float TimeToJumpApex = 0.4f;
    private float MaxJumpVelocity;
    public void Jump()
    {
       // bool newNumber = true;
        Velocity.y = MaxJumpVelocity;
       /* int clipIndex = Random.Range(0, jump_sounds.Length);
        while (newNumber) {
            if (!(clipIndex == lastrandom))
                newNumber = false;
            else
                clipIndex = Random.Range(0, jump_sounds.Length);
        }
        source1.pitch = Random.Range(lowPitchRange, highPitchRange);
        float vol = Random.Range(volLowRange, volHighRange);
        source1.PlayOneShot(jump_sounds[clipIndex],vol);
        lastrandom = clipIndex;*/
    }

    [HideInInspector]
    public Vector2 Velocity;

    #region Constants
    public readonly KeyCode JumpKey = KeyCode.Space;

    public readonly float SkinWidth = 0.03f;
    private const float MinRaySpacing = 0.1f;

    #endregion

    private ICharacterState characterState;

    private RaySpacing raySpacing;

    private float velocityXSmoothing;

    private float gravity;

    private BoxCollider2D boxCollider;

    public float VerticalClampDistance
    {
         get { return boxCollider.size.y* VerticalClampFactor;}
    }

    public RaycastOrigins GetUpdatedRaycastOrigins()
    {
        var skinnedBounds = SkinnedBounds;
        var raycastOrigins = new RaycastOrigins();
        raycastOrigins.BottomLeft = new Vector2(skinnedBounds.min.x, skinnedBounds.min.y);
        raycastOrigins.BottomRight = new Vector2(skinnedBounds.max.x, skinnedBounds.min.y);
        raycastOrigins.TopLeft = new Vector2(skinnedBounds.min.x, skinnedBounds.max.y);
        raycastOrigins.TopRight = new Vector2(skinnedBounds.max.x, skinnedBounds.max.y);
        return raycastOrigins;
    }

    public Vector2 CalculateVelocity(Vector2 input, float accelerationTime)
    {
       var targetVelocityX = input.x * MoveSpeed;
       var smoothTime = accelerationTime;
       if (Mathf.Abs(input.x) < MathHelper.FloatEpsilon)
       {
            smoothTime *= DeaccelerationScale;
       }

       Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX, ref
       velocityXSmoothing, smoothTime);
       Velocity.y += gravity* Time.deltaTime;
       return Velocity* Time.deltaTime;
    }

  public CollisionData GetHorizontalCollision(Vector2 rayDirection, Vector2 rayOrigin, float rayLength)
  {
      var collisionData = new CollisionData();
      var rayMovement = Vector2.up * raySpacing.HorizontalRaySpacing;
      for (int i = 0; i<raySpacing.HorizontalRayCount; ++i)
      {
            var hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, CollisionMask);
            if (hit)
            {
                if (hit.collider.tag == "One Way" && Velocity.y > 0)
                {
                }
                else
                {
                    rayLength = hit.distance;
                    collisionData = new CollisionData(true, rayLength, hit.normal);
                }
            }
            rayOrigin += rayMovement;
       }
  return collisionData;
  }
 
 public CollisionData GetVerticalCollision(Vector2 rayDirection, Vector2 rayOrigin, float rayLength)
 {
    var collisionData = new CollisionData();
    var rayMovement = Vector2.right * raySpacing.VerticalRaySpacing;
    for (int i = 0; i<raySpacing.VerticalRayCount; ++i)
    {
        var hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, CollisionMask);
        if (hit)
        {
                if (hit.collider.tag == "One Way" && Velocity.y > 0)
                {
                }
                else
                {
                    rayLength = hit.distance;
                    collisionData = new CollisionData(true, rayLength, hit.normal);
                }
        }
        rayOrigin += rayMovement;
    }
    return collisionData;
 }

 public void ResetVelocityX()
 {
    Velocity.x = velocityXSmoothing = 0.0f;
 }

public HorizontalDirection GetHorizontalDirection(Vector2 direction)
{
 return (HorizontalDirection)MathHelper.Sign(direction.x);
}

 public VerticalDirection GetVerticalDirection(Vector2 direction)
 {
 return (VerticalDirection)MathHelper.Sign(direction.y);
 }

    private ICharacterState GetInitialCharacterState()
    {
        ICharacterState characterState = null;
        var raycastOrigins = GetUpdatedRaycastOrigins();
        var verticalCollisionData = GetVerticalCollision(Vector2.down, raycastOrigins.BottomLeft, raySpacing.VerticalRaySpacing + SkinWidth); // Arbitrary length
     if (verticalCollisionData.Hit && verticalCollisionData.Distance < SkinWidth)
        {
            characterState = new GroundState(this);
        }
        else
        {
            characterState = new AirState(this,true);
        }
        return characterState;
    }


    private void Start()
 {
 boxCollider = GetComponent<BoxCollider2D>();
        gravity = -(2 * MaxJumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
        var positiveGravity = gravity * -1;
        MaxJumpVelocity = positiveGravity * TimeToJumpApex;
        MinJumpVelocity = Mathf.Sqrt(2 * positiveGravity * MinJumpHeight);


        CalculateRaySpacing();

        characterState = GetInitialCharacterState();
        characterState.Enter();
       // source1 = GetComponent<AudioSource>();
    }

 private void CalculateRaySpacing()
 {
 var distanceBetweenRays = Mathf.Clamp(MaxDistBetweenRays, MinRaySpacing, float.MaxValue);
 raySpacing = new RaySpacing(SkinnedBounds, distanceBetweenRays);
 }

 private Bounds SkinnedBounds
 {
    get
    {
        var bounds = boxCollider.bounds;
        bounds.Expand(-SkinWidth);
        return bounds;
    }
 }

 private void Update()
 {
     var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
     var characterStateData = characterState.Update(input, Time.deltaTime);
     transform.Translate(characterStateData.Movement);
     if (characterStateData.NewState != null)
     {
         ChangeCharacterState(input, characterStateData);
     }
 }

 private void ChangeCharacterState(Vector2 input, CharacterStateData characterStateData)
 {
     PrintStateSwitching(characterStateData);
     characterState.Exit();
     characterState = characterStateData.NewState;
     characterState.Enter();
     if (characterStateData.RunNewStateSameUpdate)
     {
        characterState.Update(input, Time.deltaTime);
     }
 }

 [Conditional("UNITY_EDITOR")]
 private void PrintStateSwitching(CharacterStateData characterStateData)
    {
        print("Switching from " + characterState.ToString() + " to " + characterStateData.NewState.ToString());
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (Mathf.Abs(other.transform.position.y - transform.position.y) <= 0.5)
            {
                other.gameObject.GetComponent<EnemyAI2D>().deathAni();
                Velocity.y = MaxJumpVelocity;
            }
            else
            {
                other.gameObject.GetComponent<EnemyAI2D>().getDamage();
                Velocity.x = -5;
            }
        }
    }*/
}