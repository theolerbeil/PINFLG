using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_straff : ia_etat {

	public float vitesseStraff;

	private float tempsMaxAvantFinStraff;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation("straff");
		nav.speed = vitesseStraff;
		nav.enabled = true;

		//float theta = ((Random.value * 0.3f) + 0.1f) * (Mathf.PI / 4.0f);
		float theta = ((Random.value * 0.6f) + 0.3f) * Mathf.PI / 2.0f;
		Vector3 dirPrincesse = -agent.directionToPrincesseDansPlanY0 ();
		Vector3 orthogonalDirPrincesse = Vector3.Cross (dirPrincesse, princesse.transform.up);

		Vector3 destination = princesse.transform.position + (dirPrincesse * agent.distanceCombatOptimale * Mathf.Cos (theta)) + ( (Random.value <= 0.5 ? -1.0f : 1.0f) * orthogonalDirPrincesse * agent.distanceCombatOptimale * Mathf.Sin (theta));

		tempsMaxAvantFinStraff = Time.time + 5.0f;
		agent.definirDestination (destination);

	}

	public override void faireEtat()
	{
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse () <= agent.distanceRepousse) {

			changerEtat (GetComponent<gob_E_combat> ());

		} else if (agent.destinationCouranteAtteinte () || Time.time >= tempsMaxAvantFinStraff) {
			changerEtat (GetComponent<gob_E_combat> ());
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
