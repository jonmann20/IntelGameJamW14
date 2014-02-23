using UnityEngine;
using System.Collections;

public class GlobalCheckScript : MonoBehaviour {

	public GameObject GLOBALPrefab;

	void Start () {
		GameObject global = GameObject.Find("GLOBAL") as GameObject;
		if(global == null)
		{
			Instantiate(global);
		}
	}
}
