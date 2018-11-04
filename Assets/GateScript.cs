using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

    public string StageName;
    Renderer coliderrender;
    PlayerControl playerscript;
    GameObject Gate;
    TextMesh title;
    public int Stagesize;
    public bool completed;
    TimerScript timer;

    
   // Use this for initialization
    void Start () {
        timer = GameObject.Find("Timer").GetComponent<TimerScript>();
        StageName = transform.parent.name;
        completed = false;

        Gate = transform.parent.gameObject;
        title = Gate.transform.Find("gateTitle").gameObject.GetComponent<TextMesh>();
        title.text = StageName;

        
        coliderrender = GetComponent<Renderer>();
        Debug.Log(transform.parent.name);
       
        playerscript = GameObject.Find("player").GetComponent<PlayerControl>();

        GameObject[] pickups = GameObject.FindGameObjectsWithTag("collectable");

        Stagesize = 0;
        foreach(GameObject item in pickups) {
            char lastchar = item.transform.name.ToString()[item.transform.name.ToString().Length - 1];
            if (lastchar.ToString() == StageName[StageName.Length-1].ToString()) {
                Stagesize++;
            }

        }



    }
	
	// Update is called once per frame
	void Update () {
        coliderrender.enabled = false;
        if (completed == true) { return; }

        if (playerscript.course == StageName && playerscript.Score==Stagesize) {
            completed = true;
            title.text = timer.timetext.text;
            playerscript.completedStages.Add(StageName);
            playerscript.stageCompleteFeeback();
        }

		
	}



    void OnTriggerEnter(Collider collision) {

        playerscript.course = StageName;


        Debug.Log("I got collided with"+StageName);

    }
}
