using UnityEngine;
using System.Collections;

public class EzGUI : MonoBehaviour {

	public static EzGUI that;
	
	public const float FULLW = 1920;
	public const float FULLH = 1080;
	
	public const float HALFW = FULLW / 2;
	public const float HALFH = FULLH / 2;

	public static void scaleGUI(){
		float rx = Screen.width / FULLW;
		float ry = Screen.height / FULLH;
		
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1)); 
	}

	public static void placeTxt(string str, int fontSize, float x, float y, bool black=false) {
		GUIContent content = new GUIContent(str);
		
		GUIStyle style = new GUIStyle();
		style.fontSize = fontSize;
		style.normal.textColor = black ? Color.black : Color.white;
		
		Vector2 size = style.CalcSize(content);
		GUI.Label(new Rect(x - size.x/2, y - size.y, size.x, size.y), content, style);
	}

	public static bool placeBtn(string str, int fontSize, float x, float y){
		GUIContent content = new GUIContent(str);

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleCenter;
		//style.wordWrap = true;
		style.fontSize = fontSize;

		Vector2 size = style.CalcSize(content);
		return GUI.Button(new Rect(x - size.x/2, y - size.y, size.x, size.y), content, style);
	}
	
	public static void blinkTxt(string str, int fontSize, float x, float y, bool black=false) {
		GUIContent content = new GUIContent(str);

		float f = black ? 0 : 1;

		GUIStyle style = new GUIStyle();
		style.fontSize = fontSize;
		style.normal.textColor = new Color(f, f, f, Mathf.PingPong(Time.time, 1));
		
		Vector2 size = style.CalcSize(content);
		GUI.Label(new Rect(x - size.x/2, y - size.y, size.x, size.y), content, style);
	}
}
