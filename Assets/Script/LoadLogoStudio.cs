using UnityEngine;
using System.Collections;

public class LoadLogoStudio : MonoBehaviour {
	private Texture studioLogo;

	// Use this for initialization
	void Start () {
		studioLogo = Resources.Load ("LogoStudio") as Texture;
	}
	
	// Update is called once per frame
	void OnGUI() {
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), studioLogo);
		Application.LoadLevel (1);
	}

	
	void Update () {
		
	}
}
