using UnityEngine;
using System.Collections;

public class hellmountainControl : MonoBehaviour {
	//public GameObject player;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z + 90);
	}
}
