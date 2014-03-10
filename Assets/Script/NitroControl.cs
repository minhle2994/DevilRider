using UnityEngine;
using System.Collections;

public class NitroControl : MonoBehaviour {
	float nitro = 0;
	private float maxSpeed = 40f;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		GUI.Box (new Rect (10, 50, 100, 20), "nitro");
		if (nitro > 0)
			GUI.Button (new Rect (10, 50, nitro, 20), "");
	}
	
	// Update is called once per frame
	void Update () {
		float currentSpeed = this.GetComponent<PlayerControl> ().MovingSpeed;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0 && nitro > 0) {
				this.GetComponent<PlayerControl> ().MovingSpeed = Mathf.Min(currentSpeed+0.7f, maxSpeed);
				
				nitro -= 1;
			}
			else 
				if (this.GetComponent<PlayerControl> ().MovingSpeed > 15)
					this.GetComponent<PlayerControl> ().MovingSpeed -= 0.03f;
		} 
		else {
			if (Input.GetKey(KeyCode.UpArrow) && nitro > 0){
				this.GetComponent<PlayerControl> ().MovingSpeed = Mathf.Min(currentSpeed+0.7f, maxSpeed);
				nitro -= 1;
			}
			else 
				if (this.GetComponent<PlayerControl> ().MovingSpeed > 15)
					this.GetComponent<PlayerControl> ().MovingSpeed -= 0.03f;
				this.GetComponent<PlayerControl>();
		}
		
		if (nitro == 0 && this.GetComponent<PlayerControl> ().MovingSpeed > 15)
			this.GetComponent<PlayerControl> ().MovingSpeed -= 0.03f;    
	}
	
	void OnTriggerEnter (Collider other){
		if (other.name == "Nitro") {
			Vector3 np = other.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
			other.transform.position = np;
			
			nitro += 20;
			if (nitro > 100)
				nitro = 100;
			
		}
	}
}
