using UnityEngine;
using System.Collections;

public class ghostRiderControl : MonoBehaviour {
	private Animator ghostRiderAnimator;
	// Use this for initialization
	void Start () {
		 ghostRiderAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0, 0, 12 * Time.deltaTime), Space.World);

		if (Camera.main.transform.position.z >= (this.transform.position.z + 20)) {
			Vector3 np = this.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4f, 4f), 0, Random.Range(80, 90));
			this.transform.position = np;
			if (this.renderer.enabled == false)
				this.renderer.enabled = true;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.name == "Gun") {
			Vector3 np = other.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
			other.transform.position = np;
		}
		if (other.name == "Nitro") {
			Vector3 np = other.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
			other.transform.position = np;
		}
		if (other.name == "DevilRider") {		
			ghostRiderAnimator.Play("Dead");		
		}
	}
}
