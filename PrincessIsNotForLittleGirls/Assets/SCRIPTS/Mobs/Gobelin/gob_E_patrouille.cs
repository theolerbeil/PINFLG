using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_patrouille : ia_etat {

    public float vitesse;
	public float delaisAChaqueArret;

    public ia_pointInteret[] chemin;

    private int indiceCheminActuel;
	private float delaisActuel;
	private bool enChemin;

    // Use this for initialization
    void Start () {
        base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		this.delaisActuel = 0.0f;
    }

    public override void entrerEtat()
	{
        setAnimation("walking");
		indiceCheminActuel = 0;
		nav.enabled = true;
        suivreChemin();
		this.enChemin = true;
    }

    public override void faireEtat()
    {
		if(agent.princesseReperee()) {
			changerEtat(this.GetComponent<gob_E_poursuite>());
		}

		if (enChemin) {

			if (agent.destinationCouranteAtteinte ()) {

				setAnimation("searching");
				enChemin = false;
				this.delaisActuel = Time.time + this.delaisAChaqueArret;
			}
		} else if (Time.time > this.delaisActuel) {

			setAnimation("walking");
			indiceCheminActuel = (indiceCheminActuel + 1) % chemin.Length;
			suivreChemin ();
			enChemin = true;

		} else {

			if(agent.princesseRepereeAvecAttention()) {
				changerEtat(this.GetComponent<gob_E_poursuite>());
			}
		}
    }

    public override void sortirEtat()
    {
        
    }

    private void suivreChemin()
    {
        agent.definirDestination(chemin[indiceCheminActuel].transform.position);
        nav.speed = vitesse;
    }
}
