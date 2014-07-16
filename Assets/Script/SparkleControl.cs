using UnityEngine;
using System.Collections;

public class SparkleControl : MonoBehaviour {
	public GameObject gun;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = gun.transform.position;
	}
}
