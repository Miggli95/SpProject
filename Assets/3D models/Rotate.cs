using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public int z;
    public int x;
    public int y;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}
