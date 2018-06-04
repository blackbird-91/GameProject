using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	[SerializeField]
	private Transform charPosition;
	public Animator erikaAnimController;
	// Use this for initialization
	float currentSpeed = 0.0f;
	public float lerpFactor = 1.0f;
	float currentDirection = 0.0f;
	[SerializeField]
	private Transform camFollow;
	[SerializeField]
	private Transform cameraTransform;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		currentSpeed = Input.GetAxis("Vertical");
		currentDirection = Input.GetAxis ("Horizontal");
		float speed = Mathf.Abs(currentSpeed) + Mathf.Abs(currentDirection);
		speed = Mathf.Clamp (speed, -1.0f, 1.0f);
		erikaAnimController.SetFloat ("Speed", speed);
//		erikaAnimController.SetFloat ("xdirection", currentDirection);

		// draw debug ray for the stick direction
		Vector3 stickDirection = new Vector3(currentDirection, 0, currentSpeed);
		Debug.DrawRay (new Vector3 (charPosition.position.x, charPosition.position.y + 2.0f, charPosition.position.z), stickDirection, Color.blue);

		Vector3 camDirection = cameraTransform.forward;
		camDirection.y = 0.0f;
		Quaternion refShift = Quaternion.FromToRotation (Vector3.forward, camDirection);
		Vector3 MoveDirection = refShift * stickDirection;

		Quaternion charRotation = Quaternion.FromToRotation (charPosition.forward, MoveDirection);
		charPosition.rotation = Quaternion.Slerp(charPosition.rotation, charPosition.rotation * charRotation, 3.0f * Time.deltaTime);

		Debug.DrawRay (new Vector3 (charPosition.position.x, charPosition.position.y + 2.0f, charPosition.position.z), MoveDirection, Color.green);
	}
}
