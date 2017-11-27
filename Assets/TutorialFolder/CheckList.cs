using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour {

   
    List<GameObject> checkBox = new List<GameObject>();
    GameObject Menu;
    //CheckBox[] checkBox;
    //Objective[] objectives;
    // Use this for initialization
    void Start()
    {
        //CheckList P1
        Menu = GameObject.FindGameObjectWithTag("CheckList " + gameObject.name);
        for (int i = 0; i < Menu.transform.childCount; i++)
        {
            if (Menu.transform.GetChild(i).gameObject.CompareTag("CheckBox"))
            {
                checkBox.Add(Menu.transform.GetChild(i).gameObject);
            }
        }

        for  (int i = 0; i< checkBox.Count; i++)
        {
            checkBox[i].transform.GetChild(0).gameObject.SetActive(false);
            checkBox[i].GetComponent<CheckBox>().objective.playerName = gameObject.name;
            checkBox[i].GetComponent<CheckBox>().objective.id = i;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (GameObject g in checkBox)
        {
            g.transform.GetChild(0).gameObject.SetActive(g.GetComponent<CheckBox>().objective.done);
            //print("checkboxUpdate" + checkBox.Length);
        }
    }

    public Objective getObjective(int i)
    {
        return checkBox[i].GetComponent<CheckBox>().objective;
    }
}
