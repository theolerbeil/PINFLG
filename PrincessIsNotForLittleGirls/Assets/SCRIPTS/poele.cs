using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poele : Objet {

    public princesse_arme princesse;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override
    public void Activation()
    {
        princesse.SetArmeActive(EnumArmes.poele);
    }
}
