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
	[SerializeField]
	private Rigidbody erikaBody;
	[SerializeField]
	private float fallMultiplier = 2.5f;
	[SerializeField]
	private float jumpMultiplier = 1.0f;
	[SerializeField]
	private float jumpDistance = 5.0f;


	private float rotationDegreesPerSecond = 120.0f;
	private Vector3 MoveDirection = Vector3.zero;
	private bool isCharGrounded = true;
	private CapsuleCollider erikaCollider;
//	private Vector3 colHeight;
	private float colHeight;
	float oldY = 0.0f;

	void Start () {
		erikaCollider = erikaBody.transform.GetComponent<CapsuleCollider>();
//		colHeight = erikaCollider.size;
	}

	void FixedUpdate()
	{
		Quaternion charRotation = Quaternion.FromToRotation (charPosition.forward, MoveDirection);
		Quaternion newRotation = charPosition.rotation * charRotation;
		charPosition.rotation = Quaternion.Slerp(charPosition.rotation, newRotation, 3.0f * Time.deltaTime);

		if (isInJump ()) {
//						colHeight.y = erikaAnimController.GetFloat("colliderHeight");
			if (!erikaAnimController.IsInTransition(0)){
				oldY = erikaBody.transform.position.y;
//				erikaBody.transform.Translate (Vector3.up * erikaAnimController.GetFloat ("JumpCurve") * jumpMultiplier);
				erikaBody.transform.Translate (Vector3.forward * jumpDistance * Time.deltaTime);

				colHeight = erikaAnimController.GetFloat ("colliderHeight");
				erikaCollider.height = colHeight;

//				cameraTransform.Translate (Vector3.up * (erikaBody.transform.position.y - oldY));
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		currentSpeed = Input.GetAxis("Vertical");
		currentDirection = Input.GetAxis ("Horizontal");

		#region forwardTurn
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
		MoveDirection = refShift * stickDirection;

		Debug.DrawRay (new Vector3 (charPosition.position.x, charPosition.position.y + 2.0f, charPosition.position.z), MoveDirection, Color.green);
		#endregion

		if (Input.GetButtonDown ("Jump")) {
			erikaAnimController.SetTrigger ("Jump");
		}
//		print (erikaBody.velocity);

//		isCharGrounded = isGrounded();
	}

//	bool isGrounded()
//	{
//		Ray downRay = new Ray (erikaBody.transform.position, -erikaBody.transform.up);
//		RaycastHit hit;
//		if (Physics.Raycast (downRay, out hit, 0.2f)) {
//			if (hit.collider.tag == "Floor")
//				return true;
//		}
//		return false;
//	}

	public bool isInJump()
	{
		return (erikaAnimController.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Jump"));
	}

//	void LateUpdate()
//	{
//		if (isInJump())
//			cameraTransform.Translate (Vector3.up * (erikaBody.transform.position.y - oldY));
//	}
}
