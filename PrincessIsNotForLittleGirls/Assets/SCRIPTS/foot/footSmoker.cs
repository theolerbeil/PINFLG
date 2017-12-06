using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footSmoker : MonoBehaviour {

	public Transform smokeEffect;
	private bool isTriggered;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if (!isTriggered && other.tag.Equals ("wall")) {

			Instantiate (smokeEffect, this.transform.position, smokeEffect.transform.rotation);
		}
	}

	void OnTriggerExit(Collider other){

		if (isTriggered && other.tag.Equals ("wall")) {
			isTriggered = false;
		}
	}
}
