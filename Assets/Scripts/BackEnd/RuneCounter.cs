using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RuneCounter : MonoBehaviour {
    public Timer score;
    public bool small = true;
    private float timerino = 0f;
    // Use this for initialization
    void Start () {
        score = GameObject.Find("UI Camera").GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timerino > 0)
        {
            timerino -= Time.deltaTime;
            if(timerino <= 0)
            {
                this.transform.parent.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                this.GetComponent<MeshRenderer>().enabled = true;
                this.GetComponent<SphereCollider>().enabled = true;
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetType() == typeof(CharacterController) && small)
        {

            score.runeGet(other.gameObject.name,1);
            other.gameObject.GetComponent<Controller2D>().sizeUp();
            
           Destroy(this.transform.parent.gameObject);
        }
        else if (other.GetType() == typeof(CharacterController)){
            score.runeGet(other.gameObject.name,3);
            other.gameObject.GetComponent<Controller2D>().sizeUp();
            other.gameObject.GetComponent<Controller2D>().sizeUp();
            other.gameObject.GetComponent<Controller2D>().sizeUp();
            this.transform.parent.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<SphereCollider>().enabled = false;
            timerino = Random.Range(3f, 7f);
        }
    }
}
