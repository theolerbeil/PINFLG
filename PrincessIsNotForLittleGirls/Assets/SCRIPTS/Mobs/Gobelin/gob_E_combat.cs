using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_combat : ia_etat {
	
	public float porteeAttaqueSimple;
	public int degatsAttaqueSimple;

	[Tooltip("Délai en secondes entre deux attaques simples.")]
	public float delaiAttaqueSimple;

	private float delaiActuelAttaqueSimple;
	private bool attaqueSimpleEnCours;
	private Vector3 dernierePositionPrincesseConnue;
	private bool degatsAttaqueEffectues;
	private triggerArme colliderArme;
	private bool princesseEnVue;

    // Use this for initialization
    void Start () {
		base.init (); // permet d'initialiser l'état, ne pas l'oublier !
		delaiActuelAttaqueSimple = 0.0f;
		colliderArme = GetComponent<triggerArme> ();
	}

    public override void entrerEtat()
    {
		setAnimation("idleCombat");
		attaqueSimpleEnCours = false;
		degatsAttaqueEffectues = false;
    }

    public override void faireEtat()
    {
		this.transform.forward = (princesse.transform.position - this.transform.position).normalized;

		if (agent.distanceToPrincesse () > porteeAttaqueSimple) {
			
			changerEtat (GetComponent<gob_E_depacementCombat> ());

		} else {
			
			if (attaqueSimplePrete ()) {
				setAnimation ("attackSimple");
				attaqueSimpleEnCours = true;
				delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple;

			} else {

				if (agent.isActualAnimation ("attackSimple")) {
					setAnimation ("idleCombat");

					if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

						princesseVie.blesser (degatsAttaqueSimple);
						degatsAttaqueEffectues = true;
					}
				} else {

					attaqueSimpleEnCours = false;
					degatsAttaqueEffectues = false;
				}
			}
		}
    }

    public override void sortirEtat()
    {
        
	}

    private bool princesseAttaquableSimplement()
    {
        return princesseVie.enVie() && princesseAPorteeAttaqueSimple() && attaqueSimplePrete();
	}

	private bool princesseAPorteeAttaqueSimple()
	{
		return agent.distanceToPrincesse() <= porteeAttaqueSimple;
	}

	private bool attaqueSimplePrete()
	{
		return Time.time >= delaiActuelAttaqueSimple;
	}
}
