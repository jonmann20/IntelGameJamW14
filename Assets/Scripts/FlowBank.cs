using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowBank : MonoBehaviour {

	public static List<Vector2> flowPoints = new List<Vector2>();

	public void Awake()
	{
		print("INIT WITH " + GlobalScript.currentLevel);
		initFlowPoints(GlobalScript.currentLevel);
	}

	//PARSE FLOW FILE FOR STAGE stageNumber
	public static void initFlowPoints(int stageNumber)
	{
		string fileName = "stage" + stageNumber.ToString() + "flow";
		string content = (Resources.Load(fileName) as TextAsset).text;
//		print("FILE_LOADED: " + stageNumber.ToString());
		
		string xCoordinateString = "";
		string yCoordinateString = "";
		bool coordBit = false; //X-coords = false, Y-coords = true
		
		foreach(char c in content)
		{
			if(c == ' ' || c == '\n')
			{
				if(coordBit) //we have both x and y coords-- make new point
				{	
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

		foreach(Vector2 v in flowPoints)
		{
			//print(v.ToString());
		}
	}
}
