using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public float TurnSpeed;
    public float MoveSpeed;
    public bool Canjump;
    public float jumpPower;
    int i = 0;
    public bool ignoreSpin = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float lastJump;

    Rigidbody rb;
    public Transform camera;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        
        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;


        }
        else if (rb.velocity.y > 0 && state.Buttons.A == ButtonState.Released)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }



        if (!ignoreSpin)
        {
            // Make the current object turn
            //transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime * TurnSpeed, 0.0f);
        }

        //transform.position += transform.forward * Time.deltaTime * state.ThumbSticks.Left.Y*MoveSpeed;
        Vector3 camF = camera.forward;
        Vector3 camR = camera.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
   
        transform.position += (camF * state.ThumbSticks.Left.Y + camR * state.ThumbSticks.Left.X) * Time.deltaTime * MoveSpeed;
        /*
        Vector3 newpos = (camF * state.ThumbSticks.Left.Y + camR * state.ThumbSticks.Left.X) * Time.deltaTime * MoveSpeed;

        float distTemp = Vector3.Distance(newpos, transform.position);
        if (distTemp < lastJump) {
            lastJump = distTemp;
            //closer
            transform.position += newpos * Time.deltaTime * (MoveSpeed / 2);

        }
        else if (distTemp > lastJump) {
            lastJump = distTemp;
            //further
            transform.position += newpos * Time.deltaTime * MoveSpeed / 2;
        }
        */


        /*
        if (Canjump)
        {
            MoveSpeed = 40;
        }
        else {
            MoveSpeed = 5;
        }
        */
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "standable") {
            Canjump = true;
            
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "standable") {
            Canjump = false;
        }

    }


    void Jump() {

        if (Canjump)
        {
            
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
        else {
            Debug.Log("rotatin");
            StartCoroutine(Rotate(0.5f));
        }
        

    }

    IEnumerator Rotate(float duration)
    {
        ignoreSpin = true;
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, transform.right); //or transform.right if you want it to be locally based
            yield return null;
        }
        transform.rotation = startRot;
        ignoreSpin = false;
    }
}
