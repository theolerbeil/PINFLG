using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_mort : ia_etat {

	public float delaiAvantDisparition;
	public AudioClip sonMort;

	private float actualDelai;
	private bool sonJoue;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation("mort");
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		actualDelai = Time.time + delaiAvantDisparition;
		sonJoue = false;
	}

	public override void faireEtat()
	{
		if (!agent.getSoundEntity().isPlaying() && !sonJoue){
			agent.getSoundEntity().playOneShot(sonMort,1.0f);
			sonJoue = true;
		}
		if(Time.time >= actualDelai){
			this.gameObject.SetActive (false);
		}
	}

	public override void sortirEtat()
	{

	}
}
