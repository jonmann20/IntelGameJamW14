using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	protected const float MAX_HEALTH = 20;
	protected const int ANTIBODY_RESISTANCE = 20;

	protected int numAntibodiesAttached = 0;
	protected float health = MAX_HEALTH;


}
