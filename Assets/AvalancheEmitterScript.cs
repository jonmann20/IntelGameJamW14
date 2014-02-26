using UnityEngine;
using System.Collections;

public class AvalancheEmitterScript : MonoBehaviour {
	
	//PREFABS
	public GameObject Enemy1Prefab;
	public GameObject AntibodyPrefab;
	public GameObject RedBloodCellPrefab;
	
	const int ENEMY_INTERVAL = 600;
	float enemyTimer = 5;
	
	const int RED_INTERVAL = 10;
	float redTimer = 5;
	
	// Use this for initialization
	void Start () {
		(renderer as SpriteRenderer).enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		enemyTimer --;
		if(enemyTimer <= 0)
		{
			for(int i = 0; i < 20; i++)
				Instantiate(Enemy1Prefab, transform.position, Quaternion.identity);
			enemyTimer = ENEMY_INTERVAL + Random.Range(-ENEMY_INTERVAL * 0.5f, ENEMY_INTERVAL * 0.5f);;
		}
		
		redTimer --;
		if(redTimer <= 0)
		{

			Instantiate(RedBloodCellPrefab, transform.position, Quaternion.identity);
			redTimer = RED_INTERVAL + Random.Range(-RED_INTERVAL * 0.5f, RED_INTERVAL * 0.5f);
		}
	}
}
