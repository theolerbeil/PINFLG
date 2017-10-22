﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_degatsRecu : ia_etat {

	public float forceReculeVertical;
	public float forceReculeHorizontal;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		anim.Play("degatsRecu");
		rb.AddForce ((this.transform.forward * -forceReculeHorizontal) + (this.transform.up * forceReculeVertical));
	}

	public override void faireEtat()
	{
		if (agent.isActualAnimation("idleCombat")) {
			changerEtat (GetComponent<gob_E_combat>());
		}
	}

	public override void sortirEtat()
	{

	}
}
