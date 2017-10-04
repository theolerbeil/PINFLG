using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : Objet {

    private princesse_arme princesse;
    public EnumArmes arme;

    // Use this for initialization
    void Start () {
        princesse = GameObject.FindGameObjectWithTag("Player").GetComponent<princesse_arme>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    override
    public void Activation()
    {
        princesse.SetArmeActive(arme);
    }
}
