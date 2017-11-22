using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_objetProgression : MonoBehaviour {

	private List<EnumObjetProgression> listObjet;

	// Use this for initialization
	void Start () {
		listObjet = new List<EnumObjetProgression> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void addItem(EnumObjetProgression objetProgression,GameObject objetRammasser){
		listObjet.Add (objetProgression);
		objetRammasser.SetActive(false);
	}
}

public enum EnumObjetProgression
{
	caisse,
}