using UnityEngine;
using System.Collections;

public class MultiplyEnemy : Enemy {

	static int NumberOfCancer = 0;
    static int finalState = 3;
    int timesSplit = 0;

	const int SPLIT_INTERVAL = 360;
	int splitTimer = SPLIT_INTERVAL;

	void Start()
	{
		NumberOfCancer ++;
		ANTIBODY_RESISTANCE = 5;
		health = 15;
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

    public void Reset (int value){
		timesSplit = value;
	}

    private void Split()
    {
		if(NumberOfCancer > 50)
			return;

        GameObject clone = Resources.Load("MultiplyEnemy") as GameObject;

		Vector3 currentPos = gameObject.transform.position;
		Vector3 newPos1 = new Vector3(currentPos.x + 0.1f, currentPos.y, 0);
		Vector3 newPos2 = new Vector3(currentPos.x - 0.1f, currentPos.y, 0);
		GameObject firstEnemy = Instantiate(clone, newPos1, Quaternion.identity) as GameObject;
		GameObject secondEnemy = Instantiate(clone, newPos2, Quaternion.identity) as GameObject;

        firstEnemy.GetComponent<MultiplyEnemy>().Reset(++timesSplit);
        secondEnemy.GetComponent<MultiplyEnemy>().Reset(++timesSplit);

        Destroy(gameObject);
    }

	public override void kill(){
		NumberOfCancer --;
		Game.points += 10;
		
		if(Game.points % 50 == 0){
			// new life!!
		}
		
		Destroy(gameObject);
	}
}
