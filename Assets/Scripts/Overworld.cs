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

	bool sizeDown = false;

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

		Vector3 resize = Vector3.zero;
		if(levelLocations[cur].transform.localScale.x > 1.3f){
			sizeDown = true;
		}
		else if(levelLocations[cur].transform.localScale.x < 1){
			sizeDown = false;
		}

		float s = 0.8f * Time.deltaTime;

		if(sizeDown){
			resize = new Vector3(-s, -s, 0);
		}
		else {
			resize = new Vector3(s, s, 0);
		}

		levelLocations[cur].transform.localScale += resize; //Vector3.Lerp(levelLocations[cur].transform.localScale, levelLocations[cur].transform.localScale + resize, 1f);
	}

	void OnGUI(){
		EzGUI.scaleGUI();

		EzGUI.placeTxt("Click to select", 47, EzGUI.FULLW - 210, 80);
		EzGUI.placeTxt("a level:", 47, EzGUI.FULLW - 210, 180);
		EzGUI.placeTxt("Level " + (cur + 1) + " selected", 60, 480, 165);
		EzGUI.blinkTxt("Press Enter", 47, 480, EzGUI.HALFH);
	}

	void updateMap(bool isRight){
		if(isRight && cur < max){	// right
			levelLocations[cur].transform.localScale = new Vector3(1, 1, 1);
			levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
			levelLocations[++cur].GetComponent<SpriteRenderer>().color = highlight;
			isPressed = true;
		}
		else if(!isRight && cur > 0){			// left
			levelLocations[cur].transform.localScale = new Vector3(1, 1, 1);
			levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
			levelLocations[--cur].GetComponent<SpriteRenderer>().color = highlight;
			isPressed = true;
		}
	}

	public void updateMapByNum(int n){
		levelLocations[cur].transform.localScale = new Vector3(1, 1, 1);
		levelLocations[cur].GetComponent<SpriteRenderer>().color = Color.white;
		levelLocations[n].GetComponent<SpriteRenderer>().color = highlight;

		cur = n;
		GlobalScript.currentLevel = n + 1;
	}
}
