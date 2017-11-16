using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace interactable
{
    public interface IInteractable
    {

        bool Interact(Controller2D player);
        void Enable();

    }
}
