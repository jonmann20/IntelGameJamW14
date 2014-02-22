using UnityEngine;
using System.Collections;

public class OverworldLocation : MonoBehaviour {

	public int num;

	void OnMouseDown(){
		Overworld.that.updateMapByNum(num);
	}
}
