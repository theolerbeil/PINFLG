using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gobelin_ia : ia_agent {

    public float porteeAttaqueSimple;
    public int degatsAttaqueSimple;

    [Tooltip("Délai en secondes entre deux attaques simples.")]
    public float delaiAttaqueSimple;

    private float delaiActuelAttaqueSimple;

    // Use this for initialization
    void Start () {

        base.init();
        pointsInteret = GameObject.FindObjectsOfType<ia_PI_gobelin>();

        delaiActuelAttaqueSimple = 0.0f;

        definirDestination("test");
       // definirDestination(princesse.transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (destinationCouranteAtteinte())
        {
            nav.isStopped = true;

            if (princesseAttaquableSimplement())
            {
                Debug.Log("Attaque simple");
                attaquerSimplementPrincesse();
            }
            else
            {
                definirDestination(princesse.transform.position);
                nav.isStopped = false;
            }
        }
    }

    /// <summary>
    /// Permet de savoir si l'agent peut effectuer une attaque simple contre la princesse.
    /// </summary>
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
