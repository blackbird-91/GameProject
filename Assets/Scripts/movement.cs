using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	Transform thisPosition;
	public Animator erikaAnimController;
	// Use this for initialization
	float currentSpeed = 0.0f;
	float lerpFactor = 0.2f;
	float currentDirection = 0.0f;
	void Start () {
		thisPosition = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				currentSpeed = Mathf.Lerp(currentSpeed, 2.0f, lerpFactor);
			} else {
				currentSpeed = Mathf.Lerp(currentSpeed, 1.0f, lerpFactor);
			}
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				currentSpeed = Mathf.Lerp (currentSpeed, -2.0f, 0.2f);
			} else {
				currentSpeed = Mathf.Lerp (currentSpeed, -1.0f, 0.2f);
			}
		}
		else
		{
			if (currentSpeed != 0.0f) {
				currentSpeed = Mathf.Lerp (currentSpeed, 0.0f, lerpFactor);
			}
		}
		erikaAnimController.SetFloat ("Speed", currentSpeed);

		// Movement on the side including running
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			currentDirection = Mathf.Lerp (currentDirection, 1.0f, 0.5f);
		} else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			currentDirection = Mathf.Lerp (currentDirection, -1.0f, 0.5f);
		} else {
			currentDirection = Mathf.Lerp (currentDirection, 0.0f, 0.5f);
		}
		erikaAnimController.SetFloat ("xdirection", currentDirection);


		if (Input.GetKeyDown (KeyCode.E)) {
			erikaAnimController.SetBool ("disarm", true);
		}

	}
}
