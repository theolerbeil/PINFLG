﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_attackPuissante : ia_etat {

	public int pourcentageUtilisationAttaquePuissante;
	public int degatsAttaquePuissante;
	public float forceDeSautAttaquePuissante;
	public float forceAvancementAttaquePuissante;

	private bool degatsAttaqueEffectues;
	private triggerArme colliderArme;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		colliderArme = GetComponent<triggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		int rand = Random.Range (1, 100);

		if (rand <= pourcentageUtilisationAttaquePuissante) {

			degatsAttaqueEffectues = false;
			rb.AddForce (this.transform.up * forceDeSautAttaquePuissante + this.transform.forward * forceAvancementAttaquePuissante);
			setAnimation ("attackPuissante");

		} else {

			changerEtat(this.GetComponent<gob_E_combat>());
		}
	}

	public override void faireEtat()
	{
		if (!agent.isActualAnimation("idleCombat")) { // l'attaque puissante est toujours en cours
			
			if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

				princesseVie.blesser (degatsAttaquePuissante);
				degatsAttaqueEffectues = true;
			}

		} else {
			changerEtat(this.GetComponent<gob_E_combat>());
		}
	}

	public override void sortirEtat()
	{

	}
}
