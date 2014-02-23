using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

    int demoTime = 5;
    int numberOfLvl = 4;

    float startTime;

	public GameObject bg;

	enum TitleState { START, SELECT, INSTRUCTIONS };
	TitleState titleState = TitleState.START;

    void Update(){
        if (Time.time - startTime >= demoTime)
        {
            SuperGlobal.isDemo = true;
            Application.LoadLevel("stage5");
            //Application.LoadLevel("stage" + (int)(Random.value * numberOfLvl));
        }
    }

	void OnGUI(){
		EzGUI.scaleGUI();

		switch(titleState){
			case TitleState.START:
				bg.GetComponent<SpriteRenderer>().enabled = true;

				EzGUI.placeTxt("Blood Cell Brigade", 70, EzGUI.FULLW - 450, 290);
				EzGUI.blinkTxt("Press Start", 55, EzGUI.FULLW - 440, EzGUI.FULLH - 90);
				
				if(Input.GetButtonDown("Start")){
					titleState = TitleState.SELECT;
				}
				break;
			case TitleState.SELECT:
				bg.GetComponent<SpriteRenderer>().enabled = false;


				EzGUI.placeTxt("Blood Cell Brigade", 70, EzGUI.FULLW - 450, 290);
				if(EzGUI.placeBtn("Start Game", 55, EzGUI.FULLW - 390, EzGUI.HALFH)){
					Application.LoadLevel("overworld");
					return;
				}

				if(EzGUI.placeBtn("Instructions", 55, EzGUI.FULLW - 390, EzGUI.HALFH + 70)){
					titleState = TitleState.INSTRUCTIONS;
					return;
				}

				if(EzGUI.placeBtn("Quit", 55, EzGUI.FULLW - 390, EzGUI.HALFH + 140)){
					Application.Quit();
				}

				break;
			case TitleState.INSTRUCTIONS:
				bg.GetComponent<SpriteRenderer>().enabled = false;

				if(EzGUI.placeBtn("Back", 50, 150, 90)){
					titleState = TitleState.SELECT;
				}
				
				EzGUI.placeTxt("How to Play", 55, EzGUI.HALFW, 200);
				EzGUI.placeTxt("Goal: kill all the viruses and bacteria and get to the goal", 50, 650, EzGUI.HALFH - 200);
				EzGUI.placeTxt("Move: Left Click", 50, 350, EzGUI.HALFH);
				EzGUI.placeTxt("Shoot antibodies: Right Click", 50, 490, EzGUI.HALFH + 80);
				
				break;
		}
	}
}
