using UnityEngine;
using System.Collections;

public class MultiplyEnemy : Enemy {

	static int NumberOfCancer = 0;

	const int SPLIT_INTERVAL = 360;
	int splitTimer = SPLIT_INTERVAL;

	void Start()
	{
		NumberOfCancer ++;
		ANTIBODY_RESISTANCE = 5;
		health = 15;

		float ratio = Random.Range(0.9f, 1.1f);
		transform.localScale *= ratio;
	}

	void Update () {
        float ratio = (health / MAX_HEALTH);

		splitTimer --;
		if(splitTimer <= 0 && numAntibodiesAttached < ANTIBODY_RESISTANCE)
		{
			Split();
			splitTimer = SPLIT_INTERVAL;
		}

        transform.localScale = new Vector3(ratio, ratio, ratio);
	}

    private void Split()
    {
		if(NumberOfCancer > 50)
			return;

        GameObject clone = Resources.Load("MultiplyEnemy") as GameObject;

		Vector3 currentPos = gameObject.transform.position;
		float xNudge = Random.Range(-0.1f, 0.1f);
		float yNudge = Random.Range(-0.1f, 0.1f);

		Vector3 newPos1 = new Vector3(currentPos.x + xNudge, currentPos.y + yNudge, 0);
		GameObject firstEnemy = Instantiate(clone, newPos1, Quaternion.identity) as GameObject;
    }
	
	protected override void AuxKill()
	{
		NumberOfCancer --;
	}
	protected override int getPointValue() { return 10; }
}
