using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_combat : ia_etat {
	
	public float porteeAttaqueSimple;
	public int degatsAttaqueSimple;

	[Tooltip("Délai en secondes entre deux attaques simples.")]
	public float delaiAttaqueSimple;
	public float forceReculeAttaqueSimple;

	public float delaiEntreDeuxEsquives;
	public float pourcentageEsquive;

	private float delaiActuelAttaqueSimple;
	private float delaiActuelEntreDeuxEsquives;
	private bool degatsAttaqueEffectues;
	private triggerArme colliderArme;
	private bool princesseEnVue;

    // Use this for initialization
    void Start () {
		base.init (); // permet d'initialiser l'état, ne pas l'oublier !
		delaiActuelAttaqueSimple = 0.0f;
		delaiActuelEntreDeuxEsquives = 0.0f;
		colliderArme = GetComponent<triggerArme> ();
	}

    public override void entrerEtat()
    {
		setAnimation("idleCombat");
		degatsAttaqueEffectues = false;

		if (attaqueSimplePrete ()) {
			delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple * Random.value;
		}
    }

    public override void faireEtat()
    {
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse () > porteeAttaqueSimple && !agent.isActualAnimation ("attackSimple")) {
			
			changerEtat (GetComponent<gob_E_depacementCombat> ());

		} else if (!agent.isActualAnimation ("attackSimple") && esquivePrete() && princesseArme.isAttaqueEnCours() && Vector3.Angle(-princesse.transform.forward, this.transform.forward) <= 20.0f) {

			delaiActuelEntreDeuxEsquives = Time.time + delaiEntreDeuxEsquives;

			float rand = Random.value;

			if (rand <= pourcentageEsquive) {

				changerEtat (GetComponent<gob_E_esquive> ());
			}

		} else {
			
			if (attaqueSimplePrete ()) {
				setAnimation ("attackSimple");
				delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple;

			} else {

				if (agent.isActualAnimation ("attackSimple")) {
					setAnimation ("idleCombat");

					if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

						princesseVie.blesser (degatsAttaqueSimple, this.gameObject, forceReculeAttaqueSimple);
						degatsAttaqueEffectues = true;
					}
				} else {
					degatsAttaqueEffectues = false;
				}
			}
		}
    }

    public override void sortirEtat()
    {
        
	}

	private bool esquivePrete() {
		return Time.time >= delaiActuelEntreDeuxEsquives;
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
