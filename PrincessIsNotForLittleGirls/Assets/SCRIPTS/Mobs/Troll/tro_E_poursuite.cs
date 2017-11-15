using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_poursuite : ia_etat {

	public float vitesse;
	public float dureeRecherchePrincesse;
	public float distanceEntreeCombat;

	public ia_etat etatSiPrincessePerdue;

	private Vector3 dernierePositionPrincesseConnue;
	private bool princessePerdue;
	private float delaiActuelRecherche;
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
		delaiActuelRecherche = 0.0f;
		dernierePositionPrincesseConnue = princesse.transform.position;
		agent.definirDestination(dernierePositionPrincesseConnue);
		princessePerdue = false;
		enRotation = true;
	}

	public override void faireEtat()
	{

		if (!princessePerdue && !agent.princesseReperee ()) {
			if (delaiActuelRecherche == 0.0f) {
				nav.enabled = false;
				princessePerdue = true;
				delaiActuelRecherche = Time.time + dureeRecherchePrincesse;
				setAnimation ("searching");
				enRotation = false;
			}

		} else if (princessePerdue) {
			if (Time.time <= delaiActuelRecherche) {

				if (agent.princesseRepereeAvecAttention ()) {
					nav.enabled = true;
					setAnimation ("running");
					princessePerdue = false;
					delaiActuelRecherche = 0.0f;
					dernierePositionPrincesseConnue = princesse.transform.position;
					agent.definirDestination (dernierePositionPrincesseConnue);
					enRotation = true;

				}
			} else {
				changerEtat (etatSiPrincessePerdue);
			}
		} else if (!dernierePositionPrincesseConnue.Equals (princesse.transform.position)) {

			dernierePositionPrincesseConnue = princesse.transform.position;
			agent.definirDestination (dernierePositionPrincesseConnue);
			enRotation = true;
		}

		if (enRotation) {

			enRotation = agent.seTournerVersPosition (dernierePositionPrincesseConnue);
		}
		
		if (agent.distanceToPrincesse() <= distanceEntreeCombat) {

			changerEtat (this.GetComponent<tro_E_combat> ());
		
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
