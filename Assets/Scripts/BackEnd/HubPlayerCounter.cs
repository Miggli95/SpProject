using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlayerCounter : MonoBehaviour {
    public List<GameObject> Particles;
    private TextMesh texty;
    int i = 0;
    // Use this for initialization
    void Start () {
        texty = this.GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        i = 0;
		foreach(GameObject p in Particles)
        {
            if (p.activeSelf)
            {
                i++;
            }
        }
        print(i);
        texty.text = "Start game" + "\n" + "Players: " + i + " / 4";
        if(i == 4)
        {

        }


    }
}
