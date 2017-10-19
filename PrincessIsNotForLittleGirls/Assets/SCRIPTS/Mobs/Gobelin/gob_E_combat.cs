using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_combat : ia_etat {

	public float vitesse;
	public float dureeRecherchePrincesse;
	public float porteeAttaqueSimple;
	public int degatsAttaqueSimple;
	public float porteeAttaquePuissante;
	public int degatsAttaquePuissante;
	public int pourcentageUtilisationAttaquePuissante;
	public float forceDeSautAttaquePuissante;
	public float forceAvancementAttaquePuissante;

	[Tooltip("Délai en secondes entre deux attaques simples.")]
	public float delaiAttaqueSimple;
	public float delaiAttaquePuissante;

	private float delaiActuelAttaqueSimple;
	private float delaiActuelAttaquePuissante;
	private float delaiActuelRecherche;
	private bool enChemin;
	private bool attaquePuissanteTestee;
	private bool attaquePuissanteEnCours;
	private Vector3 dernierePositionPrincesseConnue;
	private bool degatsAttaqueEffectues;
	private triggerArme colliderArme;
	private bool princesseEnVue;

    // Use this for initialization
    void Start () {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		delaiActuelAttaqueSimple = 0.0f;
		delaiActuelAttaquePuissante = 0.0f;
		delaiActuelRecherche = 0.0f;
		colliderArme = GetComponent<triggerArme> ();
    }

    public override void entrerEtat()
    {
        setAnimation("IsRunning");
        nav.speed = vitesse;
		nav.stoppingDistance = porteeAttaqueSimple - 0.5f;
		dernierePositionPrincesseConnue = princesse.transform.position;
		agent.definirDestination(dernierePositionPrincesseConnue);
		nav.enabled = true;
		this.enChemin = true;
		attaquePuissanteTestee = false;
		attaquePuissanteEnCours = false;
		degatsAttaqueEffectues = false;
		princesseEnVue = true;
    }

    public override void faireEtat()
    {

		if (enChemin) {

			princesseEnVue = agent.princesseReperee ();

			if (princesseEnVue) {
				
				dernierePositionPrincesseConnue = princesse.transform.position;
			}

			agent.definirDestination (dernierePositionPrincesseConnue);

			if (princesseAPorteeAttaquePuissante () && !attaquePuissanteTestee) {

				attaquePuissanteTestee = true;

				int rand = Random.Range (1, 100);

				if (rand <= pourcentageUtilisationAttaquePuissante) {
					Debug.Log ("attaque puissante");
					attaquePuissanteEnCours = true;
					enChemin = false;
					nav.enabled = false;
					rb.AddForce (this.transform.up * forceDeSautAttaquePuissante + this.transform.forward * forceAvancementAttaquePuissante);
					setAnimation ("IsAttackPuissante");
				}
			} else if (agent.destinationCouranteAtteinte ()) {

				if (princesseEnVue) {

					nav.enabled = false;
					enChemin = false;
					setAnimation ("IsIdle");

				} else {

					if (delaiActuelRecherche == 0.0f) {
						Debug.Log ("princesse perdue");
						delaiActuelRecherche = Time.time + dureeRecherchePrincesse;
						setAnimation ("IsIdle");
					}

					if(Time.time <= delaiActuelRecherche) {

						if (agent.princesseRepereeAvecAttention ()) {
							Debug.Log ("princesse retrouvée");
							setAnimation("IsRunning");
							princesseEnVue = true;
							dernierePositionPrincesseConnue = princesse.transform.position;
							agent.definirDestination (dernierePositionPrincesseConnue);

						}
					} else {
						Debug.Log ("abandon retour patrouille");
						changerEtat (GetComponent<gob_E_patrouille> ());
					}
				}
			}

		} else if (attaquePuissanteEnCours) { // l'agent a fini son déplacement vers la princesse et à décider de faire une attaque puissante
			
			if (anim.GetBool ("IsAttackPuissante")) { // l'attaque puissante est toujours en cours
				
				if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

					princesseVie.blesser (degatsAttaquePuissante);
					degatsAttaqueEffectues = true;
				}

			} else {

				attaquePuissanteEnCours = false;
				degatsAttaqueEffectues = false;
			}

		} else { // l'agent a fini son déplacement vers la princesse


		}


//		if (agent.destinationCouranteAtteinte())
//		{
//
//			nav.isStopped = true;
//			enChemin = false;
//
//			if (princesseAttaquableSimplement())
//			{
//
//				attaquerSimplementPrincesse();
//			}
//			/*   else if (princesseAPorteeAttaqueSimple())
//            {
//
//                setAnimation("IsIdle");
//            }*/
//			else
//			{
//				setAnimation("IsRunning");
//				agent.definirDestination(princesse.transform.position);
//				nav.isStopped = false;
//				enChemin = true;
//			}
//		}
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
		return (princesse.transform.position - this.transform.position).magnitude <= porteeAttaqueSimple;
	}

	private bool princesseAPorteeAttaquePuissante()
	{
		Vector3 vecDistancePrincesse = princesse.transform.position - this.transform.position;
		float angle = Vector3.Angle (this.transform.forward, vecDistancePrincesse.normalized);

		return vecDistancePrincesse.magnitude <= porteeAttaquePuissante && angle <= 10.0f;
	}

	private bool attaqueSimplePrete()
	{
		return Time.time >= delaiActuelAttaqueSimple;
	}

	private bool attaquePuissantePrete()
	{
		return Time.time >= delaiActuelAttaquePuissante;
	}

    private void attaquerSimplementPrincesse()
    {
        setAnimation("IsAttack");
        Debug.Log("Attaque simple");
        princesseVie.blesser(degatsAttaqueSimple);
        delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple;
    }
}
