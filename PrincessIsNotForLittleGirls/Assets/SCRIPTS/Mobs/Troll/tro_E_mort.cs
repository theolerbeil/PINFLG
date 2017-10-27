using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_mort : ia_etat {

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation("mort");
		anim.enabled = false;
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
	}

	public override void faireEtat()
	{
		
	}

	public override void sortirEtat()
	{

	}
}
