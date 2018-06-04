using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour {

	public Transform target;
	public Transform cameraTracker;
	public float offsetBehindPlayer;
	public float offsetAbovePlayer;
	public Vector3 angularOffset;
	public float smoothFactor;
	Vector3 targetPosition;

	public void LateUpdate()
	{
		Vector3 viewVector = cameraTracker.position - target.position;
		viewVector.y = 0f;
		viewVector.Normalize ();
		targetPosition = cameraTracker.position + 
						cameraTracker.up * offsetAbovePlayer - viewVector * offsetBehindPlayer;

		target.position = Vector3.Lerp (target.position, targetPosition, Time.deltaTime * smoothFactor);
		//target.eulerAngles = cameraTracker.transform.eulerAngles + angularOffset;
		target.LookAt(cameraTracker);
	}
}
