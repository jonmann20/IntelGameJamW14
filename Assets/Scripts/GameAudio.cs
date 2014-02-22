using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	GameObject audioHolder, _bgMusic;
	static GameObject bgMusic;

	void Awake(){
		audioHolder = new GameObject("AudioHolder");

		//setSound(ref _bgMusic, bgMusic, "bgMusic");
	}

	void setSound(ref GameObject holder, ref AudioSource src, string clip){
		holder = new GameObject(clip);
		holder.transform.parent = audioHolder.transform;
		
		src = holder.AddComponent<AudioSource>();
		src.playOnAwake = false;
		src.clip = Resources.Load<AudioClip>("Audio/" + clip);
	}
	
	public static void play(string clip){
		switch(clip) {
			case "bgMusic":
				bgMusic.audio.Play();
				break;
		}
	}
}
