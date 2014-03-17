using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {
	public int numberOfBullet = 0;
	public GameObject player;
	public PlayerControl playerControl;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("DevilRider");
		playerControl = player.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.transform.position.z >= (this.transform.position.z + 20)) {
			Vector3 np = this.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(58, 60));
			this.transform.position = np;
			if (this.renderer.enabled == false)
				this.renderer.enabled = true;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.name == "DevilRider") {
			Vector3 np = transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
			transform.position = np;
			numberOfBullet = 3;
		}
	}
}
