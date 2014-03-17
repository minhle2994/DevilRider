using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class HighScore : MonoBehaviour {
	private Texture highScoreTexture;
	public GUIStyle style1 = new GUIStyle ();

	// Use this for initialization
	void Start () {
		highScoreTexture = Resources.Load ("HighScore") as Texture;

	}

    void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), highScoreTexture);

		for (int i=0; i<10; i++){
			GUI.Label (new Rect (Screen.width/3 - 30, Screen.height/4 + i *25, Screen.width/2, Screen.height / 10),
			           i 
			           + "\t\t" 
			           + PlayerPrefs.GetString("Rank" + i.ToString() + "Name").PadRight(25).Substring(0, 20), style1);

			GUI.Label (new Rect (Screen.width/3 + 100, Screen.height/4 + i *25, Screen.width/2, Screen.height / 10),
			           "\t\t" 
			           + PlayerPrefs.GetInt("Rank" + i.ToString() + "Score"), style1);
		}

        if (GUI.Button (new Rect (0.05f*Screen.width, 4*Screen.height / 5, 150, 40), "BACK")) {
            Application.LoadLevel(1);
        }

		if (GUI.Button (new Rect (0.65f*Screen.width, 4*Screen.height / 5, 150, 40), "RESET")) {
			PlayerPrefs.DeleteAll(); 
        }
    }	

	// Update is called once per frame
	void Update () {
	
	}
}
