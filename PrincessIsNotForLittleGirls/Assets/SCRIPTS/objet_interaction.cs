using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objet_interaction : MonoBehaviour {

	public GameObject princesse;
	public float distance_activation;

	private Vector3 distance_princesse;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		distance_princesse = princesse.transform.position - transform.position;

		bool action = Input.GetKeyDown (KeyCode.E);

		if (distance_princesse.magnitude < distance_activation && action) {
			Debug.Log ("mdctyv");
		}
	}
}
