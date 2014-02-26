using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour {

	public static Camera camera;
	float minSize = 5.8f;
	
	Vector3 center;
	float size;

	List<GameObject> swarm;

	void Start(){
		camera = Camera.main;
		swarm = Swarm.that.entities;
	}

	void FixedUpdate () {
		calcBoundingBox();

		// zoom camera
		if(swarm.Count > 1 && size > minSize) {
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, size, Time.deltaTime);	
		}
		else {
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, minSize, Time.deltaTime);
		}
		
		// move camera(only if distance is big enough
		if(Vector3.Distance(camera.transform.position, center) > camera.orthographicSize * 0.1f){
			camera.transform.position = Vector3.Lerp(camera.transform.position, center, Time.deltaTime * 3);	
		}
	}
	
	// returns the 'center' between the followed objects
	void calcBoundingBox() {

		Vector2 min = new Vector2(99999, 99999);
		Vector2 max = new Vector2(-99999, -99999);
		
		foreach(GameObject s in swarm) {
			// find the min and max X and Y of the set
			// this describes an n-vertex polygon 
			// whose center is the midpoint average of all the entities

			if(s == null)
				continue;

			Vector2 p = s.transform.position;

			if(p.x < min.x)
				min.x = p.x;
			if(p.y < min.y)
				min.y = p.y;
			if(p.x > max.x)
				max.x = p.x;
			if(p.y > max.y)
				max.y = p.y;
		}

		center = new Vector3((min.x + max.x)/2, (min.y + max.y)/2, camera.transform.position.z);
		
		// calculate required zoom to see all the entities
		size = Mathf.Max(max.x - min.x, max.y - min.y) / 2;

		// +padding
		size *= 1.1f;	
	}
}
