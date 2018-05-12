using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour {

	public Transform target;
	public Transform trackedObject;
	public Vector3 offset;
	public Vector3 angularOffset;

	public void start()
	{
		target.position = trackedObject.transform.position + offset;
	}

	public void LateUpdate()
	{
		target.position = trackedObject.transform.position + offset;
		target.eulerAngles = trackedObject.transform.eulerAngles + angularOffset;
	}
}
