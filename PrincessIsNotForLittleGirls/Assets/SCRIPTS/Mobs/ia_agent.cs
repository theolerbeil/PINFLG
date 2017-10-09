using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ia_agent : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
    private GameObject princesse;
    private princesse_vie princesseVie;
    private ia_pointInteret[] pointsInteret;
    
    public ia_etat etatCourant;

    void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        princesse = GameObject.FindGameObjectWithTag("Player");
        princesseVie = princesse.GetComponent<princesse_vie>();
        pointsInteret = GameObject.FindObjectsOfType<ia_pointInteret>();
    }

    // Use this for initialization
    void Start () {
        
        etatCourant.entrerEtat();
    }
	
	// Update is called once per frame
	void Update () {

        etatCourant.faireEtat();

    }

    public NavMeshAgent getNav()
    {
        return nav;
    }

    public GameObject getPrincesse()
    {
        return princesse;
    }

    public princesse_vie getPrincesse_Vie()
    {
        return princesseVie;
    }

    public ia_pointInteret[] getPointsInteret()
    {
        return pointsInteret;
    }

    /// <summary>
    /// Définit la position de la destination actuel de l'agent.
    /// </summary>
    public void definirDestination(Vector3 positionDestination)
    {
        nav.SetDestination(positionDestination);
    }

    /// <summary>
    /// Définit le nom du point d'interet de destination actuel de l'agent.
    /// </summary>
    public void definirDestination(string nomDestination)
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
    public bool destinationCouranteAtteinte()
    {
        return (nav.pathEndPosition - this.transform.position).magnitude <= nav.stoppingDistance;
    }

    public void setAnimation(string nomAnimation)
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }

        anim.SetBool(nomAnimation, true);
    }

    /// <summary>
    /// Permet de sortir de l'état courant puis d'entrer dans le nouvel état.
    /// </summary>
    public void changerEtat(ia_etat nouvelEtat)
    {
        etatCourant.sortirEtat();
        etatCourant = nouvelEtat;
        etatCourant.entrerEtat();
    }
}
