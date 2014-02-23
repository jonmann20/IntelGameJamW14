using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItsATrap : MonoBehaviour {

	bool triggerd = false, throughDoor = false;

	void Update(){
		if(!throughDoor && !Swarm.that.inputEnabled){
			bool check = true;

			foreach(GameObject x in Swarm.that.entities){
				if(x == null) continue;

				//print (x.transform.position.x + " ?>= " + transform.position.x);

				if(x.transform.position.x >= transform.position.x){
					check = false;
					break;
				}
			}

			print ("check: " + check);
			if(check){
				Swarm.that.inputEnabled = true;
				GetComponent<PolygonCollider2D>().isTrigger = false;
				throughDoor = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if(!triggerd && col.gameObject.tag == "Player"){
			print ("triggered");

			triggerd = true;
			Swarm.that.inputEnabled = false;
			GetComponent<SpriteRenderer>().enabled = true;
		}
	}
}
