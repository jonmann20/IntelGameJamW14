using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Overworld : MonoBehaviour {

	public static Overworld that;

	public List<GameObject> levelLocations;
	int cur = 0;
	int max = 0;

	bool isPressed = false;

	void Awake(){
		that = this;
		max = levelLocations.Count - 1;

		levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.red;
	}

	void Update(){
		float hor = Input.GetAxisRaw("Horizontal");

		if(!isPressed && hor != 0){
			updateMap(hor > 0);
		}

		if(hor == 0){
			isPressed = false;
		}
	}

	void OnGUI(){
		EzGUI.scaleGUI();

		EzGUI.placeTxt("Level " + (cur + 1) + " selected", 57, EzGUI.FULLW - 220, 75);
		EzGUI.blinkTxt("Press Enter", 48, EzGUI.FULLW - 220, EzGUI.HALFH);
	}

	void updateMap(bool isRight){
		if(isRight && cur < max){	// right
			levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
			levelLocations[++cur].GetComponent<SpriteRenderer>().color = Color.red;
			isPressed = true;
		}
		else if(!isRight && cur > 0){			// left
			levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
			levelLocations[--cur].GetComponent<SpriteRenderer>().color = Color.red;
			isPressed = true;
		}
	}

	public void updateMapByNum(int n){
		levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
		levelLocations[n].GetComponent<SpriteRenderer>().color = Color.red;

		cur = n;
	}
}
