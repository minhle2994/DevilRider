using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, 0, 12 * Time.deltaTime));

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
        if (other.name == "Coin") {
            Vector3 np = other.transform.position;
            np.x = 0;
            np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(58, 60));
            other.transform.position = np;
        }
        
        if (other.name == "Gun") {
            Vector3 np = other.transform.position;
            np.x = 0;
            np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(58, 60));
            other.transform.position = np;
        }
		if (other.name == "Nitro") {
			Vector3 np = other.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
			other.transform.position = np;
		}
    }
}