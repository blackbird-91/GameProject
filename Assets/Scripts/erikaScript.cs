using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erikaScript : MonoBehaviour {

	public GameObject bow;
	public void removeBow()
	{
		Debug.Log ("removed");
		GetComponent<Animator> ().SetBool ("disarm", false);
		bow.GetComponent<SkinnedMeshRenderer> ().enabled = false;
	}
}
