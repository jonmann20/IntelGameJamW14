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

				if(x.transform.position.x + 1 >= transform.position.x){	//+1 for padding
					check = false;
					break;
				}
			}


			if(check){
				FlowEffectScript.FLOW_RATE = 0;
				Swarm.that.inputEnabled = true;
				GetComponent<PolygonCollider2D>().isTrigger = false;
				throughDoor = true;
			}
		}
	}

	void OnGUI(){
		if(!throughDoor && !Swarm.that.inputEnabled){
			EzGUI.scaleGUI();
			EzGUI.placeTxt("Cutscene", 90, EzGUI.HALFW, EzGUI.HALFH);
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if(!triggerd && col.gameObject.tag == "Player"){
			triggerd = true;
			Swarm.that.inputEnabled = false;
			GetComponent<SpriteRenderer>().enabled = true;
		}
	}
}
