using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Overworld : MonoBehaviour {

	public static Overworld that;

	public List<GameObject> levelLocations;
	int cur = 0;
	int max = 0;

	bool isPressed = false;
	Color highlight = Color.green;

	void Awake(){
		that = this;
		max = levelLocations.Count - 1;

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
			Application.LoadLevel("stage1");
		}

		Vector3 resize = Vector3.zero;
		if(levelLocations[cur].transform.localScale.x < 2){
			resize = new Vector3(1 * Time.deltaTime, 1* Time.deltaTime, 0);
		}
		else if(levelLocations[cur].transform.localScale.x > 1){
			resize = new Vector3(-1* Time.deltaTime, -1* Time.deltaTime, 0);
		}

		levelLocations[cur].transform.localScale += resize; //Vector3.Lerp(levelLocations[cur].transform.localScale, levelLocations[cur].transform.localScale + resize, 1f);
	}

	void OnGUI(){
		EzGUI.scaleGUI();

		EzGUI.placeTxt("Select a level.", 57, 520, 75);
		EzGUI.placeTxt("Level " + (cur + 1), 57, 520, 175);
		EzGUI.blinkTxt("Press Enter", 48, 520, EzGUI.HALFH);
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
	}
}
