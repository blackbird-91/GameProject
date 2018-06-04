using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class rotationScript : MonoBehaviour {

	[SerializeField]
	private Transform target;
//	[SerializeField]
//	private Transform stick;
	Transform cameraTransform;
	// Use this for initialization
	void Start () {
		cameraTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 stickDir = new Vector3 (horizontal, 0, vertical);
		Vector3 camDirection = cameraTransform.forward;
		camDirection.y = 0.0f;

		Quaternion referentialShift = Quaternion.FromToRotation (Vector3.forward, camDirection);
		Vector3 move = referentialShift * stickDir;

		Debug.DrawRay(target.position, target.forward, Color.black);
		Debug.DrawRay (target.position, stickDir, Color.blue);
//		stick.position = move;
		Debug.DrawRay(target.position, move, Color.green);
	}
}
