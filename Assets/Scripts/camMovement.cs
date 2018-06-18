using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour {

	public Transform target;
	public Transform cameraTracker;
	public Transform player;
	public float offsetBehindPlayer;
	public float offsetAbovePlayer;
	public Vector3 angularOffset;
	public float smoothFactor;
	public float dampTime;
	Vector3 camVelocity;

	Vector3 targetPosition;

	public void LateUpdate()
	{
		Vector3 viewVector = cameraTracker.position - target.position;
		viewVector.y = 0f;
		viewVector.Normalize ();
		targetPosition = cameraTracker.position + 
						cameraTracker.up * offsetAbovePlayer - viewVector * offsetBehindPlayer;

//		target.position = Vector3.Lerp (target.position, targetPosition, Time.deltaTime * smoothFactor);
		target.position = Vector3.SmoothDamp (target.position, targetPosition, ref camVelocity, dampTime);
		Quaternion diff = Quaternion.LookRotation (cameraTracker.position - targetPosition);
		target.rotation = Quaternion.Slerp (target.rotation, diff, dampTime);
//		target.LookAt(cameraTracker);


//		#region playerLookat
//		// camera look at using the players position instead of camTarget gameobject
//		Vector3 offset = player.position + (Vector3.up * 1.8f);
//		Vector3 viewVector = offset - target.position;
//		viewVector.y = 0f;
//		viewVector.Normalize();
//		targetPosition = offset + player.up * offsetAbovePlayer - viewVector * offsetBehindPlayer;
//		target.position = Vector3.SmoothDamp(target.position, targetPosition, ref camVelocity, dampTime);
//		target.LookAt(offset);
//		#endregion
	}
}
