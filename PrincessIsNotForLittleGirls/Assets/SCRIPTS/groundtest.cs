﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundtest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate (Vector3.forward * 0.1f);
	}
}
