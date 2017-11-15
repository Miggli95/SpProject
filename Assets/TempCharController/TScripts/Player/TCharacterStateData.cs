using UnityEngine;

 public struct TCharacterStateData
 {
    public Vector2 Movement;

    public TICharacterState NewState;

    public bool RunNewStateSameUpdate;

    public TCharacterStateData(Vector2 movement, TICharacterState newState, bool runNewStateSameUpdate)
    {
        Movement = movement;
        NewState = newState;
        RunNewStateSameUpdate = runNewStateSameUpdate;
    }
 }