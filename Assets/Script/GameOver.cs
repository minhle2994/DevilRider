using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public Texture gameOverTexture;
	public Texture btnReplay;
	// Use this for initialization
	void Start () {
		gameOverTexture = Resources.Load ("GameOver") as Texture;
		//btnReplay = Resources.Load ("replay") as Texture;
	}
	
	void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), gameOverTexture);

		if (GUI.Button (new Rect (Screen.width / 2 - 120, 4*Screen.height / 5, 90, 30), "REPLAY")) {
			Application.LoadLevel(2);
		}

		if (GUI.Button (new Rect (Screen.width / 2 + 50, 4*Screen.height / 5, 90, 30), "BACK")) {
			Application.LoadLevel(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}