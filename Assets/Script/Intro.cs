using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	public Texture introTexture;

	// Use this for initialization
	void Start () {
		introTexture = Resources.Load ("intro") as Texture;
	}
	
	void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), introTexture);
		
		if (GUI.Button (new Rect (9 * Screen.width / 20, Screen.height / 5, Screen.width / 10, Screen.height / 10), "START")) {
			Application.LoadLevel(1);
		}
        if (GUI.Button (new Rect (9 * Screen.width / 20, Screen.height / 3, Screen.width / 10, Screen.height / 10), "HIGH SCORE")) {
            Application.LoadLevel(3);
        }
	}

	// Update is called once per frame
	void Update () {
	
	}
}
