using UnityEngine;
using System.Collections;

public class SlopeLooping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.transform.position.z >= (this.transform.position.z + 30)) {
            Vector3 np = this.transform.position;
            np.x = 0;
            np += new Vector3(Random.Range(-3.5f, 3.5f), 0, Random.Range(400, 600));
            this.transform.position = np;
            if (this.renderer.enabled == false)
                this.renderer.enabled = true;
        }
	}
}
