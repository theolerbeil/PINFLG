﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_vie : MonoBehaviour {

	public int vieMax;
	public Transform hitEffect;

	private int vieCourante;
	private ia_agent agent;

	// Use this for initialization
	void Start () {
		vieCourante = vieMax;
		agent = GetComponent<ia_agent> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void blesser(int degats, Vector3 hitPoint) {

		if (hitEffect != null) {
			Instantiate (hitEffect, hitPoint, hitEffect.transform.rotation);
		}

		blesser (degats);
	}

	public void blesser(int degats) {
		
		vieCourante = Mathf.Max(vieCourante - degats, 0);

		if (!estEnVie()) {
			agent.mourir ();
		}
	}

	public bool estEnVie() {
		return vieCourante > 0;
	}
}
