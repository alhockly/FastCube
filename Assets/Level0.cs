using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour {
    GameObject player;
    int numstages = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
    }
	
	// Update is called once per frame
	void Update () {

        PlayerControl playercontrol = player.GetComponent<PlayerControl>();
        Debug.Log("cout" + playercontrol.completedStages.Count);
        if (playercontrol.completedStages.Count==numstages) {
            playercontrol.win = true;
        }
	}
}
