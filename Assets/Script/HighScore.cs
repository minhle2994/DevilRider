using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {
	private Texture highScoreTexture;

	// Use this for initialization
	void Start () {
		highScoreTexture = Resources.Load ("HighScore") as Texture;

	}

    void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), highScoreTexture);

		for (int i=0; i<10; i++){
			GUI.Label (new Rect (Screen.width/3 - 30, Screen.height/4 + i *25, Screen.width, Screen.height / 10),
			           i + "\t\t" + PlayerPrefs.GetString("Rank" + i.ToString() + "Name") + 
			           PlayerPrefs.GetInt("Rank" + i.ToString() + "Score"));			
		}

        if (GUI.Button (new Rect (Screen.width / 2 - 45, 4*Screen.height / 5, 90, 30), "BACK")) {
            Application.LoadLevel(1);
        }
//        if (GUI.Button (new Rect (12 * Screen.width / 20, Screen.height / 5, Screen.width / 10, Screen.height / 10), "RESET")) {
//            PlayerPrefs.SetInt("highScore", 0);
//            PlayerPrefs.SetInt("coins", 0);
//            GUI.Label (new Rect (0, 0, Screen.width, Screen.height / 10), "High Score " +   PlayerPrefs.GetInt("highScore"));  
//            GUI.Label (new Rect (0, 20, Screen.width, Screen.height / 10), "Coins " +   PlayerPrefs.GetInt("coins")); 
//        }
    }	

	// Update is called once per frame
	void Update () {
	
	}
}
