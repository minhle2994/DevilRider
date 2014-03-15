using UnityEngine;
using System.Collections;

public class EnterName : MonoBehaviour {
	private Texture sceneTexture;
	private string playerName = "playerName";
	// Use this for initialization
	void Start () {
		sceneTexture = Resources.Load ("HighScore") as Texture;
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), sceneTexture);
		
		playerName =  GUI.TextField(new Rect(10, 10, 200, 20), playerName, 25);

		if (GUI.Button (new Rect (Screen.width / 2 - 45, 4*Screen.height / 5, 90, 30), "OK")) {
			Application.LoadLevel(4);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
