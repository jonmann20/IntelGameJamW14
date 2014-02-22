using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowEffectScript : MonoBehaviour {

	const float FLOW_RATE = 0.02f;

	int currentStage = 4;
	List<Vector2> flowPoints = new List<Vector2>();

	Vector2 getNearestFlowPoint()
	{
		Vector2 closest = new Vector2(-9999, -9999);
		float smallestDiff = 100000f;

		foreach(Vector2 p in FlowBank.flowPoints)
		{
			Vector2 myPosition = transform.position;
			float diff = (myPosition - p).magnitude;

			if(diff < smallestDiff)
			{
				smallestDiff = diff;
				closest = p;
			}
		}

		return closest;
	}

	//Inefficient but good for clarity
	int getIndexOfFlowPoint(Vector2 p)
	{
		for(int i = 0; i < FlowBank.flowPoints.Count; i++)
		{
			if(FlowBank.flowPoints[i] == p)
				return i;
		}
		print("ERROR_IN: getIndexOfFlowPoint()");
		return -1;
	}

	//Differential of position
	Vector2 getDP(Vector2 nearestFlowPoint, Vector2 nextFlowPoint)
	{
		Vector2 unit = (nextFlowPoint - nearestFlowPoint);
		unit.Normalize();
		return unit * FLOW_RATE;
	}

	void Update () {
		Vector2 nearestFlowPoint = getNearestFlowPoint();
		int indexOfNearestFlowPoint = getIndexOfFlowPoint(nearestFlowPoint);

		//Get next flow point
		Vector2 nextFlowPoint = new Vector2(-999, -999); //POTENTIAL FREAKOUT (TODO: Fix edge case)
		if(FlowBank.flowPoints.Count > indexOfNearestFlowPoint + 1)
			nextFlowPoint = FlowBank.flowPoints[indexOfNearestFlowPoint + 1];

		Vector2 positionDifferential = getDP (nearestFlowPoint, nextFlowPoint);
		rigidbody2D.velocity += positionDifferential;

		Debug.DrawLine(nearestFlowPoint, nextFlowPoint, Color.green);
	}
}
