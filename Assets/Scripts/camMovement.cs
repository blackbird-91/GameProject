using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour {

	public Transform cameraTransform;
	public Transform cameraTracker;
	public Transform player;
	public float offsetBehindPlayer;
	public float offsetAbovePlayer;
	public Vector3 angularOffset;
	public float smoothFactor;
	public float dampTime;
	Vector3 camVelocity;

	Vector3 targetPosition;

	[SerializeField]
	private float camMaxDistance = 0.0f;
	[SerializeField]
	private float camMinDistance = 0.0f;

	[SerializeField]
	private float cameraRotationSpeed;
	[SerializeField]
	private bool isCamRotate;

	public void LateUpdate()
	{
		Vector3 viewVector = cameraTransform.position - cameraTracker.position;

		// camera orbit
		Quaternion camRotateAngle = Quaternion.AngleAxis (Input.GetAxis ("CamRotate") * (-cameraRotationSpeed), Vector3.up);
		viewVector = camRotateAngle * viewVector;

		viewVector.y = 0f;
		viewVector.Normalize ();
		targetPosition = cameraTracker.position + 
						cameraTracker.up * offsetAbovePlayer + viewVector * offsetBehindPlayer;

//		cameraTransform.position = Vector3.SmoothDamp (cameraTransform.position, targetPosition, ref camVelocity, dampTime);
		// smooth damp is a linear interpolator, slerp is spherical interpolator and takes angle into account
		cameraTransform.position = Vector3.Slerp (cameraTransform.position, targetPosition, dampTime);
		Quaternion diff = Quaternion.LookRotation (cameraTracker.position - targetPosition);
		cameraTransform.rotation = Quaternion.Slerp (cameraTransform.rotation, diff, dampTime);
//		camera.LookAt(cameraTracker);

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
