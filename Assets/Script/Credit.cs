using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour {
	private Texture sceneTexture;
	public GUIStyle style1 = new GUIStyle ();
	public GUIStyle style2 = new GUIStyle ();

	// Use this for initialization
	void Start () {
		sceneTexture = Resources.Load ("HighScore") as Texture;

	}

	void OnGUI(){

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), sceneTexture);

		GUI.Label (new Rect (Screen.width / 4, Screen.height / 4, 200, 20), "Credit", style1);
		GUI.Label (new Rect (Screen.width / 4, Screen.height / 4 + 50, 200, 20), "    Morbling Studio", style2);
		if (GUI.Button (new Rect (Screen.width / 2 - 45, 4*Screen.height / 5, 90, 30), "BACK")) {
			Application.LoadLevel(1);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
