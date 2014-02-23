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
		if(coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Antibody" || coll.gameObject.tag == "Decor")
			Destroy(coll.gameObject);
	}
}