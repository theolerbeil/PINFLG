using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : ObjetEnvironnement {

	public EnumArmes armeCourante;
	public EnumObjetProgression[] objetNecessaire;
	private  Animator anim;
	private princesse_objetProgression princesse;
	public ia_agent[] ennemiMort;


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
		if (armeCourante != EnumArmes.vide) {
			if (GameControl.control.ArmeCourante != armeCourante) {
				return false;
			}
		}

		foreach (EnumObjetProgression objet in objetNecessaire) {
			if(!princesse.listObjet.Contains(objet)){
				return false;
			}
		}

		foreach (ia_agent ia in ennemiMort) {
			if(ia.estEnVie()){
				return false;
			}
		}

		return true;
	}


}
