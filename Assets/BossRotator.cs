using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossRotator : MonoBehaviour

{ 
    [SerializeField]
    private RotationDirection direction;
    [SerializeField]
    private float rotationSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotationSpeed == 0)
            return;
        else
            RotateObject();

    }

    private void RotateObject()
    {
        if (direction == RotationDirection.clockwise)
            transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed), Space.World);
        else if (direction == RotationDirection.counterClockwise)
            transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed) * (-1), Space.World);
        else {
            Debug.LogError("<color=red>Error:</color> Rotation direction error");
            return;
        }
    }


}
