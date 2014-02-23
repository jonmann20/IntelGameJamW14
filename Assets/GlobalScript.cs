using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {
	
	public static int currentLevel = 0;
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
}
