    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public Text timetext;
    GameObject player;
    public float mins, seconds,milli;

    PlayerControl playercontrolscript;

    string courselastframe;

	// Use this for initialization
	void Start () {
        timetext = GetComponent<Text>();
        player = GameObject.Find("player");
        playercontrolscript = player.GetComponent<PlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!player.GetComponent<PlayerControl>().win)
        {
            mins = (int)(Time.timeSinceLevelLoad / 60f);
            seconds = (Time.timeSinceLevelLoad % 60f);
            milli = Time.timeSinceLevelLoad * 1000;
            timetext.text = mins.ToString("00")+":"+seconds.ToString("00");
            //Debug.Log(seconds);
        }



        if (playercontrolscript.course != courselastframe) {
            ResetTime();
            ResetScore();
        }


        courselastframe = playercontrolscript.course;
	}


    void ResetTime() {
        Debug.Log("RESET: course is" + playercontrolscript.course);
    }

    void ResetScore() {
        playercontrolscript.Score = 0;

    }
}
