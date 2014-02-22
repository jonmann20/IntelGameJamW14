using UnityEngine;
using System.Collections;

public delegate void Callback ();

public class Utils : MonoBehaviour {

	public static Utils that;

	void Awake(){
		that = this;
	}

	/*
		REQUIRES: call with StartCoroutine()
		MODIFIES: tForm
		EFFECTS: moves a transform from its current position to newPos over time
				 callback() is called when movement is completed
	*/
	public static IEnumerator moveToPosition(Transform tForm, Vector3 newPos, float time, Callback callback=null){ 
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;

		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime; 
			
			if(elapsedTime >= time){
				tForm.position = newPos;
				
				if(callback != null) {
					callback();
				}
			}
			
			yield return null;
		}
	}
}
