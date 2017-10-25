using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_combat : ia_etat {
	
	public float porteeAttaqueSimple;
	public int degatsAttaqueSimple;

	[Tooltip("Délai en secondes entre deux attaques simples.")]
	public float delaiAttaqueSimple;

	public float delaiEntreDeuxEsquives;
	public float pourcentageEsquive;

	private float delaiActuelAttaqueSimple;
	private float delaiActuelEntreDeuxEsquives;
	private Vector3 dernierePositionPrincesseConnue;
	private bool degatsAttaqueEffectues;
	private triggerArme colliderArme;
	private bool princesseEnVue;

    // Use this for initialization
    void Start () {
		base.init (); // permet d'initialiser l'état, ne pas l'oublier !
		delaiActuelAttaqueSimple = 0.0f;
		delaiActuelAttaqueSimple = 0.0f;
		colliderArme = GetComponent<triggerArme> ();
	}

    public override void entrerEtat()
    {
		setAnimation("idleCombat");
		degatsAttaqueEffectues = false;
		delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple * Random.value;
    }

    public override void faireEtat()
    {
		this.transform.forward = (princesse.transform.position - this.transform.position).normalized;

		if (agent.distanceToPrincesse () > porteeAttaqueSimple) {
			
			changerEtat (GetComponent<gob_E_depacementCombat> ());

		} else if (esquivePrete() && princesseArme.isAttaqueEnCours() && Vector3.Angle(-princesse.transform.forward, this.transform.forward) <= 20.0f) {

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

						princesseVie.blesser (degatsAttaqueSimple);
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
