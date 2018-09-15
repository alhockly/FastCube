using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    public bool living;
    public int Score;
    public int boostForce;
    public bool canSpin;
    Rigidbody rb;
    public Transform camera;
    public float spincooldowntime;
    public bool isattacking;
    public float attackSpinTime;
    public bool win;
    public bool paused;

    public AudioSource audioSource;
    public AudioClip Jumpsound;
    public AudioClip Pickupsound;
    public AudioClip Spinsound;
    public AudioClip Deathsound;

    GameObject Wintext;
    Text ScoreText;
    GameObject pausemenu;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        living = true;
        canSpin = true;
        Score = 0;
        win = false;
        Wintext = GameObject.Find("WinText");
        Wintext.SetActive(false);
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        pausemenu = GameObject.Find("PauseMenu");
        audioSource = GetComponent<AudioSource>();
        
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
         if (!win)                                              
        {
            ScoreText.text = Score.ToString();

            if (!paused)
            {
                Time.timeScale = 1f;
                pausemenu.SetActive(false);
                // Detect if a button was pressed this frame
                if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
                {
                    Jump();
                }

                if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed && canSpin)
                {
                    audioSource.clip = Spinsound;
                    audioSource.Play();
                    StartCoroutine(attacking(attackSpinTime));
                    canSpin = false;
                    StartCoroutine(Rotate(attackSpinTime, new Vector3(0, 1, 0)));
                    StartCoroutine(spinCooldown(spincooldowntime));

                }

                if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed)
                {

                    paused = true;
                }

                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;


                }
                else if (rb.velocity.y > 0 && state.Buttons.A == ButtonState.Released)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }

                if (!living)
                {
                    audioSource.clip = Deathsound;
                    audioSource.Play();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    
                }



                //transform.position += transform.forward * Time.deltaTime * state.ThumbSticks.Left.Y*MoveSpeed;
                Vector3 camF = camera.forward;
                Vector3 camR = camera.right;
                camF.y = 0;
                camR.y = 0;
                camF = camF.normalized;
                camR = camR.normalized;
                transform.position += (camF * state.ThumbSticks.Left.Y + camR * state.ThumbSticks.Left.X) * Time.deltaTime * MoveSpeed;

            }
            else {              //not won and paused
                Time.timeScale = 0;
                if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed)
                {

                    paused = false;
                }

                pausemenu.SetActive(true);



            }

        }
        else {
            ///YOU WIN!
            Wintext.SetActive(true);
            
            Time.timeScale = 0.5f;

            Vector3 camF = camera.forward;
            Vector3 camR = camera.right;
            camF.y = 0;
            camR.y = 0;
            camF = camF.normalized;
            camR = camR.normalized;
            transform.position += (camF * state.ThumbSticks.Left.Y + camR * state.ThumbSticks.Left.X) * Time.deltaTime * MoveSpeed;

            if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed)
            {

                LoadnextLevel();
            }

        }
       
         
    }

    public int GetPlayerScore() {
        return Score;
        }

    void LoadnextLevel() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "standable") {
            Canjump = true;
            
        }

        if (collision.gameObject.tag == "death") {
            
                living = false;
         
            
        }

        if (collision.gameObject.tag == "enemy")
        {
            if (!isattacking)
            {
                living = false;
            }
            else
            {
                Destroy(collision.gameObject);
                Score++;
            }

        }


        if (collision.gameObject.tag == "collectable") {
            Destroy(collision.gameObject);
            Score++;
            audioSource.clip = Pickupsound;
            audioSource.Play();

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
            audioSource.clip = Jumpsound;
            audioSource.Play();
        }
        else {
            Debug.Log("rotatin");
            StartCoroutine(Rotate(0.5f,new Vector3(0,0,1)));
        }
        

    }

    IEnumerator Rotate(float duration,Vector3 dir)
    {
        ignoreSpin = true;
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, dir); //or transform.right if you want it to be locally based
            yield return null;
        }
        transform.rotation = Quaternion.identity;
        ignoreSpin = false;
    }


    IEnumerator attacking(float duration)
    {
        isattacking = true;
        yield return new WaitForSecondsRealtime(duration);
        isattacking = false;
    }
    IEnumerator spinCooldown(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        canSpin = true;
    }

}
