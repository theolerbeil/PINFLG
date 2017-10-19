using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerArme : MonoBehaviour {

	private bool princesseTouchee;

	private GameObject princesse;

	// Use this for initialization
	void Start () {
		princesse = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = true;
		}
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = false;
		}
	}

	public bool IsPrincesseTouchee(){
		return princesseTouchee;
	}
}
