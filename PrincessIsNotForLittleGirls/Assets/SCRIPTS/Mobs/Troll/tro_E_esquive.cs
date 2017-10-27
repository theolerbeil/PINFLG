using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_esquive : ia_etat {

	public float forceReculeVertical;
	public float forceReculeHorizontal;

	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		anim.Play("esquive");
		rb.AddForce ((this.transform.forward * -forceReculeHorizontal) + (this.transform.up * forceReculeVertical));
		timer = Time.time + 0.1f;
	}

	public override void faireEtat()
	{
		if (Time.time > timer && agent.estAuSol()) {
			changerEtat (GetComponent<tro_E_combat>());
		}
	}

	public override void sortirEtat()
	{

	}
}
