using UnityEngine;
using System.Collections;

public class GlobalCheckScript : MonoBehaviour {

	public GameObject GLOBALPrefab;

	void Start () {
		GLOBALPrefab = Resources.Load("GLOBAL") as GameObject;
		GameObject global = GameObject.FindWithTag("Global") as GameObject;

		if(global == null)
		{
			Instantiate(GLOBALPrefab);
		}
	}
}
