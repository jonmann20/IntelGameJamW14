using UnityEngine;
using System.Collections;

public class Enemy1Script : Enemy {

	void Update () {
		float ratio = health / MAX_HEALTH;
		//print("health: " + health.ToString() + " ratio: " + ratio);
		transform.localScale = new Vector3(ratio, ratio, ratio);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Antibody")
		{
			numAntibodiesAttached ++;
			coll.gameObject.GetComponent<FlowEffectScript>().enabled = false;
			Destroy(coll.gameObject.rigidbody2D);
			//coll.gameObject.RemoveComponent()
			coll.gameObject.transform.parent = transform;

			if(numAntibodiesAttached >= ANTIBODY_RESISTANCE)
				(renderer as SpriteRenderer).color = Color.blue;
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			if(numAntibodiesAttached >= ANTIBODY_RESISTANCE)
			{
				health --;
				if(health <= 0)
					Destroy(gameObject);
			}
			else
			{
				Destroy(coll.gameObject);
			}
		}
	}
}
