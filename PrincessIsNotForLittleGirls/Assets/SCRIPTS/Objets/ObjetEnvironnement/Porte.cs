using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : ObjetEnvironnement {

	public EnumArmes armeCourante;
	public EnumObjetProgression[] objetNecessaire;
	private  Animator anim;
	private princesse_objetProgression princesse;
	public ia_agent[] ennemiMort;
	public Levier levier;


	// Use this for initialization
	void Start () {
		princesse= GameObject.FindGameObjectWithTag("Player").GetComponent<princesse_objetProgression>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	override
	public void Activation()
	{
		if (isActivable() && utilisable) {
			anim.SetBool("isOpen", true);
			utilisable = false;
			foreach (EnumObjetProgression objet in objetNecessaire) {
				princesse.removeItem (objet);
			}
		}

	}


	private bool isActivable(){
		Dictionary<EnumObjetProgression,int> listObjetPrincesse;
		listObjetPrincesse = new Dictionary<EnumObjetProgression, int> ();
		foreach (EnumObjetProgression objet in princesse.listObjet) {
			if(listObjetPrincesse.ContainsKey(objet)){
				listObjetPrincesse[objet]++;
			}else{
				listObjetPrincesse.Add (objet, 1);
			}
		}

		if (armeCourante != EnumArmes.vide) {
			if (GameControl.control.ArmeCourante != armeCourante) {
				return false;
			}
		}

		foreach (EnumObjetProgression objet in objetNecessaire) {
			if (!listObjetPrincesse.ContainsKey (objet)) {
				return false;
			} else {
				if (listObjetPrincesse [objet] > 1) {
					listObjetPrincesse [objet]--;
				} else {
					listObjetPrincesse.Remove (objet);
				}
			}
		}

		foreach (ia_agent ia in ennemiMort) {
			if(ia.estEnVie()){
				return false;
			}
		}

		if (levier != null) {
			if (!levier.etat) {
				return false;

			}
		}

		return true;
	}


}
