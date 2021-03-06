﻿using UnityEngine;
using System.Collections;

public class NitroControl : MonoBehaviour {
	float nitro = 0;
	public bool nitroState = false;
	private float maxSpeed = 60f;
	public GameObject player;
	public PlayerControl playerControl;
	public GameObject effect;

	float currentSpeed;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("xe");
		playerControl = player.GetComponent<PlayerControl> ();
		effect.transform.position = this.transform.position;
	}
	
	void OnGUI(){
		GUI.Box (new Rect (Screen.width/2 - 100, 30, 200, 40), "nitro");
		if (nitro > 0)
			GUI.Button (new Rect (Screen.width/2 - 100, 30, nitro*2, 40), "");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0 * Time.deltaTime, 120* Time.deltaTime, 0 * Time.deltaTime), Space.World);
		maxSpeed = playerControl.baseSpeed + 20;
		handleWhenCameraOver ();
		currentSpeed = playerControl.MovingSpeed;
		handleWithNitro ();
	}

	void handleWhenCameraOver(){
		if (Camera.main.transform.position.z >= (this.transform.position.z + 20)) {
			Vector3 np = this.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(300, 400));
			this.transform.position = np;
			effect.transform.position = this.transform.position;
			if (this.renderer.enabled == false)
				this.renderer.enabled = true;
		}
	}

	void handleWithNitro(){
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0 && nitro > 0) {
				nitroActionOn();
			}
			else{
				nitroActionOff();
			}
		} 
		else {
			if ((Input.GetKey(KeyCode.UpArrow)) && (nitro > 0)){
				nitroActionOn();
			}
			else{
				nitroActionOff();
			}
		}
		
		if (nitro == 0 && playerControl.MovingSpeed > playerControl.baseSpeed)
			playerControl.MovingSpeed -= 0.3f;
	}

	void nitroActionOn(){
		playerControl.MovingSpeed = Mathf.Min(currentSpeed+0.7f, maxSpeed);
		nitro -= 0.05f;
		playerControl.devilRiderAnimator.SetBool ("Nitro", true);
		playerControl.nitroState = true;
	}

	void nitroActionOff(){
		playerControl.devilRiderAnimator.SetBool ("Nitro", false);
		if (playerControl.MovingSpeed > playerControl.baseSpeed)
			playerControl.MovingSpeed -= 0.3f;
		playerControl.nitroState = false;
	}

	void OnTriggerEnter (Collider other){
		if (other.name == "xe") {

			Vector3 np = transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(300, 400));
			transform.position = np;
			effect.transform.position = this.transform.position;
			nitro += 20;
			if (nitro > 100) nitro = 100;
		}
	}
}
