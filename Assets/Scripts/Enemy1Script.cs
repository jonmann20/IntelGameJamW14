using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {

	int numAntibodiesAttached = 0;
	const int ANTIBODY_RESISTANCE = 20;
	const float MAX_HEALTH = 50;
	float health = MAX_HEALTH;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float ratio = (health / MAX_HEALTH) * 0.5f;
		print("health: " + health.ToString() + " ratio: " + ratio);
		transform.localScale = new Vector3(ratio, ratio, ratio);
	}

	void OnCollisionEnter2D(Collision2D coll) {

		print("COLLISION!");

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
		}
	}
}
