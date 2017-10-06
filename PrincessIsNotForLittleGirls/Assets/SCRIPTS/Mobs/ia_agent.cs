using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ia_agent : MonoBehaviour {

    protected NavMeshAgent nav;
    protected GameObject princesse;
    protected princesse_vie princesseVie;
    protected ia_pointInteret[] pointsInteret;
    public List<string> chemins;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void init()
    {
        nav = this.GetComponent<NavMeshAgent>();
        princesse = GameObject.FindGameObjectWithTag("Player");
        princesseVie = princesse.GetComponent<princesse_vie>();
    }

    /// <summary>
    /// Définit la position de la destination actuel de l'agent.
    /// </summary>
    protected void definirDestination(Vector3 positionDestination)
    {
        nav.SetDestination(positionDestination);
    }

    /// <summary>
    /// Définit le nom du point d'interet de destination actuel de l'agent.
    /// </summary>
    protected void definirDestination(string nomDestination)
    {
        foreach (ia_pointInteret pi in pointsInteret)
        {
            if (pi.nom.Equals(nomDestination))
            {
                nav.SetDestination(pi.transform.position);
                return;
            }
        }
    }

    /// <summary>
    /// Permet de savoir si l'agent a atteint sa destination.
    /// </summary>
    protected bool destinationCouranteAtteinte()
    {
        return (nav.pathEndPosition - this.transform.position).magnitude <= nav.stoppingDistance;
    }

    protected void patrouiller()
    {

    }
}
