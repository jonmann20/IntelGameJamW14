﻿using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

    int demoTime = 45;
    int numberOfLvl = 4;

    float startTime;

	public GameObject bg;
	public GameObject bg2;
	public GameObject characters;


	enum TitleState { START, SELECT, INSTRUCTIONS };
	TitleState titleState = TitleState.START;

    void Start()
    {
        Time.timeScale = 1.0f;
        startTime = Time.time;
    }

    void Update()
    {
        switch (titleState)
        {
            case TitleState.START:
                if (Time.time - startTime >= demoTime)
                {
                    GlobalScript.isDemo = true;
                    GlobalScript.currentLevel = (int)(Random.value * numberOfLvl) + 1;
                    Application.LoadLevel("stage" + GlobalScript.currentLevel);
                }
                break;
        }
    }

	void OnGUI(){
		EzGUI.scaleGUI();

		switch(titleState){
			case TitleState.START:
				bg.GetComponent<SpriteRenderer>().enabled = true;
				bg2.GetComponent<SpriteRenderer>().enabled = false;
				characters.SetActive(false);

				EzGUI.placeTxt("Blood Cell Brigade", 60, EzGUI.HALFW - 400, EzGUI.HALFH + 80);
				EzGUI.blinkTxt("Press Start", 45, EzGUI.HALFW - 400, EzGUI.HALFH - 10);

				if(Input.GetButtonDown("Start")){
					titleState = TitleState.SELECT;
				}
				break;
			case TitleState.SELECT:
				bg.GetComponent<SpriteRenderer>().enabled = false;
				bg2.GetComponent<SpriteRenderer>().enabled = true;
				characters.SetActive(false);

				EzGUI.placeTxt("by Nick Dedenbach, Nickie Gregor, Helen Lai,", 45, EzGUI.HALFW + 350, EzGUI.HALFH + 450);
				EzGUI.placeTxt("Sarah Ovresat, Jon Wiedmann, and Austin Yarger", 45, EzGUI.HALFW + 350, EzGUI.HALFH + 500);
				EzGUI.placeTxt("'Aquatic Ambiance' composed by David Wise for Donkey Kong Country (SNES). Rights Nintendo.", 20, EzGUI.HALFW + 350, EzGUI.HALFH + 520);
				
				EzGUI.placeTxt("Blood Cell Brigade", 70, EzGUI.FULLW - 550, 110);
				if(EzGUI.placeBtn("Start Game", 55, EzGUI.FULLW - 550, 230)){
					Application.LoadLevel("overworld");
					return;
				}

				if(EzGUI.placeBtn("Instructions", 55, EzGUI.FULLW - 550, 320)){
					titleState = TitleState.INSTRUCTIONS;
					return;
				}

				if(EzGUI.placeBtn("Quit", 55, EzGUI.FULLW - 550, 410)){
					Application.Quit();
				}

				break;
			case TitleState.INSTRUCTIONS:
				bg.GetComponent<SpriteRenderer>().enabled = false;
				bg2.GetComponent<SpriteRenderer>().enabled = false;
				characters.SetActive(true);

				if(EzGUI.placeBtn("Back", 50, 150, 90)){
					titleState = TitleState.SELECT;
				}
				
				EzGUI.placeTxt("How to Play", 55, EzGUI.HALFW, 200);
				EzGUI.placeTxt("Goal: kill all the viruses and bacteria and get to the goal", 50, 650, EzGUI.HALFH - 200);
				EzGUI.placeTxt("Move: Left Click", 50, 350, EzGUI.HALFH - 100);
				EzGUI.placeTxt("Shoot antibodies: Right Click", 50, 490, EzGUI.HALFH);

				
				EzGUI.placeTxt("White blood cell", 48, 220, EzGUI.FULLH - 40);
				EzGUI.placeTxt("Antibody", 48, 570, EzGUI.FULLH - 40);
				EzGUI.placeTxt("T4 Bacteriophage", 48, 920, EzGUI.FULLH - 40);
				EzGUI.placeTxt("Hepatitis", 48, 1270, EzGUI.FULLH - 40);
				EzGUI.placeTxt("Cancer cell", 48, 1680, EzGUI.FULLH - 40);
				
				break;
		}
	}
}
