using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowGuiScript : MonoBehaviour {

	public float xspread = 14f;
	public float yspread = 14f;
	public float width = 14f;
	public float height = 14f;
	
	public string xCoord = "none";
	public string yCoord = "none";

	public string currentStageText = "-1";

	List<Vector2> flowPoints = new List<Vector2>();
	
	void Start()
	{

	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector2 myPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			flowPoints.Add (myPoint);
		}
	}

	void OnGUI() {
		currentStageText = GUI.TextField(new Rect(10, 50, 60, 20), currentStageText, 25);
		if(GUI.Button(new Rect(70, 10, 120, 20), "Save new flow file"))
		{
			string writeContent = "";
			foreach(Vector2 p in flowPoints)
			{
				writeContent += p.x.ToString() + ' ' + p.y.ToString() + '\n';
			}

			string fileName = "stage" + currentStageText + "flow";
			System.IO.File.WriteAllText("Assets/Resources/" + fileName + ".txt", writeContent);
		}
	}
}
