using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Overworld : MonoBehaviour {

	public static Overworld that;

	public List<GameObject> levelLocations;
	public int cur = 0;
	int max = 0;

	bool isPressed = false;
	Color highlight = Color.green;

	void Awake(){
		that = this;
		max = levelLocations.Count - 1;

		GlobalScript.currentLevel = 1;
		levelLocations[cur].GetComponent<SpriteRenderer>().color = highlight;
	}

	void Update(){
		float hor = Input.GetAxisRaw("Horizontal");

		if(!isPressed && hor != 0){
			updateMap(hor > 0);
		}

		if(hor == 0){
			isPressed = false;
		}

		if(Input.GetButtonDown("Start")){
			string stageName = "stage" + GlobalScript.currentLevel.ToString();
			Application.LoadLevel(stageName);
		}
	}

	void OnGUI(){
		EzGUI.scaleGUI();

		EzGUI.placeTxt("Select a level.", 57, 220, 75);
		EzGUI.placeTxt("Level " + (cur + 1), 57, 220, 175);
		EzGUI.blinkTxt("Press Enter", 48, 220, EzGUI.HALFH);
	}

	void updateMap(bool isRight){
		if(isRight && cur < max){	// right
			levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
			levelLocations[++cur].GetComponent<SpriteRenderer>().color = highlight;
			isPressed = true;
		}
		else if(!isRight && cur > 0){			// left
			levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
			levelLocations[--cur].GetComponent<SpriteRenderer>().color = highlight;
			isPressed = true;
		}
	}

	public void updateMapByNum(int n){
		levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
		levelLocations[n].GetComponent<SpriteRenderer>().color = highlight;

		cur = n;
		GlobalScript.currentLevel = n + 1;
	}
}
