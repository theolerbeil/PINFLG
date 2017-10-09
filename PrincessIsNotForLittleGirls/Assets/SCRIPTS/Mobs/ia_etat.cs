using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ia_etat : MonoBehaviour
{

    protected ia_agent agent;

    protected NavMeshAgent nav;
    protected Animator anim;
    protected GameObject princesse;
    protected princesse_vie princesseVie;
    protected ia_pointInteret[] pointsInteret;

    protected void init()
    {
        agent = this.GetComponent<ia_agent>();
        nav = agent.getNav();
        anim = agent.getAnimator();
        princesse = agent.getPrincesse();
        princesseVie = agent.getPrincesse_Vie();
        pointsInteret = agent.getPointsInteret();
    }

    public abstract void entrerEtat();
    public abstract void faireEtat();
    public abstract void sortirEtat();

    protected void changerEtat(ia_etat nouvelEtat)
    {
        agent.changerEtat(nouvelEtat);
    }
}
