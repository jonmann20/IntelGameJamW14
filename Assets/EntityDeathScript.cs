using UnityEngine;
using System.Collections;

public class EntityDeathScript : MonoBehaviour {

	const int FLASH_TIME = 15;
	int flashTimer = FLASH_TIME;
	Color initialColor;
	bool isRed = true;

	float bounceVal = 1;
	float bounceVelocity = 0.01f;

	// Use this for initialization
	void Start () {
		initialColor = (renderer as SpriteRenderer).color;
	}
	
	// Update is called once per frame
	void Update () {
	
		bounceVal += bounceVelocity;
		bounceVelocity -= 0.01f;
		transform.localScale *= bounceVal;

		if(bounceVal <= 0)
		{
			Destroy(this);
		}

		flashTimer --;
		if(flashTimer <= 0)
		{
			isRed = !isRed;
			flashTimer = FLASH_TIME;
		}

		if(isRed)
			(renderer as SpriteRenderer).color = Color.red;
		else
			(renderer as SpriteRenderer).color = initialColor;
	}
}
