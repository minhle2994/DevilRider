using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	private Texture introTexture;
	private Texture btn_play, btn_high_score, btn_credit;
	//public AudioClip backgroundMusic;

	// Use this for initialization
	void Start () {
		introTexture = Resources.Load ("background") as Texture;
		btn_play = Resources.Load ("btn_play") as Texture;
		btn_high_score = Resources.Load ("btn_high_score") as Texture;
		btn_credit = Resources.Load ("btn_credit") as Texture;
		//audio.PlayOneShot (backgroundMusic);
	}
	
	void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), introTexture);
		
		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2 - 60, 0.75f*btn_high_score.width, 0.75f*btn_high_score.height), btn_high_score)) {
			Application.LoadLevel(4);
		}

		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2 - 140, 0.75f*btn_high_score.width, 0.75f*btn_high_score.height), btn_play)) {
			Application.LoadLevel(2);
		}

		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2 + 20, 0.75f*btn_high_score.width, 0.75f*btn_high_score.height), btn_credit)) {
			Application.LoadLevel(6);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
