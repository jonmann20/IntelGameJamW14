using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedBloodCellEmitter : MonoBehaviour {
	
	//PREFABS
	public GameObject RedBloodCellPrefab;
	
	public int RED_INTERVAL = 60;
	float redTimer;

	void Start()
	{
		(renderer as SpriteRenderer).enabled = false;
		redTimer = RED_INTERVAL + 15;
	}

	void Update () {
		redTimer --;
		if(redTimer <= 0)
		{
			Instantiate(RedBloodCellPrefab, transform.position, Quaternion.identity);
			redTimer = RED_INTERVAL + Random.Range(-RED_INTERVAL * 0.5f, RED_INTERVAL * 0.5f);
		}
	}
}