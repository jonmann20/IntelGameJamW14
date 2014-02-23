using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	protected float MAX_HEALTH = 20;
	protected int ANTIBODY_RESISTANCE = 20;

	protected int numAntibodiesAttached = 0;
	protected float health = 20;

	public GameObject EntityDeath;

	public virtual void kill(){
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

			coll.gameObject.SendMessage("disableLife");
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
