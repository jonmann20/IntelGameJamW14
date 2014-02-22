using UnityEngine;
using System.Collections;

public class FinishLineScript : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Player")
				Application.LoadLevel("overworld");
	}
}