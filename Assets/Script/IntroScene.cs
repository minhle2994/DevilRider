using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class IntroScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();	

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayBtnClick()
	{
		Application.LoadLevel(2);
	}

	public void HighScoreBtnClick(){
		((PlayGamesPlatform) Social.Active).ShowLeaderboardUI("CggIppT28DoQAhAA");
	}

	public void CreditsBtnClick(){
		Application.LoadLevel(6);
	}

	public void HighScoreBack(){
		Application.LoadLevel(1);
	}
}
