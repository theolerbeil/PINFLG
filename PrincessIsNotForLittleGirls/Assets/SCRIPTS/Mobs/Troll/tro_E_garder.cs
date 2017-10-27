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
	}

	public override void faireEtat()
	{
	/*	if (agent.princesseRepereeAvecAttention ()) {
			changerEtat (this.GetComponent<tro_E_poursuite> ());
		} else*/ if (enDeplacement) {
			if (agent.destinationCouranteAtteinte ()) {
				nav.enabled = false;
				enDeplacement = false;
				setAnimation ("garder");
				enRotation = true;
			}
		} else if (enRotation) {

			enRotation = agent.seTournerDansOrientationDe (emplacementAGarder.gameObject);
			delaiReposActuel = Time.time + (Random.value * (delaiMaxEntreRepos - delaiMinEntreRepos)) + delaiMinEntreRepos;

		} else if (Time.time >= delaiReposActuel) {
			changerEtat (GetComponent<tro_E_repos> ());
		}
	}

	public override void sortirEtat()
	{

	}
}
