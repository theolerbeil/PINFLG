using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : ObjetEnvironnement {

	public AudioClip levier;
	public ObjetEnvironnement[] listObjetEnvironnement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool isUtilisable=true;

		foreach(ObjetEnvironnement objet in listObjetEnvironnement){
			if (!objet.utilisable) {
				isUtilisable = false;
			}
		}
		utilisable = isUtilisable;
	}

	override
	public void Activation()
	{
		GetComponent<AudioSource> ().PlayOneShot (levier, 1f);
		foreach (ObjetEnvironnement objet in listObjetEnvironnement) {
			objet.Activation ();
		}
	}
}
