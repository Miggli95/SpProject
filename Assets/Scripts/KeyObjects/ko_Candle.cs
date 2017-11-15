using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ko_Candle : PickUpKeyObject {


    public void Start()
    {
        innitialize("Candle");
    }

    public override bool Use()
    {
        return false;
    }

    private new void innitialize(string id)
    {
        base.innitialize(id);
    }

}
