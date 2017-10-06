using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gobelin_ia : MonoBehaviour {

    public float porteeAttaqueSimple;
    public int degatsAttaqueSimple;

    
    public float delaiAttaqueSimple; // en seconde

    private NavMeshAgent nav;
    private GameObject princesse;
    private princesse_vie princesseVie;

    private float delaiActuelAttaqueSimple;

    // Use this for initialization
    void Start () {

        nav = this.GetComponent<NavMeshAgent>();
        princesse = GameObject.FindGameObjectWithTag("Player");
        princesseVie = princesse.GetComponent<princesse_vie>();

        delaiActuelAttaqueSimple = 0.0f;

        definirDestination(princesse.transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (destinationCouranteAtteinte())
        {
            nav.isStopped = true;

            if (princesseAttaquable())
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

    private void definirDestination(Vector3 positionDestination)
    {
        nav.SetDestination(positionDestination);
    }

    private bool destinationCouranteAtteinte()
    {
        return (nav.pathEndPosition - this.transform.position).magnitude <= nav.stoppingDistance;
    }

    private bool princesseAttaquable()
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
