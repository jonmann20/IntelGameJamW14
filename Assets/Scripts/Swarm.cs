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

		for(int i=0; i < 10; ++i){
			GameObject entity = createEntity(i);
			entity.transform.Translate(0.5f*i, 0.5f*i, 0);
		}
	}

	void Update(){
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z);
			point.z = 0;


			move(point);
		}

		checkVelocity();
	}

	GameObject createEntity(int num){
		GameObject entity = new GameObject("Entity_" + num);
		entity.AddComponent<CircleCollider2D>();

		Rigidbody2D rg = entity.AddComponent<Rigidbody2D>();
		rg.gravityScale = 0;

		SpriteRenderer sprRend = entity.AddComponent<SpriteRenderer>();
		sprRend.sprite = antibody;

		entity.transform.parent = entitiesHolder.transform;
		entities.Add(entity);

		return entity;
	}

	public void move(Vector3 p){
		for(int i=0; i < entities.Count; ++i){
			if(checkDirChange(entities[i], p)){
				entities[i].rigidbody2D.velocity = Vector3.zero;
			}

			Vector3 diff = p - entities[i].transform.position;
			entities[i].rigidbody2D.AddForce(diff.normalized * 12);
			//entities[i].rigidbody2D.velocity = diff.normalized * 2;
		}
	}

	bool checkDirChange(GameObject old, Vector3 p){
		bool wasChange = false;

		Vector3 p2old = old.transform.position - p;						// vector from new point to old position
		Vector3 x = Vector3.Project(old.rigidbody2D.velocity, p2old);	// vector from old velocity vector to p2old

		Vector3 newP2old = p2old + x;

		//print(x);

		return (newP2old.magnitude > p2old.magnitude);
	}

	void checkVelocity(){
		for(int i=0; i < entities.Count; ++i){
			Vector3 vel = entities[i].rigidbody2D.velocity;

			//print (vel.x);

			if(vel.x > 3){
				vel.x = 3;
				entities[i].rigidbody2D.velocity = new Vector3(3, vel.y, 0);
			}
			else if(vel.x < -3){
				vel.x = -3;
				entities[i].rigidbody2D.velocity = new Vector3(-3, vel.y, 0);
			}
		}
	}
}
