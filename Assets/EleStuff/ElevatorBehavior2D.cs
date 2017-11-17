using UnityEngine;
using System.Collections;

public class ElevatorBehavior2D : MonoBehaviour
{

    [SerializeField]
    private TransformDirection direction;
    [SerializeField]
    private float transformSpeed;
    [SerializeField]
    private float transformLength;
    [SerializeField]
    private float transformDelay;
    Transform parent;
    private float fixedStopTime;
    private float stopTimer;
    private int binar;
    private bool startDelay;

    public enum TransformDirection
    {
        upDown,
        leftRight,
        rightLeft,
        downUp

    }

    // Use this for initialization
    void Start()
    {
        if (direction != TransformDirection.leftRight && direction != TransformDirection.upDown && direction != TransformDirection.rightLeft && direction != TransformDirection.downUp)
            throw new UnityException("<color=red>Error: Tranform Direction Error in ElevatorBehavior2D.</color>");
        if (transformSpeed <= 0 || transformLength <= 0)
            throw new UnityException("<color=red>Error: Tranform Speed or Length Must be Greater than Zero.</color>");
        fixedStopTime = transformLength / transformSpeed;
        stopTimer = fixedStopTime;
        binar = 1;
        startDelay = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        stopTimer -= Time.deltaTime;

        if (stopTimer >= 0)
        {
            MovePlatform(binar);
        }
        else {

            if (!startDelay)
                StartCoroutine(DelayTransform());
        }
    }


    private void MovePlatform(int i)
    {
        if (direction == TransformDirection.upDown)
        {
            transform.Translate(i * Vector3.up * (Time.deltaTime * transformSpeed), Space.World);
        }
        if (direction == TransformDirection.rightLeft)
        {
            transform.Translate(i * Vector3.left * (Time.deltaTime * transformSpeed), Space.World);
        }
        if (direction == TransformDirection.downUp)
        {
            transform.Translate(i * Vector3.down * (Time.deltaTime * transformSpeed), Space.World);
        }

        else if (direction == TransformDirection.leftRight)
        {
            transform.Translate(i * Vector3.right * (Time.deltaTime * transformSpeed), Space.World);
        }

    }


    IEnumerator DelayTransform()
    {
        startDelay = true;
        yield return new WaitForSeconds(transformDelay);
        //Debug.Log("Platform Delay");

        stopTimer = fixedStopTime;
        binar = binar * (-1);

        startDelay = false;
        //Debug.Log("startDelay false");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!transform.gameObject.CompareTag("spike"))
        {
            if (other.gameObject.CompareTag("player"))
            {
                parent = other.transform.parent;
                other.transform.parent = transform;
            }
        }

        else
        {
            Debug.Log("spike");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!transform.gameObject.CompareTag("spike"))
        {

            if (other.CompareTag("player"))
            {
                other.transform.parent = null;
            }
        }
        else
        {
            Debug.Log("spike");
        }
    }

}
