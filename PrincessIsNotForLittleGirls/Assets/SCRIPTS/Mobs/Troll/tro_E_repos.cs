using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_repos : ia_etat {

	public float dureeReposMax;
	public float dureeReposMin;
	public float pourcentageAuditionEnDormant;
	public ia_etat etatRetour;

	private float delaisActuel;

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
	}

	public override void faireEtat()
	{
		if(reveilleParLaPrincesse()) {
			changerEtat(this.GetComponent<tro_E_poursuite>());
		}

		else if (Time.time >= delaisActuel) {
			changerEtat (etatRetour);
		}
	}

	public override void sortirEtat()
	{

	}

	private bool reveilleParLaPrincesse() {
		return agent.distanceToPrincesse() <= (agent.rayonAudition * pourcentageAuditionEnDormant);
	}
}
