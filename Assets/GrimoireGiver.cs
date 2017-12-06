using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PickUp;
public class GrimoireGiver : MonoBehaviour {


    private IPickUp pickup;
    public GameObject startPosition;
    public GameObject grimoire;

    public void giveGrimoire(int i)
    {
        setPickUp();
        switch (i)
        {
            case 1:
                var obj1 = GameObject.Find("P1").GetComponent<Controller2D>();
                obj1.transform.position = startPosition.transform.position;
                obj1.setPickUp(pickup);
                break;
            case 2:
                var obj2 = GameObject.Find("P2").GetComponent<Controller2D>();
                obj2.transform.position = startPosition.transform.position;
                obj2.setPickUp(pickup);
                break;
            case 3:
                var obj3 = GameObject.Find("P3").GetComponent<Controller2D>();
                obj3.transform.position = startPosition.transform.position;
                obj3.setPickUp(pickup);
                break;
            case 4:
                var obj4 = GameObject.Find("P4").GetComponent<Controller2D>();
                obj4.transform.position = startPosition.transform.position;
                obj4.setPickUp(pickup);
                break;
            default:
                break;
        }
    }

    public void setPickUp()
    {
        pickup = grimoire.GetComponent<IPickUp>();
    }
}
