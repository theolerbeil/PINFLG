using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_garder : ia_etat {

	public ia_pointInteret emplacementAGarder;
	public float vitesse;

	private bool enDeplacement;
	private bool enRotation;

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
		enRotation = false;
	}

	public override void faireEtat()
	{
		if (agent.princesseRepereeAvecAttention ()) {
			changerEtat (this.GetComponent<tro_E_poursuite> ());
		} else if (enDeplacement) {
			if (agent.destinationCouranteAtteinte ()) {
				nav.enabled = false;
				enDeplacement = false;
				setAnimation ("garder");
				enRotation = true;
			}
		} else if (enRotation) {

			enRotation = agent.seTournerDansOrientationDe (emplacementAGarder.gameObject);
		}
	}

	public override void sortirEtat()
	{

	}
}
