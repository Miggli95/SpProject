using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardLevelGoal : MonoBehaviour {

    public int complexity;
    public StaticKeyObject[] keyObjects;
    private int currentposition = 0;

    private void Update()
    {
        checkComplete(); //doing a check each frame if keyobject at current position is complete
    }

    private void checkComplete()
    {
        if (keyObjects[currentposition].getisComplete())
        {
            if(currentposition == keyObjects.Length) //is object at the end of the level goal array?
            {
                //Sacrifice player that completed the goal.
                //award points based on complexity(?) and position
            }
            //enable next object and increment currentposition by 1
        }
    }




}
