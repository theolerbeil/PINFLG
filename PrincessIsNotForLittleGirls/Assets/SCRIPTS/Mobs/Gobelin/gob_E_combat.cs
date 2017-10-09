﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gob_E_combat : ia_etat {

    public float vitesse;
    public float porteeAttaqueSimple;
    public int degatsAttaqueSimple;

    [Tooltip("Délai en secondes entre deux attaques simples.")]
    public float delaiAttaqueSimple;

    private float delaiActuelAttaqueSimple;

    // Use this for initialization
    void Start () {
        base.init(); // permet d'initialiser l'état, ne pas l'oublier !
        delaiActuelAttaqueSimple = 0.0f;
    }

    public override void entrerEtat()
    {
        anim.SetBool("IsRunning", true);
        nav.speed = vitesse;
        agent.definirDestination(princesse.transform.position);
        nav.isStopped = false;
    }

    public override void faireEtat()
    {
        if (agent.destinationCouranteAtteinte())
        {
            nav.isStopped = true;

            if (princesseAttaquableSimplement())
            {
                Debug.Log("Attaque simple");
                attaquerSimplementPrincesse();
            }
            else
            {
                agent.definirDestination(princesse.transform.position);
                nav.isStopped = false;
            }
        }
    }

    public override void sortirEtat()
    {
        anim.SetBool("IsRunning", false);
    }

    private bool princesseAttaquableSimplement()
    {
        return princesseVie.enVie() && princesseAPorteeAttaqueSimple() && attaqueSimplePrete();
    }

    private bool princesseAPorteeAttaqueSimple()
    {
        return (princesse.transform.position - this.transform.position).magnitude <= porteeAttaqueSimple;
    }

    private bool attaqueSimplePrete()
    {
        return Time.time >= delaiActuelAttaqueSimple;
    }

    private void attaquerSimplementPrincesse()
    {
        princesseVie.blesser(degatsAttaqueSimple);
        delaiActuelAttaqueSimple = Time.time + delaiAttaqueSimple;
    }
}
