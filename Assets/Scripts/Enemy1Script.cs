using UnityEngine;
using System.Collections;

public class Enemy1Script : Enemy {

	void Start()
	{
	
	}

	void Update () {
		float ratio = (health / MAX_HEALTH) * 0.5f;
		transform.localScale = new Vector3(ratio, ratio, ratio);
	}
}
