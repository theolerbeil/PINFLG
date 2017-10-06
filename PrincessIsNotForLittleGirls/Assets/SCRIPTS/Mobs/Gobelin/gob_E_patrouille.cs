using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_patrouille : ia_etat {

    public ia_pointInteret[] chemin;

    private int indiceCheminActuel;

    // Use this for initialization
    void Start () {
        base.init(); // permet d'initialiser l'état, ne pas l'oublier !
    }

    public override void entrerEtat()
    {
        indiceCheminActuel = 0;
        suivreChemin();
        nav.isStopped = false;
    }

    public override void faireEtat()
    {
        if (agent.destinationCouranteAtteinte())
        {
            indiceCheminActuel = (indiceCheminActuel + 1) % chemin.Length;
            suivreChemin();
        }
    }

    public override void sortirEtat()
    {
        
    }

    private void suivreChemin()
    {
        agent.definirDestination(chemin[indiceCheminActuel].transform.position);
    }
}
