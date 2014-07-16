using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {
	public int numberOfBullet = 0;
	public GameObject player;
	public GameObject Effect;
	public PlayerControl playerControl;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("xe");
		playerControl = player.GetComponent<PlayerControl> ();
		Effect.transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(40 * Time.deltaTime, 120* Time.deltaTime, 50 * Time.deltaTime), Space.Self);
		if (Camera.main.transform.position.z >= (this.transform.position.z + 20)) {
			Vector3 np = this.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(80, 90));
			this.transform.position = np;
			Effect.transform.position = this.transform.position;
			if (this.renderer.enabled == false)
				this.renderer.enabled = true;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.name == "xe") {
			Vector3 np = transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(360, 400));
			transform.position = np;
			numberOfBullet = 3;
			Effect.transform.position = this.transform.position;
		}
	}
}
