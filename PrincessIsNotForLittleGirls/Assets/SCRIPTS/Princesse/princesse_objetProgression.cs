using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_objetProgression : MonoBehaviour {

	public List<EnumObjetProgression> listObjet;
	private affichage_objetActuel hud;

	// Use this for initialization
	void Start () {
		listObjet = new List<EnumObjetProgression> ();
		hud = GameObject.FindGameObjectWithTag ("affichage_ObjetActuel").GetComponent<affichage_objetActuel> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void addItem(EnumObjetProgression objetProgression,GameObject objetRammasser){
		listObjet.Add (objetProgression);
		objetRammasser.SetActive(false);
	}

	public void removeItem(EnumObjetProgression objetProgression){
		listObjet.Remove (objetProgression);
		hud.objetUtilise (objetProgression);
	}
}

public enum EnumObjetProgression
{
	caisse,
	key,
}