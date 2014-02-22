using UnityEngine;
using System.Collections;

public class StreamEmitterScript : MonoBehaviour {

	//PREFABS
	public GameObject Enemy1Prefab;
	public GameObject AntibodyPrefab;

	const int ENEMY_INTERVAL = 360;
	float enemyTimer = ENEMY_INTERVAL;

	const int ANTIBODY_INTERVAL = 360;
	float antibodyTimer = ANTIBODY_INTERVAL + 90;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		enemyTimer --;
		if(enemyTimer <= 0)
		{
			Instantiate(Enemy1Prefab, transform.position, Quaternion.identity);
			enemyTimer = ENEMY_INTERVAL + Random.Range(-ENEMY_INTERVAL * 0.5f, ENEMY_INTERVAL * 0.5f);;
		}

		antibodyTimer --;
		if(antibodyTimer <= 0)
		{
			Instantiate(AntibodyPrefab, transform.position, Quaternion.identity);
			antibodyTimer = ANTIBODY_INTERVAL + Random.Range(-ANTIBODY_INTERVAL * 0.5f, ANTIBODY_INTERVAL * 0.5f);
		}
	}
}
