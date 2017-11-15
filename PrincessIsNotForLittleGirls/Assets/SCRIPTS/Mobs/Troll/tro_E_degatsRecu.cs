using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_degatsRecu : ia_etat {

	public float forceReculeVertical;
	public float forceReculeHorizontal;

	private float facteurRecule;

	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		nav.enabled = false;
		facteurRecule = princesseArme.getFacteurReculeArmeActuelle();
		anim.Play("degatsRecu");

		Vector3 directionRecule = (this.transform.position - princesse.transform.position).normalized;

		rb.velocity = Vector3.zero;
		rb.AddForce ((directionRecule * (forceReculeHorizontal * facteurRecule)) + (this.transform.up * (forceReculeVertical * facteurRecule)));
		timer = Time.time + 1.0f;
	}

	public override void faireEtat()
	{
		if (Time.time > timer) {
			changerEtat (GetComponent<tro_E_combat>());
		}
	}

	public override void sortirEtat()
	{

	}

	public void setFacteurRecule(float facteurRecule){
		this.facteurRecule = facteurRecule;
	}
}
