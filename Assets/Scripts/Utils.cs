using UnityEngine;
using System.Collections;

public delegate void Callback ();

public class Utils : MonoBehaviour {

	public static Utils that;

	void Awake(){
		that = this;
	}

	public static IEnumerator moveToPosition(Transform tForm, Vector3 newPos, float time, Callback callback){
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
