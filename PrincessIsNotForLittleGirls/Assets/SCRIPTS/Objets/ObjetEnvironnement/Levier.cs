using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour {

	public ObjetEnvironnement[] listObjetEnvironnement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (ObjetEnvironnement objet in listObjetEnvironnement) {
			objet.Activation ();
		}
	}
}
