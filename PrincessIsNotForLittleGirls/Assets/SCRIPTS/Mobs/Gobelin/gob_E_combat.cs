using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_combat : ia_etat {
	
	public float porteeAttaqueSimple;
	public int degatsAttaqueSimple;

	[Tooltip("Délai en secondes entre deux attaques simples.")]
	public float delaiAttaqueSimple;
	public float forceReculeAttaqueSimple;

	public float sautLateralForceAvant;
	public float sautLateralForceCote;
	public float sautLateralForceHauteur;

	public float delaiEntreDeuxEsquives;
	public float pourcentageEsquive;

	public float delaiStraff;
	public float pourcentageStraff;

	public AudioClip sonAttaque;

	private float delaiActuelAttaqueSimple;
	private float delaiActuelEntreDeuxEsquives;
	private float delaiActuelStraff;
	private bool degatsAttaqueEffectues;
	private triggerArme colliderArme;
	private bool princesseEnVue;
	private bool attaqueEnCours;

    // Use this for initialization
    void Start () {
		base.init (); // permet d'initialiser l'état, ne pas l'oublier !
		delaiActuelAttaqueSimple = 0.0f;
		delaiActuelEntreDeuxEsquives = 0.0f;
		colliderArme = GetComponent<triggerArme> ();
		attaqueEnCours = false;
	}

    public override void entrerEtat()
    {
		setAnimation("idleCombat");

		delaiActuelStraff = Time.time + delaiStraff * Random.value;
    }

    public override void faireEtat()
    {
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse () > porteeAttaqueSimple && !attaqueEnCours) {
			
			changerEtat (GetComponent<gob_E_depacementCombat> ());

		} else if (!attaqueEnCours && esquivePrete() && princesseArme.isAttaqueEnCours() && Vector3.Angle(-princesse.transform.forward, this.transform.forward) <= 20.0f) {

			delaiActuelEntreDeuxEsquives = Time.time + delaiEntreDeuxEsquives;

			float rand = Random.value;

			if (rand <= pourcentageEsquive) {

				changerEtat (GetComponent<gob_E_esquive> ());
			}

		} else if (!attaqueEnCours && agent.distanceToPrincesse() <= agent.distanceRepousse) {

			setAnimation ("repousse");
			agent.getAudio().PlayOneShot(sonAttaque,1.0f);
			attaqueEnCours = true;
			degatsAttaqueEffectues = false;
			delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple;
			delaiActuelStraff += 0.5f;
			
		} else if (!attaqueEnCours && Time.time >= delaiActuelStraff) {

			float rand = Random.value;
			delaiActuelStraff = Time.time + delaiStraff;
			if (rand <= pourcentageStraff) {
				changerEtat (GetComponent<gob_E_straff> ());
			}

		} else {
			
			if (!attaqueEnCours && attaqueSimplePrete ()) {

				float aleatoire = Random.value;

				if (aleatoire <= 0.25) {
					setAnimation ("attack1");
				} else if (aleatoire <= 0.5) {
					setAnimation ("attack2");
				} else if (aleatoire <= 0.75) {
					setAnimation ("attack3");
				} else {
					setAnimation ("attack4");
					rb.AddForce (this.transform.right * sautLateralForceCote + this.transform.forward * sautLateralForceAvant + this.transform.up * sautLateralForceHauteur);
				}
				agent.getAudio().PlayOneShot(sonAttaque,1.0f);
				attaqueEnCours = true;
				degatsAttaqueEffectues = false;
				delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple;
				delaiActuelStraff += 0.5f;

			} else {

				if (agent.isActualAnimation("attack1") || agent.isActualAnimation("attack2") || agent.isActualAnimation("attack3") || agent.isActualAnimation("attack4") || agent.isActualAnimation("repousse")) {
					
					setAnimation ("idleCombat");

					if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

						princesseVie.blesser (degatsAttaqueSimple, this.gameObject, forceReculeAttaqueSimple);
						degatsAttaqueEffectues = true;
					}
				} else if (attaqueEnCours && Time.time >= delaiActuelAttaqueSimple - delaiAttaqueSimple + 1.0f) {
					degatsAttaqueEffectues = false;
					attaqueEnCours = false;
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
