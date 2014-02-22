using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swarm : MonoBehaviour {

	public static Swarm that;
	GameObject entitiesHolder;

	public Sprite antibody, bloodCell_white;

	List<GameObject> entities;


	void Awake(){
		that = this;
		entitiesHolder = new GameObject("EntitiesHolder");
		entities = new List<GameObject>();

		for(int i=0; i < 5; ++i){
			GameObject entity = createEntity(i);
			entity.transform.Translate(0.5f*i, 0.5f*i, 0);
		}
	}

	void Update(){
	
	}

	GameObject createEntity(int num){
		GameObject entity = new GameObject("Entity_" + num);
		entity.AddComponent<CircleCollider2D>();
		SpriteRenderer sprRend = entity.AddComponent<SpriteRenderer>();
		sprRend.sprite = antibody;

		entity.transform.parent = entitiesHolder.transform;
		entities.Add(entity);

		return entity;
	}

	public void move(Vector3 p){
		for(int i=0; i < entities.Count; ++i){
			Utils.moveToPosition(entities[i].transform, p, 2f, null);
		}
	}
}
