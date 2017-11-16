using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardLevelGoal : MonoBehaviour {

    public int complexity;
    public StaticKeyObject[] keyObjects;
    private int currentposition = 0;

    public void Start()
    {
        complexity = keyObjects.Length;
    }

    private void Update()
    {
        checkComplete(); //doing a check each frame if keyobject at current position is complete
    }

    private void checkComplete()
    {
        StaticKeyObject key = keyObjects[currentposition];
        if (key.getisComplete())
        {
            if(currentposition == keyObjects.Length) //is object at the end of the level goal array?
            {
                //Sacrifice player that completed the goal. Who sacrifices? StandardLevelGoal, StaticKeyObject? Does standardlevelgoal tell statickeyobject to sacrifice player?
                //award points based on complexity(?) and position
                if(key.getInteractingPlayer() != null)
                {
                    Controller2D player = key.getInteractingPlayer();
                    //sacrifice interacting player and inform game manager about player and score
                }
            } else
            {
                currentposition++;
                keyObjects[currentposition].Enable();
            }
            //enable next object and increment currentposition by 1
        }
    }




}
