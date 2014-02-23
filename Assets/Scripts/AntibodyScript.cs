using UnityEngine;
using System.Collections;

public class AntibodyScript : MonoBehaviour {
	
	int life = 120;
	void Start () {
		
	}
	
	void Update () {
		life --;
		if(life <= 0)
			Destroy(gameObject);
	}
}
