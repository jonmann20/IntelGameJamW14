using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	protected const float MAX_HEALTH = 20;
	protected const int ANTIBODY_RESISTANCE = 20;

	protected int numAntibodiesAttached = 0;
	protected float health = MAX_HEALTH;

	public GameObject EntityDeath;

	void kill(){
		Game.points += 10;

		if(Game.points % 50 == 0){
			// new life!!
		}

		Destroy(gameObject);
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
		
		if (coll.gameObject.tag == "Player")
		{
			if(numAntibodiesAttached >= ANTIBODY_RESISTANCE)
			{
				--health;
				if(health <= 0){
					kill();
				}
			}
			else
			{
				Instantiate(EntityDeath, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
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
				--health;
				if(health <= 0){
					kill();
				}
			}
		}
	}
}
