using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour {
    GameObject player;
    int winscore = 3;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
    }
	
	// Update is called once per frame
	void Update () {

        PlayerControl playercontrol = player.GetComponent<PlayerControl>();

        if (playercontrol.Score ==winscore) {
            playercontrol.win = true;
        }
	}
}
