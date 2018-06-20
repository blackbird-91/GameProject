using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charInput : MonoBehaviour {

	[SerializeField]
	private Transform charPosition;
	public Animator erikaAnimController;
	// Use this for initialization
	float currentSpeed = 0.0f;
	float currentDirection = 0.0f;
	[SerializeField]
	private Transform cameraTransform;
	[SerializeField]
	private Rigidbody erikaBody;


//	variables related to jumping
	// fall distance used to trigger the jump loop animation
	private float fallDistance = 0.0f;
	[SerializeField]
	private float jumpMultiplier = 1.0f;
	[SerializeField]
	private float jumpDistance = 5.0f;
	[SerializeField]
	private float fallMultiplier = 2.0f;
	private RaycastHit hit;


	private Vector3 MoveDirection = Vector3.zero;
//	private bool isCharGrounded = true;
	private CapsuleCollider erikaCollider;
//	private Vector3 colHeight;
	private float colHeight;
//	float jumpPos = 0.0f;
	Vector3 colliderCenter = Vector3.zero;
	private bool jumpPressed = false;

	// animation names
	static int idleState = Animator.StringToHash("Idle");
	static int moveState = Animator.StringToHash("Movement");
	static int jumpState = Animator.StringToHash("Jump");
	static int fallState = Animator.StringToHash("fall a loop");
	static int fallLandState = Animator.StringToHash("fall a land to idle");

	// weapon properties
	private bool weaponDrawn = false;
	private bool arrowDrawn = false;
	private float weaponCharge = 0.0f;

	AnimatorStateInfo erikaState;

	void Start () {
		erikaCollider = erikaBody.transform.GetComponent<CapsuleCollider>();
//		colHeight = erikaCollider.size;
		erikaState = erikaAnimController.GetCurrentAnimatorStateInfo(0);
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

		if (Input.GetButtonDown ("Jump") && erikaState.shortNameHash == moveState) {
			erikaAnimController.SetTrigger ("Jump");
			jumpPressed = true;
		}

		if (Input.GetButton ("Fire1") && !arrowDrawn) {
			erikaAnimController.SetTrigger ("drawArrow");
			arrowDrawn = true;
		} else if (arrowDrawn && Input.GetButtonUp("Fire1")) {
			erikaAnimController.SetTrigger ("releaseArrow");
			arrowDrawn = false;
		}
	}

	void FixedUpdate()
	{
		Quaternion charRotation = Quaternion.FromToRotation (charPosition.forward, MoveDirection);
		Quaternion newRotation = charPosition.rotation * charRotation;
		charPosition.rotation = Quaternion.Slerp(charPosition.rotation, newRotation, 3.0f * Time.fixedDeltaTime);

		erikaState = erikaAnimController.GetCurrentAnimatorStateInfo(0);
		isGrounded();
//						colHeight.y = erikaAnimController.GetFloat("colliderHeight");
		if (!erikaAnimController.IsInTransition(0)){
			if (jumpPressed) {
				erikaBody.velocity += Vector3.up * jumpMultiplier;
//				erikaBody.velocity += erikaBody.transform.forward * jumpDistance;
				jumpPressed = false;
			}
			if (erikaState.shortNameHash == jumpState || erikaState.shortNameHash == fallState) {
				if (erikaState.shortNameHash != fallState) {
//					jumpPos = erikaAnimController.GetFloat ("JumpCurve") * jumpMultiplier;
//					erikaBody.transform.Translate (Vector3.up * jumpPos);
					erikaBody.transform.Translate (Vector3.forward * jumpDistance * Time.fixedDeltaTime);
				}
				colHeight = erikaAnimController.GetFloat ("colliderHeight");
				erikaCollider.height = colHeight;
				colliderCenter = erikaCollider.center;
				colliderCenter.y = erikaAnimController.GetFloat("colliderYPos");
				erikaCollider.center = colliderCenter;
//				cameraTransform.Translate (Vector3.up * jumpPos);
			}
			checkFallLoop ();
		}
		if (erikaBody.velocity.y < 0) {
			erikaBody.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
		}
	}



	public void checkFallLoop()
	{
		if (erikaState.shortNameHash == fallState) {
			erikaAnimController.SetBool ("Fall", false);
			if (fallDistance <= 0.3f)
				erikaAnimController.SetTrigger ("FallLand");
			else
				return;
		} else if (fallDistance > 5f)
			erikaAnimController.SetBool ("Fall", true);
		else
			erikaAnimController.MatchTarget (hit.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask (new Vector3 (0, 1, 0), 0), 0.22f, 0.68f);
	}


	void isGrounded()
	{
		Ray downRay = new Ray (erikaCollider.bounds.min, -erikaBody.transform.up);
		if (Physics.Raycast (downRay, out hit, 20f)) {
			fallDistance = hit.distance;
			if (hit.distance <= 0.2f && hit.collider.CompareTag ("Floor"))
				return;
		}
		return;
	}
//	void LateUpdate()
//	{
//		if (isInJump())
//			cameraTransform.Translate (Vector3.up * (erikaBody.transform.position.y - oldY));
//	}
}
