using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_mort : ia_etat {

	public float delaiAvantDisparition;

	private float actualDelai;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		anim.Play("mort");
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		actualDelai = Time.time + delaiAvantDisparition;
	}

	public override void faireEtat()
	{
		if(Time.time >= actualDelai){
			this.gameObject.SetActive (false);
		}
	}

	public override void sortirEtat()
	{

	}
}
