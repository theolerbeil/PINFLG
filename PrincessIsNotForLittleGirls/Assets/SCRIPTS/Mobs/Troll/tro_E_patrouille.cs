using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tro_E_patrouille : ia_etat {

    public float vitesse;
	public float delaisAChaqueArret;

    public ia_pointInteret[] chemin;

    private int indiceCheminActuel;
	private float delaisActuel;
	private bool enChemin;
	private int indiceDernierPointRejoint;

    // Use this for initialization
    void Start () {
        base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		this.delaisActuel = 0.0f;
		indiceDernierPointRejoint = -1;
    }

    public override void entrerEtat()
	{
        setAnimation("walking");
		indiceCheminActuel = indiceDernierPointRejoint;
		nav.enabled = true;
        suivreChemin();
		this.enChemin = true;
    }

    public override void faireEtat()
    {
		if(agent.princesseReperee()) {
			changerEtat(this.GetComponent<tro_E_poursuite>());
		}

		if (enChemin) {

			if (agent.destinationCouranteAtteinte ()) {
				
				indiceDernierPointRejoint = indiceCheminActuel;
				setAnimation("searching");
				enChemin = false;
				this.delaisActuel = Time.time + this.delaisAChaqueArret;
			}
		} else if (Time.time > this.delaisActuel) {

			setAnimation("walking");
			suivreChemin ();
			enChemin = true;

		} else {

			if(agent.princesseRepereeAvecAttention()) {
				changerEtat(this.GetComponent<tro_E_poursuite>());
			}
		}
    }

    public override void sortirEtat()
    {
        
    }

    private void suivreChemin()
    {
		indiceCheminActuel = (indiceCheminActuel + 1) % chemin.Length;
        agent.definirDestination(chemin[indiceCheminActuel].transform.position);
        nav.speed = vitesse;
    }
}
