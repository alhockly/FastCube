using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupscript : MonoBehaviour {

    PlayerControl playerscript;
    Renderer renderer;
    public bool active;
    public Material notactive;

    


// Use this for initialization
void Start () {
        renderer = GetComponent<Renderer>();
        playerscript = GameObject.Find("player").GetComponent<PlayerControl>();
        notactive = Resources.Load("Material/grey.mat", typeof(Material)) as Material;
        active = false;
        //renderer.material = notactive;
    }
	
	// Update is called once per frame
	void Update () {
        char courseparent = transform.name[transform.name.Length - 1];

        if (playerscript.course == "") {
            active = false;
            return;
            //normal look
        }

        //Debug.Log("if "+playerscript.courdse+" contains "+courseparent.ToString());

        
        if (playerscript.course.Contains(courseparent.ToString()))
        {
            active = true;
       
            renderer.material.color = new Color(1f, 1f, 1f,1f);
            ///be bright

        }
        else {
            active = false;
            renderer.material.color = new Color(1f,0.1f,0.25f,0.2f);
            //be dim

        }
    }


    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.name == "player" && active==true) {

            playerscript.Pickup(this.gameObject);

        }
    }
}
