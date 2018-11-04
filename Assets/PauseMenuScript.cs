using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    Text Item0, Item1, Item2, Item3;

    int pointer;
    // Use this for initialization
    void Start () {
        pointer = 0;
        Item0 = GameObject.Find("Item0").GetComponent<Text>();
        Item1 = GameObject.Find("Item1").GetComponent<Text>();
        Item2 = GameObject.Find("Item2").GetComponent<Text>();
        Item3 = GameObject.Find("Item3").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

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



        if (state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released)
        {
            pointer++;
        }
        else {
            if (state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released) {
                pointer--;
            }
        }

        if (pointer < 0) { pointer = 0; }
        if (pointer > 3) { pointer = 3; }

        Selectmenu(pointer);


        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released) {
            PressMenuItem(pointer);
        }


    }

    void PressMenuItem(int pointer) {
        switch (pointer) {
            
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;

            default:
                return;
        }

    }

    void Selectmenu(int pointer) {

        switch (pointer) {

            case 0:
                Item0.fontSize = 170;
                Item1.fontSize = 100;
                Item2.fontSize = 100;
                Item3.fontSize = 100;

                Item0.color= Color.HSVToRGB(0.1f,1f,0.94f);
                Item1.color = Color.black;
                Item2.color = Color.black;
                Item3.color = Color.black;
                return;
            case 1:
                Item0.fontSize = 100;
                Item1.fontSize = 170;
                Item2.fontSize = 100;
                Item3.fontSize = 100;

                Item0.color = Color.black;
                Item1.color = Color.HSVToRGB(0.1f, 1f, 0.94f);
                Item2.color = Color.black;
                Item3.color = Color.black;
                return;
            case 2:
                Item0.fontSize = 100;
                Item1.fontSize = 100;
                Item2.fontSize = 170;
                Item3.fontSize = 100;

                Item0.color = Color.black;
                Item1.color = Color.black;
                Item2.color = Color.HSVToRGB(0.1f, 1f, 0.94f);
                Item3.color = Color.black;
                return;
            case 3:
                Item0.fontSize = 100;
                Item1.fontSize = 100;
                Item2.fontSize = 100;
                Item3.fontSize = 170;

                Item0.color = Color.black;
                Item1.color = Color.black;
                Item2.color = Color.black;
                Item3.color = Color.HSVToRGB(0.1f, 1f, 0.94f);
                return;

            default:
                return;
        }

    }
}
