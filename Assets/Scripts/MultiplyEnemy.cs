using UnityEngine;
using System.Collections;

public class MultiplyEnemy : Enemy {

    static int finalState = 3;
    int timesSplit = 0;

	void Update () {
        float ratio = (health / MAX_HEALTH);
       // print("health: " + health.ToString() + " ratio: " + ratio);

        transform.localScale = new Vector3(ratio, ratio, ratio);
	}

    public void Reset (int value){
		timesSplit = value;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Antibody")
		{
			numAntibodiesAttached ++;
			coll.gameObject.GetComponent<FlowEffectScript>().enabled = false;
			Destroy(coll.gameObject.rigidbody2D);
			coll.gameObject.transform.parent = transform;

            if (numAntibodiesAttached >= ANTIBODY_RESISTANCE) 
            {
                (renderer as SpriteRenderer).color = Color.red;
            }	
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
                if (timesSplit != finalState)
                {
                    Split();
                }
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

    private void Split()
    {
        GameObject clone = Resources.Load("MultiplyEnemy") as GameObject;

        GameObject firstEnemy = Instantiate(clone, gameObject.transform.position, Quaternion.identity) as GameObject;
        GameObject secondEnemy = Instantiate(clone, gameObject.transform.position, Quaternion.identity) as GameObject;

        firstEnemy.transform.rigidbody2D.velocity = new Vector3(1, 0, 0) * 5.0f;
        secondEnemy.transform.rigidbody2D.velocity = new Vector3(-1, 0, 0) * 5.0f;

        firstEnemy.GetComponent<MultiplyEnemy>().Reset(++timesSplit);
        secondEnemy.GetComponent<MultiplyEnemy>().Reset(++timesSplit);

        Destroy(gameObject);
    }
}
