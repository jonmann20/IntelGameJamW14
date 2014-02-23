using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {
	
	public static int currentLevel = 0;
    public static bool isDemo = false;

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
}
