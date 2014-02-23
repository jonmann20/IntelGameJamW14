using UnityEngine;
using System.Collections;

public class StreamEmitterScript : MonoBehaviour {

	//PREFABS
	public GameObject Enemy1Prefab;
	public GameObject AntibodyPrefab;
	public GameObject RedBloodCellPrefab;

	const int ENEMY_INTERVAL = 360;
	float enemyTimer = ENEMY_INTERVAL;

	const int ANTIBODY_INTERVAL = 360;
	float antibodyTimer = ANTIBODY_INTERVAL + 90;

	const int RED_INTERVAL = 360;
	float redTimer = RED_INTERVAL + 15;

	// Use this for initialization
	void Start () {
		(renderer as SpriteRenderer).enabled = false;
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

		redTimer --;
		if(redTimer <= 0)
		{
			Instantiate(RedBloodCellPrefab, transform.position, Quaternion.identity);
			redTimer = RED_INTERVAL + Random.Range(-RED_INTERVAL * 0.5f, RED_INTERVAL * 0.5f);
		}
	}
}
