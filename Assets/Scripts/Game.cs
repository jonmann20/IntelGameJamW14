using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	const int POINTS_FOR_EXTRA_ENTITY = 50;

    public Font font;
    public Texture border;

    public GUIStyle pauseStyle = new GUIStyle();
    public GUIStyle buttonStyle;

    bool fadeOut = true;
    float alphaCounter = 0f;

    public static Game that;

    public bool paused = false;

    public enum PauseState { SELECT, INSTRUCTIONS };
    public PauseState pauseState;

	public static int points = 0;


    void Awake () {
        that = this;
    }

    void Start(){
        pauseStyle.normal.textColor = Color.white;
        pauseStyle.font = font;
        pauseStyle.fontSize = 40;
        pauseStyle.alignment = TextAnchor.MiddleCenter;
        pauseStyle.wordWrap = true;
        buttonStyle = new GUIStyle(pauseStyle);
        buttonStyle.fontSize = 14;
        buttonStyle.hover.textColor = new Color(255, 0, 0, 1);
    }

	void Update () {
        if (Input.GetButtonUp("Pause") && !GlobalScript.isDemo) {
            togglePause();
        }
        else if (GlobalScript.isDemo) {
            if (Input.GetButtonUp("Start")) {
                Application.LoadLevel("title");
            }
        }
	}

    void OnGUI() {
        if (that.paused) {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), " "); 
            switch (pauseState) {
                case PauseState.SELECT:
                    GUI.TextArea(new Rect(Screen.width / 2 - 500, Screen.height / 4 - 50, 1000, 100), "PAUSED", pauseStyle);

                    if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 25), "Resume", buttonStyle))
                    {
                        togglePause();
                        return;
                    }
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 25), "How to Play", buttonStyle))
                    {
                        pauseState = PauseState.INSTRUCTIONS;
                        return;
                    }
                    if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 25), "Back to Overworld", buttonStyle))
                    {
                        Application.LoadLevel("overworld");
                        return;
                    }
                    if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 100, 100, 25), "Quit", buttonStyle))
                    {
                        Application.LoadLevel("title");
                        return;
                    }
                    break;
            }
        }
        else if (!GlobalScript.isDemo)
        {
            EzGUI.scaleGUI();
            EzGUI.placeTxt("Score: " + points, 55, EzGUI.HALFW, 65);
        }
        else
        {
            EzGUI.scaleGUI();
            EzGUI.blinkTxt("Press <Enter> to Play", 80, EzGUI.HALFW, EzGUI.HALFH);
        }
    }
        
    void togglePause(){
        if (!that.paused) {
            pauseState = PauseState.SELECT;
            Time.timeScale = 0.0f;
        }
        else {
            Time.timeScale = 1.0f;
        }

        that.paused = !that.paused;
    }

	//bool: got over POINTS_FOR_EXTRA_ENTITY points
	public static bool increasePoints(int n)
	{
		points += n;
		if(points >= POINTS_FOR_EXTRA_ENTITY)
		{
			points -= POINTS_FOR_EXTRA_ENTITY;
			return true;
		}
		return false;
	}
}
