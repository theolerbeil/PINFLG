using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objet : MonoBehaviour {

	public string descriptionObjet;
	public string nomObjet;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract void Activation();
}
