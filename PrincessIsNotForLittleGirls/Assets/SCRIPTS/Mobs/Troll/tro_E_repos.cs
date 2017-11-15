using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_repos : ia_etat {

	public float dureeReposMax;
	public float dureeReposMin;
	public float pourcentageAuditionEnDormant;
	public ia_etat etatRetour;

	private float delaisActuel;
	private bool endormi;
	private bool reveilParPrincesse;
	private bool reveilParTemps;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation("repos");
		delaisActuel = Time.time + (Random.value * (dureeReposMax - dureeReposMin)) + dureeReposMin;
		reveilParPrincesse = false;
		reveilParTemps = false;
		endormi = true;
	}

	public override void faireEtat()
	{
		if(reveilleParLaPrincesse()) {
			setAnimation("running");
			reveilParPrincesse = true;
			endormi = false;
		}

		else if (endormi && Time.time >= delaisActuel) {
			setAnimation("running");
			reveilParTemps = true;
			endormi = false;
		}

		else if (reveilParPrincesse && agent.isActualAnimation("running")) {
			changerEtat(this.GetComponent<tro_E_poursuite>());
		}

		else if (reveilParTemps && agent.isActualAnimation("running")) {
			changerEtat (etatRetour);
		}
	}

	public override void sortirEtat()
	{

	}

	private bool reveilleParLaPrincesse() {
		return endormi && agent.distanceToPrincesse() <= (agent.rayonAudition * pourcentageAuditionEnDormant);
	}
}
