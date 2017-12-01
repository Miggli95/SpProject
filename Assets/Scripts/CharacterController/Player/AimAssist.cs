using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{

    public float angle;
    public Quaternion rotation;
    public GameObject aim;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aim == null)
        {
            aim = GetComponent<Ghost>().mortarAim;
        }

        Vector3 aimPos = Camera.main.WorldToScreenPoint(aim.transform.position);
        Vector3 origin = Camera.main.WorldToScreenPoint(transform.position);//kanske vara parent transform
        aimPos.x = aimPos.x - origin.x;
        aimPos.y = aimPos.y - origin.y;

        float angle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        this.angle = angle;
        rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
