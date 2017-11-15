using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PickUp
{
    public interface IPickUp
    {


        IPickUp PickUp();
        bool Use();
        void Drop();
        string getID();

    }
}
