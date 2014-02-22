using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public Font font;
	public Texture border;

	public GUIStyle headingStyle = new GUIStyle();
	public GUIStyle textStyle;
	public GUIStyle buttonStyle;
	public GUIStyle subheadingStyle;
	GUIStyle blinkFadeStyle;
	
	public enum TitleState { START, SELECT, INSTRUCTIONS };
	public TitleState titleState = TitleState.START;

	bool fadeOut = true;
	float alphaCounter = 0f;

	TitleState ctaPointer = TitleState.START;
	string[] cta = new string[3]{"Start", "Instructions", "Quit"};

	void Start(){
		headingStyle.normal.textColor = Color.white;
		headingStyle.font = font;
		headingStyle.fontSize = 40;
		headingStyle.alignment = TextAnchor.MiddleCenter;
		headingStyle.wordWrap = true;
		subheadingStyle = new GUIStyle(headingStyle);
		subheadingStyle.fontSize = 18;		
		blinkFadeStyle = new GUIStyle(subheadingStyle);
		textStyle = new GUIStyle(headingStyle);
		textStyle.fontSize = 14;
		textStyle.alignment = TextAnchor.UpperLeft;
		buttonStyle = new GUIStyle(headingStyle);
		buttonStyle.fontSize = 14;
	}

	void Update(){

		switch (titleState) {
			case TitleState.START:
				// blink cta
				if(alphaCounter >= 1.5f){
					fadeOut = true;
				}
				else if(alphaCounter <= 0f){
					fadeOut = false;
				}

				if(Time.frameCount % 1 == 0){
					alphaCounter += fadeOut ? -0.028f : 0.034f;
				}

				blinkFadeStyle.normal.textColor = new Color(255, 255, 255, alphaCounter);

				break;

		}
	}

	void OnGUI() {
		Time.timeScale = 1.0f;
		// GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Resources.Load<Texture>("Images/titleBack"), ScaleMode.ScaleAndCrop);

		switch (titleState) {
			case TitleState.START:

			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 - 50, 1000, 100), "Blood Cell Brigade", headingStyle);

			GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height/2, 400, 100), "Press Enter", blinkFadeStyle);
			
			if(Input.GetButtonUp("Start")){
				titleState = TitleState.SELECT;				
				return;
			}
			
			break;
		case TitleState.SELECT:
			GUI.TextArea(new Rect(Screen.width/2 - 500, Screen.height/4 - 50, 1000, 100), "Blood Cell Brigade", headingStyle);

			string[] c = new string[3];
			cta.CopyTo(c, 0);

			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 - 50, 100, 25), c[0], buttonStyle)) {
				Application.LoadLevel("overworld");
				return;
			}
			if(GUI.Button (new Rect(Screen.width/2 - 100, Screen.height/2, 200, 25), c[1], buttonStyle)) {
				titleState = TitleState.INSTRUCTIONS;
				return;
			}
			if(GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 100, 25), c[2], buttonStyle)) {
				Application.Quit();
				return;
			}
			
			break;
		case TitleState.INSTRUCTIONS:
			if(GUI.Button(new Rect(8, 13, 70, 25), "Back", buttonStyle)){
				titleState = TitleState.SELECT;
				return;
			}
			
			GUI.TextArea(new Rect(Screen.width/2 - 500, 15, 1000, 100), "How to Play", headingStyle);
			
			//Skills
			GUI.TextArea(new Rect(Screen.width/4 - 150, Screen.height/7, 270, 100), "Instructions", subheadingStyle);
			GUI.TextArea(new Rect(Screen.width/4, Screen.height/7 + 82, 700, 300), "We need them", textStyle);
			
			break;
		default:
			
			break;
		}
	}
	
	
}
