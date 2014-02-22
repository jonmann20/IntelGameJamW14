using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static Game that;

    public bool paused = false;

    void Awake () {
        that = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause")) {
            togglePause();
        }
        //Debug.Log(Time.timeScale);
	}

        
    void togglePause(){
        if (!that.paused) {
            Time.timeScale = 0.0f;
        }
        else {
            Time.timeScale = 1.0f;
        }

        that.paused = !that.paused;
    }
}
