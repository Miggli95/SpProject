using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PickUp
{
    public interface IPickUp
    {


        IPickUp PickUp();
        IPickUp PickUp(Controller2D player);
        bool Use(Controller2D player);
        void Drop();
        string getID();
        void Outline();
        void removeOutline();
        void updatePos(Vector3 pos);
        void Consume();
        void KnockAway(Vector3 dir);
    }
}
