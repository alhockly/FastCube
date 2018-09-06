using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CameraControl : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;


    private const float Y_ANGLE_MIN=0f;
    private const float Y_ANGLE_MAX=100f;
    // Use this for initialization
    void Start() {
        Xangle = 28;
        Yangle = -19;
    }

    public GameObject target;
   
    public float Xsensitivity = 1f, Ysensitivity = 1f;

    public float Xangle = 0f;
    public float Yangle = 0f;
    float x = 0;
    float y = 0;

    public float distance= 10f;

    void Update()
    {


        prevState = state;
        state = GamePad.GetState(playerIndex);


        Xangle += state.ThumbSticks.Right.X;
        Yangle += state.ThumbSticks.Right.Y;

        //Yangle = Mathf.Clamp(Yangle, Y_ANGLE_MIN, Y_ANGLE_MAX);

    }

    void LateUpdate() {

        Vector3 dir = new Vector3(0, 0, distance);

        Quaternion rotation = Quaternion.Euler(Yangle, Xangle, 0);

        transform.position = target.transform.position + rotation * dir;

        transform.LookAt(target.transform.position);
    }
}
