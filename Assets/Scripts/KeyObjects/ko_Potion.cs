using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ko_Potion : PickUpKeyObject {

    public string colour;
    public GameObject liquid;

    public void Start()
    {
        if (colour != null)
            base.Initialize(colour);
        
    }

    public override void Consume()
    {
        liquid.GetComponent<MeshRenderer>().enabled = false;
    }

}
