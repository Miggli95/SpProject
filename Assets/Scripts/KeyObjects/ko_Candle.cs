using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ko_Candle : PickUpKeyObject {


    public void Start()
    {
        Initialize("Candle");
    }

    private new void Initialize(string id)
    {
        base.Initialize(id);
    }

}
