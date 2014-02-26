using UnityEngine;
using System.Collections;

public class RedBloodCellScript : MonoBehaviour {

	int life = 360;
	Vector2 previousVel = new Vector2(0, 0);
	void Start () {
		float ratio = Random.Range(0.9f, 1.1f);
		print(ratio);
		transform.localScale *= ratio;
	}

	void Update () {
		if((rigidbody2D.velocity - previousVel).magnitude < 0.1f)
			life --;
		else
			life = 500;

		if(life <= 0)
			Destroy(gameObject);

		previousVel = rigidbody2D.velocity;
	}
}
