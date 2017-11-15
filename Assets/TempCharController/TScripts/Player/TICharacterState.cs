using UnityEngine;

public interface TICharacterState
 {
 void Enter();

 TCharacterStateData Update(Vector2 input, float deltaTime);

 void Exit();
 }
