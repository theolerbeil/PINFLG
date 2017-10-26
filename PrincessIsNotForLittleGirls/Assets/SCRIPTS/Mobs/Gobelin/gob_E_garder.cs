using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_garder : ia_etat {

	public ia_pointInteret emplacementAGarder;
	public float vitesse;

	private bool enDeplacement;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation("running");
		nav.speed = vitesse;
		nav.enabled = true;
		agent.definirDestination(emplacementAGarder);
		enDeplacement = true;
	}

	public override void faireEtat()
	{
		if (agent.princesseRepereeAvecAttention ()) {
			changerEtat (this.GetComponent<gob_E_poursuite> ());
		} else if (enDeplacement) {
			if (agent.destinationCouranteAtteinte()) {
				nav.enabled = false;
				enDeplacement = false;
				setAnimation("garder");
				this.transform.forward = emplacementAGarder.transform.forward;
			}
		}
	}

	public override void sortirEtat()
	{

	}
}
