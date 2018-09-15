using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    GameObject player;
    public int range;
    public int moveSpeed;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
      
        
        if(Vector3.Distance(player.transform.position, transform.position) < range && !player.GetComponent<PlayerControl>().paused) {
            print("player in range");

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed*Time.deltaTime);
        }

		
	}
}
