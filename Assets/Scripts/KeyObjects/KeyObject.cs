using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour {

    protected string id;
    private bool isEnabled;
    public bool IsEnabled {
        get
        {
            return isEnabled;
        }
        set
        {
            isEnabled = value;
        }

    }

    public KeyObject(string id)
    {
        this.id = id;
    }
}
