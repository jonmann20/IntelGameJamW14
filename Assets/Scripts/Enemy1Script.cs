using UnityEngine;
using System.Collections;

public class Enemy1Script : Enemy {

	void Start()
	{
		float ratio = Random.Range(0.9f, 1.1f);
		print(ratio);
		transform.localScale *= ratio;
	}

	void Update () {
		float ratio = (health / MAX_HEALTH) * 0.5f;
		transform.localScale = new Vector3(ratio, ratio, ratio);
	}
	protected override int getPointValue() { return 10; }
}
