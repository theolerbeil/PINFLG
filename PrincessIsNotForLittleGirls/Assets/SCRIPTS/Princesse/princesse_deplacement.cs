using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_deplacement : MonoBehaviour {

	public float vitesse;
	public float force_saut;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		new Vector3 (moveHorizontal, 0.0f, moveVertical);

		bool saut = Input.GetKeyDown (KeyCode.Space);

		if (saut) {
			Debug.Log ("vrgx");
			rb.AddForce (new Vector3(0.0f, force_saut, 0.0f));
		}

		transform.position += movement * vitesse;
	}


}
