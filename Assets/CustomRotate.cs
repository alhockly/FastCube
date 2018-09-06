using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRotate : MonoBehaviour {

    public int turnspeed;
    public string axis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (axis == "y" || axis=="Y") {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * turnspeed);

        }
        if (axis == "x" || axis == "X")
        {
            transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * turnspeed);

        }
        if (axis == "z" || axis == "Z")
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * turnspeed);

        }
        
    }
}
