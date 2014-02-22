using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowEffectScript : MonoBehaviour {

	const float FLOW_RATE = 0.02f;

	int currentStage = 4;
	List<Vector2> flowPoints = new List<Vector2>();


	void Start () {
		initFlowPoints(currentStage);
	}

	//PARSE FLOW FILE FOR STAGE stageNumber
	void initFlowPoints(int stageNumber)
	{
		string fileName = "stage" + stageNumber.ToString() + "flow";
		string content = (Resources.Load(fileName) as TextAsset).text;
		print("FILE_LOADED: " + stageNumber.ToString());

		string xCoordinateString = "";
		string yCoordinateString = "";
		bool coordBit = false; //X-coords = false, Y-coords = true

		foreach(char c in content)
		{
			if(c == ' ' || c == '\n')
			{
				if(coordBit) //we have both x and y coords-- make new point
				{
					print("new point");
					print("xcoord: " + xCoordinateString);
					print("ycoord: " + yCoordinateString);

					Vector2 newPoint = new Vector2(float.Parse (xCoordinateString), float.Parse(yCoordinateString));
					flowPoints.Add(newPoint);
					xCoordinateString = "";
					yCoordinateString = "";
				}
				coordBit = !coordBit;
				continue;
			}
			if(!coordBit)
			{
				xCoordinateString += c;
				continue;
			}
			if(coordBit)
			{
				yCoordinateString += c;
				continue;
			}
		}

		print("PANTS!");
		print("final x: " + xCoordinateString + ']');
		print("final y: " + yCoordinateString + ']');
		//HANDLE FINAL POINT
		//flowPoints.Add(new Vector2(float.Parse (xCoordinateString), float.Parse(yCoordinateString)));

		print("FLOW POINTS:");
		foreach(Vector2 v in flowPoints)
		{
			print(v.ToString());
		}
	}

	Vector2 getNearestFlowPoint()
	{
		Vector2 closest = new Vector2(-9999, -9999);
		float smallestDiff = 100000f;

		foreach(Vector2 p in flowPoints)
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
		for(int i = 0; i < flowPoints.Count; i++)
		{
			if(flowPoints[i] == p)
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
		if(flowPoints.Count > indexOfNearestFlowPoint + 1)
			nextFlowPoint = flowPoints[indexOfNearestFlowPoint + 1];

		Vector2 positionDifferential = getDP (nearestFlowPoint, nextFlowPoint);
		transform.position += new Vector3(positionDifferential.x, positionDifferential.y, 0);

		Debug.DrawLine(nearestFlowPoint, nextFlowPoint, Color.green);

        Debug.Log(Time.timeScale);
	}
}
