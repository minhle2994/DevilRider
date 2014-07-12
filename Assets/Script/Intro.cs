using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	public UIButton MainBtn;
	public UIButton RestartBtn;
	public UIButton ResumeBtn;
	//public AudioClip backgroundMusic;

	// Use this for initialization
	void Start () {
		MainBtn.gameObject.SetActive (false);
		RestartBtn.gameObject.SetActive (false);
		ResumeBtn.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void PauseBtnClick()
	{
		Time.timeScale = 0;
		MainBtn.gameObject.SetActive (true);
		RestartBtn.gameObject.SetActive (true);
		ResumeBtn.gameObject.SetActive (true);
	}
	
	public void PlayBtnClick()
	{
		Application.LoadLevel(2);
	}

	public void MainBack(){
		Application.LoadLevel(1);
	}
	
	public void ResumeBtnClick(){
		MainBtn.gameObject.SetActive (false);
		RestartBtn.gameObject.SetActive (false);
		ResumeBtn.gameObject.SetActive (false);
		Time.timeScale = 1;
	}
}
