using UnityEngine;
using System.Collections;

public class RedBloodCellSprite : MonoBehaviour {

	int life = 360;
	Vector2 previousVel = new Vector2(0, 0);
	void Start () {
	
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
