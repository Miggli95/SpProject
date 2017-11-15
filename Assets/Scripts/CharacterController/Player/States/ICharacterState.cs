using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{

    // Use this for initialization
    void Enter();
    CharacterStateData Update(Vector2 input, float deltaTime);
    void Exit();
}
