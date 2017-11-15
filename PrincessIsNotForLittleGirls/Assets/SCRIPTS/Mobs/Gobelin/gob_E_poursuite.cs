using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_poursuite : ia_etat {

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

		if (!princessePerdue) {

			if (agent.princesseReperee () && !dernierePositionPrincesseConnue.Equals (princesse.transform.position)) {
				
				dernierePositionPrincesseConnue = princesse.transform.position;
				agent.definirDestination (dernierePositionPrincesseConnue);
				enRotation = true;
			}
		}

		if (enRotation) {

			enRotation = agent.seTournerVersPosition (dernierePositionPrincesseConnue);
		}

		if (agent.distanceToPrincesse() <= agent.distanceCombatOptimale) {

			changerEtat (this.GetComponent<gob_E_combat> ());

		} else if (agent.distanceToPrincesse() <= distanceEntreeCombat && Vector3.Angle(this.transform.forward, princesse.transform.position - this.transform.position) <= 10.0f) {

			changerEtat(this.GetComponent<gob_E_attackPuissante>());

		} else if (agent.destinationCouranteAtteinte ()) {

			if (delaiActuelRecherche == 0.0f) {
				princessePerdue = true;
				delaiActuelRecherche = Time.time + dureeRecherchePrincesse;
				setAnimation ("searching");
				enRotation = false;
			}

			if(Time.time <= delaiActuelRecherche) {

				if (agent.princesseRepereeAvecAttention ()) {
					setAnimation("running");
					princessePerdue = false;
					delaiActuelRecherche = 0.0f;
					dernierePositionPrincesseConnue = princesse.transform.position;
					agent.definirDestination (dernierePositionPrincesseConnue);
					enRotation = true;

				}
			} else {
				changerEtat (etatSiPrincessePerdue);
			}
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
