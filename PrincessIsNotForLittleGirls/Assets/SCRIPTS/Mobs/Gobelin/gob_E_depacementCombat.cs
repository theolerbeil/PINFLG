using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_depacementCombat : ia_etat {

	public float vitesse;
	public float distanceSortieCombat;

	private Vector3 dernierePositionPrincesseConnue;
	private float distancePrincesse;

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
		dernierePositionPrincesseConnue = princesse.transform.position;
		agent.definirDestination(dernierePositionPrincesseConnue);
	}

	public override void faireEtat()
	{
		if (agent.princesseReperee ()) {

			if (!dernierePositionPrincesseConnue.Equals (princesse.transform.position)) {

				dernierePositionPrincesseConnue = princesse.transform.position;
				agent.definirDestination (dernierePositionPrincesseConnue);
			}

			distancePrincesse = agent.distanceToPrincesse ();

			if (distancePrincesse >= distanceSortieCombat) {
				changerEtat (this.GetComponent<gob_E_poursuite> ());
			} else if (distancePrincesse <= agent.distanceCombatOptimale) {
				changerEtat (this.GetComponent<gob_E_combat> ());
			}

		} else {
			changerEtat(this.GetComponent<gob_E_poursuite>());
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
