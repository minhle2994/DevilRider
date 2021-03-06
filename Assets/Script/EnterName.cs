﻿using UnityEngine;
using System.Collections;

public class EnterName : MonoBehaviour {
	private Texture sceneTexture;
	private string playerName = "";
	public GUIStyle style1 = new GUIStyle ();

	// Use this for initialization
	void Start () {
		sceneTexture = Resources.Load ("HighScore") as Texture;
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), sceneTexture);

		GUI.Label (new Rect (Screen.width / 4, Screen.height / 3 - 60, 200, 20), "Enter your name", style1);

		playerName =  GUI.TextField(new Rect(Screen.width / 4, Screen.height/3, 200, 40), playerName, 20);
		PlayerPrefs.SetString ("Rank" + PlayerPrefs.GetInt ("Rank") + "Name", playerName);
		PlayerPrefs.SetInt ("Rank" + PlayerPrefs.GetInt ("Rank") + "Score", PlayerPrefs.GetInt ("Score"));
		if (GUI.Button (new Rect (Screen.width / 2 - 60, 4*Screen.height / 5, 120, 40), "OK")) {
			Application.LoadLevel(4);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
