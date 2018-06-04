using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityScript : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 orbitVector = target.position - transform.position;

		Quaternion orbitRotation = Quaternion.LookRotation (orbitVector);
		transform.rotation = Quaternion.Slerp (transform.rotation, orbitRotation, Time.deltaTime);
		transform.Translate (0f, 0f, 5 * Time.deltaTime);
	}
}
