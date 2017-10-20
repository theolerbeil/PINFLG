using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_poursuite : ia_etat {

	public float vitesse;
	public float dureeRecherchePrincesse;
	public float distanceEntreeCombat;

	private Vector3 dernierePositionPrincesseConnue;
	private bool princessePerdue;
	private float delaiActuelRecherche;


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
	}

	public override void faireEtat()
	{

		if (!princessePerdue) {

			if (agent.princesseReperee () && !dernierePositionPrincesseConnue.Equals (princesse.transform.position)) {
				
				dernierePositionPrincesseConnue = princesse.transform.position;
				agent.definirDestination (dernierePositionPrincesseConnue);
			}
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
			}

			if(Time.time <= delaiActuelRecherche) {

				if (agent.princesseRepereeAvecAttention ()) {
					setAnimation("running");
					princessePerdue = false;
					delaiActuelRecherche = 0.0f;
					dernierePositionPrincesseConnue = princesse.transform.position;
					agent.definirDestination (dernierePositionPrincesseConnue);

				}
			} else {
				changerEtat (GetComponent<gob_E_patrouille> ());
			}
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
