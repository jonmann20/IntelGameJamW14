using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swarm : MonoBehaviour {

	public static Swarm that;
	GameObject entitiesHolder;
	GameObject entityPrefab;
	GameObject AntibodyPrefab;

	public Sprite antibody, bloodCell_white;

	public List<GameObject> entities;

	const float ANTIBODY_SHOT_SPEED = 10.0f;

	void Awake(){
		that = this;
		entitiesHolder = new GameObject("EntitiesHolder");
		entities = new List<GameObject>();
		entityPrefab = Resources.Load<GameObject>("Entity");
		AntibodyPrefab = Resources.Load<GameObject>("Antibody");

		for(int i=0; i < 10; ++i){
			GameObject entity = createEntity(new Vector3(2, 2, 0));
		}
	}

	void Update(){
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z);
			point.z = 0;

			move(point);
		}

		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z);

			foreach(GameObject g in entities)
			{
				GameObject newAntibody = Instantiate(AntibodyPrefab, g.transform.position, Quaternion.identity) as GameObject;
				Vector3 unit3 = point - g.transform.position;
				Vector2 unit2 = new Vector2(unit3.x, unit3.y);
				unit2.Normalize();
				print("unit: " + unit2.ToString());
				newAntibody.rigidbody2D.velocity = unit2 * ANTIBODY_SHOT_SPEED;

			}
		}

		checkVelocity();
	}

	GameObject createEntity(Vector3 pos){
		GameObject entity = Instantiate(entityPrefab, pos, Quaternion.identity) as GameObject;

		entity.transform.parent = entitiesHolder.transform;
		entities.Add(entity);

		return entity;
	}

	public void move(Vector3 p){
		for(int i=0; i < entities.Count; ++i){
			if(checkDirChange(entities[i], p)){
				entities[i].rigidbody2D.velocity *= 0.25f;
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
