using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_garder : ia_etat {

	public ia_pointInteret emplacementAGarder;
	public float vitesse;
	public float delaiMinEntreRepos;
	public float delaiMaxEntreRepos;

	private bool enDeplacement;
	private bool enRotation;
	private bool enGarde;
	private float delaiReposActuel;


	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		nav.enabled = true;
		nav.speed = vitesse;
		agent.definirDestination(emplacementAGarder);
		setAnimation ("running");
		enDeplacement = true;
		enRotation = false;
		enGarde = false;
	}

	public override void faireEtat()
	{
		if (agent.princesseReperee ()) {
			changerEtat (this.GetComponent<tro_E_poursuite> ());

		} else if (!enDeplacement && agent.princesseRepereeAvecAttention ()) {
			changerEtat (this.GetComponent<tro_E_poursuite> ());

		} else if (enDeplacement) {
			if (agent.destinationCouranteAtteinte ()) {
				nav.enabled = false;
				enDeplacement = false;
				enRotation = true;
			}
		} else if (enRotation) {

			setAnimation ("tourner");
			enRotation = agent.seTournerDansOrientationDe (emplacementAGarder.gameObject);

		} else if (!enGarde && !enRotation) {
			enGarde = true;
			delaiReposActuel = Time.time + (Random.value * (delaiMaxEntreRepos - delaiMinEntreRepos)) + delaiMinEntreRepos;
			setAnimation ("garder");
			
		} else if (Time.time >= delaiReposActuel) {
			changerEtat (GetComponent<tro_E_repos> ());
		}
	}

	public override void sortirEtat()
	{

	}
}
