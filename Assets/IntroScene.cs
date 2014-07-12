using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayBtnClick()
	{
		Application.LoadLevel(2);
	}

	public void HighScoreBtnClick(){
		Application.LoadLevel(4);
	}

	public void CreditsBtnClick(){
		Application.LoadLevel(6);
	}

	public void HighScoreBack(){
		Application.LoadLevel(1);
	}
}
