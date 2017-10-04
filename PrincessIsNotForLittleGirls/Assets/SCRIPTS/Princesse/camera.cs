using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	private const float ANGLE_MIN_Y = 30f;
	private const float ANGLE_MAX_Y = 50f;
    
	public GameObject cible;
	public float vitesse_rotation = 5;
	public float horizontal;
	public float vertical;
    public float distanceMax;
    public float hauteurFocus;
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

        Vector3 direction = this.transform.position - cible.transform.position;
        RaycastHit hitInfo;

        Physics.Raycast(cible.transform.position, direction, out hitInfo);

        float distance = hitInfo.distance == 0.0f ? distanceMax : Mathf.Min(hitInfo.distance, distanceMax);
        
        Vector3 dir = new Vector3 (0, 0,-distance);
		Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);
		camera_transform.position = cible.transform.position + rotation * dir;
		camera_transform.LookAt (cible.transform.position + new Vector3(0.0f, hauteurFocus, 0.0f));

		camera_transform.transform.Rotate(new Vector3(-20, 0, 0));
	}
}
