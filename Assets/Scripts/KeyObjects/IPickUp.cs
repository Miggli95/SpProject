using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PickUp
{
    public interface IPickUp
    {

        IPickUp PickUp();
        void Use();
        void Drop();

    }
}
