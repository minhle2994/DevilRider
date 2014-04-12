using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

	// Use this for initialization
	void Start () {	
		//this.transform.rotation = Quaternion.Euler (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, 0, 12 * Time.deltaTime), Space.World);
	}
}