using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {
	// Use this for initialization
	void Start () {
	            
	}

    void OnGUI(){
        GUI.Label (new Rect (0, 0, Screen.width, Screen.height / 10), "High Score " +   PlayerPrefs.GetInt("highScore"));  
        GUI.Label (new Rect (0, 20, Screen.width, Screen.height / 10), "Coins " +   PlayerPrefs.GetInt("coins")); 
        if (GUI.Button (new Rect (6 * Screen.width / 20, Screen.height / 5, Screen.width / 10, Screen.height / 10), "BACK")) {
            Application.LoadLevel(0);
        }
        if (GUI.Button (new Rect (12 * Screen.width / 20, Screen.height / 5, Screen.width / 10, Screen.height / 10), "RESET")) {
            PlayerPrefs.SetInt("highScore", 0);
            PlayerPrefs.SetInt("coins", 0);
            GUI.Label (new Rect (0, 0, Screen.width, Screen.height / 10), "High Score " +   PlayerPrefs.GetInt("highScore"));  
            GUI.Label (new Rect (0, 20, Screen.width, Screen.height / 10), "Coins " +   PlayerPrefs.GetInt("coins")); 
        }
    }	

	// Update is called once per frame
	void Update () {
	
	}
}
