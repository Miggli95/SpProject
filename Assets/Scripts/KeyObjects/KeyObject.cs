using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour {

    protected string id;
    /*private bool isEnabled;       Does keyobjects that can be picked up need to be turned on/off=
    public bool IsEnabled {     
        get
        {
            return isEnabled;
        }
        set
        {
            isEnabled = value;
        }

    }*/

    protected void innitialize(string id)
    {
        this.id = id;
    }
}
