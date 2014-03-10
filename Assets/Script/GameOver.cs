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

		if (GUI.Button (new Rect (6 * Screen.width / 20, 4 * Screen.height / 5, Screen.width / 10, Screen.height / 10), "REPLAY")) {
			Application.LoadLevel(1);
		}

        if (GUI.Button (new Rect (12 * Screen.width / 20, 4 * Screen.height / 5, Screen.width / 10, Screen.height / 10), "BACK")) {
            Application.LoadLevel(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}