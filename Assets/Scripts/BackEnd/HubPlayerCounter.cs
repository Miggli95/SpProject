using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPlayerCounter : MonoBehaviour {
    public List<GameObject> Particles;
    private TextMesh texty;
    int i = 0;
    private GameManager manager;
    // Use this for initialization
    void Start () {
        texty = this.GetComponent<TextMesh>();
        manager = GameObject.Find("UI Camera").GetComponent<GameManager>();
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
        texty.text = "Start game" + "\n" + "Players: " + i + " / " + manager.connectedControllers;
        if(i == manager.connectedControllers)
        {
            switch (this.name)
            {
                case "Level1":
                    SceneManager.LoadScene("Level8");
                    break;
                case "Level2":
                    SceneManager.LoadScene("level9");
                    break;
                case "Level3":
                    SceneManager.LoadScene("Level7(BigxSmaller");
                    break;
            }
        
        }


    }
}
