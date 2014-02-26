using UnityEngine;
using System.Collections;

public class Enemy2Script : Enemy {

	int moveTimer = 60;
	Vector2 hitPoint = new Vector2(0, 0);

	void Start()
	{

	}

	void Update () {
		float ratio = (health / MAX_HEALTH) * 0.5f;
		transform.localScale = new Vector3(ratio, ratio, ratio);
		/*
		moveTimer --;
		if(moveTimer <= 0)
		{
			print("line");
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 10);
			hitPoint = hit.point;
			StartCoroutine(Utils.moveToPosition(transform, hitPoint, 2.0f));
			moveTimer = 60;
		}*/

	}

	protected override int getPointValue() { return 5; }
}
