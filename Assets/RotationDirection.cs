using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection
{
    clockwise,
    counterClockwise
}



public class ObjectRotater3D : MonoBehaviour
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
            transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed), Space.Self);
        else if (direction == RotationDirection.counterClockwise)
            transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed) * (-1), Space.Self);
        else
        {
            Debug.LogError("<color=red>Error:</color> Rotation direction error");
            return;
        }
    }


}