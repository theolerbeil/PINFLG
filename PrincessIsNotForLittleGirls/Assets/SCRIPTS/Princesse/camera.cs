using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	private const float ANGLE_MIN_Y = 40f;
	private const float ANGLE_MAX_Y = 40f;
    
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
		Cursor.visible = false;
		Vector3 dir = new Vector3 (0, 0,-distance);
		Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);
		camera_transform.position = cible.transform.position + rotation * dir;
		camera_transform.LookAt (cible.transform.position);

		camera_transform.transform.Rotate(new Vector3(-20, 0, 0));
	}
}
