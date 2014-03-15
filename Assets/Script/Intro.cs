﻿using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	private Texture introTexture;
	private Texture btn_play, btn_high_score, btn_credit;

	// Use this for initialization
	void Start () {
		introTexture = Resources.Load ("background") as Texture;
		btn_play = Resources.Load ("btn_play") as Texture;
		btn_high_score = Resources.Load ("btn_high_score") as Texture;
		btn_credit = Resources.Load ("btn_credit") as Texture;
	}
	
	void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), introTexture);
		
		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2, btn_high_score.width/2, btn_high_score.height/2), btn_high_score)) {
			Application.LoadLevel(4);
		}

		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2 - 50, btn_high_score.width/2, btn_high_score.height/2), btn_play)) {
			Application.LoadLevel(2);
		}

		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2 + 50, btn_high_score.width/2, btn_high_score.height/2), btn_credit)) {
			Application.LoadLevel(4);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
