using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	private const float ANGLE_MIN_Y = 20f;
	private const float ANGLE_MAX_Y = 50f;
	private GameObject cible;
	private SkinnedMeshRenderer skinPrincesse;

	public float vitesse_rotation = 5;
	public float horizontal;
	public float vertical;
    public float distanceMax;
    public float hauteurFocus;
	public float distanceAvantTransparence;
	public Transform camera_transform;

	void Start() {
		camera_transform = transform;
		cible = GameObject.FindGameObjectWithTag ("Player");
		skinPrincesse = GameObject.FindGameObjectWithTag ("PrincesseBody").GetComponent<SkinnedMeshRenderer>();
	}

	void Update(){
		if(InputManager.GetKeyAxis("Mouse X") >= 0.8 || InputManager.GetKeyAxis("Mouse X") <= -0.8)
			horizontal += InputManager.GetKeyAxis("Mouse X");
		if(InputManager.GetKeyAxis("Mouse Y") >= 0.8 || InputManager.GetKeyAxis("Mouse Y") <= -0.8)
			vertical += InputManager.GetKeyAxis("Mouse Y");

		vertical = Mathf.Clamp (vertical, ANGLE_MIN_Y, ANGLE_MAX_Y);
		//Debug.Log ("horizonral : " + horizontal);
		//Debug.Log ("Vertical : " + vertical);
	}


	void LateUpdate() { 
		Cursor.visible = false;

        Vector3 direction = this.transform.position - cible.transform.position;
        RaycastHit hitInfo;

        Physics.Raycast(cible.transform.position, direction, out hitInfo);

        float distance = hitInfo.distance == 0.0f ? distanceMax : Mathf.Min(hitInfo.distance, distanceMax);
        
        Vector3 dir = new Vector3 (0, 0,-distance);
		Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);
		camera_transform.position = cible.transform.position + rotation * dir + cible.transform.up * hauteurFocus;
		camera_transform.LookAt (cible.transform.position + cible.transform.up * hauteurFocus + cible.transform.forward * 0.1f);

		camera_transform.transform.Rotate(new Vector3(-20, 0, 0));

		if (distance < distanceAvantTransparence) {
			float alpha = Mathf.Clamp(distance / (distanceAvantTransparence * 0.66f), 0.0f, 1.0f);
			for (int i=0; i<skinPrincesse.materials.Length; i++) {
				skinPrincesse.materials[i].color = new Color (skinPrincesse.materials[i].color.r, skinPrincesse.materials[i].color.g, skinPrincesse.materials[i].color.b, 0.0f);
			}
		} else {
			for (int i=0; i<skinPrincesse.materials.Length; i++) {
				skinPrincesse.materials[i].color = new Color (skinPrincesse.materials[i].color.r, skinPrincesse.materials[i].color.g, skinPrincesse.materials[i].color.b, 1.0f);
			}
		}
	}
}
