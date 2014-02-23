using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swarm : MonoBehaviour {

	public static Swarm that;

	Sprite[] sprites;	// {blink, hit, lick, smile} .... blink --> lick --> smile ...

	GameObject entitiesHolder;
	GameObject entityPrefab;
	GameObject AntibodyPrefab;

	public bool inputEnabled = true;

	Sprite antibody, bloodCell_white;

	public List<GameObject> entities;

	const float ANTIBODY_SHOT_SPEED = 10.0f;
	const float MAX_SPEED = 10.5f;

	int spriteTimer = 120;

	void Awake(){
		that = this;
		entitiesHolder = new GameObject("EntitiesHolder");
		entities = new List<GameObject>();
		entityPrefab = Resources.Load<GameObject>("Entity");
		AntibodyPrefab = Resources.Load<GameObject>("Antibody");

		sprites = Resources.LoadAll<Sprite>("BloodCellWhite");

		for(int i=0; i < 10; ++i){
			GameObject entity = createEntity(new Vector3(2.5f + Random.Range(0.0f, 1.0f), 2, 0));
		}
	}

	void Update(){
		if(inputEnabled){
			checkInput();
		}
	
		if(--spriteTimer <= 0){
			updateSpriteStates();
		}

		//checkVelocity();
	}

	void updateSpriteStates(){
		for(int i=0; i < entities.Count; ++i){
			GameObject g = entities[i];

			if(g == null){
				continue;
			}

			SpriteRenderer sRend = g.GetComponent<SpriteRenderer>();
	
			if(sRend.sprite.name == "bcw_smile"){
				sRend.sprite = sprites[0];
			}
			else if(sRend.sprite.name == "bcw_lick"){
				sRend.sprite = sprites[3];
			}
			else if(sRend.sprite.name == "bcw_blink"){
				sRend.sprite = sprites[2];
			}
			else {
				sRend.sprite = sprites[0];
			}
		}

		spriteTimer = 120;
	}

	void checkInput(){
        if (Input.GetMouseButton(0) && !GlobalScript.isDemo)
        {

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z);
			point.z = 0;

			move(point);
		}

        if (Input.GetMouseButtonDown(1) && !GlobalScript.isDemo)
        {
			print("EXECUTING");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z);

			//LAUNCH ANTIBODIES
			foreach(GameObject g in entities)
			{
				if(g == null)
					continue;

				GameObject newAntibody = Instantiate(AntibodyPrefab, g.transform.position, Quaternion.identity) as GameObject;
				Vector3 unit3 = point - g.transform.position;
				Vector2 unit2 = new Vector2(unit3.x + Random.Range(-0.5f, 0.5f), unit3.y + Random.Range(-0.5f, 0.5f));
				unit2.Normalize();
				newAntibody.rigidbody2D.velocity = unit2 * ANTIBODY_SHOT_SPEED;

			}
		}

		checkVelocity();

        if (!isThereACellStillAlive())
        {
            Application.LoadLevel("stage" + GlobalScript.currentLevel.ToString());
            Game.points = 0;
        }
	}

	GameObject createEntity(Vector3 pos){
		GameObject entity = Instantiate(entityPrefab, pos, Quaternion.identity) as GameObject;

		entity.transform.parent = entitiesHolder.transform;
		entities.Add(entity);

		SpriteRenderer sRend = entity.GetComponent<SpriteRenderer>();

		int idx = Random.Range(0, 4);
		if(idx == 1){ // no "hit" state for start
			idx = 0;
		}
		print (idx);
		sRend.sprite = sprites[idx];

		return entity;
	}

	public void move(Vector3 p){
		for(int i=0; i < entities.Count; ++i){
			if(entities[i] == null)
				continue;

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
			if(entities[i] == null)
				continue;

			Vector3 vel = entities[i].rigidbody2D.velocity;

			if(vel.x > MAX_SPEED){
				vel.x = MAX_SPEED;
				entities[i].rigidbody2D.velocity = new Vector3(MAX_SPEED, vel.y, 0);
			}
			else if(vel.x < -MAX_SPEED){
				vel.x = -MAX_SPEED;
				entities[i].rigidbody2D.velocity = new Vector3(-MAX_SPEED, vel.y, 0);
			}
		}
	}

	bool isThereACellStillAlive()
	{
		foreach(GameObject g in entities)
		{
			if(g != null)
				return true;
		}

		return false;
	}
}
