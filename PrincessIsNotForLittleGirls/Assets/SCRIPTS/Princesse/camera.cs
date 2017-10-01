using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	private const float ANGLE_MIN_Y = 0.0f;
	private const float ANGLE_MAX_Y = 50.0f;
    
	public GameObject cible;
	public float vitesse_rotation = 5;
	public float horizontal;
	public float vertical;
	public float distance = 10.0f;
	public Transform camera_transform;

	void Start() {
		camera_transform = transform;
	}

	void Update(){
		horizontal += Input.GetAxis("Mouse X");
		vertical += Input.GetAxis("Mouse Y");

		vertical = Mathf.Clamp (vertical, ANGLE_MIN_Y, ANGLE_MAX_Y);
	}


	void LateUpdate() { 

		Vector3 dir = new Vector3 (0, 0,-distance);
		Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);
		camera_transform.position = cible.transform.position + rotation * dir;
		camera_transform.LookAt (cible.transform.position);


	}
}
